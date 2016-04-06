using Newtonsoft.Json.Linq;
using System;

namespace Console_with_O365.Scenarios
{
    public class SyncOutlookEvents : Scenario
    {
        public override void Run()
        {
            var tmgr = new UserTokenManagement();

            var token = tmgr.AcquireToken(Settings.ResourceUrlOfExchangeOnline);

            var api = new O365.OutlookCalendarAPI(token);

            var result = api.SyncEventsAsync(new JObject {
                { "startDateTime", "2016-03-28T00:00:00Z" },
                { "endDateTime", "2016-04-01T00:00:00Z" } })
                .Result;

            Console.WriteLine(result.ToString());
        }
    }
}
