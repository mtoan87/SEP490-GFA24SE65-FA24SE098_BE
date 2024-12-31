using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.DTO.ChildProgressDTO
{
    public class ChildProgressSummaryDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime? Date { get; set; }
        public string Category { get; set; }
        public int? EventId { get; set; }
        public int? ActivityId { get; set; }
        public string EventName { get; set; }
        public string ActivityName { get; set; }
    }
}
