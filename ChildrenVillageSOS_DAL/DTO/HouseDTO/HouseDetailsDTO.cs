using ChildrenVillageSOS_DAL.DTO.ChildDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.DTO.HouseDTO
{
    public class HouseDetailsDTO
    {
        public string Id { get; set; } = null!;

        public string HouseName { get; set; } = null!;

        public int? HouseNumber { get; set; }

        public string? Location { get; set; }

        public string? HouseOwner { get; set; }

        public int? CurrentMembers { get; set; }

        public DateTime FoundationDate { get; set; }

        public string MaintenanceStatus { get; set; } = null!;

        public List<ChildSummaryDTO> Children { get; set; }
    }
}
