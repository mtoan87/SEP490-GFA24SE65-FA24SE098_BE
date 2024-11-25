using ChildrenVillageSOS_DAL.DTO.HouseDTO;
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
            dt.Columns.Add("HouseId", typeof(string));
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
                    HouseId = house.Id,
                    HouseName = house.HouseName,
                    HouseNumber = house.HouseNumber,
                    Location = house.Location,
                    Description = house.Description,
                    HouseMember = house.HouseMember,
                    HouseOwner = house.HouseOwner,
                    Status = house.Status,
                    UserAccountId = house.UserAccountId,
                    VillageId = house.VillageId,
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
                    HouseId = h.Id,
                    HouseName = h.HouseName,
                    HouseNumber = h.HouseNumber,
                    Location = h.Location,
                    Description = h.Description,
                    HouseMember = h.HouseMember,
                    HouseOwner = h.HouseOwner,
                    Status = h.Status,
                    UserAccountId = h.UserAccountId,
                    VillageId = h.VillageId,
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
                    HouseId = h.Id,
                    HouseName = h.HouseName,
                    HouseNumber = h.HouseNumber,
                    Location = h.Location,
                    Description = h.Description,
                    HouseMember = h.HouseMember,
                    HouseOwner = h.HouseOwner,
                    Status = h.Status,
                    UserAccountId = h.UserAccountId,
                    VillageId = h.VillageId,
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
    }
}
