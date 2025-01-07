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
    public class EmailConfig
    {
        public string From { get; set; }
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class AppConfiguration
    {
        public GoogleSetting GoogleSetting { get; set; }
        public EmailConfig EmailConfiguration { get; set; }
    }
}
