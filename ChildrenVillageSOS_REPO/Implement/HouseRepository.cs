using ChildrenVillageSOS_DAL.DTO.ChildDTO;
using ChildrenVillageSOS_DAL.DTO.HouseDTO;
using ChildrenVillageSOS_DAL.DTO.InventoryDTO;
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
    public class HouseRepository : RepositoryGeneric<House>, IHouseRepository
    {
        public HouseRepository(SoschildrenVillageDbContext context) : base(context)
        {

        }
        public DataTable getHouse()
        {
            DataTable dt = new DataTable();
            dt.TableName = "HouseData";
            dt.Columns.Add("Id", typeof(string));
            dt.Columns.Add("HouseName", typeof(string));
            dt.Columns.Add("HouseNumber", typeof(int));
            dt.Columns.Add("Location", typeof(string));
            dt.Columns.Add("Description", typeof(string));
            dt.Columns.Add("HouseMember", typeof(int));
            dt.Columns.Add("HouseOwner", typeof(string));
            dt.Columns.Add("Status", typeof(string));
            dt.Columns.Add("HouserMotherId", typeof(string));
            dt.Columns.Add("VillageId", typeof(string));
            var _list = this._context.Houses.ToList();
            if (_list.Count > 0)
            {
                _list.ForEach(item =>
                {
                    dt.Rows.Add(
                        item.Id,
                        item.HouseName,
                        item.HouseNumber,
                        item.Location,
                        item.Description,
                        item.HouseMember,
                        item.HouseOwner,
                        item.Status,
                        item.UserAccountId,
                        item.VillageId);
                });
            }
            return dt;
        }
        public async Task<IEnumerable<House>> GetAllNotDeletedAsync()
        {
            // Sử dụng Include để lấy các hình ảnh liên quan đến Event
            return await _context.Houses
                                 .Include(e => e.Images)  // Dùng Include để lấy các hình ảnh liên quan
                                 .Where(e => !e.IsDeleted) // Nếu cần, lọc các sự kiện không bị xóa
                                 .ToListAsync();
        }

        public HouseResponseDTO GetHouseByIdWithImg(string houseId)
        {
            var houseDetails = _context.Houses
                .Where(house => house.Id == houseId && !house.IsDeleted) // Kiểm tra không bị xóa
                .Select(house => new HouseResponseDTO
                {
                    Id = house.Id,
                    HouseName = house.HouseName,
                    HouseNumber = house.HouseNumber,
                    Location = house.Location,
                    Description = house.Description,
                    HouseMember = house.HouseMember,
                    CurrentMembers = house.CurrentMembers,
                    HouseOwner = house.HouseOwner,
                    Status = house.Status,
                    UserAccountId = house.UserAccountId,
                    VillageId = house.VillageId,
                    FoundationDate = house.FoundationDate,
                    LastInspectionDate = house.LastInspectionDate,
                    MaintenanceStatus = house.MaintenanceStatus,
                    CreatedBy = house.CreatedBy,
                    ModifiedBy = house.ModifiedBy,
                  
                    CreatedDate = house.CreatedDate,
                    ModifiedDate = house.ModifiedDate,
                    ImageUrls = house.Images.Where(img => !img.IsDeleted) // Lọc các ảnh không bị xóa
                                            .Select(img => img.UrlPath) // Lấy URL của ảnh
                                            .ToArray()                 // Chuyển thành mảng
                })
                .FirstOrDefault(); // Lấy kết quả đầu tiên (hoặc null nếu không có)

            return houseDetails;
        }

        public async Task<string?> GetUserAccountIdByHouseId(string houseId)
        {
            var house = await _context.Houses
                .Where(h => h.Id == houseId && (h.IsDeleted == false || h.IsDeleted == null))
                .Select(h => h.UserAccountId)
                .FirstOrDefaultAsync();

            return house;
        }
        public House? GetHouseByUserAccountId(string userAccountId)
        {
            return _context.Houses.FirstOrDefault(h => h.UserAccountId == userAccountId && !h.IsDeleted);
        }
        //public async Task<HouseResponseDTO[]> GetAllHouseAsync()
        //{
        //    return await _context.Houses
        //        .Where(h => !h.IsDeleted)
        //        .Select(h => new HouseResponseDTO
        //        {
        //            HouseId = h.Id,
        //            HouseName = h.HouseName,
        //            HouseNumber = h.HouseNumber,
        //            Location = h.Location,
        //            Description = h.Description,
        //            HouseMember = h.HouseMember,
        //            HouseOwner = h.HouseOwner,
        //            Status = h.Status,
        //            UserAccountId = h.UserAccountId,
        //            VillageId = h.VillageId,
        //            IsDeleted = h.IsDeleted,
        //            ImageUrls = h.Images
        //                .Where(i => !i.IsDeleted)  // Exclude deleted images
        //                .Select(i => i.UrlPath)    // Select image URLs
        //                .ToArray()                 // Convert to array
        //        })
        //        .ToArrayAsync();  // Execute query and convert the result to an array asynchronously
        //}
        public async Task<HouseResponseDTO[]> GetAllHouseIsDeleteAsync()
        {
            return await _context.Houses
                .Where(h => h.IsDeleted)
                .Select(h => new HouseResponseDTO
                {
                    Id = h.Id,
                    HouseName = h.HouseName,
                    HouseNumber = h.HouseNumber,
                    Location = h.Location,
                    Description = h.Description,
                    HouseMember = h.HouseMember,
                    CurrentMembers = _context.Children.Count(c => c.HouseId == h.Id && !c.IsDeleted),
                    HouseOwner = h.HouseOwner,
                    Status = h.Status,
                    UserAccountId = h.UserAccountId,
                    VillageId = h.VillageId,
                    FoundationDate = h.FoundationDate,
                    LastInspectionDate = h.LastInspectionDate,
                    MaintenanceStatus = h.MaintenanceStatus,
                    CreatedBy = h.CreatedBy,
                    ModifiedBy = h.ModifiedBy,
                   
                    IsDeleted = h.IsDeleted,
                    ImageUrls = h.Images
                        .Where(i => !i.IsDeleted)  // Exclude deleted images
                        .Select(i => i.UrlPath)    // Select image URLs
                        .ToArray()                 // Convert to array
                })
                .ToArrayAsync();  // Execute query and convert the result to an array asynchronously
        }
        public async Task<HouseResponseDTO[]> GetHouseByVillageIdAsync(string villageId)
        {
            return await _context.Houses
                .Where(h => h.VillageId == villageId && (h.IsDeleted == null || h.IsDeleted == false)) // Ensure the house is not deleted
                .Select(h => new HouseResponseDTO
                {
                    Id = h.Id,
                    HouseName = h.HouseName,
                    HouseNumber = h.HouseNumber,
                    Location = h.Location,
                    Description = h.Description,
                    HouseMember = h.HouseMember,
                    CurrentMembers = h.CurrentMembers,
                    HouseOwner = h.HouseOwner,
                    Status = h.Status,
                    UserAccountId = h.UserAccountId,
                    VillageId = h.VillageId,
                    FoundationDate = h.FoundationDate,
                    LastInspectionDate = h.LastInspectionDate,
                    MaintenanceStatus = h.MaintenanceStatus,
                    CreatedBy = h.CreatedBy,
                    ModifiedBy = h.ModifiedBy,
                   
                    IsDeleted = h.IsDeleted,
                    ImageUrls = h.Images
                        .Where(i => !i.IsDeleted)  // Exclude deleted images
                        .Select(i => i.UrlPath)    // Select image URLs
                        .ToArray()                 // Convert to array
                })
                .ToArrayAsync();  // Convert the result to an array asynchronously
        }

        public async Task<string> GetHouseNameByIdAsync(string houseId)
        {
            if (string.IsNullOrEmpty(houseId))
                throw new ArgumentException("House ID cannot be null or empty.", nameof(houseId));

            var houseName = await _context.Houses
                .Where(h => h.Id == houseId && h.IsDeleted == false)
                .Select(h => h.HouseName)
                .FirstOrDefaultAsync();

            if (houseName == null)
                throw new KeyNotFoundException($"No house found with ID: {houseId}");

            return houseName;
        }

        public async Task<HouseDetailsDTO> GetHouseDetails(string houseId)
        {
            var house = await _context.Houses
                .Include(h => h.Children.Where(c => !c.IsDeleted))
                .FirstOrDefaultAsync(h => h.Id == houseId);

            if (house == null)
            {
                throw new Exception("House not found.");
            }

            var currentMembers = house.Children.Count;

            // Get the list of Inventory belonging to House
            var inventoryList = await _context.Inventories
                .Where(i => i.BelongsTo == "House" && i.BelongsToId == houseId)
                .Select(i => new InventorySummaryDTO
                {
                    Id = i.Id,
                    ItemName = i.ItemName,
                    Quantity = i.Quantity,
                    Purpose = i.Purpose ?? "Not Specified",
                    MaintenanceStatus = i.MaintenanceStatus,
                    LastInspectionDate = i.LastInspectionDate
                }).ToListAsync();

            // Calculate age
            var children = house.Children.ToList();
            var ageGroups = new Dictionary<string, int>
                {
                    { "0-5", 0 },
                    { "6-10", 0 },
                    { "11-15", 0 },
                    { "16-18", 0 }
                };
            var totalAge = 0;
            var maleCount = 0;
            var femaleCount = 0;

            foreach (var child in children)
            {
                var age = AgeCalculator.CalculateAge(child.Dob);
                totalAge += age;

                // Calculate age group
                var ageGroup = AgeCalculator.GetAgeGroup(child.Dob);
                var ageGroupLabel = AgeCalculator.GetAgeGroupLabel(ageGroup);
                if (!string.IsNullOrEmpty(ageGroupLabel))
                {
                    ageGroups[ageGroupLabel]++;
                }

                // Count children gender
                if (child.Gender?.ToLower() == "male")
                {
                    maleCount++;
                }
                else if (child.Gender?.ToLower() == "female")
                {
                    femaleCount++;
                }
            }

            var averageAge = children.Count > 0 ? (double)totalAge / children.Count : 0;

            //var achievementCount = await _context.AcademicReports
            //    .Where(ar => house.Children.Select(c => c.Id).Contains(ar.ChildId))
            //    .CountAsync();
         
            var childrenList = children.Select(c => new ChildSummaryDTO
            {
                Id = c.Id,
                ChildName = c.ChildName ?? "Unknown",
                HealthStatus = c.HealthStatus ?? "Unknown",
                Gender = c.Gender ?? "Unknown",
                Dob = c.Dob
            }).ToList();

            var result = new HouseDetailsDTO
            {
                Id = house.Id,
                HouseName = house.HouseName,
                HouseNumber = house.HouseNumber,
                Location = house.Location,
                Description = house.Description,
                HouseOwner = house.HouseOwner ?? "Unknown",
                HouseMember = house.HouseMember,
                CurrentMembers = currentMembers,
                FoundationDate = house.FoundationDate,
                MaintenanceStatus = house.MaintenanceStatus,
                Children = childrenList,
                Inventory = inventoryList,
                AgeGroups = ageGroups.Where(kvp => kvp.Value > 0).ToDictionary(kvp => kvp.Key, kvp => kvp.Value),
                AverageAge = averageAge,
                MaleCount = maleCount,
                FemaleCount = femaleCount,
                //AchievementCount = achievementCount
            };

            return result;
        }
        public async Task<List<House>> GetHousesByIdsAsync(List<string> houseIds)
        {
            if (houseIds == null || !houseIds.Any())
            {
                throw new ArgumentException("The houseIds list cannot be null or empty.");
            }

            return await _context.Houses
                .Where(h => houseIds.Contains(h.Id) && !h.IsDeleted) // Kiểm tra danh sách HouseId và loại bỏ các bản ghi đã bị xóa
                .ToListAsync();
        }
        public async Task<List<House>> SearchHouses(SearchHouseDTO searchHouseDTO)
        {
            var query = _context.Houses.AsQueryable();

            // Nếu có SearchTerm, tìm kiếm trong các cột cần tìm
            if (!string.IsNullOrEmpty(searchHouseDTO.SearchTerm))
            {
                query = query.Where(x =>
                    (x.Id.ToString().Contains(searchHouseDTO.SearchTerm) ||
                     x.HouseName.Contains(searchHouseDTO.SearchTerm) ||
                     x.HouseNumber.Value.ToString().Contains(searchHouseDTO.SearchTerm) ||
                     x.Location.Contains(searchHouseDTO.SearchTerm) ||
                     x.HouseOwner.Contains(searchHouseDTO.SearchTerm) ||
                     x.UserAccountId.Contains(searchHouseDTO.SearchTerm) ||
                     x.VillageId.Contains(searchHouseDTO.SearchTerm) ||
                     x.MaintenanceStatus.Contains(searchHouseDTO.SearchTerm) ||
                     x.Status.Contains(searchHouseDTO.SearchTerm)
                    )
                );
            }
            return await query.ToListAsync();
        }
    }
}
