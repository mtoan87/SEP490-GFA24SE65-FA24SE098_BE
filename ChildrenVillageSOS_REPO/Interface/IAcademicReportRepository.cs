using ChildrenVillageSOS_DAL.DTO.BookingDTO;
using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_REPO.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_REPO.Interface
{
    public interface IAcademicReportRepository : IRepositoryGeneric<AcademicReport>
    {
        Task<List<AcademicReport>> GetAcademicPerformanceDistribution();
    }
}
