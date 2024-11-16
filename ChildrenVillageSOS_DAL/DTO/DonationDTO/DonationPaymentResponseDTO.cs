using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.DTO.DonationDTO
{
    public class DonationPaymentResponseDTO
    {
       
            public string PaymentUrl { get; set; }
            public int EventId { get; set; }
            public int? FacilitiesWalletId { get; set; }
            public int? FoodStuffWalletId { get; set; }
            public int? SystemWalletId { get; set; }
            public int? HealthWalletId { get; set; }
            public int? NecessitiesWalletId { get; set; }
        
    }
}
