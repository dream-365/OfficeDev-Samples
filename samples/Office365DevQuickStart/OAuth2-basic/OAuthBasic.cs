using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAuth2_basic
{
    class OAuthBasic
    {
        public void Run()
        {
            var oauth = new OauthConfiguration
            {
                Authority = "https://login.microsoftonline.com",
                Tenant = "common",
                ClientId = "24bb1725-54d9-4190-b58c-ba5347fb336e",
                RedirectURI = "http://localhost"
            };

            var flow = new AuthorizationCodeGrantFlow();

            flow.Go(oauth);
        }
    }
}
