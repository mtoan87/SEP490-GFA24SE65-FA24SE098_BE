using ChildrenVillageSOS_DAL.DTO.AcademicReportDTO;
using ChildrenVillageSOS_DAL.DTO.EventDTO;
using ChildrenVillageSOS_DAL.Helpers;
using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_REPO.Implement;
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
            var existingReport = await _academicReportRepository.FindAsync(r => r.ChildId == createReport.ChildId);

            if (existingReport != null)
            {
                throw new InvalidOperationException($"An academic report already exists for ChildId {createReport.ChildId}.");
            }

            var newReport = new AcademicReport
            {
                Diploma = createReport.Diploma,
                SchoolLevel = createReport.SchoolLevel,
                ChildId = createReport.ChildId,
                SchoolId = createReport.SchoolId,
                Gpa = createReport.Gpa,
                SchoolReport = SchoolReportGenerator.GetSchoolReportFromGPA(createReport.Gpa),
                Semester = createReport.Semester,
                AcademicYear = createReport.AcademicYear,
                Remarks = createReport.Remarks,
                Achievement = createReport.Achievement,
                Status = createReport.Status,
                Class = createReport.Class,
                Feedback = createReport.Feedback,
                CreatedBy = createReport.CreatedBy,
                CreatedDate = DateTime.Now,
                IsDeleted = false

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

            editReport.Diploma = updateReport.Diploma;
            editReport.SchoolLevel = updateReport.SchoolLevel;
            editReport.ChildId = updateReport.ChildId;
            editReport.SchoolId = updateReport.SchoolId;
            editReport.Gpa = updateReport.Gpa;
            editReport.SchoolReport = SchoolReportGenerator.GetSchoolReportFromGPA(editReport.Gpa);
            editReport.Semester = updateReport.Semester;
            editReport.AcademicYear = updateReport.AcademicYear;
            editReport.Remarks = updateReport.Remarks;
            editReport.Achievement = updateReport.Achievement;
            editReport.Status = updateReport.Status;
            editReport.Class = updateReport.Class;
            editReport.Feedback = updateReport.Feedback;
            editReport.ModifiedBy = updateReport.ModifiedBy;
            editReport.ModifiedDate = DateTime.Now;    

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

        public async Task<AcademicReport> RestoreAcademicReport(int id)
        {
            var report = await _academicReportRepository.GetByIdAsync(id);
            if (report == null)
            {
                throw new Exception($"Academic report with ID {id} not found");
            }

            if (report.IsDeleted)
            {
                report.IsDeleted = false;
                await _academicReportRepository.UpdateAsync(report);
            }

            return report;
        }
    }
}
