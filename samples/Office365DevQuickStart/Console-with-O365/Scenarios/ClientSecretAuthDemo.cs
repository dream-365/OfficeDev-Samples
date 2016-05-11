using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_with_O365.Scenarios
{
    public class ClientSecretAuthDemo : Scenario
    {
        public override void Run()
        {
            var clientId = "c9d7131d-955e-4286-953d-db0867f5b827";

            var authority = "https://login.microsoftonline.com/MOD878411.onmicrosoft.com";

            AuthenticationContext authenticationContext = new AuthenticationContext(authority, false);

            var clientCredential = new ClientCredential(clientId, "{client_sceret}");

            var authenticationResult = authenticationContext.AcquireTokenAsync(Settings.ResourceUrlOfGraph, clientCredential).Result;

            var token = authenticationResult.AccessToken;
        }
    }
}
