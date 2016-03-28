using System;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace OAuth2_basic
{
    /// <summary>
    /// Authorization Code Grant Flow
    /// https://msdn.microsoft.com/en-us/library/azure/dn645542.aspx
    /// </summary>
    public class AuthorizationCodeGrantFlow
    {
        public void Go(OauthConfiguration oauth)
        {
            var auth = new OauthWebAuthHelper(oauth);

            var tokenResponse = auth.AcquireTokenWithResource(resource: "https://outlook.office.com");

            Console.WriteLine("access token:");

            Console.WriteLine(tokenResponse);

            var accessToken = tokenResponse.GetValue("access_token").Value<string>();

            var validator = new JsonWebTokenValidator();

            var jwt = validator.Validate(accessToken);

            Console.WriteLine(JsonConvert.SerializeObject(jwt.Payload, Formatting.Indented));

            Console.ReadLine();
        }
    }
}
