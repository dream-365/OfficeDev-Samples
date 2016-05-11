using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_with_O365.Scenarios
{
    public class AcquireUserTokenDemo : Scenario
    {
        public override void Run()
        {
            var authority = "https://login.microsoftonline.com/MOD878411.onmicrosoft.com";

            var redirectUri = new Uri("http://localhost");

            var resourceUrl = "https://graph.microsoft.com";

            var clientId = "dcd68e75-54d4-451c-9dfb-dbc43833ec1a";

            AuthenticationContext authenticationContext = new AuthenticationContext(authority, false);

            AuthenticationResult authenticationResult = authenticationContext.AcquireToken(resourceUrl,
                clientId, redirectUri, PromptBehavior.Always);


            authenticationContext.AcquireToken("{resource_id}", "{client_id}", new UserCredential("user_name", "[password]"));

            var token = authenticationResult.AccessToken;
        }
    }
}
