using Newtonsoft.Json.Linq;
using System;

namespace Console_with_O365
{
    class Program
    {
        static void Main(string[] args)
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

            var task = api.CreateEventAsync(body, "jec@microsoft320.onmicrosoft.com");

            task.Wait();
        }
    }
}
