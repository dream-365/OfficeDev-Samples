using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Web;

namespace OAuth2_basic
{
    public class OauthWebAuthHelper
    {
        private OauthConfiguration _configuration;

        private const string OAUTH2_AUTHORIZE_V1_SUFFIX = @"/oauth2/";

        private const string OAUTH2_AUTHORIZE_SUFFIX   = @"/oauth2/v2.0";

        public OauthWebAuthHelper(OauthConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void LogOut()
        {
            var dialog = new WebBrowserDialog();

            dialog.Open(string.Format("{0}/logout", EndPointUrl));
        }

        protected string EndPointUrl {
            get
            {
                return string.Format("{0}/{1}/{2}", _configuration.Authority, _configuration.Tenant, OAUTH2_AUTHORIZE_V1_SUFFIX);
            }
        }

        protected string RequestAccessTokenUrl
        {
            get
            {
                return string.Format("{0}/{1}/oauth2/token", _configuration.Authority, _configuration.Tenant);
            }
        }

        
        public JObject GetAuthorizationCode()
        {
            JObject response = new JObject();

            var parameters = new Dictionary<string, string>
                {
                    { "response_type", "code" },
                    { "client_id", _configuration.ClientId },
                    { "redirect_uri", _configuration.RedirectURI },
                    { "prompt", "login"} 
                };

            var requestUrl = string.Format("{0}/authorize?{1}", EndPointUrl, BuildQueryString(parameters));

            var dialog = new WebBrowserDialog();

            dialog.OnNavigated((sender, arg) => {
                if (arg.Url.AbsoluteUri.StartsWith(_configuration.RedirectURI))
                {
                    var collection = HttpUtility.ParseQueryString(arg.Url.Query);

                    foreach (var key in collection.AllKeys)
                    {
                        response.Add(key, collection[key]);
                    }

                    dialog.Close();
                }
            });

            dialog.Open(requestUrl);

            return response;
        }


        public JObject GetAuthorizationCode(string scope)
        {
            throw new System.NotImplementedException();
        }

        public JObject AcquireTokenWithResource(string resource)
        {
            var codeResponse = GetAuthorizationCode();

            var code = codeResponse.GetValue("code").Value<string>();

            var parameters = new Dictionary<string, string>
                {
                    { "resource", resource},
                    { "client_id", _configuration.ClientId },
                    { "code",  code},
                    { "grant_type", "authorization_code" },
                    { "redirect_uri", _configuration.RedirectURI},
                };

            var client = new HttpClient();

            var content = new StringContent(BuildQueryString(parameters), Encoding.GetEncoding("utf-8"), "application/x-www-form-urlencoded");

            var url = string.Format("{0}/token", EndPointUrl);

            var response = client.PostAsync(url, content).Result;

            var text = response.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject(text) as JObject;
        }


        public JObject AcquireTokenWithScope(string scope)
        {
            throw new System.NotImplementedException();
        }

        private string BuildQueryString(IDictionary<string, string> parameters)
        {
            var list = new List<string>();

            foreach (var parameter in parameters)
            {
                list.Add(string.Format("{0}={1}", parameter.Key, HttpUtility.UrlEncode(parameter.Value)));
            }

            return string.Join("&", list);
        }
    }
}
