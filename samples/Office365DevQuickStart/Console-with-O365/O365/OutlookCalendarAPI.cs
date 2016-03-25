using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Console_with_O365.O365
{
    public class OutlookCalendarAPI : BaseApi
    {
        private const string ENDPOINT_URI = "https://outlook.office.com/api/v2.0";

        private const string ENDPOINT_URI_V1 = "https://outlook.office.com/api/v1.0";

        public OutlookCalendarAPI(string token) : base(token)
        {

        }

        public Task<JObject> GetCalendarsAsync()
        {
            return GetAsync(Api("me/calendars"));
        }

        public async Task<JObject> CreateEventAsync(JObject body)
        {
            var text = body.ToString();

            var content = new StringContent(text, Encoding.GetEncoding("utf-8"), "application/json");

            var response = await _httpClient.PostAsync(Api("me/events"), content);

            var result = await response.Content.ReadAsStringAsync();

            return Deserialize(result);
        }

        public Task<JObject> SyncEventsAsync(JObject parameters)
        {
            var list = new List<string>();

            foreach(var parameter in parameters)
            {
                list.Add(string.Format("{0}={1}", parameter.Key, parameter.Value.ToString()));
            }

            var api = Api("me/calendarview?" + string.Join("&", list));

            return GetAsync(api);
        }

        public Task<JObject> GetEventsAsync()
        {
            return GetAsync(string.Format("{0}/{1}", ENDPOINT_URI_V1, "me/events"));
        }

        private string Api(string api)
        {
            return string.Format("{0}/{1}", ENDPOINT_URI, api);
        }
    }
}
