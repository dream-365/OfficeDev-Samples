using Newtonsoft.Json.Linq;

namespace Console_with_O365
{
    public class CreateFullDayEventForUser : Scenario
    {
        public override void Run()
        {
            var tmgr = new ApplicationTokenManagement();

            var token = tmgr.AcquireToken(Settings.ResourceUrlOfGraph);

            var api = new Graph.GraphCalendarAPI(token);

            JObject body = new JObject
            {
                {"subject", "Create from Office 365 API"},
                {"start", new JObject { { "dateTime", "2016-03-09T00:00:00"}, { "timeZone", "China Standard Time" } } },
                {"end", new JObject { { "dateTime", "2016-03-10T00:00:00"}, { "timeZone", "China Standard Time" } } },
                {"isAllDay", true }
            };

            var task = api.CreateEventAsync(body, "user@yourcompany.com");

            task.Wait();
        }
    }
}
