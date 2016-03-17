using Newtonsoft.Json.Linq;
using System;

namespace OAuth2_basic
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var oauth = new OauthConfiguration
            {
                Authority = "https://login.microsoftonline.com",
                Tenant = "common",
                ClientId = "{client_id}",
                RedirectURI = "{redirect_uri}"
            };

            var flow = new AuthorizationCodeGrantFlow();

            flow.Go(oauth);
        }
    }
}
