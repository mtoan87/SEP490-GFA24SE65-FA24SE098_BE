using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.DTO.TransferHistoryDTO
{
    public class UpdateTransferHistoryDTO
    {
        public string ChildId { get; set; } = null!;

        public string FromHouseId { get; set; } = null!;

        public string ToHouseId { get; set; } = null!;

        public DateTime TransferDate { get; set; }

        public string Status { get; set; } = null!;

        public string? Notes { get; set; }

        public string? HandledBy { get; set; }

        public string? ModifiedBy { get; set; }

    }
}
