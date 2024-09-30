using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.DTO.UserDTO
{
    public class UpdateUserDTO
    {
        public string Id { get; set; } = null!;

        public string UserName { get; set; } = null!;

        public string UserEmail { get; set; } = null!;

        public string Password { get; set; } = null!;

        public long Phone { get; set; }

        public string Address { get; set; } = null!;

        public DateOnly Dob { get; set; }

        public string Gender { get; set; } = null!;

        public string Country { get; set; } = null!;

        public string Status { get; set; } = null!;

        public int? RoleId { get; set; }
        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }
    }
}
