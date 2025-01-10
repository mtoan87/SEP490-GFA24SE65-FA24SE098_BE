using System;
using System.ComponentModel.DataAnnotations;

namespace ChildrenVillageSOS_DAL.DTO.UserDTO
{
    public class ChangePassUserDTO
    {
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "NewPassword is required.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "NewPassword must be at least 6 characters long.")]
        //[RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$", ErrorMessage = "NewPassword must contain at least one letter and one number.")]
        public string NewPassword { get; set; } = null!;

        [Required(ErrorMessage = "ConfirmPassword is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "ConfirmPassword must be at least 6 characters long.")]
        [Compare("NewPassword", ErrorMessage = "NewPassword and ConfirmPassword must match.")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
