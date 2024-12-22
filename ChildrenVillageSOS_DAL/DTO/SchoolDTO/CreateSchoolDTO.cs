using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.DTO.SchoolDTO
{
    public class CreateSchoolDTO
    {
        public string SchoolName { get; set; } = null!;

        public string? Address { get; set; }

        public string? SchoolType { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Email { get; set; }

        public string? CreatedBy { get; set; }
    }
}
