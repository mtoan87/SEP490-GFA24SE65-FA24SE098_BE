using ChildrenVillageSOS_DAL.DTO.House;
using ChildrenVillageSOS_DAL.Helpers;
using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_REPO.Implement;
using ChildrenVillageSOS_REPO.Interface;
using ChildrenVillageSOS_SERVICE.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_SERVICE.Implement
{
    public class HouseService : IHouseService
    {
        private readonly IHouseRepository _houseRepository;

        public HouseService(IHouseRepository houseRepository)
        {
            _houseRepository = houseRepository;
        }

        public async Task<IEnumerable<House>> GetAllHouses()
        {
            return await _houseRepository.GetAllAsync();
        }

        public async Task<House> GetHouseById(string id)
        {
            return await _houseRepository.GetByIdAsync(id);
        }

        public async Task<House> CreateHouse(CreateHouseDTO createHouse)
        {
            // Lấy toàn bộ danh sách HouseId hiện có
            var allHouseIds = await _houseRepository.Entities()
                                                    .Select(h => h.Id)
                                                    .ToListAsync();

            // Sử dụng hàm GenerateId từ IdGenerator
            string newHouseId = IdGenerator.GenerateId(allHouseIds, "H");

            var newHouse = new House
            {
                Id = newHouseId,
                HouseName = createHouse.HouseName,
                HouseNumber = createHouse.HouseNumber,
                Location = createHouse.Location,
                Description = createHouse.Description,
                HouseMember = createHouse.HouseMember,
                HouseOwner = createHouse.HouseOwner,
                UserAccountId = createHouse.UserAccountId,
                VillageId = createHouse.VillageId,
                IsDeleted = createHouse.IsDeleted
            };

            await _houseRepository.AddAsync(newHouse);
            return newHouse;
        }

        public async Task<House> UpdateHouse(string id, UpdateHouseDTO updateHouse)
        {
            var existingHouse = await _houseRepository.GetByIdAsync(id);
            if (existingHouse == null)
            {
                throw new Exception($"House with ID {id} not found!");
            }

            existingHouse.HouseName = updateHouse.HouseName;
            existingHouse.HouseNumber = updateHouse.HouseNumber;
            existingHouse.Location = updateHouse.Location;
            existingHouse.Description = updateHouse.Description;
            existingHouse.HouseMember = updateHouse.HouseMember;
            existingHouse.HouseOwner = updateHouse.HouseOwner;
            existingHouse.UserAccountId = updateHouse.UserAccountId;
            existingHouse.VillageId = updateHouse.VillageId;
            existingHouse.IsDeleted = updateHouse.IsDeleted;

            await _houseRepository.UpdateAsync(existingHouse);
            return existingHouse;
        }

        public async Task<House> DeleteHouse(string id)
        {
            var house = await _houseRepository.GetByIdAsync(id);
            if (house == null)
            {
                throw new Exception($"House with ID {id} not found!");
            }

            await _houseRepository.RemoveAsync(house);
            return house;
        }
    }
}
