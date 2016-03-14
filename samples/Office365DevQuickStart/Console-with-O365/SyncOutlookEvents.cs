using Newtonsoft.Json.Linq;
using System;

namespace Console_with_O365
{
    public class SyncOutlookEvents : Scenario
    {
        public override void Run()
        {
            var tmgr = new UserTokenManagement();

            var token = tmgr.AcquireToken(Settings.ResourceUrlOfExchangeOnline);

            var api = new O365.OutlookCalendarAPI(token);

            var task = api.SyncEventsAsync(new JObject { { "startDateTime", "2016-03-08T00:00:00Z" }, { "endDateTime", "2016-03-11T00:00:00Z" } });

            task.Wait();

            var result = task.Result;

            Console.WriteLine(result.ToString());
        }
    }
}
