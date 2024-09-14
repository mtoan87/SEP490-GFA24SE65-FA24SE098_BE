using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.DTO.DonationDetail
{
    public class UpdateDonationDetailDTO
    {
        public int? DonationId { get; set; }

        public double Donation { get; set; }

        public DateTime Datetime { get; set; }

        public string Description { get; set; }

        public string VillageId { get; set; }

        public string HouseId { get; set; }

        public string Status { get; set; }

        public bool? IsDelete { get; set; }
    }
}
