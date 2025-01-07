using ChildrenVillageSOS_DAL.DTO.TransferRequestDTO;
using ChildrenVillageSOS_DAL.DTO.VillageDTO;
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

        public async Task<List<TransferRequest>> SearchTransferRequests(SearchTransferRequestDTO searchTransferRequestDTO)
        {
            var query = _context.TransferRequests.AsQueryable();

            // Nếu có SearchTerm, tìm kiếm trong các cột cần tìm
            if (!string.IsNullOrEmpty(searchTransferRequestDTO.SearchTerm))
            {
                query = query.Where(x =>
                    (x.Id.ToString().Contains(searchTransferRequestDTO.SearchTerm) ||
                     x.ChildId.Contains(searchTransferRequestDTO.SearchTerm) ||
                     x.FromHouseId.Contains(searchTransferRequestDTO.SearchTerm) ||
                     x.ToHouseId.Contains(searchTransferRequestDTO.SearchTerm) ||
                     x.RequestReason.Contains(searchTransferRequestDTO.SearchTerm) ||
                     x.Status.Contains(searchTransferRequestDTO.SearchTerm) ||
                     x.RequestDate.Value.ToString("yyyy-MM-dd").Contains(searchTransferRequestDTO.SearchTerm)
                    )
                );
            }
            return await query.ToListAsync();
        }
    }
}
