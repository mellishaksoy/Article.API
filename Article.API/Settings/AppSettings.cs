using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Article.API.Settings
{
    public class AppSettings
    {
        public string ApiName { get; set; }
        public int ApiVersion { get; set; }
        public IdentityServerSetting IdentityServerSetting { get; set; }
    }

    public class IdentityServerSetting
    {
        public string IdentityServerUrl { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Scope { get; set; }
    }
}
