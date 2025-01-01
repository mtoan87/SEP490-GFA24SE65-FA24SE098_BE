using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.DTO.EventDTO
{
    public class EventDonateDTO
    {
        public string? UserName { get; set; }
        public string? UserAccountId { get; set; }
        public decimal? Amount { get; set; }
        public string? UserEmail { get; set; }        
        public long? Phone { get; set; }
        public string? Description { get; set; }
        public string? Address { get; set; }

    }
}
