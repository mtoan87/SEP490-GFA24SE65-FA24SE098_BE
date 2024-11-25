using ChildrenVillageSOS_DAL.DTO.AcademicReportDTO;
using ChildrenVillageSOS_DAL.DTO.EventDTO;
using ChildrenVillageSOS_DAL.Helpers;
using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_REPO.Interface;
using ChildrenVillageSOS_SERVICE.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_SERVICE.Implement
{
    public class AcademicReportService : IAcademicReportService
    {
        private readonly IAcademicReportRepository _academicReportRepository;

        public AcademicReportService(IAcademicReportRepository academicReportRepository)
        {
            _academicReportRepository = academicReportRepository;
        }

        public async Task<IEnumerable<AcademicReport>> GetAllAcademicReports()
        {
            return await _academicReportRepository.GetAllAsync();
        }

        public async Task<AcademicReport> GetAcademicReportById(int id)
        {
            var report = await _academicReportRepository.GetByIdAsync(id);
            if (report == null)
            {
                throw new Exception($"Academic report with ID {id} not found!");
            }
            return report;
        }

        public async Task<AcademicReport> CreateAcademicReport(CreateAcademicReportDTO createReport)
        {
            var newReport = new AcademicReport
            {
                Diploma = createReport.Diploma,
                ChildId = createReport.ChildId,
                Gpa = createReport.Gpa,
                SchoolReport = SchoolReportGenerator.GetSchoolReportFromGPA(createReport.Gpa),
            };
            await _academicReportRepository.AddAsync(newReport);
            return newReport;
        }

        public async Task<AcademicReport> UpdateAcademicReport(int id, UpdateAcademicReportDTO updateReport)
        {
            var editReport = await _academicReportRepository.GetByIdAsync(id);
            if (editReport == null)
            {
                throw new Exception($"Academic report with ID {id} not found!");
            }

            // Update fields
            editReport.Diploma = updateReport.Diploma;
            editReport.ChildId = updateReport.ChildId;
            editReport.Gpa = updateReport.Gpa;
            editReport.SchoolReport = SchoolReportGenerator.GetSchoolReportFromGPA(editReport.Gpa);

            await _academicReportRepository.UpdateAsync(editReport);
            return editReport;
        }

        public async Task<AcademicReport> DeleteAcademicReport(int id)
        {
            var report = await _academicReportRepository.GetByIdAsync(id);
            if (report == null)
            {
                throw new Exception($"Academic report with ID {id} not found!");
            }
            await _academicReportRepository.RemoveAsync(report);
            return report;
        }
    }
}
