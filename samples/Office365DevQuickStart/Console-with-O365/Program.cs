using Newtonsoft.Json.Linq;
using System;

namespace Console_with_O365
{
    class Program
    {
        static void Main(string[] args)
        {
            var tmgr = new UserTokenManagement();

            var token = tmgr.AcquireToken(Settings.ResourceUrlOfGraph);

            var api = new Graph.GraphMailAPI(token);

            var task = api.SearchAsync("NOT from:support-jec@hotmail.com AND NOT to:jec@officedevgroup.onmicrosoft.com AND NOT cc:support-jec@hotmail.com AND NOT bcc:jec@microsoft.com");

            task.Wait();

            var result = task.Result;

            var mails = result.GetValue("value") as JArray;

            foreach(JObject mail in mails)
            {
               Console.WriteLine(mail.GetValue("subject").Value<string>());
            }
        }
    }
}
