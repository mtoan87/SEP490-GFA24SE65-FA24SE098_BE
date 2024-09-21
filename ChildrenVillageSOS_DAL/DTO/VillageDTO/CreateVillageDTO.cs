using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.DTO.VillageDTO
{
    public class CreateVillageDTO
    {
        public string Id { get; set; } = null!;

        public string VillageName { get; set; } = null!;

        public string Location { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string Status { get; set; } = null!;

        public string? UserAccountId { get; set; }

    }
}
