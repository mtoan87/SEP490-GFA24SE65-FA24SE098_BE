using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.Helpers
{
    public class GoogleSetting
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
    public class AppConfiguration
    {
        public GoogleSetting GoogleSetting { get; set; }
    }
}
