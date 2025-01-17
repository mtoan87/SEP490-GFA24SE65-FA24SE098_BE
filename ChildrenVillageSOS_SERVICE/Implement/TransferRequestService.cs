﻿using ChildrenVillageSOS_DAL.DTO.TransferRequestDTO;
using ChildrenVillageSOS_DAL.DTO.VillageDTO;
using ChildrenVillageSOS_DAL.Enum;
using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_REPO.Implement;
using ChildrenVillageSOS_REPO.Interface;
using ChildrenVillageSOS_SERVICE.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_SERVICE.Implement
{
    public class TransferRequestService : ITransferRequestService
    {
        private readonly ITransferRequestRepository _transferRequestRepository;
        private readonly IVillageRepository _villageRepository;
        private readonly IChildRepository _childRepository;
        private readonly IHouseRepository _houseRepository;
        private readonly ITransferHistoryRepository _transferHistoryRepository;
        private readonly IUserAccountRepository _userAccountRepository;

        public TransferRequestService(
        IVillageRepository villageRepository,
        ITransferRequestRepository transferRequestRepository,
        IChildRepository childRepository,
        IHouseRepository houseRepository,
        IUserAccountRepository userAccountRepository,
        ITransferHistoryRepository transferHistoryRepository)
        {
            _villageRepository = villageRepository;
            _transferRequestRepository = transferRequestRepository;
            _childRepository = childRepository;
            _houseRepository = houseRepository;
            _userAccountRepository = userAccountRepository;
            _transferHistoryRepository = transferHistoryRepository;
        }
        public async Task<TransferRequest> GetTransferRequestById(int id)
        {
            var request = await _transferRequestRepository.GetTransferRequestWithDetails(id);
            if (request == null)
                throw new InvalidOperationException("Transfer request not found");
            return request;
        }

        public async Task<IEnumerable<TransferRequest>> GetAllTransferRequestsWithDetails()
        {
            return await _transferRequestRepository.GetAllTransferRequestsWithDetails();
        }

        public async Task<IEnumerable<TransferRequest>> GetAllTransferRequests()
        {
            return await _transferRequestRepository.GetAllNotDeletedAsync();
        }

        public async Task<IEnumerable<TransferRequest>> GetTransferRequestsByHouse(string houseId)
        {
            return await _transferRequestRepository.GetTransferRequestsByHouse(houseId);
        }

        public async Task<TransferRequest> CreateTransferRequest(CreateTransferRequestDTO dto)
        {
            var child = await _childRepository.GetByIdAsync(dto.ChildId);
            if (child == null)
                throw new InvalidOperationException("Child not found");

            var fromHouse = await _houseRepository.GetByIdAsync(dto.FromHouseId);
            if (fromHouse == null)
                throw new InvalidOperationException("From House not found");

            if (!string.IsNullOrEmpty(dto.ToHouseId))
            {
                var toHouse = await _houseRepository.GetByIdAsync(dto.ToHouseId);
                if (toHouse == null)
                    throw new InvalidOperationException("To House not found");

                // Check if ToHouseId matches FromHouseId
                if (dto.ToHouseId == dto.FromHouseId)
                {
                    throw new InvalidOperationException("The destination house cannot be the same as the current house");
                }

                // Check if ToHouseId matches child's HouseId
                if (dto.ToHouseId == child.HouseId)
                {
                    throw new InvalidOperationException("The child is already in the specified destination house");
                }

                // Check the current number of members of the house HouseMother want to move to
                if (toHouse.CurrentMembers >= 10)
                {
                    throw new InvalidOperationException("The destination house has reached its maximum capacity of 10 members");
                }
            }

            var transferRequest = new TransferRequest
            {
                ChildId = dto.ChildId,
                FromHouseId = dto.FromHouseId,
                ToHouseId = dto.ToHouseId,
                RequestDate = DateTime.Now,
                Status = TransferStatus.Pending.ToString(),
                RequestReason = dto.RequestReason,
                CreatedBy = dto.CreatedBy,
                CreatedDate = DateTime.Now,
                IsDeleted = false
            };

            await _transferRequestRepository.AddAsync(transferRequest);
            return transferRequest;
        }

        public async Task<TransferRequest> UpdateTransferRequest(int id, UpdateTransferRequestDTO dto, string currentUserId)
        {
            var transferRequest = await _transferRequestRepository.GetAsync(
                x => x.Id == id,
                includeProperties: "Child,FromHouse,ToHouse"
            );
            if (transferRequest == null)
                throw new InvalidOperationException("Transfer request not found");

            // Lấy thông tin user hiện tại
            var currentUser = await _userAccountRepository.GetByIdAsync(currentUserId);
            if (currentUser == null)
                throw new InvalidOperationException("User not found");

            // Kiểm tra quyền cơ bản
            if (!await HasPermissionToUpdateTransfer(currentUser, transferRequest))
                throw new InvalidOperationException("You don't have permission to update this transfer request");

            if (transferRequest.Status == TransferStatus.Completed.ToString())
                throw new InvalidOperationException("Cannot update a completed transfer request");

            if (transferRequest.Status == TransferStatus.Rejected.ToString())
                throw new InvalidOperationException("Cannot update a rejected transfer request");

            // Admin/Director confirms lần 1
            if (transferRequest.Status == TransferStatus.Pending.ToString())
            {
                if (!IsAdminOrDirector(currentUser))
                    throw new InvalidOperationException("Only Admin or Director can approve/reject pending transfers");

                if (dto.Status == TransferStatus.InProcess.ToString())
                {
                    transferRequest.Status = TransferStatus.InProcess.ToString();
                    transferRequest.ModifiedBy = currentUserId;
                    transferRequest.ModifiedDate = DateTime.Now;
                    transferRequest.DirectorNote = dto.DirectorNote;

                    await _transferRequestRepository.UpdateAsync(transferRequest);
                    return transferRequest;
                }
                else if (dto.Status == TransferStatus.Rejected.ToString())
                {
                    var transferHistory = new TransferHistory
                    {
                        ChildId = transferRequest.ChildId,
                        FromHouseId = transferRequest.FromHouseId,
                        ToHouseId = transferRequest.ToHouseId,
                        TransferDate = DateTime.Now,
                        Status = TransferStatus.Rejected.ToString(),
                        HandledBy = currentUserId,
                        CreatedDate = DateTime.Now,
                        IsDeleted = false,
                        RejectionReason = dto.DirectorNote,
                        CreatedBy = transferRequest.CreatedBy
                    };

                    await _transferHistoryRepository.AddAsync(transferHistory);
                    transferRequest.Status = TransferStatus.Rejected.ToString();
                    transferRequest.ModifiedBy = currentUserId;
                    transferRequest.ModifiedDate = DateTime.Now;
                    transferRequest.DirectorNote = dto.DirectorNote;

                    await _transferRequestRepository.UpdateAsync(transferRequest);
                    return transferRequest;
                }
            }

            // HouseMother2 confirms
            if (transferRequest.Status == TransferStatus.InProcess.ToString())
            {
                var isTargetHouseMother = await IsHouseMotherOfHouse(currentUser.Id, transferRequest.ToHouseId);
                if (!isTargetHouseMother)
                    throw new InvalidOperationException("Only the House Mother of the target house can confirm this transfer");

                if (dto.Status == TransferStatus.ReadyToTransfer.ToString() ||
                    dto.Status == TransferStatus.DeclinedToTransfer.ToString())
                {
                    transferRequest.Status = dto.Status;
                    transferRequest.ModifiedBy = currentUserId;
                    transferRequest.ModifiedDate = DateTime.Now;
                    //transferRequest.DirectorNote = dto.DirectorNote;

                    await _transferRequestRepository.UpdateAsync(transferRequest);
                    return transferRequest;
                }
            }
            var directorVillage = _villageRepository.GetVillageByHouseId(transferRequest.FromHouseId);

            // Admin/Director confirms lần 2 - Final approval
            if (transferRequest.Status == TransferStatus.ReadyToTransfer.ToString() &&
                dto.Status == TransferStatus.Completed.ToString())
            {
                if (currentUserId != directorVillage.UserAccountId)
                {
                    throw new InvalidOperationException($"Only Director of village:{directorVillage.VillageName} can give the final approval");
                }

                if (!IsAdminOrDirector(currentUser))
                    throw new InvalidOperationException("Only Admin or Director can give final approval");

                var transferHistory = new TransferHistory
                {
                    ChildId = transferRequest.ChildId,
                    FromHouseId = transferRequest.FromHouseId,
                    ToHouseId = transferRequest.ToHouseId,
                    TransferDate = DateTime.Now,
                    Status = TransferStatus.Completed.ToString(),
                    Notes = dto.DirectorNote,
                    HandledBy = currentUserId,
                    CreatedDate = DateTime.Now,
                    IsDeleted = false,
                    CreatedBy = transferRequest.CreatedBy
                };

                await _transferHistoryRepository.AddAsync(transferHistory);

                // Cập nhật thông tin nhà của trẻ
                var child = await _childRepository.GetAsync(x => x.Id == transferRequest.ChildId);
                if (child != null)
                {
                    child.HouseId = transferRequest.ToHouseId;
                    await _childRepository.UpdateAsync(child);
                }

                transferRequest.Status = TransferStatus.Completed.ToString();
                transferRequest.ModifiedBy = currentUserId;
                transferRequest.ModifiedDate = DateTime.Now;
                transferRequest.DirectorNote = dto.DirectorNote;
                transferRequest.ApprovedBy = currentUserId;

                await _transferRequestRepository.UpdateAsync(transferRequest);
                return transferRequest;
            }

            // Admin/Director confirms lần 2 - Final rejection
            if (transferRequest.Status == TransferStatus.DeclinedToTransfer.ToString() &&
                dto.Status == TransferStatus.Rejected.ToString())
            {
                if (!IsAdminOrDirector(currentUser))
                    throw new InvalidOperationException("Only Admin or Director can give final rejection");

                var transferHistory = new TransferHistory
                {
                    ChildId = transferRequest.ChildId,
                    FromHouseId = transferRequest.FromHouseId,
                    ToHouseId = transferRequest.ToHouseId,
                    TransferDate = DateTime.Now,
                    Status = TransferStatus.Rejected.ToString(),
                    HandledBy = currentUserId,
                    CreatedDate = DateTime.Now,
                    IsDeleted = false,
                    RejectionReason = dto.DirectorNote,
                    CreatedBy = transferRequest.CreatedBy
                };

                await _transferHistoryRepository.AddAsync(transferHistory);

                transferRequest.Status = TransferStatus.Rejected.ToString();
                transferRequest.ModifiedBy = currentUserId;
                transferRequest.ModifiedDate = DateTime.Now;
                transferRequest.DirectorNote = dto.DirectorNote;

                await _transferRequestRepository.UpdateAsync(transferRequest);
                return transferRequest;
            }

            return transferRequest;
        }

        private async Task<bool> HasPermissionToUpdateTransfer(UserAccount user, TransferRequest transfer)
        {
            if (IsAdminOrDirector(user)) return true;

            if (user.RoleId == 3) // Role HouseMother
            {
                var isSourceHouseMother = await IsHouseMotherOfHouse(user.Id, transfer.FromHouseId);
                var isTargetHouseMother = await IsHouseMotherOfHouse(user.Id, transfer.ToHouseId);
                return isSourceHouseMother || isTargetHouseMother;
            }
            
            return false;
        }

        private bool IsAdminOrDirector(UserAccount user)
        {
            return user.RoleId == 1 || user.RoleId == 6;
        }

        private async Task<bool> IsHouseMotherOfHouse(string userId, string houseId)
        {
            var house = await _houseRepository.GetAsync(x => x.Id == houseId);
            return house != null && house.UserAccountId == userId;
        }

        public async Task<TransferRequest> UpdateTransferRequest(int id, UpdateTransferRequestDTO dto)
        {
            var transferRequest = await _transferRequestRepository.GetTransferRequestWithDetails(id);
            if (transferRequest == null)
                throw new InvalidOperationException("Transfer request not found");

            if (transferRequest.Status == TransferStatus.Completed.ToString())
                throw new InvalidOperationException("Cannot update a completed transfer request");

            if (transferRequest.Status == TransferStatus.Rejected.ToString())
                throw new InvalidOperationException("Cannot update a rejected transfer request");

            // Admin/Director confirms lần 1
            if (transferRequest.Status == TransferStatus.Pending.ToString())
            {
                if (dto.Status == TransferStatus.InProcess.ToString())
                {
                    transferRequest.Status = TransferStatus.InProcess.ToString();
                    transferRequest.ModifiedBy = dto.ModifiedBy;
                    transferRequest.ModifiedDate = DateTime.Now;
                    transferRequest.DirectorNote = dto.DirectorNote;

                    await _transferRequestRepository.UpdateAsync(transferRequest);
                    return transferRequest;
                }
                else if (dto.Status == TransferStatus.Rejected.ToString())
                {
                    var transferHistory = new TransferHistory
                    {
                        ChildId = transferRequest.ChildId,
                        FromHouseId = transferRequest.FromHouseId,
                        ToHouseId = transferRequest.ToHouseId,
                        TransferDate = DateTime.Now,
                        Status = TransferStatus.Rejected.ToString(),
                        HandledBy = dto.ModifiedBy,
                        CreatedDate = DateTime.Now,
                        IsDeleted = false,
                        RejectionReason = dto.DirectorNote,
                        CreatedBy = transferRequest.CreatedBy
                    };

                    await _transferHistoryRepository.AddAsync(transferHistory);

                    await _transferRequestRepository.RemoveAsync(transferRequest);

                    transferRequest.Status = TransferStatus.Rejected.ToString();
                    transferRequest.ModifiedBy = dto.ModifiedBy;
                    transferRequest.ModifiedDate = DateTime.Now;
                    transferRequest.DirectorNote = dto.DirectorNote;

                    await _transferRequestRepository.UpdateAsync(transferRequest);
                    return transferRequest;
                }
            }

            // HouseMother2 confirms
            if (transferRequest.Status == TransferStatus.InProcess.ToString())
            {
                if (dto.Status == TransferStatus.ReadyToTransfer.ToString() ||
                    dto.Status == TransferStatus.DeclinedToTransfer.ToString())
                {
                    transferRequest.Status = dto.Status;
                    transferRequest.ModifiedBy = dto.ModifiedBy;
                    transferRequest.ModifiedDate = DateTime.Now;
                    transferRequest.DirectorNote = dto.DirectorNote;

                    await _transferRequestRepository.UpdateAsync(transferRequest);
                    return transferRequest;
                }
            }

            // Admin/Director confirms lần 2 - Final approval
            if (transferRequest.Status == TransferStatus.ReadyToTransfer.ToString() &&
                dto.Status == TransferStatus.Completed.ToString())
            {
                var transferHistory = new TransferHistory
                {
                    ChildId = transferRequest.ChildId,
                    FromHouseId = transferRequest.FromHouseId,
                    ToHouseId = transferRequest.ToHouseId,
                    TransferDate = DateTime.Now,
                    Status = TransferStatus.Completed.ToString(),
                    Notes = dto.DirectorNote,
                    HandledBy = dto.ModifiedBy,
                    CreatedDate = DateTime.Now,
                    IsDeleted = false,
                    CreatedBy = transferRequest.CreatedBy
                };

                await _transferHistoryRepository.AddAsync(transferHistory);

                await _transferRequestRepository.RemoveAsync(transferRequest);

                // Cập nhật thông tin nhà của trẻ
                var child = await _childRepository.GetByIdAsync(transferRequest.ChildId);
                child.HouseId = transferRequest.ToHouseId;
                await _childRepository.UpdateAsync(child);

                transferRequest.Status = TransferStatus.Completed.ToString();
                transferRequest.ModifiedBy = dto.ModifiedBy;
                transferRequest.ModifiedDate = DateTime.Now;
                transferRequest.DirectorNote = dto.DirectorNote;

                await _transferRequestRepository.UpdateAsync(transferRequest);
                return transferRequest;
            }

            // Admin/Director confirms lần 2 - Final rejection
            if (transferRequest.Status == TransferStatus.DeclinedToTransfer.ToString() &&
                dto.Status == TransferStatus.Rejected.ToString())
            {
                var transferHistory = new TransferHistory
                {
                    ChildId = transferRequest.ChildId,
                    FromHouseId = transferRequest.FromHouseId,
                    ToHouseId = transferRequest.ToHouseId,
                    TransferDate = DateTime.Now,
                    Status = TransferStatus.Rejected.ToString(),
                    HandledBy = dto.ModifiedBy,
                    CreatedDate = DateTime.Now,
                    IsDeleted = false,
                    RejectionReason = dto.DirectorNote,
                    CreatedBy = transferRequest.CreatedBy
                };

                await _transferHistoryRepository.AddAsync(transferHistory);

                await _transferRequestRepository.RemoveAsync(transferRequest);

                transferRequest.Status = TransferStatus.Rejected.ToString();
                transferRequest.ModifiedBy = dto.ModifiedBy;
                transferRequest.ModifiedDate = DateTime.Now;
                transferRequest.DirectorNote = dto.DirectorNote;

                await _transferRequestRepository.UpdateAsync(transferRequest);
                return transferRequest;
            }

            // Nếu trạng thái không phải Approved hoặc Rejected, cho phép chỉnh sửa
            transferRequest.FromHouseId = dto.FromHouseId;
            transferRequest.ToHouseId = dto.ToHouseId;
            transferRequest.RequestReason = dto.RequestReason;
            transferRequest.Status = dto.Status;
            transferRequest.DirectorNote = dto.DirectorNote;
            transferRequest.ModifiedBy = dto.ModifiedBy;
            transferRequest.ModifiedDate = DateTime.Now;
            transferRequest.ApprovedBy = dto.ApprovedBy;

            await _transferRequestRepository.UpdateAsync(transferRequest);

            return transferRequest;

            //throw new InvalidOperationException("Invalid status transition");
        }

        public async Task<TransferRequest> DeleteTransferRequest(int id)
        {
            var transferRequest = await _transferRequestRepository.GetByIdAsync(id);
            if (transferRequest == null)
            {
                throw new Exception($"Transfer request with ID {id} not found");
            }

            if (transferRequest.IsDeleted == true)
            {
                await _transferRequestRepository.RemoveAsync(transferRequest);
            }
            else
            {
                transferRequest.IsDeleted = true;
                transferRequest.ModifiedDate = DateTime.Now;
                await _transferRequestRepository.UpdateAsync(transferRequest);
            }
            return transferRequest;
        }

        public async Task<TransferRequest> RestoreTransferRequest(int id)
        {
            var transferRequest = await _transferRequestRepository.GetByIdAsync(id);
            if (transferRequest == null)
            {
                throw new Exception($"Transfer request with ID {id} not found");
            }

            if (transferRequest.IsDeleted == true)
            {
                transferRequest.IsDeleted = false;
                transferRequest.ModifiedDate = DateTime.Now;
                await _transferRequestRepository.UpdateAsync(transferRequest);
            }
            return transferRequest;
        }

        public async Task<List<TransferRequest>> SearchTransferRequests(SearchTransferRequestDTO searchTransferRequestDTO)
        {
            return await _transferRequestRepository.SearchTransferRequests(searchTransferRequestDTO);
        }

    }
}
