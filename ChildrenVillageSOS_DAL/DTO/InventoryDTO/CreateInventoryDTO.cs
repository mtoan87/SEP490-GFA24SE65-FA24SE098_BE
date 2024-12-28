using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.DTO.InventoryDTO
{
    public class CreateInventoryDTO
    {
        public string ItemName { get; set; } = null!;

        public string? Description { get; set; }

        public int Quantity { get; set; }

        public string? Purpose { get; set; }

        public string BelongsTo { get; set; } = null!;

        public string BelongsToId { get; set; } = null!;

        public DateTime? PurchaseDate { get; set; }

        public DateTime? LastInspectionDate { get; set; }

        public string MaintenanceStatus { get; set; } = null!;

        public string? CreatedBy { get; set; }

        public List<IFormFile>? Img { get; set; }
    }
}
