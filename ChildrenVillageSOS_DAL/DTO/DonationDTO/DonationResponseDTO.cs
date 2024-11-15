using ChildrenVillageSOS_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.DTO.DonationDTO
{
    public class DonationResponseDTO
    {
        public int Id { get; set; }

        public string UserAccountId { get; set; }

        public string DonationType { get; set; }

        public DateTime DateTime { get; set; }

        public decimal Amount { get; set; }

        public string Description { get; set; }

        public string Status { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }
        public virtual UserAccount UserAccount { get; set; }
    }
}
