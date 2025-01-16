using ChildrenVillageSOS_DAL.DTO.DashboardDTO.TopStatCards;
using ChildrenVillageSOS_DAL.DTO.DonationDTO;
using ChildrenVillageSOS_DAL.DTO.EventDTO;
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
    public class EventRepository : RepositoryGeneric<Event>, IEventRepository
    {
        public EventRepository(SoschildrenVillageDbContext context) : base(context)
        {
            
        }

        public async Task<IEnumerable<Event>> GetByHouseIdAsync(string houseId)
        {
            return await _context.Events
                .Include(e => e.Village) // Include related data if needed
                .Where(e => e.Village.Houses.Any(h => h.Id == houseId))
                .ToListAsync();
        }

        public async Task<IEnumerable<Event>> GetByVillageIdAsync(string villageId)
        {
            return await _context.Events
                .Include(e => e.Village) // Include related data if needed
                .Where(e => e.VillageId == villageId)
                .ToListAsync();
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
                    EventCode = e.EventCode,
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
        public async Task<EventResponseDTO[]> GetAllEventArrayAsync()
        {
            return await _context.Events
                .Where(e => !e.IsDeleted)
                .Select(e => new EventResponseDTO
                {
                    Id = e.Id,
                    EventCode = e.EventCode,
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
        public async Task<EventResponseDTO[]> SearchEventArrayAsync(string searchTerm)
        {
            var query = _context.Events
                .Where(e => !e.IsDeleted); // Filter out deleted events

            if (!string.IsNullOrEmpty(searchTerm))
            {
                // Split the searchTerm into an array of terms
                string[] searchTerms = searchTerm.Split(' ').ToArray();

                // Apply search filters based on the array of search terms
                query = query.Where(e =>
                    searchTerms.All(term =>
                        (e.Id.ToString().Contains(term) ||
                         e.EventCode.Contains(term) ||
                         e.Name.Contains(term) ||
                         e.Status.Contains(term)
                        )
                    )
                );
            }

            var events = await query.ToListAsync(); // Fetch the events from the database

            // Now handle the date formatting on the client-side
            var result = events.Select(e => new EventResponseDTO
            {
                Id = e.Id,
                EventCode = e.EventCode,
                Name = e.Name,
                FacilitiesWalletId = e.FacilitiesWalletId,
                FoodStuffWalletId = e.FoodStuffWalletId,
                SystemWalletId = e.SystemWalletId,
                HealthWalletId = e.HealthWalletId,
                NecessitiesWalletId = e.NecessitiesWalletId,
                Description = e.Description,
                StartTime = e.StartTime,
                EndTime = e.EndTime,
                Amount = e.Amount ?? 0, // Use default value if null
                CurrentAmount = e.CurrentAmount ?? 0,
                AmountLimit = e.AmountLimit ?? 0,
                Status = e.Status,
                VillageId = e.VillageId,
                CreatedDate = e.CreatedDate,
                ImageUrls = e.Images
                            .Where(img => !img.IsDeleted) // Filter out deleted images
                            .Select(img => img.UrlPath)
                            .ToArray(), // Convert to array
            }).ToArray();

            return result;
        }

        public EventResponseDTO GetEventById(int eventId)
        {
            var eventDetails = _context.Events
                .Where(e => e.Id == eventId && !e.IsDeleted) // Ensure the event is not deleted
                .Select(e => new EventResponseDTO
                {
                    Id = e.Id,
                    EventCode = e.EventCode,
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
                .Where(e => !e.IsDeleted && e.Status == "Active")
                .CountAsync();

            return new TotalEventsStatDTO
            {
                TotalEvents = totalEvents,
                OnGoingEvents = onGoingEvents
            };
        }

        public async Task<EventDetailsDTO> GetEventDetails(int eventId)
        {
            var eventDetails = await _context.Events
                .Include(e => e.Donations.Where(d => !d.IsDeleted))
                .FirstOrDefaultAsync(e => e.Id == eventId && !e.IsDeleted);

            if (eventDetails == null)
            {
                throw new Exception("Event not found.");
            }

            // Lấy danh sách các donor/sponsor đã donate
            var donors = eventDetails.Donations.Select(d => new DonorDTO
            {
                Id = d.Id,
                UserName = d.UserName ?? "Unknown",
                UserEmail = d.UserEmail ?? "Unknown",
                Phone = d.Phone ?? "Unknown",
                Address = d.Address ?? "Unknown",
                DonationType = d.DonationType,
                Amount = d.Amount,
                DateTime = d.DateTime,
                Description = d.Description
            }).ToList();

            var result = new EventDetailsDTO
            {
                Id = eventDetails.Id,
                Name = eventDetails.Name,
                Description = eventDetails.Description,
                StartTime = eventDetails.StartTime,
                EndTime = eventDetails.EndTime,
                Amount = eventDetails.Amount,
                CurrentAmount = eventDetails.CurrentAmount,
                AmountLimit = eventDetails.AmountLimit,
                Status = eventDetails.Status,
                VillageId = eventDetails.VillageId,
                Donors = donors
            };

            return result;
        }

        public async Task<List<Event>> SearchEvents(SearchEventDTO searchEventDTO)
        {
            var query = _context.Events.AsQueryable();

            // Nếu có SearchTerm, tìm kiếm trong các cột cần tìm
            if (!string.IsNullOrEmpty(searchEventDTO.SearchTerm))
            {
                query = query.Where(x =>
                    (x.Id.ToString().Contains(searchEventDTO.SearchTerm) ||
                     x.EventCode.Contains(searchEventDTO.SearchTerm) ||
                     x.Name.Contains(searchEventDTO.SearchTerm) ||
                     x.EventCode.Contains(searchEventDTO.SearchTerm) ||
                     x.StartTime.Value.ToString("yyyy-MM-dd").Contains(searchEventDTO.SearchTerm) ||
                     x.EndTime.Value.ToString("yyyy-MM-dd").Contains(searchEventDTO.SearchTerm) ||
                     x.Status.Contains(searchEventDTO.SearchTerm)
                    )
                );
            }
            return await query.ToListAsync();
        }
    }
}
