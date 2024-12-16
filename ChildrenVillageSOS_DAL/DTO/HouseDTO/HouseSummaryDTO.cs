using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.DTO.HouseDTO
{
    public class HouseSummaryDTO
    {
        public string Id { get; set; } = null!;
        public string HouseName { get; set; }
        public string HouseOwner { get; set; }
        public int TotalChildren { get; set; }
    }
}
