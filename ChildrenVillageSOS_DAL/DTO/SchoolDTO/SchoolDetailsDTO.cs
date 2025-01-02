using ChildrenVillageSOS_DAL.DTO.ChildDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.DTO.SchoolDTO
{
    public class SchoolDetailsDTO
    {
        public string Id { get; set; }
        public string SchoolName { get; set; }
        public string? Address { get; set; }
        public string? SchoolType { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public int CurrentStudents { get; set; }
        public List<ChildSummaryDTO> Children { get; set; } = new List<ChildSummaryDTO>();
    }
}
