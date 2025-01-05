using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.DTO.DashboardDTO.Helper
{
    public class MonthlyDonationDetail
    {
        public int Month { get; set; }
        public decimal EventAmount { get; set; }
        public decimal ChildAmount { get; set; }
        public decimal WalletAmount { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
