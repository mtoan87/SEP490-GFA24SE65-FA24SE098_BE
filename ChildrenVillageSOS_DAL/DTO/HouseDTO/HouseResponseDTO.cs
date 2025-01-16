using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.DTO.HouseDTO
{
    public class HouseResponseDTO
    {
        public string Id { get; set; }

        public string HouseName { get; set; }

        public int? HouseNumber { get; set; }

        public string Location { get; set; }

        public string Description { get; set; }

        public int? HouseMember { get; set; }

        public int? CurrentMembers { get; set; }

        public string HouseOwner { get; set; }

        public string Status { get; set; }

        public string UserAccountId { get; set; }

        public string? UserName { get; set; }

        public string VillageId { get; set; }

        public string? VillageName { get; set; }

        public DateTime? FoundationDate { get; set; }

        public DateTime? LastInspectionDate { get; set; }

        public string MaintenanceStatus { get; set; } = null!;

        public string? CreatedBy { get; set; }

        public string? ModifiedBy { get; set; }

      

        public bool? IsDeleted { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string[] ImageUrls { get; set; } = new string[0];
    }
}
