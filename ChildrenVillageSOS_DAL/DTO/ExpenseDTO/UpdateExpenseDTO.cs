using System;
using System.ComponentModel.DataAnnotations;

namespace ChildrenVillageSOS_DAL.DTO.ExpenseDTO
{
    public class UpdateExpenseDTO
    {
        [Required(ErrorMessage = "ExpenseAmount is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "ExpenseAmount must be greater than 0.")]
        public decimal ExpenseAmount { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "HouseId is required.")]
        [StringLength(100, ErrorMessage = "HouseId cannot exceed 100 characters.")]
        public string HouseId { get; set; }
    }
}
