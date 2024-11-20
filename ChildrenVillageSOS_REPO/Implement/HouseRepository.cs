using ChildrenVillageSOS_DAL.DTO.HouseDTO;
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
    public class HouseRepository : RepositoryGeneric<House>, IHouseRepository
    {
        public HouseRepository(SoschildrenVillageDbContext context) : base(context)
        {

        }
        public async Task<string?> GetUserAccountIdByHouseId(string houseId)
        {
            var house = await _context.Houses
                .Where(h => h.Id == houseId && (h.IsDeleted == false || h.IsDeleted == null))
                .Select(h => h.UserAccountId)
                .FirstOrDefaultAsync();

            return house;
        }
        public async Task<HouseResponseDTO[]> GetAllHouseAsync()
        {
            return await _context.Houses
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
