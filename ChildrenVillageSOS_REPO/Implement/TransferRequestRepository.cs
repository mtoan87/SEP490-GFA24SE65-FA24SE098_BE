using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_REPO.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_REPO.Implement
{
    public class TransferRequestRepository : RepositoryGeneric<TransferRequest>, ITransferRequestRepository
    {
        public TransferRequestRepository(SoschildrenVillageDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<TransferRequest>> GetTransferRequestsByHouse(string houseId)
        {
            return await _context.TransferRequests
                .Include(t => t.Child)
                .Include(t => t.FromHouse)
                .Include(t => t.ToHouse)
                .Where(t => !t.IsDeleted && (t.FromHouseId == houseId || t.ToHouseId == houseId))
                .ToListAsync();
        }

        // Get Id
        public async Task<TransferRequest> GetTransferRequestWithDetails(int requestId)
        {
            return await _context.TransferRequests
                .Include(t => t.Child)
                .Include(t => t.FromHouse)
                .Include(t => t.ToHouse)
                .FirstOrDefaultAsync(t => t.Id == requestId && !t.IsDeleted);
        }

        // Get All
        public async Task<IEnumerable<TransferRequest>> GetAllTransferRequestsWithDetails()
        {
            return await _context.TransferRequests
                .Include(t => t.Child)
                .Include(t => t.FromHouse)
                .Include(t => t.ToHouse)
                .Where(t => !t.IsDeleted && (t.Status == "Pending" || t.Status == "Rejected"))
                .ToListAsync();
        }
    }
}
