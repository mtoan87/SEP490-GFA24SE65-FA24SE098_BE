using ChildrenVillageSOS_DAL.DTO.ChildProgressDTO;
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
    public class ChildProgressRepository : RepositoryGeneric<ChildProgress>, IChildProgressRepository
    {
        public ChildProgressRepository(SoschildrenVillageDbContext context) : base(context)
        {

        }
        public async Task<List<ChildProgress>> SearchChildProgresses(SearchChildProgressDTO searchChildProgressDTO)
        {
            var query = _context.ChildProgresses.AsQueryable();

            // Nếu có SearchTerm, tìm kiếm trong các cột cần tìm
            if (!string.IsNullOrEmpty(searchChildProgressDTO.SearchTerm))
            {
                query = query.Where(x =>
                    (x.Id.ToString().Contains(searchChildProgressDTO.SearchTerm) ||
                     x.ChildId.Contains(searchChildProgressDTO.SearchTerm) ||
                     x.Description.Contains(searchChildProgressDTO.SearchTerm) ||
                     x.Category.Contains(searchChildProgressDTO.SearchTerm) ||
                     x.EventId.Value.ToString().Contains(searchChildProgressDTO.SearchTerm) ||
                     x.ActivityId.Value.ToString().Contains(searchChildProgressDTO.SearchTerm) ||
                     x.Date.Value.ToString("yyyy-MM-dd").Contains(searchChildProgressDTO.SearchTerm)
                    )
                );
            }
            return await query.ToListAsync();
        }
    }
}
