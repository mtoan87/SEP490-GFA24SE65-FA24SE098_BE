using ChildrenVillageSOS_DAL.DTO.DashboardDTO.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.DTO.DashboardDTO.Charts
{
    public class DonationTrendsDTO
    {
        public int Year { get; set; }
        public List<MonthlyDonationDetail> MonthlyDetails { get; set; }
    }
}
