using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.DTO.InventoryDTO
{
    public class InventorySummaryDTO
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public string Purpose { get; set; }
        public string MaintenanceStatus { get; set; }
        public DateTime? LastInspectionDate { get; set; }
    }
}
