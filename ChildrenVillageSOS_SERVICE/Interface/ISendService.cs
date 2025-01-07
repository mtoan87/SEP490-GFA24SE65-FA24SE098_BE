using ChildrenVillageSOS_DAL.DTO.SendEmail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_SERVICE.Interface
{
    public interface ISendService
    {
        Task SendEmail(Message message);
    }
}
