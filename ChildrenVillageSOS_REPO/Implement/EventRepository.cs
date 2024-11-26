using ChildrenVillageSOS_DAL.DTO.DashboardDTO.TopStatCards;
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
    public class EventRepository : RepositoryGeneric<Event>, IEventRepository
    {
        public EventRepository(SoschildrenVillageDbContext context) : base(context)
        {
            
        }
        public async Task<IEnumerable<Event>> GetAllNotDeletedAsync()
        {
            // Sử dụng Include để lấy các hình ảnh liên quan đến Event
            return await _context.Events
                                 .Include(e => e.Images)  // Dùng Include để lấy các hình ảnh liên quan
                                 .Where(e => !e.IsDeleted) // Nếu cần, lọc các sự kiện không bị xóa
                                 .ToListAsync();
        }


        public async Task<EventResponseDTO[]> GetAllEventIsDeleteAsync()
        {
            return await _context.Events
                .Where(e => e.IsDeleted) 
                .Select(e => new EventResponseDTO
                {
                    Id = e.Id,
                    Name = e.Name,
                    FacilitiesWalletId = e.FacilitiesWalletId,
                    FoodStuffWalletId = e.FoodStuffWalletId,
                    SystemWalletId = e.SystemWalletId,
                    HealthWalletId = e.HealthWalletId,
                    NecessitiesWalletId = e.NecessitiesWalletId,
                    Description = e.Description,
                    StartTime = e.StartTime,
                    EndTime = e.EndTime,
                    Amount = e.Amount ?? 0, // Sử dụng giá trị mặc định nếu null
                    CurrentAmount = e.CurrentAmount ?? 0,
                    AmountLimit = e.AmountLimit ?? 0,
                    Status = e.Status,
                    VillageId = e.VillageId,
                    CreatedDate = e.CreatedDate,
                    ImageUrls = e.Images
                                .Where(img => !img.IsDeleted) // Lọc hình ảnh chưa bị xóa
                                .Select(img => img.UrlPath)
                                .ToArray() // Chuyển thành mảng
                })
                .ToArrayAsync(); // Thực thi truy vấn và trả về mảng
        }

        public EventResponseDTO GetEventById(int eventId)
        {
            var eventDetails = _context.Events
                .Where(e => e.Id == eventId && !e.IsDeleted) // Ensure the event is not deleted
                .Select(e => new EventResponseDTO
                {
                    Id = e.Id,
                    Name = e.Name,
                    Description = e.Description,
                    StartTime = e.StartTime ?? default,  // Default value if null
                    EndTime = e.EndTime ?? default,      // Default value if null
                    Amount = e.Amount ?? 0,              // Default value if null
                    CurrentAmount = e.CurrentAmount ?? 0, // Default value if null
                    AmountLimit = e.AmountLimit ?? 0,    // Default value if null
                    Status = e.Status,
                    VillageId = e.VillageId,
                    FacilitiesWalletId = e.FacilitiesWalletId,
                    FoodStuffWalletId = e.FoodStuffWalletId,
                    SystemWalletId = e.SystemWalletId,
                    HealthWalletId = e.HealthWalletId,
                    NecessitiesWalletId = e.NecessitiesWalletId,
                    CreatedDate = e.CreatedDate,
                    ImageUrls = e.Images
                        .Where(i => !i.IsDeleted)  // Exclude deleted images
                        .Select(i => i.UrlPath)
                        .ToArray()  // Convert to array
                })
                .FirstOrDefault();  // Get the first (or default) result

            return eventDetails;
        }    

        public async Task<TotalEventsStatDTO> GetTotalEventsStatAsync()
        {
            // Tổng số sự kiện trong hệ thống (không bị xóa)
            var totalEvents = await _context.Events
                .Where(e => !e.IsDeleted)
                .CountAsync();

            // Số sự kiện đang active
            var onGoingEvents = await _context.Events
                .Where(e => !e.IsDeleted && e.Status == "Scheduled")
                .CountAsync();

            return new TotalEventsStatDTO
            {
                TotalEvents = totalEvents,
                OnGoingEvents = onGoingEvents
            };
        }

    }
}
