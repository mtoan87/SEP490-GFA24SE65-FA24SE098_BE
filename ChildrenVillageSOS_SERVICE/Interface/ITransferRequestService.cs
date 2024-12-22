using ChildrenVillageSOS_DAL.DTO.TransferRequestDTO;
using ChildrenVillageSOS_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_SERVICE.Interface
{
    public interface ITransferRequestService
    {
        Task<IEnumerable<TransferRequest>> GetAllTransferRequests();
        Task<TransferRequest> GetTransferRequestById(int id);
        Task<TransferRequest> CreateTransferRequest(CreateTransferRequestDTO createTransferRequest);
        Task<TransferRequest> UpdateTransferRequest(int id, UpdateTransferRequestDTO updateTransferRequest);
        Task<TransferRequest> DeleteTransferRequest(int id);
        Task<TransferRequest> RestoreTransferRequest(int id);
    }

}
