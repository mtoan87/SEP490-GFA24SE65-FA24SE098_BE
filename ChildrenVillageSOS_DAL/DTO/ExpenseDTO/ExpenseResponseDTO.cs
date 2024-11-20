﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.DTO.ExpenseDTO
{
    public class ExpenseResponseDTO
    {
        public int Id { get; set; }

        public decimal? ExpenseAmount { get; set; }

        public string Description { get; set; }

        public DateTime? Expenseday { get; set; }

        public string Status { get; set; }

        public int? SystemWalletId { get; set; }

        public int? FacilitiesWalletId { get; set; }

        public int? FoodStuffWalletId { get; set; }

        public int? HealthWalletId { get; set; }

        public int? NecessitiesWalletId { get; set; }

        public string HouseId { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }
    }
}