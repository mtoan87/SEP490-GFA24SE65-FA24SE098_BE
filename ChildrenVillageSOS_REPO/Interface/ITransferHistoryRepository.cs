using ChildrenVillageSOS_DAL.DTO.TransferHistoryDTO;
using ChildrenVillageSOS_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_REPO.Interface
{
    public interface ITransferHistoryRepository : IRepositoryGeneric<TransferHistory>
    {
        Task<List<TransferHistory>> SearchTransferHistories(SearchTransferHistoryDTO searchTransferHistoryDTO);
    }
}
