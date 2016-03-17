using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Windows.Forms;

namespace OAuth2_basic
{
    public class OauthWebAuthHelper
    {
        private OauthConfiguration _configuration;



        public OauthWebAuthHelper(OauthConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void LogOut()
        {
            var dialog = new WebBrowserDialog();

            dialog.Open(LogOutUrl);
        }

        protected string RequestAuthorizationCodeUrl {
            get
            {
                return string.Format("{0}/{1}/oauth2/authorize", _configuration.Authority, _configuration.Tenant);
            }
        }

        protected string RequestAccessTokenUrl
        {
            get
            {
                return string.Format("{0}/{1}/oauth2/token", _configuration.Authority, _configuration.Tenant);
            }
        }

        protected string LogOutUrl
        {
            get
            {
                return string.Format("{0}/{1}/oauth2/logout", _configuration.Authority, _configuration.Tenant);
            }
        }
        
        public JObject RequestAuthorizationCode()
        {
            JObject response = new JObject();

            var parameters = new Dictionary<string, string>
                {
                    { "response_type", "code" },
                    { "client_id", _configuration.ClientId },
                    { "redirect_uri", _configuration.RedirectURI },
                    { "prompt", "login"} 
                };

            var requestUrl = RequestAuthorizationCodeUrl + "?" + BuildQueryString(parameters);

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

        public JObject RequestAccessToken(string code, string resource)
        {
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

            var task = client.PostAsync("https://login.microsoftonline.com/common/oauth2/token", content);

            task.Wait();

            var response = task.Result;

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
