using ChildrenVillageSOS_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_REPO.Interface
{
    public interface ITransferRequestRepository : IRepositoryGeneric<TransferRequest>
    {
        Task<IEnumerable<TransferRequest>> GetTransferRequestsByHouse(string houseId);
        Task<TransferRequest> GetTransferRequestWithDetails(int requestId);
        Task<IEnumerable<TransferRequest>> GetAllTransferRequestsWithDetails();
    }
}
