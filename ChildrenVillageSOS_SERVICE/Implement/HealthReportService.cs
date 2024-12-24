using ChildrenVillageSOS_DAL.DTO.HealthReportDTO;
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
    public class HealthReportService : IHealthReportService
    {
        private readonly IHealthReportRepository _healthReportRepository;

        public HealthReportService(IHealthReportRepository healthReportRepository)
        {
            _healthReportRepository = healthReportRepository;
        }

        public async Task<IEnumerable<HealthReport>> GetAllHealthReports()
        {
            return await _healthReportRepository.GetAllAsync();
        }

        public async Task<HealthReport> GetHealthReportById(int id)
        {
            var report = await _healthReportRepository.GetByIdAsync(id);
            if (report == null)
            {
                throw new Exception($"HealthReport with ID {id} not found!");
            }
            return report;
        }

        public async Task<HealthReport> CreateHealthReport(CreateHealthReportDTO createReport)
        {
            var existingReport = await _healthReportRepository.FindAsync(r => r.ChildId == createReport.ChildId);

            if (existingReport != null)
            {
                throw new InvalidOperationException($"An health report already exists for ChildId {createReport.ChildId}.");
            }

            var newReport = new HealthReport
            {
                ChildId = createReport.ChildId,
                NutritionalStatus = createReport.NutritionalStatus,
                MedicalHistory = createReport.MedicalHistory,
                VaccinationStatus = createReport.VaccinationStatus,
                Weight = createReport.Weight,
                Height = createReport.Height,
                CheckupDate = createReport.CheckupDate,
                DoctorName = createReport.DoctorName,
                Recommendations = createReport.Recommendations,
                HealthStatus = createReport.HealthStatus,
                FollowUpDate = createReport.FollowUpDate,
                Illnesses = createReport.Illnesses,
                Allergies = createReport.Allergies,
                HealthCertificate = createReport.HealthCertificate,
                Status = createReport.Status,
                CreatedBy = createReport.CreatedBy,
                CreatedDate = DateTime.Now,
                IsDeleted = false
            };

            await _healthReportRepository.AddAsync(newReport);
            return newReport;
        }

        public async Task<HealthReport> UpdateHealthReport(int id, UpdateHealthReportDTO updateReport)
        {
            var existingReport = await _healthReportRepository.GetByIdAsync(id);
            if (existingReport == null)
            {
                throw new Exception($"HealthReport with ID {id} not found!");
            }

            existingReport.ChildId = updateReport.ChildId;
            existingReport.NutritionalStatus = updateReport.NutritionalStatus;
            existingReport.MedicalHistory = updateReport.MedicalHistory;
            existingReport.VaccinationStatus = updateReport.VaccinationStatus;
            existingReport.Weight = updateReport.Weight;
            existingReport.Height = updateReport.Height;
            existingReport.CheckupDate = updateReport.CheckupDate;
            existingReport.DoctorName = updateReport.DoctorName;
            existingReport.Recommendations = updateReport.Recommendations;
            existingReport.HealthStatus = updateReport.HealthStatus;
            existingReport.FollowUpDate = updateReport.FollowUpDate;
            existingReport.Illnesses = updateReport.Illnesses;
            existingReport.Allergies = updateReport.Allergies;
            existingReport.HealthCertificate = updateReport.HealthCertificate;
            existingReport.Status = updateReport.Status;
            existingReport.ModifiedBy = updateReport.ModifiedBy;
            existingReport.ModifiedDate = DateTime.Now;

            await _healthReportRepository.UpdateAsync(existingReport);
            return existingReport;
        }

        public async Task<HealthReport> DeleteHealthReport(int id)
        {
            var report = await _healthReportRepository.GetByIdAsync(id);
            if (report == null)
            {
                throw new Exception($"Health report with ID {id} not found!");
            }

            await _healthReportRepository.RemoveAsync(report);
            return report;
        }

        public async Task<HealthReport> RestoreHealthReport(int id)
        {
            var report = await _healthReportRepository.GetByIdAsync(id);
            if (report == null)
            {
                throw new Exception($"Health report with ID {id} not found");
            }

            if (report.IsDeleted)
            {
                report.IsDeleted = false;
                await _healthReportRepository.UpdateAsync(report);
            }

            return report;
        }
    }
}
