using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.DTO.DonationDTO
{
    public class CreateDonationDTO
    {
        public string? UserAccountId { get; set; }       

        public string Description { get; set; } = null!;

        public string DonationType { get; set; }

        public DateTime DateTime { get; set; }
        public int? EventId { get; set; }

        public string? ChildId { get; set; }
        public decimal Amount { get; set; }


    }
}
