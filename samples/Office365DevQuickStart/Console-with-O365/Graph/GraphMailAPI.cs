using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Console_with_O365.Graph
{
    public class GraphMailAPI : BaseApi
    {
        private const string BASE_URI = "https://graph.microsoft.com/v1.0";

        public GraphMailAPI(string token) : base(token)
        {
            
        }
        public async Task<JObject> SearchAsync(string text) {

            var uri = string.Format("{0}/{1}?$search=\"{2}\"", BASE_URI, "me/mailFolders/Inbox/messages", text);

            var response = await _httpClient.GetAsync(uri);

            var result = await response.Content.ReadAsStringAsync();

            return Deserialize(result);
        }
    }
}
