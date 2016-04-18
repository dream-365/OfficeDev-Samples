using System.Configuration;

namespace AspNetMvc_with_O365
{
    public class OAuthSettings
    {

        private static string _clientId = ConfigurationManager.AppSettings["ida:ClientId"];
        private static string _clientSecret = ConfigurationManager.AppSettings["ida:ClientSecret"];
        private static string _tenantId = ConfigurationManager.AppSettings["ida:TenantID"];

        public static string ClientId { get { return _clientId; } }

        public static string TenantId { get { return _tenantId; } }

        public static string ClientSecret { get { return _clientSecret; } }
    }
}