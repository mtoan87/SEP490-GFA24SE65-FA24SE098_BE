﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.DTO.TransferRequestDTO
{
    public class CreateTransferRequestDTO
    {
        public string ChildId { get; set; } = null!;

        public string FromHouseId { get; set; } = null!;

        public string? ToHouseId { get; set; }

        public DateTime? RequestDate { get; set; }

        public string Status { get; set; } = null!;

        public string? RequestReason { get; set; }

        public string? CreatedBy { get; set; }

    }
}
