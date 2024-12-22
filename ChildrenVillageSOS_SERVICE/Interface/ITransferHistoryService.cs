using ChildrenVillageSOS_DAL.DTO.TransferHistoryDTO;
using ChildrenVillageSOS_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_SERVICE.Interface
{
    public interface ITransferHistoryService
    {
        Task<IEnumerable<TransferHistory>> GetAllTransferHistories();
        Task<TransferHistory> GetTransferHistoryById(int id);
        Task<TransferHistory> CreateTransferHistory(CreateTransferHistoryDTO createTransferHistory);
        Task<TransferHistory> UpdateTransferHistory(int id, UpdateTransferHistoryDTO updateTransferHistory);
        Task<TransferHistory> DeleteTransferHistory(int id);
        Task<TransferHistory> RestoreTransferHistory(int id);
    }
}
