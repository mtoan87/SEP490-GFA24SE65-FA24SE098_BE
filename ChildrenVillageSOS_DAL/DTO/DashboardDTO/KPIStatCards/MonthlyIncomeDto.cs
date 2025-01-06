using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.DTO.DashboardDTO.KPIStatCards
{
    public class MonthlyIncomeDto
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal TotalIncome { get; set; }
    }
}
