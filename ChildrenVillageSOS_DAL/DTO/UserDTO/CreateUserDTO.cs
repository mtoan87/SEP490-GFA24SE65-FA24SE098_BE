using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChildrenVillageSOS_DAL.DTO.UserDTO
{
    public class CreateUserDTO
    {
        [Required(ErrorMessage = "UserName is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "UserName must be between 3 and 100 characters.")]
        public string UserName { get; set; } = null!;

        [Required(ErrorMessage = "UserEmail is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string UserEmail { get; set; } = null!;

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; set; } = null!;

        [Phone(ErrorMessage = "Invalid phone number.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; } = null!;

        [Required(ErrorMessage = "Date of Birth (Dob) is required.")]
        public DateTime Dob { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        public string Gender { get; set; } = null!;
        public int RoleId { get; set; }

        [Required(ErrorMessage = "Country is required.")]
        public string Country { get; set; } = null!;

        [Required(ErrorMessage = "Profile image is required.")]
        public List<IFormFile> Img { get; set; }
    }
}
