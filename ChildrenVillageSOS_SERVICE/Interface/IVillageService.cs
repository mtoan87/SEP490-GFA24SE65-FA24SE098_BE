using ChildrenVillageSOS_DAL.DTO.VillageDTO;
using ChildrenVillageSOS_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_SERVICE.Interface
{
    public interface IVillageService
    {
        Task<IEnumerable<Village>> GetAllVillage();
        Task<Village> GetVillageById(string villageId);
        Task<Village> CreateVillage(CreateVillageDTO createVillage);
        Task<Village> UpdateVillage(string villageId, UpdateVillageDTO updateVillage);
        Task<Village> DeleteVillage(string villageId);

        List<Village> GetVillagesDonatedByUser(string userAccountId);
    }
}
