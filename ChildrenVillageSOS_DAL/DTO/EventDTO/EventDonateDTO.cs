﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.DTO.EventDTO
{
    public class EventDonateDTO
    {
        public string? UserAccountId { get; set; }
        public decimal? Amount { get; set; }
       
        
    }
}
