using ChildrenVillageSOS_DAL.DTO.VillageDTO;
using ChildrenVillageSOS_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_REPO.Interface
{
    public interface IVillageRepository : IRepositoryGeneric<Village>
    {
        List<Village> GetVillagesDonatedByUser(string userAccountId);
        VillageResponseDTO GetVillageByIdWithImg(string villageId);
        //Dashboard:
        Task<IEnumerable<Village>> GetVillagesWithHouses();
    }
}
