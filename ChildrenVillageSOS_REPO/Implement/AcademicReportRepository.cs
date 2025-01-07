using ChildrenVillageSOS_DAL.DTO.AcademicReportDTO;
using ChildrenVillageSOS_DAL.DTO.VillageDTO;
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
    public class AcademicReportRepository : RepositoryGeneric<AcademicReport>, IAcademicReportRepository
    {
        public AcademicReportRepository(SoschildrenVillageDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<AcademicReport>> GetAllNotDeletedAsync()
        {
            return await _context.AcademicReports
                                 .Include(e => e.Images)
                                 .Where(e => !e.IsDeleted)
                                 .ToListAsync();
        }

        public async Task<AcademicReportResponseDTO[]> GetAllAcademicReportIsDeleteAsync()
        {
            return await _context.AcademicReports
                .Where(ar => ar.IsDeleted) // Lọc các báo cáo đã bị đánh dấu xóa
                .Select(ar => new AcademicReportResponseDTO
                {
                    Id = ar.Id,
                    Diploma = ar.Diploma,
                    SchoolLevel = ar.SchoolLevel,
                    ChildId = ar.ChildId,
                    SchoolId = ar.SchoolId,
                    Gpa = ar.Gpa,
                    SchoolReport = ar.SchoolReport,
                    Semester = ar.Semester,
                    AcademicYear = ar.AcademicYear,
                    Remarks = ar.Remarks,
                    Achievement = ar.Achievement,
                    Status = ar.Status,
                    Class = ar.Class,
                    Feedback = ar.Feedback,
                    IsDeleted = ar.IsDeleted,
                    CreatedBy = ar.CreatedBy,
                    CreatedDate = ar.CreatedDate,
                    ModifiedBy = ar.ModifiedBy,
                    ModifiedDate = ar.ModifiedDate,
                    ImageUrls = ar.Images
                        .Where(img => !img.IsDeleted) // Lọc các hình ảnh chưa bị xóa
                        .Select(img => img.UrlPath)
                        .ToArray()
                })
                .ToArrayAsync();
        }

        public AcademicReportResponseDTO GetAcademicReportByIdWithImg(int academicReportId)
        {
            var academicReportDetails = _context.AcademicReports
                .Where(ar => ar.Id == academicReportId && !ar.IsDeleted) // Lọc báo cáo chưa bị xóa
                .Select(ar => new AcademicReportResponseDTO
                {
                    Id = ar.Id,
                    Diploma = ar.Diploma,
                    SchoolLevel = ar.SchoolLevel,
                    ChildId = ar.ChildId,
                    SchoolId = ar.SchoolId,
                    Gpa = ar.Gpa,
                    SchoolReport = ar.SchoolReport,
                    Semester = ar.Semester,
                    AcademicYear = ar.AcademicYear,
                    Remarks = ar.Remarks,
                    Achievement = ar.Achievement,
                    Status = ar.Status,
                    Class = ar.Class,
                    Feedback = ar.Feedback,
                    IsDeleted = ar.IsDeleted,
                    CreatedBy = ar.CreatedBy,
                    CreatedDate = ar.CreatedDate,
                    ModifiedBy = ar.ModifiedBy,
                    ModifiedDate = ar.ModifiedDate,
                    ImageUrls = ar.Images
                        .Where(img => !img.IsDeleted) // Lọc các hình ảnh chưa bị xóa
                        .Select(img => img.UrlPath)
                        .ToArray()
                })
                .FirstOrDefault();

            return academicReportDetails;
        }


        public async Task<List<AcademicReport>> GetAcademicPerformanceDistribution()
        {
            return await _context.AcademicReports
                //.Where(x => x.Diploma != null && x.SchoolReport != null)
                .Where(x => x.SchoolLevel != null && x.SchoolReport != null)
                .ToListAsync();
        }

        public async Task<List<AcademicReport>> SearchAcademicReports(SearchAcademicReportDTO searchAcademicReportDTO)
        {
            var query = _context.AcademicReports.AsQueryable();

            // Nếu có SearchTerm, tìm kiếm trong các cột cần tìm
            if (!string.IsNullOrEmpty(searchAcademicReportDTO.SearchTerm))
            {
                query = query.Where(x =>
                    (x.Id.ToString().Contains(searchAcademicReportDTO.SearchTerm) ||
                     x.Diploma.Contains(searchAcademicReportDTO.SearchTerm) ||
                     x.ChildId.Contains(searchAcademicReportDTO.SearchTerm) ||
                     x.SchoolReport.Contains(searchAcademicReportDTO.SearchTerm) ||
                     x.Gpa.Value.ToString().Contains(searchAcademicReportDTO.SearchTerm) ||
                     x.SchoolLevel.Contains(searchAcademicReportDTO.SearchTerm) ||
                     x.SchoolId.Contains(searchAcademicReportDTO.SearchTerm) ||
                     x.Semester.Contains(searchAcademicReportDTO.SearchTerm) ||
                     x.AcademicYear.Contains(searchAcademicReportDTO.SearchTerm) ||
                     x.Remarks.Contains(searchAcademicReportDTO.SearchTerm) ||
                     x.Achievement.Contains(searchAcademicReportDTO.SearchTerm) ||
                     x.Status.Contains(searchAcademicReportDTO.SearchTerm)
                    )
                );
            }
            return await query.ToListAsync();
        }
    }
}
