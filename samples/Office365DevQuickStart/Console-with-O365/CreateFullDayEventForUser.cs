using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_with_O365
{
    public class CreateFullDayEventForUser
    {
        public void Run()
        {
            var tmgr = new ApplicationTokenManagement();

            var token = tmgr.AcquireToken(Settings.ResourceUrlOfGraph);

            var api = new Graph.GraphCalendarAPI(token);

            JObject body = new JObject
            {
                {"subject", "Create from Office 365 API"},
                {"start", new JObject { { "DateTime", "2016-03-09T00:00:00"}, { "TimeZone", "China Standard Time" } } },
                {"end", new JObject { { "DateTime", "2016-03-10T00:00:00"}, { "TimeZone", "China Standard Time" } } },
                {"isAllDay", true }
            };

            var task = api.CreateEventAsync(body, "user@yourcompany.com");

            task.Wait();
        }
    }
}
