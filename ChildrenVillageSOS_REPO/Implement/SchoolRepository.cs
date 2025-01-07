using ChildrenVillageSOS_DAL.DTO.ChildDTO;
using ChildrenVillageSOS_DAL.DTO.SchoolDTO;
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
    public class SchoolRepository : RepositoryGeneric<School>, ISchoolRepository
    {
        public SchoolRepository(SoschildrenVillageDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<School>> GetAllNotDeletedAsync()
        {

            return await _context.Schools
                                 .Include(e => e.Images)
                                 .Where(e => !e.IsDeleted)
                                 .ToListAsync();
        }

        public async Task<SchoolResponseDTO[]> GetAllSchoolsIsDeleted()
        {
            return await _context.Schools
                .Where(s => s.IsDeleted) // Lọc các trường đã bị đánh dấu xóa
                .Select(s => new SchoolResponseDTO
                {
                    Id = s.Id,
                    SchoolName = s.SchoolName ?? string.Empty,
                    Address = s.Address ?? string.Empty,
                    SchoolType = s.SchoolType ?? string.Empty,
                    PhoneNumber = s.PhoneNumber ?? string.Empty,
                    Email = s.Email ?? string.Empty,
                    IsDeleted = s.IsDeleted,
                    CreatedBy = s.CreatedBy,
                    CreatedDate = s.CreatedDate,
                    ModifiedBy = s.ModifiedBy,
                    ModifiedDate = s.ModifiedDate,
                    ImageUrls = s.Images
                        .Where(img => !img.IsDeleted) // Lọc các hình ảnh chưa bị xóa
                        .Select(img => img.UrlPath)
                        .ToArray() // Chuyển thành mảng
                })
                .ToArrayAsync(); // Trả về mảng bất đồng bộ
        }

        public SchoolResponseDTO GetSchoolByIdWithImg(string schoolId)
        {
            var schoolDetails = _context.Schools
                .Where(school => school.Id == schoolId && !school.IsDeleted) // Kiểm tra ID và không bị xóa
                .Select(school => new SchoolResponseDTO
                {
                    Id = school.Id,
                    SchoolName = school.SchoolName ?? string.Empty,
                    Address = school.Address ?? string.Empty,
                    SchoolType = school.SchoolType ?? string.Empty,
                    PhoneNumber = school.PhoneNumber ?? string.Empty,
                    Email = school.Email ?? string.Empty,
                    IsDeleted = school.IsDeleted,
                    CreatedBy = school.CreatedBy,
                    CreatedDate = school.CreatedDate,
                    ModifiedBy = school.ModifiedBy,
                    ModifiedDate = school.ModifiedDate,
                    ImageUrls = school.Images
                        .Where(img => !img.IsDeleted) // Lọc các hình ảnh chưa bị xóa
                        .Select(img => img.UrlPath)
                        .ToArray() // Chuyển thành mảng
                })
                .FirstOrDefault(); // Lấy kết quả đầu tiên hoặc null nếu không tìm thấy

            return schoolDetails;
        }

        public async Task<SchoolDetailsDTO> GetSchoolDetails(string schoolId)
        {
            // Truy xuất thông tin trường học
            var school = await _context.Schools
                .Include(s => s.Children.Where(c => !c.IsDeleted && c.SchoolId == schoolId))
                    .ThenInclude(c => c.AcademicReports.OrderByDescending(ar => ar.AcademicYear).ThenByDescending(ar => ar.Semester))
                .FirstOrDefaultAsync(s => s.Id == schoolId);

            if (school == null)
            {
                throw new Exception("School not found.");
            }

            var currentStudents = school.Children.Count;

            // Lấy danh sách trẻ đang theo học và chuyển đổi sang DTO
            var childrenList = school.Children.Select(c =>
            {
                var latestAcademicReport = c.AcademicReports.FirstOrDefault();
                return new ChildSummaryDTO
                {
                    Id = c.Id,
                    ChildName = c.ChildName ?? "Unknown",
                    AcademicYear = latestAcademicReport?.AcademicYear ?? "Unknown",
                    Semester = latestAcademicReport?.Semester ?? "Unknown",
                    Class = latestAcademicReport?.Class ?? "Unknown"
                };
            }).ToList();

            // Tạo DTO kết quả
            var result = new SchoolDetailsDTO
            {
                Id = school.Id,
                SchoolName = school.SchoolName,
                Address = school.Address,
                SchoolType = school.SchoolType,
                PhoneNumber = school.PhoneNumber,
                Email = school.Email,
                CurrentStudents = currentStudents,
                Children = childrenList
            };

            return result;
        }
        
        public async Task<List<School>> SearchSchools(SearchSchoolDTO searchSchoolDTO)
        {
            var query = _context.Schools.AsQueryable();

            // Nếu có SearchTerm, tìm kiếm trong các cột cần tìm
            if (!string.IsNullOrEmpty(searchSchoolDTO.SearchTerm))
            {
                query = query.Where(x =>
                    (x.Id.ToString().Contains(searchSchoolDTO.SearchTerm) ||
                     x.SchoolName.Contains(searchSchoolDTO.SearchTerm) ||
                     x.Address.Contains(searchSchoolDTO.SearchTerm) ||
                     x.SchoolType.Contains(searchSchoolDTO.SearchTerm) ||
                     x.PhoneNumber.Contains(searchSchoolDTO.SearchTerm) ||
                     x.Email.Contains(searchSchoolDTO.SearchTerm) 
                    )
                );
            }
            return await query.ToListAsync();
        }
    }
}
