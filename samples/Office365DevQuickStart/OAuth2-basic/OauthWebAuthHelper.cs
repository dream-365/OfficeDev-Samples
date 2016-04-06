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
        public enum Version {
            V1 = 1,
            V2 = 2
        }

        private OauthConfiguration _configuration;

        private const string OAUTH2_AUTHORIZE_V1_SUFFIX = @"oauth2/";

        private const string OAUTH2_AUTHORIZE_V2_SUFFIX   = @"oauth2/v2.0";

        private string _authorizeSuffix;

        public OauthWebAuthHelper(OauthConfiguration configuration, Version version = Version.V1)
        {
            _configuration = configuration;

            switch (version)
            {
                case Version.V1: _authorizeSuffix = OAUTH2_AUTHORIZE_V1_SUFFIX; break;

                case Version.V2: _authorizeSuffix = OAUTH2_AUTHORIZE_V2_SUFFIX; break;
            }
        }

        public void LogOut()
        {
            var dialog = new WebBrowserDialog();

            dialog.Open(string.Format("{0}/logout", EndPointUrl));
        }

        protected string EndPointUrl {
            get
            {
                return string.Format("{0}/{1}/{2}", _configuration.Authority, _configuration.Tenant, _authorizeSuffix);
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
            JObject response = new JObject();

            var parameters = new Dictionary<string, string>
                {
                    { "response_type", "code" },
                    { "client_id", _configuration.ClientId },
                    { "redirect_uri", _configuration.RedirectURI },
                    { "prompt", "login"},
                    { "scope", scope}
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
            var codeResponse = GetAuthorizationCode(scope);

            var code = codeResponse.GetValue("code").Value<string>();

            var parameters = new Dictionary<string, string>
                {
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
