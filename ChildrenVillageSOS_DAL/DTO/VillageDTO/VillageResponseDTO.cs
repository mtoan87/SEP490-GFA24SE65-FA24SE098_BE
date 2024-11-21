using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.DTO.VillageDTO
{
    public class VillageResponseDTO
    {
        public string Id { get; set; }

        public string VillageName { get; set; }

        public string Location { get; set; }

        public string Description { get; set; }

        public string Status { get; set; }

        public string UserAccountId { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string[] ImageUrls { get; set; } = new string[0];
    }
}
