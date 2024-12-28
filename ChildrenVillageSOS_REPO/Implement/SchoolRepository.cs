using ChildrenVillageSOS_DAL.DTO.SchoolDTO;
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
    }
}
