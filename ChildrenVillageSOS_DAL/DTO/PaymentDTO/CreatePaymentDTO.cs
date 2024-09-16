using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.DTO.PaymentDTO
{
    public class CreatePaymentDTO
    {
        public string PaymentMethod { get; set; }

        public int DonationId { get; set; }

        public string Status { get; set; }

        public double Amount { get; set; }
    }
}
