﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.DTO.IncomeDTO
{
    public class UpdateIncomeDTO
    {
        public int? DonationId { get; set; }

        public string UserAccountId { get; set; }

        public string HouseId { get; set; }

        public bool IsDelete { get; set; }

    }
}
