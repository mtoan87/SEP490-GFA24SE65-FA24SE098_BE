using ChildrenVillageSOS_DAL.DTO.AcademicReportDTO;
using ChildrenVillageSOS_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_SERVICE.Interface
{
    public interface IAcademicReportService
    {
        Task<IEnumerable<AcademicReport>> GetAllAcademicReports();
        Task<AcademicReport> GetAcademicReportById(int id);
        Task<AcademicReport> CreateAcademicReport(CreateAcademicReportDTO createReport);
        Task<AcademicReport> UpdateAcademicReport(int id, UpdateAcademicReportDTO updateReport);
        Task<AcademicReport> DeleteAcademicReport(int id);
        Task<AcademicReport> RestoreAcademicReport(int id);
        Task<AcademicReportResponseDTO[]> GetAllAcademicReportIsDeleteAsync();
        Task<IEnumerable<AcademicReportResponseDTO>> GetAllAcademicReportWithImg();
        Task<AcademicReportResponseDTO> GetAcademicReportByIdWithImg(int id);
        Task<List<AcademicReport>> SearchAcademicReports(SearchAcademicReportDTO searchAcademicReportDTO);
    }
}
