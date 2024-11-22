using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.DTO.DashboardDTO.Charts
{
    public class PaymentMethodStatsDTO
    {
        public string PaymentMethod { get; set; }
        public int NumberOfUses { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
