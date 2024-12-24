using ChildrenVillageSOS_DAL.DTO.TransferRequestDTO;
using ChildrenVillageSOS_DAL.Models;
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
        private readonly IChildRepository _childRepository;
        private readonly IHouseRepository _houseRepository;
        private readonly ITransferHistoryRepository _transferHistoryRepository;

        public TransferRequestService(
        ITransferRequestRepository transferRequestRepository,
        IChildRepository childRepository,
        IHouseRepository houseRepository,
        ITransferHistoryRepository transferHistoryRepository)
        {
            _transferRequestRepository = transferRequestRepository;
            _childRepository = childRepository;
            _houseRepository = houseRepository;
            _transferHistoryRepository = transferHistoryRepository;
        }
        public async Task<TransferRequest> GetTransferRequestById(int id)
        {
            var request = await _transferRequestRepository.GetTransferRequestWithDetails(id);
            if (request == null)
                throw new InvalidOperationException("Transfer request not found");
            return request;
        }

        public async Task<IEnumerable<TransferRequest>> GetAllTransferRequests()
        {
            return await _transferRequestRepository.GetAllTransferRequestsWithDetails();
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
            }

            var transferRequest = new TransferRequest
            {
                ChildId = dto.ChildId,
                FromHouseId = dto.FromHouseId,
                ToHouseId = dto.ToHouseId,
                RequestDate = DateTime.UtcNow,
                Status = "Pending",
                RequestReason = dto.RequestReason,
                CreatedBy = dto.CreatedBy,
                CreatedDate = DateTime.UtcNow,
                IsDeleted = false
            };

            await _transferRequestRepository.AddAsync(transferRequest);
            return transferRequest;
        }

        public async Task<TransferRequest> UpdateTransferRequest(int id, UpdateTransferRequestDTO dto)
        {
            var transferRequest = await _transferRequestRepository.GetTransferRequestWithDetails(id);
            if (transferRequest == null)
                throw new InvalidOperationException("Transfer request not found");

            transferRequest.Status = dto.Status;
            transferRequest.DirectorNote = dto.DirectorNote;
            transferRequest.ModifiedBy = dto.ModifiedBy;
            transferRequest.ModifiedDate = DateTime.UtcNow;
            transferRequest.ApprovedBy = dto.ApprovedBy;

            if (dto.Status == "Approved")
            {
                var transferHistory = new TransferHistory
                {
                    ChildId = transferRequest.ChildId,
                    FromHouseId = transferRequest.FromHouseId,
                    ToHouseId = transferRequest.ToHouseId,
                    TransferDate = DateTime.UtcNow,
                    Status = "Completed",
                    HandledBy = dto.ApprovedBy,
                    CreatedDate = DateTime.UtcNow,
                    IsDeleted = false
                };

                await _transferHistoryRepository.AddAsync(transferHistory);

                var child = await _childRepository.GetByIdAsync(transferRequest.ChildId);
                child.HouseId = transferRequest.ToHouseId;
                await _childRepository.UpdateAsync(child);
            }

            await _transferRequestRepository.UpdateAsync(transferRequest);
            return transferRequest;
        }
    }
}
