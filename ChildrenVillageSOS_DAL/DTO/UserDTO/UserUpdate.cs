using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.DTO.UserDTO
{
    public class UserUpdate
    {
        [StringLength(100, MinimumLength = 3, ErrorMessage = "UserName must be between 3 and 100 characters.")]
        public string? UserName { get; set; } = null!;

        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string? UserEmail { get; set; } = null!;

        //[StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
        //public string? Password { get; set; } = null!;

        [Phone(ErrorMessage = "Invalid phone number.")]
        public string? Phone { get; set; }

        [StringLength(200, ErrorMessage = "Address is too long.")]
        public string? Address { get; set; } = null!;

        public DateTime? Dob { get; set; }

        [StringLength(10, ErrorMessage = "Gender is too long.")]
        public string? Gender { get; set; } = null!;

        [StringLength(100, ErrorMessage = "Country is too long.")]
        public string? Country { get; set; } = null!;

        [StringLength(50, ErrorMessage = "Status is too long.")]
        public string? Status { get; set; } = null!;

        [Range(1, int.MaxValue, ErrorMessage = "RoleId must be a valid role ID.")]
        public int RoleId { get; set; }

        public List<IFormFile>? Img { get; set; }

        public List<string>? ImgToDelete { get; set; }
    }
}
