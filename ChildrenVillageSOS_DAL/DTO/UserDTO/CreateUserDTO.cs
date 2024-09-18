using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.DTO.UserDTO
{
    public class CreateUserDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }

        public string UserEmail { get; set; }

        public string Password { get; set; }

        public long Phone { get; set; }

        public string Address { get; set; }

/*        public DateOnly Dob { get; set; }*/

        public string Gender { get; set; }

        public string Country { get; set; }

        public int? RoleId { get; set; }
    }
}
