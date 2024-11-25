using ChildrenVillageSOS_DAL.DTO.HealthReportDTO;
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
            var newReport = new HealthReport
            {
                ChildId = createReport.ChildId,
                NutritionalStatus = createReport.NutritionalStatus,
                MedicalHistory = createReport.MedicalHistory,
                HealthCertificate = createReport.HealthCertificate,
                VaccinationStatus = createReport.VaccinationStatus,
                Weight = createReport.Weight,
                Height = createReport.Height,
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

            existingReport.NutritionalStatus = updateReport.NutritionalStatus;
            existingReport.MedicalHistory = updateReport.MedicalHistory;
            existingReport.HealthCertificate = updateReport.HealthCertificate;
            existingReport.VaccinationStatus = updateReport.VaccinationStatus;
            existingReport.Weight = updateReport.Weight;
            existingReport.Height = updateReport.Height;

            await _healthReportRepository.UpdateAsync(existingReport);
            return existingReport;
        }

        public async Task<HealthReport> DeleteHealthReport(int id)
        {
            var report = await _healthReportRepository.GetByIdAsync(id);
            if (report == null)
            {
                throw new Exception($"HealthReport with ID {id} not found!");
            }

            await _healthReportRepository.RemoveAsync(report);
            return report;
        }
    }
}
