using ChildrenVillageSOS_DAL.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.DTO.PaymentDTO
{
    public class CreatePaymentDTO
    {
        [Required]
        [EnumDataType(typeof(PaymentMethod))]
        public string PaymentMethod { get; set; }

        public int DonationId { get; set; }
        [Required]
        [EnumDataType(typeof(DonateStatus))]
        public string Status { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public decimal Amount { get; set; }
    }
}
