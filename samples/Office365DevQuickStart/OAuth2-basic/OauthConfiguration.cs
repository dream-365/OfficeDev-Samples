using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAuth2_basic
{
    public class OauthConfiguration
    {
        public string Authority { get; set; }

        public string Tenant { get; set; }

        public string ClientId { get; set; }

        public string RedirectURI { get; set; }
    }
}
