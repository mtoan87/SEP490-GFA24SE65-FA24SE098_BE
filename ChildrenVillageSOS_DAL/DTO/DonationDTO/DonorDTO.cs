using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.DTO.DonationDTO
{
    public class DonorDTO
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? UserEmail { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? DonationType { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateTime { get; set; }
        public string? Description { get; set; }
    }
}
