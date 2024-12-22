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

        public TransferRequestService(ITransferRequestRepository transferRequestRepository)
        {
            _transferRequestRepository = transferRequestRepository;
        }

        public async Task<IEnumerable<TransferRequest>> GetAllTransferRequests()
        {
            return await _transferRequestRepository.GetAllNotDeletedAsync();
        }

        public async Task<TransferRequest> GetTransferRequestById(int id)
        {
            return await _transferRequestRepository.GetByIdAsync(id);
        }

        public async Task<TransferRequest> CreateTransferRequest(CreateTransferRequestDTO createTransferRequest)
        {
            var newTransferRequest = new TransferRequest
            {
                ChildId = createTransferRequest.ChildId,
                FromHouseId = createTransferRequest.FromHouseId,
                ToHouseId = createTransferRequest.ToHouseId,
                RequestDate = createTransferRequest.RequestDate ?? DateTime.Now,
                Status = createTransferRequest.Status,
                DirectorNote = createTransferRequest.DirectorNote,
                RequestReason = createTransferRequest.RequestReason,
                ApprovedBy = createTransferRequest.ApprovedBy,
                CreatedBy = createTransferRequest.CreatedBy,
                CreatedDate = DateTime.Now,
                IsDeleted = false
            };

            await _transferRequestRepository.AddAsync(newTransferRequest);
            return newTransferRequest;
        }

        public async Task<TransferRequest> UpdateTransferRequest(int id, UpdateTransferRequestDTO updateTransferRequest)
        {
            var existingTransferRequest = await _transferRequestRepository.GetByIdAsync(id);
            if (existingTransferRequest == null)
            {
                throw new Exception($"TransferRequest with ID {id} not found!");
            }

            existingTransferRequest.ChildId = updateTransferRequest.ChildId;
            existingTransferRequest.FromHouseId = updateTransferRequest.FromHouseId;
            existingTransferRequest.ToHouseId = updateTransferRequest.ToHouseId;
            existingTransferRequest.RequestDate = updateTransferRequest.RequestDate ?? DateTime.Now;
            existingTransferRequest.Status = updateTransferRequest.Status;
            existingTransferRequest.DirectorNote = updateTransferRequest.DirectorNote;
            existingTransferRequest.RequestReason = updateTransferRequest.RequestReason;
            existingTransferRequest.ApprovedBy = updateTransferRequest.ApprovedBy;
            existingTransferRequest.ModifiedBy = updateTransferRequest.ModifiedBy;
            existingTransferRequest.ModifiedDate = DateTime.Now;

            await _transferRequestRepository.UpdateAsync(existingTransferRequest);
            return existingTransferRequest;
        }

        public async Task<TransferRequest> DeleteTransferRequest(int id)
        {
            var transferRequest = await _transferRequestRepository.GetByIdAsync(id);
            if (transferRequest == null)
            {
                throw new Exception($"TransferRequest with ID {id} not found");
            }

            if (transferRequest.IsDeleted)
            {
                await _transferRequestRepository.RemoveAsync(transferRequest);
            }
            else
            {
                transferRequest.IsDeleted = true;
                await _transferRequestRepository.UpdateAsync(transferRequest);
            }

            return transferRequest;
        }

        public async Task<TransferRequest> RestoreTransferRequest(int id)
        {
            var transferRequest = await _transferRequestRepository.GetByIdAsync(id);
            if (transferRequest == null)
            {
                throw new Exception($"TransferRequest with ID {id} not found");
            }

            if (transferRequest.IsDeleted)
            {
                transferRequest.IsDeleted = false;
                await _transferRequestRepository.UpdateAsync(transferRequest);
            }

            return transferRequest;
        }
    }
}
