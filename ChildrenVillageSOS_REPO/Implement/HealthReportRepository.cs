using ChildrenVillageSOS_DAL.DTO.HealthReportDTO;
using ChildrenVillageSOS_DAL.DTO.VillageDTO;
using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_REPO.Interface;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
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

        public async Task<List<HealthReport>> SearchHealthReports(SearchHealthReportDTO searchHealthReportDTO)
        {
            var query = _context.HealthReports.AsQueryable();

            // Nếu có SearchTerm, tìm kiếm trong các cột cần tìm
            if (!string.IsNullOrEmpty(searchHealthReportDTO.SearchTerm))
            {
                query = query.Where(x =>
                    (x.Id.ToString().Contains(searchHealthReportDTO.SearchTerm) ||
                     x.ChildId.Contains(searchHealthReportDTO.SearchTerm) ||
                     x.NutritionalStatus.Contains(searchHealthReportDTO.SearchTerm) ||
                     x.MedicalHistory.Contains(searchHealthReportDTO.SearchTerm) ||
                     x.VaccinationStatus.Contains(searchHealthReportDTO.SearchTerm) ||
                     x.Weight.Value.ToString().Contains(searchHealthReportDTO.SearchTerm) ||
                     x.Height.Value.ToString().Contains(searchHealthReportDTO.SearchTerm) ||
                     x.DoctorName.Contains(searchHealthReportDTO.SearchTerm) ||
                     x.CheckupDate.Value.ToString("yyyy-MM-dd").Contains(searchHealthReportDTO.SearchTerm) ||
                     x.Recommendations.Contains(searchHealthReportDTO.SearchTerm) ||
                     x.HealthStatus.Contains(searchHealthReportDTO.SearchTerm) ||
                     x.FollowUpDate.Value.ToString("yyyy-MM-dd").Contains(searchHealthReportDTO.SearchTerm) ||
                     x.Illnesses.Contains(searchHealthReportDTO.SearchTerm) ||
                     x.Allergies.Contains(searchHealthReportDTO.SearchTerm) ||
                     x.Status.Contains(searchHealthReportDTO.SearchTerm) 
                    )
                );
            }
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<HealthReport>> GetReportsByChildIdsAsync(IEnumerable<string> childIds)
        {
            return await _context.HealthReports
                .Where(hr => childIds.Contains(hr.ChildId) && !hr.IsDeleted)
                .ToListAsync();
        }
    }
}
