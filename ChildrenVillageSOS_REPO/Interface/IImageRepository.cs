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
        public Task<Image?> GetByChildIdAsync(string childId);
        public Task<Image?> GetByHouseIdAsync(string houseId);
        public Task<Image?> GetByVillageIdAsync(string villageId);
        public Task<Image?> GetByEventIdAsync(int eventId);
        public Task<Image?> GetByUserAccountIdAsync(string userAccountId);
    }
}
