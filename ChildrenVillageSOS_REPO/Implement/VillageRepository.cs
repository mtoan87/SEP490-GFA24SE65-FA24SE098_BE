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
    public class VillageRepository : RepositoryGeneric<Village>, IVillageRepository
    {
        public VillageRepository(SoschildrenVillageDbContext context) : base(context)
        {

        }

        //lay toan bo anh
        public async Task<IEnumerable<Village>> GetAllNotDeletedAsync()
        {
            // Sử dụng Include để lấy các hình ảnh liên quan đến Event
            return await _context.Villages
                                 .Include(e => e.Images)  // Dùng Include để lấy các hình ảnh liên quan
                                 .Where(e => !e.IsDeleted) // Nếu cần, lọc các sự kiện không bị xóa
                                 .ToListAsync();
        }

        public VillageResponseDTO GetVillageByIdWithImg(string villageId)
        {
            var villageDetails = _context.Villages
                .Where(village => village.Id == villageId && !village.IsDeleted) // Kiểm tra ID và không bị xóa
                .Select(village => new VillageResponseDTO
                {
                    Id = village.Id,
                    VillageName = village.VillageName,
                    Location = village.Location,
                    Description = village.Description,
                    Status = village.Status,
                    UserAccountId = village.UserAccountId,
                    CreatedDate = village.CreatedDate,
                    ModifiedDate = village.ModifiedDate,
                    ImageUrls = village.Images.Where(img => !img.IsDeleted) // Lọc hình ảnh chưa bị xóa
                                              .Select(img => img.UrlPath)  // Lấy đường dẫn URL
                                              .ToArray()                  // Chuyển thành mảng
                })
                .FirstOrDefault(); // Lấy kết quả đầu tiên hoặc null nếu không tìm thấy

            return villageDetails;
        }

        public List<Village> GetVillagesDonatedByUser(string userAccountId)
        {
            using (var dbContext = new SoschildrenVillageDbContext())
            {
                var villageIds = dbContext.Donations
                    .Where(d => d.UserAccountId == userAccountId && (!d.IsDeleted.HasValue || !d.IsDeleted.Value)) // Lọc Donation theo UserAccountId
                    .Join(
                        dbContext.Events, // Tham gia với bảng Events
                        donation => donation.Id, // DonationId từ Donation
                        eventEntity => eventEntity.Id, // EventId từ Event
                        (donation, eventEntity) => eventEntity.VillageId // Lấy VillageId từ Event
                    )
                     // Loại bỏ các giá trị trùng lặp của VillageId
                    .ToList(); // Lấy danh sách các VillageId mà người dùng đã donate

                var villages = dbContext.Villages
                     
                    .ToList(); // Trả về danh sách các làng

                return villages;
            }
        }
    }
}
