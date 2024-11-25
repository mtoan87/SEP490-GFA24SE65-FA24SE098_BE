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
    public class AcademicReportRepository : RepositoryGeneric<AcademicReport>, IAcademicReportRepository
    {
        public AcademicReportRepository(SoschildrenVillageDbContext context) : base(context)
        {

        }

        public async Task<List<AcademicReport>> GetAcademicPerformanceDistribution()
        {
            return await _context.AcademicReports
                .Where(x => x.Diploma != null && x.SchoolReport != null)
                .ToListAsync();
        }
    }
}
