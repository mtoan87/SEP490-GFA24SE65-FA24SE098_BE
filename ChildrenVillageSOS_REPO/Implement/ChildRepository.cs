using ChildrenVillageSOS_DAL.DTO.ChildDTO;
using ChildrenVillageSOS_DAL.DTO.EventDTO;
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
    public class ChildRepository : RepositoryGeneric<Child>, IChildRepository
    {
        public ChildRepository(SoschildrenVillageDbContext context) : base(context)
        {

        }
        public async Task<IEnumerable<Child>> GetAllAsync()
        {
            // Sử dụng Include để lấy các hình ảnh liên quan đến Event
            return await _context.Children
                                 .Include(e => e.Images)  // Dùng Include để lấy các hình ảnh liên quan
                                 .Where(e => !e.IsDeleted) // Nếu cần, lọc các sự kiện không bị xóa
                                 .ToListAsync();
        }
        public ChildResponseDTO GetChildByIdWithImg(string childId)
        {
            var childDetails = _context.Children
                .Where(x => x.Id == childId && !x.IsDeleted) // Ensure the event is not deleted
                .Select(x => new ChildResponseDTO
                {
                    Id = x.Id,
                    ChildName = x.ChildName,
                    HealthStatus = x.HealthStatus,
                    HouseId = x.HouseId,
                    FacilitiesWalletId = x.FacilitiesWalletId,
                    SystemWalletId = x.SystemWalletId,
                    FoodStuffWalletId = x.FoodStuffWalletId,
                    HealthWalletId = x.HealthWalletId,
                    NecessitiesWalletId = x.NecessitiesWalletId,
                    Amount = x.Amount ?? 0,
                    CurrentAmount = x.CurrentAmount ?? 0,
                    AmountLimit = x.AmountLimit ?? 0,
                    Gender = x.Gender,
                    Dob = x.Dob,
                    CreatedDate = x.CreatedDate,
                    Status = x.Status,
                    ImageUrls = x.Images.Where(img => !img.IsDeleted)
                                     .Select(img => img.UrlPath)
                                     .ToArray()
                })
                .FirstOrDefault();  // Get the first (or default) result

            return childDetails;
        }
    }
}
