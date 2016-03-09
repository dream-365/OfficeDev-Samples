using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;

namespace Console_with_O365
{
    public class UserTokenManagement
    {
        public string AcquireToken(string resrouceUrl)
        {
            var redirectUri = new Uri(Settings.RedirectUri);

            AuthenticationContext authenticationContext = new AuthenticationContext(Settings.AuthString, false);

            AuthenticationResult userAuthnResult = authenticationContext.AcquireToken(resrouceUrl,
                Settings.ClientIdForUserAuth, redirectUri, PromptBehavior.Always);

            var token = userAuthnResult.AccessToken;

            return token;
        }
    }
}
