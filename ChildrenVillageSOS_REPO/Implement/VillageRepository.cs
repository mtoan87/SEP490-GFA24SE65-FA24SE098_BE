using ChildrenVillageSOS_DAL.DTO.ActivityDTO;
using ChildrenVillageSOS_DAL.DTO.ChildDTO;
using ChildrenVillageSOS_DAL.DTO.HouseDTO;
using ChildrenVillageSOS_DAL.DTO.VillageDTO;
using ChildrenVillageSOS_DAL.Helpers;
using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_REPO.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
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

        public async Task<VillageResponseDTO[]> GetVillageByEventIDAsync(int eventId)
        {
            return await _context.Events
                .Where(e => e.Id == eventId && !e.IsDeleted)
                .Select(e => e.Village)  // Lấy danh sách villages liên quan đến Event
                .Where(v => !v.IsDeleted) // Chỉ lấy các Village chưa bị xóa
                .Select(v => new VillageResponseDTO
                {
                    Id = v.Id,
                    VillageName = v.VillageName ?? string.Empty,
                    EstablishedDate = v.EstablishedDate,
                    TotalHouses = v.Houses.Count(h => !h.IsDeleted),
                    TotalChildren = v.Houses
                        .SelectMany(h => h.Children)
                        .Count(c => !c.IsDeleted),
                    ContactNumber = v.ContactNumber,
                    Location = v.Location ?? string.Empty,
                    Description = v.Description ?? string.Empty,
                    Status = "Active",
                    UserAccountId = v.UserAccountId,
                    CreatedBy = v.CreatedBy,
                    ModifiedBy = v.ModifiedBy,
                    RoleName = v.RoleName,
                    IsDeleted = v.IsDeleted,
                    CreatedDate = v.CreatedDate,
                    ModifiedDate = v.ModifiedDate,
                    ImageUrls = v.Images
                        .Where(img => !img.IsDeleted)
                        .Select(img => img.UrlPath)
                        .ToArray()
                })
                .ToArrayAsync();
        }

        public async Task<VillageResponseDTO[]> GetAllVillageIsDelete()
        {
            return await _context.Villages
                .Where(v => v.IsDeleted)
                .Select(v => new VillageResponseDTO
                {
                    Id = v.Id,
                    VillageName = v.VillageName ?? string.Empty,
                    EstablishedDate = v.EstablishedDate,
                    TotalHouses = v.Houses.Count(h => !h.IsDeleted),
                    TotalChildren = v.Houses
                        .SelectMany(h => h.Children)
                        .Count(c => !c.IsDeleted),
                    ContactNumber = v.ContactNumber,
                    Location = v.Location ?? string.Empty,
                    Description = v.Description ?? string.Empty,
                    Status = "Active",
                    UserAccountId = v.UserAccountId,
                    CreatedBy = v.CreatedBy,
                    ModifiedBy = v.ModifiedBy,
                    RoleName = v.RoleName,
                    IsDeleted = v.IsDeleted,
                    CreatedDate = v.CreatedDate,
                    ModifiedDate = v.ModifiedDate,
                    ImageUrls = v.Images
                        .Where(img => !img.IsDeleted) // Lọc các hình ảnh chưa bị xóa
                        .Select(img => img.UrlPath)
                        .ToArray() // Chuyển thành mảng
                })
                .ToArrayAsync(); // Chuyển kết quả thành mảng (Array) bất đồng bộ
        }

        public DataTable getVillage()
        {
            DataTable dt = new DataTable();
            dt.TableName = "VillageData";
            dt.Columns.Add("VillageId", typeof(string));
            dt.Columns.Add("VillageName", typeof(string));
            dt.Columns.Add("Location", typeof(string));
            dt.Columns.Add("Description", typeof(string));
            dt.Columns.Add("Status", typeof(string));
            dt.Columns.Add("UserAccountId", typeof(string));
           
            var _list = this._context.Villages.ToList();
            if (_list.Count > 0)
            {
                _list.ForEach(item =>
                {
                    dt.Rows.Add(
                        item.Id,
                        item.VillageName,
                        item.Location,
                        item.Description,
                        item.Status,
                        item.UserAccountId
                        );
                });
            }
            return dt;
        }
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
                    VillageName = village.VillageName ?? string.Empty,
                    EstablishedDate = village.EstablishedDate,
                    TotalHouses = village.Houses.Count(h => !h.IsDeleted),
                    TotalChildren = village.Houses
                        .SelectMany(h => h.Children)
                        .Count(c => !c.IsDeleted),
                    ContactNumber = village.ContactNumber,
                    Location = village.Location ?? string.Empty,
                    Description = village.Description ?? string.Empty,
                    Status = "Active",
                    UserAccountId = village.UserAccountId,
                    CreatedBy = village.CreatedBy,
                    ModifiedBy = village.ModifiedBy,
                    RoleName = village.RoleName,
                    IsDeleted = village.IsDeleted,
                    CreatedDate = village.CreatedDate,
                    ModifiedDate = village.ModifiedDate,
                    ImageUrls = village.Images
                        .Where(img => !img.IsDeleted) // Lọc các hình ảnh chưa bị xóa
                        .Select(img => img.UrlPath)
                        .ToArray() // Chuyển thành mảng                // Chuyển thành mảng
                })
                .FirstOrDefault(); // Lấy kết quả đầu tiên hoặc null nếu không tìm thấy

            return villageDetails;
        }

        public List<Village> GetVillagesDonatedByUser(string userAccountId)
        {
            using (var dbContext = new SoschildrenVillageDbContext())
            {
                var villageIds = dbContext.Donations
                    .Where(d => d.UserAccountId == userAccountId && (!d.IsDeleted)) // Kiểm tra IsDeleted
                    .Join(
                        dbContext.Events, // Tham gia với bảng Events
                        donation => donation.Id, // DonationId từ Donation
                        eventEntity => eventEntity.Id, // EventId từ Event
                        (donation, eventEntity) => eventEntity.VillageId // Lấy VillageId từ Event
                    )
                    .Distinct() // Loại bỏ các giá trị trùng lặp của VillageId
                    .ToList(); // Lấy danh sách các VillageId mà người dùng đã donate

                var villages = dbContext.Villages
                    .Where(v => villageIds.Contains(v.Id)) // Chỉ lấy các làng có trong danh sách villageIds
                    .ToList(); // Trả về danh sách các làng

                return villages;
            }
        }


        //Dashboard cho 1 lang co bao nhieu nha
        public async Task<IEnumerable<Village>> GetVillagesWithHouses()
        {
            return await _context.Villages
                .Include(v => v.Houses)
                .Where(v => !v.IsDeleted)
                .ToListAsync();
        }

        //public async Task UpdateVillageStatistics(string villageId)
        //{
        //    var village = await _context.Villages
        //        .Include(v => v.Houses)
        //        .ThenInclude(h => h.Children)
        //        .FirstOrDefaultAsync(v => v.Id == villageId && !v.IsDeleted);

        //    if (village != null)
        //    {
        //        village.TotalHouses = village.Houses.Count(h => !h.IsDeleted);
        //        village.TotalChildren = village.Houses
        //            .SelectMany(h => h.Children)
        //            .Count(c => !c.IsDeleted);

        //        _context.Villages.Update(village);
        //        await _context.SaveChangesAsync();
        //    }
        //}

        public async Task<IEnumerable<Village>> GetVillagesWithHousesAndChildrenAsync()
        {
            return await _context.Villages
                .Include(v => v.Houses)
                .ThenInclude(h => h.Children)
                .Include(v => v.Images)
                .Where(v => !v.IsDeleted)
                .ToListAsync();
        }

        public async Task<VillageDetailsDTO> GetVillageDetails(string villageId)
        {
            var village = await _context.Villages
                .Include(v => v.Houses.Where(h => !h.IsDeleted))
                .ThenInclude(h => h.Children.Where(c => !c.IsDeleted))            
                .FirstOrDefaultAsync(v => v.Id == villageId);

            if (village == null)
            {
                throw new Exception("Village not found.");
            }

            var activities = await _context.Activities
                .Where(a => a.VillageId == villageId && !a.IsDeleted)
                .Select(a => new ActivitySummaryDTO
            {
                Id = a.Id,
                ActivityName = a.ActivityName,
                Description = a.Description,
                StartDate = a.StartDate,
                EndDate = a.EndDate,
                Address = a.Address,
                ActivityType = a.ActivityType,
                Organizer = a.Organizer,
                Status = a.Status,

            }).ToListAsync();

            var totalHouses = village.Houses.Count;
            var totalChildren = village.Houses.Sum(h => h.Children.Count);

            var totalHouseOwners = village.Houses
                .Where(h => !string.IsNullOrEmpty(h.HouseOwner))
                .Select(h => h.HouseOwner)
                .Distinct()
                .Count();

            var totalMatureChildren = village.Houses
                .SelectMany(h => h.Children)
                .Count(c => AgeCalculator.CalculateAge(c.Dob) >= 18);

            var housesArray = village.Houses.Select(h => new HouseSummaryDTO
            {
                Id = h.Id,
                HouseName = h.HouseName,
                HouseOwner = h.HouseOwner ?? "Unknown",
                TotalChildren = h.Children.Count()
            }).ToList();

            var result = new VillageDetailsDTO
            {
                Id = village.Id,
                VillageName = village.VillageName,
                Location = village.Location,
                EstablishedDate = village.EstablishedDate,
                Description = village.Description,
                ContactNumber = village.ContactNumber,
                TotalHouses = totalHouses,
                TotalChildren = totalChildren,
                Houses = housesArray,
                TotalHouseOwners = totalHouseOwners,
                TotalMatureChildren = totalMatureChildren,
                Activities = activities
            };

            return result;
        }
    }
}
