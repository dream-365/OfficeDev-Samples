using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace Console_with_O365
{
    public class ApplicationTokenManagement
    {
        public string AcquireToken(string resource)
        {
            AuthenticationContext authenticationContext = new AuthenticationContext(Settings.AuthString, false);

            // Config for OAuth client credentials 
            ClientCredential clientCred = new ClientCredential(Settings.ClientId, Settings.ClientSecret);

            AuthenticationResult authenticationResult = authenticationContext.AcquireToken(resource, clientCred);

            string token = authenticationResult.AccessToken;

            return token;
        }

    }
}
