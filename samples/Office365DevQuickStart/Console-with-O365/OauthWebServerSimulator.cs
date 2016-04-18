using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Windows.Forms;

namespace Console_with_O365
{
    public class OauthWebServerSimulator
    {
        public enum Version {
            V1 = 1,
            V2 = 2
        }

        private OauthWebConfiguration _configuration;

        private const string OAUTH2_AUTHORIZE_V1_SUFFIX = @"oauth2/";

        private const string OAUTH2_AUTHORIZE_V2_SUFFIX   = @"oauth2/v2.0";

        private string _authorizeSuffix;

        public OauthWebServerSimulator(OauthWebConfiguration configuration, Version version = Version.V1)
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
                return string.Format("{0}/{1}/{2}", _configuration.Authority, _configuration.TenantId, _authorizeSuffix);
            }
        }

        
        public JObject Login()
        {
            JObject response = new JObject();

            var parameters = new Dictionary<string, string>
                {
                    { "response_type", "code id_token" },
                    { "client_id", _configuration.ClientId },
                    { "nonce", Guid.NewGuid().ToString()},
                    { "redirect_uri", _configuration.RedirectURI },
                    { "prompt", "login"} 
                };

            var requestUrl = string.Format("{0}/authorize?{1}", EndPointUrl, BuildQueryString(parameters));

            var dialog = new WebBrowserDialog();

            dialog.OnNavigated((sender, arg) => {
                if (arg.Url.AbsoluteUri.StartsWith(_configuration.RedirectURI))
                {
                    var browser = sender as WebBrowser;

                    var query = browser.Document.Url.Fragment.TrimStart('#');

                    var collection = HttpUtility.ParseQueryString(query);

                    foreach (var key in collection.AllKeys)
                    {
                        response.Add(key, collection[key]);
                    }

                    // dialog.Close();
                }
            });

            dialog.Open(requestUrl);

            return response;
        }


        /// <summary>
        /// Only supported in App Model 2.0
        /// </summary>
        /// <param name="scope"></param>
        /// <returns></returns>
        public JObject LoginWithScope(string scope)
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
