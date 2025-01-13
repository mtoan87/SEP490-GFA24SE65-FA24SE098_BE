using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.DTO.ExpenseDTO
{
    public class RequestSpecialExpenseDTO
    {
        public string Description { get; set; }
        public string HouseId { get; set; }
        public string RequestedBy { get; set; }
        public List<string> SelectedChildrenIds { get; set; } // Sử dụng string thay vì int
    }
}
