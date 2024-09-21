using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.DTO.TransactionDTO
{
    public class CreateTransactionDTO
    {
        

        public int? SystemWalletId { get; set; }

        public decimal Amount { get; set; }

        public DateTime DateTime { get; set; }

        public string Status { get; set; }

        public int? DonationId { get; set; }

        public int? IncomeId { get; set; }
    }
}
