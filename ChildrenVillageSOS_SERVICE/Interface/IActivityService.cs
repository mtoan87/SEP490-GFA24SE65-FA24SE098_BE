using ChildrenVillageSOS_DAL.DTO.ActivityDTO;
using ChildrenVillageSOS_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_SERVICE.Interface
{
    public interface IActivityService
    {
        Task<IEnumerable<Activity>> GetAllActivities();
        Task<Activity> GetActivityById(int id);
        Task<Activity> CreateActivity(CreateActivityDTO createActivity);
        Task<Activity> UpdateActivity(int id, UpdateActivityDTO updateActivity);
        Task<Activity> DeleteActivity(int id);
        Task<Activity> RestoreActivity(int id);
        Task<IEnumerable<ActivityResponseDTO>> GetAllActivityWithImg();
        Task<ActivityResponseDTO> GetActivityByIdWithImg(int activityId);
        Task<ActivityResponseDTO[]> GetAllActivityIsDeleteAsync();
        Task<List<Activity>> SearchActivities(SearchActivityDTO searchActivityDTO);
    }
}
