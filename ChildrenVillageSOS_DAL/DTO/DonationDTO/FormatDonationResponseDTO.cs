using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.DTO.DonationDTO
{
    public class FormatDonationResponseDTO
    {
        public int Id { get; set; }
        public int? FacilitiesWalletId { get; set; }

        public int? SystemWalletId { get; set; }

        public int? FoodStuffWalletId { get; set; }

        public int? HealthWalletId { get; set; }

        public int? NecessitiesWalletId { get; set; }
        public string UserAccountId { get; set; }

        public string DonationType { get; set; }

        public DateTime DateTime { get; set; }
        public int? EventId { get; set; }

        public string? ChildId { get; set; }
        public decimal Amount { get; set; }

        public string Description { get; set; }

        public string Status { get; set; }

    }
}
