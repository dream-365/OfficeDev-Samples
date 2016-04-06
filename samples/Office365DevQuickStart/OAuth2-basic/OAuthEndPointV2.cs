using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAuth2_basic
{
    class OAuthEndPointV2
    {
        public void Run()
        {
            var oauth = new OauthConfiguration
            {
                Authority = "https://login.microsoftonline.com",
                Tenant = "common",
                ClientId = "f6eddbd9-e3bb-4610-9ebb-0fc31291ab1e",
                RedirectURI = "urn:ietf:wg:oauth:2.0:oob"
            };

            var helper = new OauthWebAuthHelper(oauth, OauthWebAuthHelper.Version.V2);

            var token = helper.AcquireTokenWithScope("Files.Read");
        }
    }
}
