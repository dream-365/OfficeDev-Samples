using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Office365.Discovery;
using Microsoft.Office365.OutlookServices;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AspNetMvc_with_O365
{
    public class ServiceClientFactory
    {
        private string _userUniqueId;

        private string _objectId;

        private AuthenticationContext _authContext;

        public ServiceClientFactory()
        {
            _userUniqueId = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;

            _objectId = ClaimsPrincipal.Current.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier").Value;

            Assert.ThrowExceptionIfIsNullOrWhiteSpace(_userUniqueId, "User UniqueId should be be null, empty or write space");

            Assert.ThrowExceptionIfIsNullOrWhiteSpace(_objectId, "User UniqueId should be be null, empty or write space");

            var authority = string.Format("{0}/{1}", ServiceConstants.AzureADEndPoint, OAuthSettings.TenantId);

            _authContext = new AuthenticationContext(authority, new SqlDBTokenCache(_userUniqueId));
        }

        public async Task<OutlookServicesClient> CreateOutlookServicesClientWithAsync(string capabilityName)
        {
            try
            {
                DiscoveryClient discClient = new DiscoveryClient(new Uri(ServiceConstants.DiscoverySvcEndpointUri),
                async () =>
                {
                    var authResult = await _authContext.AcquireTokenSilentAsync(ServiceConstants.DiscoverySvcResourceId,
                                                                               new ClientCredential(OAuthSettings.ClientId,
                                                                                                    OAuthSettings.ClientSecret),
                                                                               new UserIdentifier(_objectId,
                                                                                                  UserIdentifierType.UniqueId));

                    return authResult.AccessToken;
                });

                var dcr = await discClient.DiscoverCapabilityAsync(capabilityName);

                return new OutlookServicesClient(dcr.ServiceEndpointUri,
                    async () =>
                    {
                        var authResult = await _authContext.AcquireTokenSilentAsync(dcr.ServiceResourceId,
                                                                                   new ClientCredential(OAuthSettings.ClientId,
                                                                                                        OAuthSettings.ClientSecret),
                                                                                   new UserIdentifier(_objectId,
                                                                                                      UserIdentifierType.UniqueId));

                        return authResult.AccessToken;
                    });
            }catch(AdalException exception)
            {
                if (exception.ErrorCode == AdalError.FailedToAcquireTokenSilently)
                {
                    _authContext.TokenCache.Clear();

                    throw exception;
                }

                return null;
            }
        }
    }
}