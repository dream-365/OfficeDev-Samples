using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Console_with_O365.Graph
{
    public class GraphCalendarAPI :  BaseApi
    {
        private const string BASE_URI = "https://graph.microsoft.com/v1.0";

        public GraphCalendarAPI(string token) : base(token)
        {

        }

        public async Task<JObject> ListCalendarsAsync()
        {
            var uri = "https://graph.microsoft.com/v1.0/me/calendars";

            var result = await _httpClient.GetAsync(uri);

            var text = await result.Content.ReadAsStringAsync();

            return Newtonsoft.Json.JsonConvert.DeserializeObject(text) as JObject;
        }

        public async Task<JObject> CreateEventAsync(JObject body, string user)
        {
            string uri = string.Format("{0}/users/{1}/events", BASE_URI, user);

            var text = body.ToString();

            var content = new StringContent(text, Encoding.GetEncoding("utf-8"), "application/json");

            var response = await _httpClient.PostAsync(uri, content);

            var result = await response.Content.ReadAsStringAsync();

            return Deserialize(result);
        }
    }
}
