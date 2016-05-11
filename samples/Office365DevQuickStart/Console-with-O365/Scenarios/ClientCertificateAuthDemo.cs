using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Console_with_O365.Scenarios
{
    public class ClientCertificateAuthDemo : Scenario
    {
        public override void Run()
        {
            var clientId = "c9d7131d-955e-4286-953d-db0867f5b827";

            var authority = "https://login.microsoftonline.com/MOD878411.onmicrosoft.com";

            AuthenticationContext authenticationContext = new AuthenticationContext(authority, false);

            var x509Certificate2 = new X509Certificate2(@"C:\Workbench\certificates\office_365_app.pfx", "123456");

            ClientAssertionCertificate clientAssertionCertificate = new ClientAssertionCertificate(clientId, x509Certificate2);

            var authenticationResult = authenticationContext.AcquireTokenAsync(Settings.ResourceUrlOfGraph, clientAssertionCertificate).Result;

            var token = authenticationResult.AccessToken;
        }
    }
}
