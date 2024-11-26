using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.DTO.DashboardDTO.Charts
{
    public class AcademicPerformanceDistributionDTO
    {
        public string Diploma { get; set; }
        public int ExcellentCount { get; set; }
        public int VeryGoodCount { get; set; }
        public int GoodCount { get; set; }
        public int AverageCount { get; set; }
        public int BelowAverageCount { get; set; }
        public double ExcellentPercentage { get; set; }
        public double VeryGoodPercentage { get; set; }
        public double GoodPercentage { get; set; }
        public double AveragePercentage { get; set; }
        public double BelowAveragePercentage { get; set; }
    }
}
