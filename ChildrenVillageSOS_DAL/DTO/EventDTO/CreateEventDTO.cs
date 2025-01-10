using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.DTO.EventDTO
{  
   public class CreateEventDTO
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string? Name { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? Description { get; set; }
        public int? FacilitiesWalletId { get; set; }
        public int? FoodStuffWalletId { get; set; }

        public int? SystemWalletId { get; set; }

        public int? HealthWalletId { get; set; }

        public int? NecessitiesWalletId { get; set; }
        [Required(ErrorMessage = "StartTime is required.")]
        public DateTime? StartTime { get; set; }
        [Required(ErrorMessage = "StartTime is required.")]
        public DateTime? EndTime { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Amount must be a positive value.")]
        public decimal? Amount { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "AmountLimit must be a positive value.")]
        public decimal? AmountLimit { get; set; }
        public string? VillageId { get; set; }

        public List<IFormFile>? Img { get; set; }
    }
}
