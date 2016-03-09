using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Console_with_O365
{
    public abstract class BaseApi
    {
        private string _token;

        protected HttpClient _httpClient;

        public BaseApi(string token)
        {
            _token = token;

            _httpClient = new HttpClient();

            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + _token);
        }

        protected JObject Deserialize(string text)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject(text) as JObject;
        }
    }
}
