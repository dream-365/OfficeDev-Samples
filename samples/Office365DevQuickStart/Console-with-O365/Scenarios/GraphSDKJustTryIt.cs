using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_with_O365.Scenarios
{
    public class GraphSDKJustTryIt : Scenario
    {
        public override void Run()
        {
            var client = new GraphServiceClient(new GraphAuthenticationProvider(), null);

            client.Groups["group_id"].Members["member_id"].Reference.Request().DeleteAsync();

            throw new NotImplementedException();
        }
    }
}
