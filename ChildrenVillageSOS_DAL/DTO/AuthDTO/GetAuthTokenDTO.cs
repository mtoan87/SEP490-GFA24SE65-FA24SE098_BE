using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.DTO.AuthDTO
{
    public class GetAuthTokenDTO
    {
        public string? AccessToken { get; set; }
        public int? RoleId { get; set; }
        public string? UserAccountId { get; set; }
    }
}
