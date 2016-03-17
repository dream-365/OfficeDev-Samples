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

            var codeResponse = auth.RequestAuthorizationCode();

            Console.WriteLine("Authorization Code:");
            Console.WriteLine(codeResponse);

            var tokenResponse = auth.RequestAccessToken(
                code: codeResponse.GetValue("code").Value<string>(), resource: "https://outlook.office.com");

            Console.WriteLine("Access Token:");
            Console.WriteLine(tokenResponse);

            var accessToken = tokenResponse.GetValue("access_token").Value<string>();

            var parts = accessToken.Split('.');

            var header = Utility.DecodeBase64String(parts[0]);

            var data = Utility.DecodeBase64String(parts[1]);

            Console.WriteLine("JWT header:");
            Console.WriteLine(JsonConvert.DeserializeObject(header).ToString());

            Console.WriteLine("JWT data:");
            Console.WriteLine(JsonConvert.DeserializeObject(data).ToString());

            Console.ReadLine();
        }
    }
}
