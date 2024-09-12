using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.DTO.VillageDTO
{
    public class UpdateVillageDTO
    {
        public string VillageName { get; set; }

        public string Location { get; set; }

        public string Description { get; set; }

        public string UserAccountId { get; set; }
    }
}
