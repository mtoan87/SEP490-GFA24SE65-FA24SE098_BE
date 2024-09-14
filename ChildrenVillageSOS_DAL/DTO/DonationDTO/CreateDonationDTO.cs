using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.DTO.DonationDTO
{
    public class CreateDonationDTO
    {
        public string UserAccountId { get; set; }

        public bool? IsDelete { get; set; }
    }
}
