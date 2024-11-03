using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.DTO.PaymentDTO
{
    public class PaymentRequest
    {
        public string UserAccountId { get; set; } // Thêm UserAccountId
        public decimal Amount { get; set; }
        
    }
}
