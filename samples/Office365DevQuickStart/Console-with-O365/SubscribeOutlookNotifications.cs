using System;

namespace Console_with_O365
{
    public class SubscribeOutlookNotifications : Scenario
    {
        public override void Run()
        {
            var tmgr = new UserTokenManagement();

            var token = tmgr.AcquireToken(Settings.ResourceUrlOfExchangeOnline);

            var api = new O365.OutlookNotificationsAPI(token);

            var task = api.Subscribe("https://outlook.office.com/api/v2.0/me/events", "Created");

            task.Wait();

            var result = task.Result;

            Console.WriteLine(result.ToString());
        }
    }
}
