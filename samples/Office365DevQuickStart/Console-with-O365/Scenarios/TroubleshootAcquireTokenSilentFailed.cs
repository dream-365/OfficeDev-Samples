using System;
using System.Collections.Generic;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tokens = System.IdentityModel.Tokens;
using Microsoft.Office365.Discovery;

namespace Console_with_O365.Scenarios
{
    public class TroubleshootAcquireTokenSilentFailed : Scenario
    {
        [STAThread]
        public override void Run()
        {
            RunInSTAThread(() =>
            {
                var webconfig = new OauthWebConfiguration
                {
                    Authority = "https://login.microsoftonline.com",
                    TenantId = "e07d220e-f4c2-441f-ad9a-9d6fa59b4e52",
                    ClientSecret = "xuelnXKmXR8MjJ20CRie3oPvCxjFw83JRi4aRXzxvdE=",
                    ClientId = "6b8eb9cf-3784-41e6-ac55-7b814dcf2c11",
                    RedirectURI = "https://localhost:44300"
                };

                // simulate the web server scenerio, user login and pass the code and id_token to server
                var simulaor = new OauthWebServerSimulator(webconfig);

                var response = simulaor.Login();

                Console.WriteLine(response); 

                var idToken = response.Value<string>("id_token");

                // use id token to authenticate the user (skip the validation here)
                Tokens.JwtSecurityToken jwt = new Tokens.JwtSecurityToken(idToken);

                object uniquename;
                object oid;

                jwt.Payload.TryGetValue("unique_name", out uniquename);
                jwt.Payload.TryGetValue("oid", out oid);

                var signInUserId = uniquename as string;
                var userObjectId = oid as string;

                var code = response.Value<string>("code");

                // use code to acquire the access token and cache in memory
                var resource = "https://graph.windows.net";

                var discoveryServiceEndpointUri = "https://api.office.com/discovery/v1.0/me/";

                var discoveryServiceResourceId = "https://api.office.com/discovery/";

                AuthenticationContext authContext = new AuthenticationContext(webconfig.Authority, new InMemoryTokenCache(signInUserId));

                ClientCredential credential = new ClientCredential(webconfig.ClientId, webconfig.ClientSecret);

                authContext.AcquireTokenByAuthorizationCode(code, new Uri(webconfig.RedirectURI), credential, resource);

                DiscoveryClient discoveryClient = new DiscoveryClient(new Uri(discoveryServiceEndpointUri),
                    async () =>
                    {
                        var authResult = await authContext.AcquireTokenSilentAsync(discoveryServiceResourceId,
                                                                                   new ClientCredential(webconfig.ClientId,
                                                                                                        webconfig.ClientSecret),
                                                                                   new UserIdentifier(userObjectId,
                                                                                                      UserIdentifierType.UniqueId));

                        return authResult.AccessToken;
                    });

                var dcr = discoveryClient.DiscoverCapabilityAsync("Calendar").Result;
            });
        }

        private void RunInSTAThread(Action action)
        {
            var thread = new System.Threading.Thread(() => { action(); });

            thread.SetApartmentState(System.Threading.ApartmentState.STA);

            thread.Start();

            while (true) { System.Threading.Thread.Sleep(1000); };
        }
    }
}
