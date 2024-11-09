using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.DTO.VillageDTO
{
    public class UpdateVillageDTO
    {
        public string VillageName { get; set; } = null!;

        public string Location { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string Status { get; set; }

        public string UserAccountId { get; set; }
        public List<IFormFile> Img { get; set; }

    }
}
