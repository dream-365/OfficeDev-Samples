using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_with_O365
{
    public class TokenManagement
    {
        public string GetTokenForApplication()
        {
            AuthenticationContext authenticationContext = new AuthenticationContext(Settings.AuthString, false);

            // Config for OAuth client credentials 
            ClientCredential clientCred = new ClientCredential(Settings.ClientId, Settings.ClientSecret);

            AuthenticationResult authenticationResult = authenticationContext.AcquireToken(Settings.ResourceUrl, clientCred);

            string token = authenticationResult.AccessToken;

            return token;
        }

        public string GetTokenForUser()
        {
            var redirectUri = new Uri(Settings.RedirectUri);

            AuthenticationContext authenticationContext = new AuthenticationContext(Settings.AuthString, false);

            AuthenticationResult userAuthnResult = authenticationContext.AcquireToken(Settings.ResourceUrl,
                Settings.ClientIdForUserAuth, redirectUri, PromptBehavior.Always);

            var token = userAuthnResult.AccessToken;

            return token;
        }
    }
}
