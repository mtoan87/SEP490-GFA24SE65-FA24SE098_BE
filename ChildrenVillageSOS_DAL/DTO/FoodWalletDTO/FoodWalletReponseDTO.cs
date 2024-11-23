using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.DTO.FoodWalletDTO
{
    public class FoodWalletReponseDTO
    {
        public int Id { get; set; }

        public decimal Budget { get; set; }

        public string UserAccountId { get; set; }
    }
}
