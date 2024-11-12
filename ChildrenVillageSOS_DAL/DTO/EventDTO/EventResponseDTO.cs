using ChildrenVillageSOS_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.DTO.EventDTO
{
    public class EventResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? FacilitiesWalletId { get; set; }

        public int? FoodStuffWalletId { get; set; }

        public int? SystemWalletId { get; set; }

        public int? HealthWalletId { get; set; }

        public int? NecessitiesWalletId { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal Amount { get; set; }
        public decimal CurrentAmount { get; set; }
        public decimal AmountLimit { get; set; }
        public string Status { get; set; }

        // Chỉ trả về mảng các URL của hình ảnh
        public string[] ImageUrls { get; set; } = new string[0];
    }
}

