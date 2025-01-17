﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.DTO.IncomeDTO
{
    public class IncomeResponseDTO
    {
        public int Id { get; set; }
        public int? DonationId { get; set; }
        public decimal? Amount { get; set; }
        public DateTime ReceiveDay { get; set; }
        public string? Status { get; set; }
        public string? UserAccountId { get; set; }
        public int? FacilitiesWalletId { get; set; }
        public int? FoodStuffWalletId { get; set; }
        public int? HealthWalletId { get; set; }
        public int? NecessitiesWalletId { get; set; }
        public int? SystemWalletId { get; set; }
        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set;}
    }
}
