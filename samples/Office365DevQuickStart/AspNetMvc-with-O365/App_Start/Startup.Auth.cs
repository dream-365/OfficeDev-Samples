using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace AspNetMvc_with_O365
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);

            app.UseCookieAuthentication(new CookieAuthenticationOptions());

            var authority = string.Format("{0}/{1}", ServiceConstants.AzureADEndPoint, OAuthSettings.TenantId);

            var options = new OpenIdConnectAuthenticationOptions {
                ClientId = OAuthSettings.ClientId,
                Authority = authority
            };

            options.Notifications = new OpenIdConnectAuthenticationNotifications
            {
                AuthorizationCodeReceived = (context) => {

                    ClientCredential credential = new ClientCredential(OAuthSettings.ClientId, OAuthSettings.ClientSecret);

                    string nameIdentifier = context.AuthenticationTicket.Identity.FindFirst(ClaimTypes.NameIdentifier).Value;

                    AuthenticationContext authContext = new AuthenticationContext(authority, new SqlDBTokenCache(nameIdentifier));

                    authContext.AcquireTokenByAuthorizationCode(
                        authorizationCode: context.Code, 
                        redirectUri: new Uri(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path)), 
                        clientCredential: credential, 
                        resource: ServiceConstants.WindowsGraphResourceId);

                    return Task.FromResult(0);
                },

                RedirectToIdentityProvider = (context) =>
                {
                    string appBaseUrl = context.Request.Scheme + "://" + context.Request.Host + context.Request.PathBase;

                    context.ProtocolMessage.RedirectUri = appBaseUrl + "/";
                    context.ProtocolMessage.PostLogoutRedirectUri = appBaseUrl;

                    return Task.FromResult(0);
                },

                AuthenticationFailed = (context) =>
                {
                    context.HandleResponse();

                    return Task.FromResult(0);
                }
            };

            app.UseOpenIdConnectAuthentication(options);
        }
    }
}