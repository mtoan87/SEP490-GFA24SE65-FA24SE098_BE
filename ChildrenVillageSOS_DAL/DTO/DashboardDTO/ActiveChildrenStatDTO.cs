using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.DTO.DashboardDTO
{
    public class ActiveChildrenStatDTO
    {
        public int TotalActiveChildren { get; set; }
        public int ChangeThisMonth { get; set; } // Có thể tính được dương hoặc âm
    }
}
