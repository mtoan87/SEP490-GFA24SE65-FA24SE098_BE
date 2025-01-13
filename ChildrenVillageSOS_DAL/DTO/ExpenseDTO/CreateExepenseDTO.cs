using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.DTO.ExpenseDTO
{
    public class CreateExepenseDTO
    {
        public decimal ExpenseAmount { get; set; }

        public string Description { get; set; }
        
        public int? SystemWalletId { get; set; }
      
        public int? FacilitiesWalletId { get; set; }

        public int? FoodStuffWalletId { get; set; }

        public int? HealthWalletId { get; set; }

        public int? NecessitiesWalletId { get; set; }

        public string HouseId { get; set; }

        public string? RequestedBy { get; set; }

        public string? ApprovedBy { get; set; }

        public string? ChildId { get; set; }

    }
}
