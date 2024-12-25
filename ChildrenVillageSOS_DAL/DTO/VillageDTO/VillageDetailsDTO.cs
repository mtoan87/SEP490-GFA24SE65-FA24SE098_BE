using ChildrenVillageSOS_DAL.DTO.HouseDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.DTO.VillageDTO
{
    public class VillageDetailsDTO
    {
        public string Id { get; set; }
        public string VillageName { get; set; }
        public string Location { get; set; }
        public int? TotalHouses { get; set; }
        public int? TotalChildren { get; set; }
        public DateTime? EstablishedDate { get; set; }
        public string? ContactNumber { get; set; }
        public string? Description { get; set; }
        public int TotalHouseOwners { get; set; }
        public int TotalMatureChildren { get; set; }
        public List<HouseSummaryDTO> Houses { get; set; }
    }
}
