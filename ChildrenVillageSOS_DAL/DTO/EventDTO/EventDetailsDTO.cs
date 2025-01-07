using ChildrenVillageSOS_DAL.DTO.DonationDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.DTO.EventDTO
{
    public class EventDetailsDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public decimal? Amount { get; set; }
        public decimal? CurrentAmount { get; set; }
        public decimal? AmountLimit { get; set; }
        public string? Status { get; set; }
        public string? VillageId { get; set; }
        public List<DonorDTO> Donors { get; set; } = new List<DonorDTO>();
    }
}
