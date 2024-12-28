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
        Task<List<Image>> GetByInventoryIdAsync(int inventoryId);
        Task<List<Image>> GetByActivityIdAsync(int activityId);
        Task<List<Image>> GetByHealthReportIdAsync(int healthReportId);
        Task<List<Image>> GetByAcademicReportIdAsync(int academicReportId);
        Task<List<Image>> GetBySchoolIdAsync(string schoolId);
    }
}
