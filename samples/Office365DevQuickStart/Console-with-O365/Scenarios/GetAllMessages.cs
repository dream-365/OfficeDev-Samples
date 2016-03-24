using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_with_O365.Scenarios
{
    public class GetAllMessages : Scenario
    {
        public override void Run()
        {
            var tmgr = new UserTokenManagement();

            var token = tmgr.AcquireToken(Settings.ResourceUrlOfExchangeOnline);

            var api = new O365.OutlookMessageAPI(token);

            var result = api.GetMessagesAsync().Result;

            var value = result.GetValue("value") as JArray;

            var length = value.Count;

            Console.WriteLine(result.ToString());
        }
    }
}
