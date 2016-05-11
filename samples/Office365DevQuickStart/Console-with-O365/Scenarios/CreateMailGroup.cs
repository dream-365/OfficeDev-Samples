using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Console_with_O365.Scenarios
{
    public class CreateMailGroup : Scenario
    {
        public override void Run()
        {
            string authority = "https://login.windows.net/MOD878411.onmicrosoft.com";

            string clientId = "dcd68e75-54d4-451c-9dfb-dbc43833ec1a";

            Uri redirectUri = new Uri("http://localhost");

            string resourceUrl = "https://graph.microsoft.com";

            HttpClient client = new HttpClient();

            AuthenticationContext authenticationContext = new AuthenticationContext(authority, false);

            AuthenticationResult authenticationResult = authenticationContext.AcquireToken(resourceUrl,
                clientId, redirectUri, PromptBehavior.Always);

            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + authenticationResult.AccessToken);

            string content = @"{
              'displayName': 'mailgrouptest',
              'groupTypes': ['Unified'],
              'mailEnabled': true,
              'mailNickname': 'mailalias1@MOD878411.onmicrosoft.com',
              'securityEnabled': false
            }";

            var httpContent = new StringContent(content, Encoding.GetEncoding("utf-8"), "application/json");

            var response = client.PostAsync("https://graph.microsoft.com/v1.0/groups", httpContent).Result;

            Console.WriteLine(response.Content.ReadAsStringAsync().Result);
        }
    }
}
