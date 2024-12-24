using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.DTO.TransferRequestDTO
{
    public class TransferRequestResponseDTO
    {
        public int Id { get; set; }
        public string ChildName { get; set; }
        public string FromHouseName { get; set; }
        public string? ToHouseName { get; set; }
        public DateTime? RequestDate { get; set; }
        public string Status { get; set; }
        public string? DirectorNote { get; set; }
        public string? RequestReason { get; set; }
    }
}
