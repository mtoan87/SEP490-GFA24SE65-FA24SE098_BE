using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.DTO.SendEmail
{
    public class Message
    {
        public string Subject { get; set; }
        public string Content { get; set; }
        public List<string> Recipients { get; set; } = new List<string>();
    }

}
