using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.DTO.VillageDTO
{
    public class CreateVillageDTO
    {
        public string VillageName { get; set; }

        public DateTime? EstablishedDate { get; set; }

        public int? TotalHouses { get; set; }

        public int? TotalChildren { get; set; }

        public string? ContactNumber { get; set; }

        public string Location { get; set; }

        public string Description { get; set; }

        public string Status { get; set; }

        public string UserAccountId { get; set; }

        public string? CreatedBy { get; set; }

        public string? RoleName { get; set; }

        public List<IFormFile>? Img { get; set; }

    }
}
