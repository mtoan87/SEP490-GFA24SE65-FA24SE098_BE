using ChildrenVillageSOS_DAL.DTO.HealthReportDTO;
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
    public class HealthReportRepository : RepositoryGeneric<HealthReport>, IHealthReportRepository
    {
        public HealthReportRepository(SoschildrenVillageDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<HealthReport>> GetAllNotDeletedAsync()
        {
            return await _context.HealthReports
                                 .Include(e => e.Images)
                                 .Where(e => !e.IsDeleted)
                                 .ToListAsync();
        }

        public async Task<HealthReportResponseDTO[]> GetAllHealthReportIsDeleteAsync()
        {
            return await _context.HealthReports
                .Where(hr => hr.IsDeleted)
                .Select(hr => new HealthReportResponseDTO
                {
                    Id = hr.Id,
                    ChildId = hr.ChildId,
                    NutritionalStatus = hr.NutritionalStatus,
                    MedicalHistory = hr.MedicalHistory,
                    VaccinationStatus = hr.VaccinationStatus,
                    Weight = hr.Weight,
                    Height = hr.Height,
                    CheckupDate = hr.CheckupDate,
                    DoctorName = hr.DoctorName,
                    Recommendations = hr.Recommendations,
                    HealthStatus = hr.HealthStatus,
                    FollowUpDate = hr.FollowUpDate,
                    Illnesses = hr.Illnesses,
                    Allergies = hr.Allergies,
                  
                    Status = hr.Status,
                    IsDeleted = hr.IsDeleted,
                    CreatedBy = hr.CreatedBy,
                    CreatedDate = hr.CreatedDate,
                    ModifiedBy = hr.ModifiedBy,
                    ModifiedDate = hr.ModifiedDate,
                    ImageUrls = hr.Images
                        .Where(img => !img.IsDeleted)
                        .Select(img => img.UrlPath)
                        .ToArray()
                })
                .ToArrayAsync();
        }

        public HealthReportResponseDTO GetHealthReportByIdWithImg(int healthReportId)
        {
            var healthReportDetails = _context.HealthReports
                .Where(hr => hr.Id == healthReportId && !hr.IsDeleted) // Lọc HealthReport chưa bị xóa
                .Select(hr => new HealthReportResponseDTO
                {
                    Id = hr.Id,
                    ChildId = hr.ChildId,
                    NutritionalStatus = hr.NutritionalStatus,
                    MedicalHistory = hr.MedicalHistory,
                    VaccinationStatus = hr.VaccinationStatus,
                    Weight = hr.Weight,
                    Height = hr.Height,
                    CheckupDate = hr.CheckupDate,
                    DoctorName = hr.DoctorName,
                    Recommendations = hr.Recommendations,
                    HealthStatus = hr.HealthStatus,
                    FollowUpDate = hr.FollowUpDate,
                    Illnesses = hr.Illnesses,
                    Allergies = hr.Allergies,
                  
                    Status = hr.Status,
                    IsDeleted = hr.IsDeleted,
                    CreatedBy = hr.CreatedBy,
                    CreatedDate = hr.CreatedDate,
                    ModifiedBy = hr.ModifiedBy,
                    ModifiedDate = hr.ModifiedDate,
                    ImageUrls = hr.Images
                        .Where(img => !img.IsDeleted)
                        .Select(img => img.UrlPath)
                        .ToArray()
                })
                .FirstOrDefault();

            return healthReportDetails;
        }
    }
}
