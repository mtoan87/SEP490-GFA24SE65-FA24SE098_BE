using ChildrenVillageSOS_DAL.DTO.SubjectDetailDTO;
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
    public class SubjectDetailRepository : RepositoryGeneric<SubjectDetail>, ISubjectDetailRepository
    {
        public SubjectDetailRepository(SoschildrenVillageDbContext context) : base(context)
        {

        }

        public async Task<List<SubjectDetail>> SearchSubjects(SearchSubjectDTO searchSubjectDTO)
        {
            var query = _context.SubjectDetails.AsQueryable();

            // Nếu có SearchTerm, tìm kiếm trong các cột cần tìm
            if (!string.IsNullOrEmpty(searchSubjectDTO.SearchTerm))
            {
                query = query.Where(x =>
                    (x.Id.ToString().Contains(searchSubjectDTO.SearchTerm) ||
                     x.AcademicReportId.ToString().Contains(searchSubjectDTO.SearchTerm) ||
                     x.SubjectName.Contains(searchSubjectDTO.SearchTerm) ||
                     x.Remarks.Contains(searchSubjectDTO.SearchTerm) ||
                     x.Score.Value.ToString().Contains(searchSubjectDTO.SearchTerm) 
                    )
                );
            }
            return await query.ToListAsync();
        }
    }
}
