using ChildrenVillageSOS_DAL.DTO.TransferHistoryDTO;
using ChildrenVillageSOS_DAL.DTO.VillageDTO;
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
    public class TransferHistoryService : ITransferHistoryService
    {
        private readonly ITransferHistoryRepository _transferHistoryRepository;

        public TransferHistoryService(ITransferHistoryRepository transferHistoryRepository)
        {
            _transferHistoryRepository = transferHistoryRepository;
        }

        public async Task<IEnumerable<TransferHistory>> GetAllTransferHistories()
        {
            return await _transferHistoryRepository.GetAllNotDeletedAsync();
        }

        public async Task<TransferHistory> GetTransferHistoryById(int id)
        {
            return await _transferHistoryRepository.GetByIdAsync(id);
        }

        public async Task<TransferHistory> CreateTransferHistory(CreateTransferHistoryDTO createTransferHistory)
        {
            var newTransferHistory = new TransferHistory
            {
                ChildId = createTransferHistory.ChildId,
                FromHouseId = createTransferHistory.FromHouseId,
                ToHouseId = createTransferHistory.ToHouseId,
                TransferDate = createTransferHistory.TransferDate,
                Status = createTransferHistory.Status,
                Notes = createTransferHistory.Notes,
                HandledBy = createTransferHistory.HandledBy,
                CreatedDate = DateTime.Now,
                IsDeleted = false
            };

            await _transferHistoryRepository.AddAsync(newTransferHistory);
            return newTransferHistory;
        }

        public async Task<TransferHistory> UpdateTransferHistory(int id, UpdateTransferHistoryDTO updateTransferHistory)
        {
            var existingTransferHistory = await _transferHistoryRepository.GetByIdAsync(id);
            if (existingTransferHistory == null)
            {
                throw new Exception($"TransferHistory with ID {id} not found!");
            }

            existingTransferHistory.ChildId = updateTransferHistory.ChildId;
            existingTransferHistory.FromHouseId = updateTransferHistory.FromHouseId;
            existingTransferHistory.ToHouseId = updateTransferHistory.ToHouseId;
            existingTransferHistory.TransferDate = updateTransferHistory.TransferDate;
            existingTransferHistory.Status = updateTransferHistory.Status;
            existingTransferHistory.Notes = updateTransferHistory.Notes;
            existingTransferHistory.HandledBy = updateTransferHistory.HandledBy;
            existingTransferHistory.ModifiedBy = updateTransferHistory.ModifiedBy;
            existingTransferHistory.ModifiedDate = DateTime.Now;

            await _transferHistoryRepository.UpdateAsync(existingTransferHistory);
            return existingTransferHistory;
        }

        public async Task<TransferHistory> DeleteTransferHistory(int id)
        {
            var transferHistory = await _transferHistoryRepository.GetByIdAsync(id);
            if (transferHistory == null)
            {
                throw new Exception($"TransferHistory with ID {id} not found");
            }

            if (transferHistory.IsDeleted)
            {
                await _transferHistoryRepository.RemoveAsync(transferHistory);
            }
            else
            {
                transferHistory.IsDeleted = true;
                await _transferHistoryRepository.UpdateAsync(transferHistory);
            }

            return transferHistory;
        }

        public async Task<TransferHistory> RestoreTransferHistory(int id)
        {
            var transferRequest = await _transferHistoryRepository.GetByIdAsync(id);
            if (transferRequest == null)
            {
                throw new Exception($"TransferRequest with ID {id} not found");
            }

            if (transferRequest.IsDeleted)
            {
                transferRequest.IsDeleted = false;
                await _transferHistoryRepository.UpdateAsync(transferRequest);
            }

            return transferRequest;
        }

        public async Task<List<TransferHistory>> SearchTransferHistories(SearchTransferHistoryDTO searchTransferHistoryDTO)
        {
            return await _transferHistoryRepository.SearchTransferHistories(searchTransferHistoryDTO);
        }

    }
}
