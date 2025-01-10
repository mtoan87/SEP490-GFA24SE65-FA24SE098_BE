using ChildrenVillageSOS_DAL.DTO.ChildNeedsDTO;
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
    public class ChildNeedRepository : RepositoryGeneric<ChildNeed>, IChildNeedRepository
    {
        public ChildNeedRepository(SoschildrenVillageDbContext context) : base(context)
        {

        }

        public async Task<List<ChildNeed>> SearchChildNeeds(SearchChildNeedsDTO searchChildNeedsDTO)
        {
            var query = _context.ChildNeeds.AsQueryable();

            // Nếu có SearchTerm, tìm kiếm trong các cột cần tìm
            if (!string.IsNullOrEmpty(searchChildNeedsDTO.SearchTerm))
            {
                query = query.Where(x =>
                    (x.Id.ToString().Contains(searchChildNeedsDTO.SearchTerm) ||
                     x.ChildId.Contains(searchChildNeedsDTO.SearchTerm) ||
                     x.NeedDescription.Contains(searchChildNeedsDTO.SearchTerm) ||
                     x.NeedType.Contains(searchChildNeedsDTO.SearchTerm) ||
                     x.Priority.Contains(searchChildNeedsDTO.SearchTerm) ||
                     x.Remarks.Contains(searchChildNeedsDTO.SearchTerm) ||
                     x.Status.Contains(searchChildNeedsDTO.SearchTerm)
                    )
                );
            }
            return await query.ToListAsync();
        }
    }
}
