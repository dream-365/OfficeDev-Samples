using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.IO;

namespace Console_with_O365
{
    public class UserTokenManagement
    {
        private const string TOKEN_CACHE_FIIE = "token_cache.json";

        protected bool TryAcquireToken(out string token)
        {
            token = string.Empty;

            if(!File.Exists(TOKEN_CACHE_FIIE))
            {
                return false;
            }

            var text = File.ReadAllText(TOKEN_CACHE_FIIE);

            var authenticationResult = AuthenticationResult.Deserialize(text);

            if(authenticationResult.ExpiresOn < DateTime.Now)
            {
                return false;
            }

            token = authenticationResult.AccessToken;

            return true;
        }

        public string AcquireToken(string resrouceUrl)
        {
            string token;

            if(TryAcquireToken(out token))
            {
                return token;
            }

            var redirectUri = new Uri(Settings.RedirectUri);

            AuthenticationContext authenticationContext = new AuthenticationContext(Settings.AuthString, false);

            AuthenticationResult authenticationResult = authenticationContext.AcquireToken(resrouceUrl,
                Settings.ClientIdForUserAuth, redirectUri, PromptBehavior.Always);

            File.WriteAllText(TOKEN_CACHE_FIIE, authenticationResult.Serialize());

            token = authenticationResult.AccessToken;

            return token;
        }
    }
}
