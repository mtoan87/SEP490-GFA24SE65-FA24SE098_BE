using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.DTO.PaymentDTO
{
    public class PaymentRequest
    {
        [Required]
        public string UserAccountId { get; set; }
        [Required]
        public decimal Amount { get; set; }
        
    }
}
