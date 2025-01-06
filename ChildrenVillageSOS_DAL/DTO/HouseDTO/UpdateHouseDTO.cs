using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.DTO.House
{
    public class UpdateHouseDTO
    {
        public string HouseName { get; set; }

        public int? HouseNumber { get; set; }

        public string Location { get; set; }

        public string Description { get; set; }

        public int? HouseMember { get; set; }

        public int? CurrentMembers { get; set; }

        public string HouseOwner { get; set; }

        public string Status { get; set; }

        public string UserAccountId { get; set; }

        public string VillageId { get; set; }

        public DateTime FoundationDate { get; set; }

        public DateTime? LastInspectionDate { get; set; }

        public string MaintenanceStatus { get; set; } = null!;

        public string? ModifiedBy { get; set; }

        

        public bool IsDeleted { get; set; }

        public List<IFormFile>? Img { get; set; }

        public List<string>? ImgToDelete { get; set; }
    }
}
