using ChildrenVillageSOS_DAL.DTO.TransferHistoryDTO;
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
    public  class TransferHistoryRepository : RepositoryGeneric<TransferHistory>, ITransferHistoryRepository
    {
        public TransferHistoryRepository(SoschildrenVillageDbContext context) : base(context)
        {

        }

        public async Task<List<TransferHistory>> SearchTransferHistories(SearchTransferHistoryDTO searchTransferHistoryDTO)
        {
            var query = _context.TransferHistories.AsQueryable();

            // Nếu có SearchTerm, tìm kiếm trong các cột cần tìm
            if (!string.IsNullOrEmpty(searchTransferHistoryDTO.SearchTerm))
            {
                query = query.Where(x =>
                    (x.Id.ToString().Contains(searchTransferHistoryDTO.SearchTerm) ||
                     x.ChildId.Contains(searchTransferHistoryDTO.SearchTerm) ||
                     x.FromHouseId.Contains(searchTransferHistoryDTO.SearchTerm) ||
                     x.ToHouseId.Contains(searchTransferHistoryDTO.SearchTerm) ||
                     x.Notes.Contains(searchTransferHistoryDTO.SearchTerm) ||
                     x.Status.Contains(searchTransferHistoryDTO.SearchTerm) ||
                     x.TransferDate.Value.ToString("yyyy-MM-dd").Contains(searchTransferHistoryDTO.SearchTerm)
                    )
                );
            }
            return await query.ToListAsync();
        }
    }
}
