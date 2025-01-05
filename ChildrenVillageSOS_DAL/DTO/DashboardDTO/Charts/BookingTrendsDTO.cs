using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.DTO.DashboardDTO.Charts
{
    public class BookingTrendsDTO
    {
        public List<int> BookingCounts { get; set; }
        public List<string> Labels { get; set; }
        public string TimeFrame { get; set; } // "Week", "Month", "Year"

        public BookingTrendsDTO()
        {
            BookingCounts = new List<int>();
            Labels = new List<string>();
        }
    }
}
