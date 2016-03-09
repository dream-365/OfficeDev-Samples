using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace Console_with_O365
{
    public class GraphCalendarAPI :  BaseApi
    {
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
    }
}
