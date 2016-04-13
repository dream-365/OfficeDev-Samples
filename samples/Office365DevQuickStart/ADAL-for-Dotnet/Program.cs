using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ADAL_for_Dotnet
{
    class Program
    {
        static void Main(string[] args)
        {
            var clientId = "{YOUR_CLIENT_ID}";

            var authority = "https://login.microsoftonline.com/{TENENT_ID}";

            var resource = "https://outlook.office365.com";

            AuthenticationContext authenticationContext = new AuthenticationContext(authority, false);

            var x509Certificate2 = new X509Certificate2(@"{FILE_PATH}\office_365_app.pfx", "PASS_WORD");

            ClientAssertionCertificate clientAssertionCertificate = new ClientAssertionCertificate(clientId, x509Certificate2);

            var authenticationResult = authenticationContext.AcquireTokenAsync(resource, clientAssertionCertificate).Result;

            var token = authenticationResult.AccessToken;
        }
    }
}
