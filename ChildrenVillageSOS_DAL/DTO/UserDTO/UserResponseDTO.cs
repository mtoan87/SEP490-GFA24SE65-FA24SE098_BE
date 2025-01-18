using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.DTO.UserDTO
{
    public class UserResponseDTO
    {
        public string Id { get; set; } = null!;

        public string? UserName { get; set; }

        public string? UserEmail { get; set; }

        public string? Password { get; set; }

        public string? Phone { get; set; }

        public string? Address { get; set; }

        public DateTime? Dob { get; set; }

        public string? Gender { get; set; }

        public string? Country { get; set; }

        public string? Status { get; set; }

        public int? RoleId { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }
        public string[] ImageUrls { get; set; } = new string[0];
    }
}
