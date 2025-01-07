using ChildrenVillageSOS_DAL.DTO.HealthReportDTO;
using ChildrenVillageSOS_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_SERVICE.Interface
{
    public interface IHealthReportService
    {
        Task<IEnumerable<HealthReport>> GetAllHealthReports();
        Task<HealthReport> GetHealthReportById(int id);
        Task<HealthReport> CreateHealthReport(CreateHealthReportDTO createReport);
        Task<HealthReport> UpdateHealthReport(int id, UpdateHealthReportDTO updateReport);
        Task<HealthReport> DeleteHealthReport(int id);
        Task<HealthReport> RestoreHealthReport(int id);
        Task<HealthReportResponseDTO> GetHealthReportByIdWithImg(int id);
        Task<HealthReportResponseDTO[]> GetAllHealthReportIsDeleteAsync();
        Task<IEnumerable<HealthReportResponseDTO>> GetAllHealthReportWithImg();
        Task<List<HealthReport>> SearchHealthReports(SearchHealthReportDTO searchHealthReportDTO);
    }
}
