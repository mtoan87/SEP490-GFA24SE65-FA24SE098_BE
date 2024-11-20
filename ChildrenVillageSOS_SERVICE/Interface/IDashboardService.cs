using ChildrenVillageSOS_DAL.DTO.DashboardDTO.TopStatCards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_SERVICE.Interface
{
    public interface IDashboardService
    {
        //TopStatCards
        Task<ActiveChildrenStatDTO> GetActiveChildrenStatAsync();
        Task<TotalUsersStatDTO> GetTotalUsersStatAsync();
        Task<TotalEventsStatDTO> GetTotalEventsStatAsync();
    }
}
