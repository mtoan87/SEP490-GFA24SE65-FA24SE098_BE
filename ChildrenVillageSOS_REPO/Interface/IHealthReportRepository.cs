using ChildrenVillageSOS_DAL.DTO.HealthReportDTO;
using ChildrenVillageSOS_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_REPO.Interface
{
    public interface IHealthReportRepository : IRepositoryGeneric<HealthReport>
    {
        Task<HealthReportResponseDTO[]> GetAllHealthReportIsDeleteAsync();
        HealthReportResponseDTO GetHealthReportByIdWithImg(int healthReportId);
    }
}
