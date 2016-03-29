using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_with_O365.Scenarios
{
    public class ListOffice365Services : Scenario
    {
        public override void Run()
        {
            var tmgr = new UserTokenManagement();

            // var token = tmgr.AcquireToken("https://api.office.com/discovery/");

            var token = tmgr.AcquireToken("https://microsoft320-my.sharepoint.com/");

            var api = new O365.ServiceDiscoveryAPI(token);

            var services = api.GetServicesAsync().Result;

            Console.WriteLine(services.ToString());
        }
    }
}
