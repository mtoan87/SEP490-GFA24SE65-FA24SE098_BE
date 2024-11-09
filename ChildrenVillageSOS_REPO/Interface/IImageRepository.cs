using ChildrenVillageSOS_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_REPO.Interface
{
    public interface IImageRepository : IRepositoryGeneric<Image>
    {
        Task<List<Image>> GetByChildIdAsync(string childId);
        Task<List<Image>> GetByHouseIdAsync(string houseId);
        Task<List<Image>> GetByVillageIdAsync(string villageId);
        Task<List<Image>> GetByEventIdAsync(int eventId);
        Task<List<Image>> GetByUserAccountIdAsync(string userAccountId);
    }
}
