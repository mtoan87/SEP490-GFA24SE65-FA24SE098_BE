using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.DTO.DashboardDTO.TopStatCards
{
    public class TotalUsersStatDTO
    {
        public int TotalUsers { get; set; }
        public int NewUsersThisMonth { get; set; }
        public int NewUsersThisWeek { get; set; }
    }
}
