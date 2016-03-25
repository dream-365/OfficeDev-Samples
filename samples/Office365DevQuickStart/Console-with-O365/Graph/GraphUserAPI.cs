using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_with_O365.Graph
{
    public class GraphUserAPI : BaseApi
    {
        public GraphUserAPI(string token) : base(token)
        {

        }

        private const string BASE_URI = "https://graph.microsoft.com/v1.0";

        public async Task<JObject> ListUsersAsync()
        {
            var uri = string.Format("{0}/{1}", BASE_URI, "users");

            var response = await _httpClient.GetAsync(uri);

            var result = await response.Content.ReadAsStringAsync();

            return Deserialize(result);
        }
    }
}
