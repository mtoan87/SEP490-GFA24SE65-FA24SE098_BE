using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.DTO.EventDTO
{
    public class UpdateEventDTO
    {
        [Required(ErrorMessage = "EventCode is required.")]
        [StringLength(50, ErrorMessage = "EventCode cannot exceed 50 characters.")]
        public string? EventCode { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(200, ErrorMessage = "Name cannot exceed 200 characters.")]
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

        [Required(ErrorMessage = "EndTime is required.")]
        public DateTime? EndTime { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        [StringLength(50, ErrorMessage = "Status cannot exceed 50 characters.")]
        public string? Status { get; set; }

        public bool IsDeleted { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "CurrentAmount must be greater than or equal to 0.")]
        public decimal? CurrentAmount { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0.")]
        public decimal? Amount { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "AmountLimit must be greater than 0.")]
        public decimal? AmountLimit { get; set; }

        public List<IFormFile>? Img { get; set; }

        public List<string>? ImgToDelete { get; set; }
    }
}

