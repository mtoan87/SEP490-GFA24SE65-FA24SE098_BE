using ChildrenVillageSOS_DAL.DTO.PaymentDTO;
using ChildrenVillageSOS_DAL.DTO.VillageDTO;
using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_REPO.Implement;
using ChildrenVillageSOS_REPO.Interface;
using ChildrenVillageSOS_SERVICE.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_SERVICE.Implement
{
    public class VillageService : IVillageService
    {
        private readonly IVillageRepository _villageRepository;
        public VillageService(IVillageRepository villageRepository)
        {
            _villageRepository = villageRepository;
        }
        public async Task<IEnumerable<Village>> GetAllVillage()
        {
            return await _villageRepository.GetAllAsync();
        }
        public async Task<Village> GetVillageById(string villageId)
        {
            return await _villageRepository.GetByIdAsync(villageId);
        }
        public async Task<Village> CreateVillage(CreateVillageDTO createVillage)
        {
            var newVillage = new Village
            {
                VillageId = createVillage.VillageId,
                VillageName = createVillage.VillageName,
                Location = createVillage.Location,
                Description = createVillage.Description,
                UserAccountId = createVillage.UserAccountId,
            };
            await _villageRepository.AddAsync(newVillage);
            return newVillage;
        }
        public async Task<Village> UpdateVillage(string villageId, UpdateVillageDTO updateVillage)
        {
            var updaVillage = await _villageRepository.GetByIdAsync(villageId);
            if (updaVillage == null)
            {
                throw new Exception($"Expense with ID{villageId} not found!");
            }
            updaVillage.VillageName = updateVillage.VillageName;

            updaVillage.Location = updateVillage.Location;
            updaVillage.Description = updateVillage.Description;
            updaVillage.UserAccountId = updateVillage.UserAccountId;
            await _villageRepository.UpdateAsync(updaVillage);
            return updaVillage;

        }
        public async Task<Village> DeleteVillage(string villageId)
        {
            var vil = await _villageRepository.GetByIdAsync(villageId);
            if (vil == null)
            {
                throw new Exception($"Village with ID{villageId} not found");
            }
            await _villageRepository.RemoveAsync(vil);
            return vil;
        }
    }
}
