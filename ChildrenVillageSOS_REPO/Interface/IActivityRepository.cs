using ChildrenVillageSOS_DAL.DTO.ActivityDTO;
using ChildrenVillageSOS_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_REPO.Interface
{
    public interface IActivityRepository : IRepositoryGeneric<Activity>
    {
        Task<ActivityResponseDTO[]> GetAllActivityIsDeleteAsync();
        ActivityResponseDTO GetActivityByIdWithImg(int activityId);

    }
}
