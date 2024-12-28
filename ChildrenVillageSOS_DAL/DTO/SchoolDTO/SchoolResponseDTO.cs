using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.DTO.SchoolDTO
{
    public class SchoolResponseDTO
    {
        public string Id { get; set; } = null!;

        public string SchoolName { get; set; } = null!;

        public string? Address { get; set; }

        public string? SchoolType { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Email { get; set; }

        public bool IsDeleted { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string[] ImageUrls { get; set; } = new string[0];
    }
}
