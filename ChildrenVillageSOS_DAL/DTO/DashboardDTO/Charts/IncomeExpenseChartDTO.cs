using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.DTO.DashboardDTO.Charts
{
    public class IncomeExpenseChartDTO
    {
        public List<decimal> IncomeData { get; set; }
        public List<decimal> ExpenseData { get; set; }
        public List<string> Labels { get; set; }

        public IncomeExpenseChartDTO()
        {
            IncomeData = new List<decimal>();
            ExpenseData = new List<decimal>();
            Labels = new List<string>();
        }
    }
}
