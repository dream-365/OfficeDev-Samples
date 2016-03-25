using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_with_O365.Scenarios
{
    public class CreateAppointment : Scenario
    {
        public override void Run()
        {
            var tmgr = new UserTokenManagement();

            var token = tmgr.AcquireToken(Settings.ResourceUrlOfExchangeOnline);

            var api = new O365.OutlookCalendarAPI(token);

            JObject body = new JObject
            {
                {"Subject", "Create from Office 365 API"},
                {"Start", new JObject { { "DateTime", "2016-03-24T15:00:00"}, { "TimeZone", "China Standard Time" } } },
                {"End", new JObject { { "DateTime", "2016-03-24T16:00:00"}, { "TimeZone", "China Standard Time" } } }
            };

            var text = body.ToString();

            var result = api.CreateEventAsync(body).Result;
        }
    }
}
