using ChildrenVillageSOS_DAL.DTO.ChildDTO;
using ChildrenVillageSOS_DAL.DTO.InventoryDTO;
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

        public string? Description { get; set; }

        public string? HouseOwner { get; set; }

        public int? HouseMember { get; set; }

        public int? CurrentMembers { get; set; }

        public DateTime FoundationDate { get; set; }

        public string MaintenanceStatus { get; set; } = null!;

        public List<ChildSummaryDTO> Children { get; set; } = new();

        public List<InventorySummaryDTO> Inventory { get; set; } = new();

        public Dictionary<string, int> AgeGroups { get; set; }

        public double AverageAge { get; set; }

        public int MaleCount { get; set; }

        public int FemaleCount { get; set; }

        public int AchievementCount { get; set; }
    }
}
