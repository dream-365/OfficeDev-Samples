using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Console_with_O365.O365
{
    public class OutlookCalendarAPI : BaseApi
    {
        private const string ENDPOINT_URI = "https://outlook.office.com/api/v2.0";

        public OutlookCalendarAPI(string token) : base(token)
        {

        }

        public Task<JObject> GetCalendarsAsync()
        {
            return GetAsync(Api("me/calendars"));
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

        private string Api(string api)
        {
            return string.Format("{0}/{1}", ENDPOINT_URI, api);
        }
    }
}
