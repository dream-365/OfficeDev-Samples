using Microsoft.Office365.SharePoint.CoreServices;
using Microsoft.OneDrive.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Office365SPoClient_console
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new SharePointClient(
                new Uri("https://microsoft320-my.sharepoint.com/_api/v2.0/"), 
                () => {
                    return Task.FromResult("eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6Ik1uQ19WWmNBVGZNNXBPWWlKSE1iYTlnb0VLWSIsImtpZCI6Ik1uQ19WWmNBVGZNNXBPWWlKSE1iYTlnb0VLWSJ9.eyJhdWQiOiJodHRwczovL21pY3Jvc29mdDMyMC1teS5zaGFyZXBvaW50LmNvbS8iLCJpc3MiOiJodHRwczovL3N0cy53aW5kb3dzLm5ldC9lMDdkMjIwZS1mNGMyLTQ0MWYtYWQ5YS05ZDZmYTU5YjRlNTIvIiwiaWF0IjoxNDU5MTY0ODI0LCJuYmYiOjE0NTkxNjQ4MjQsImV4cCI6MTQ1OTE2ODcyNCwiYWNyIjoiMSIsImFtciI6WyJwd2QiXSwiYXBwaWQiOiIyNGJiMTcyNS01NGQ5LTQxOTAtYjU4Yy1iYTUzNDdmYjMzNmUiLCJhcHBpZGFjciI6IjAiLCJmYW1pbHlfbmFtZSI6IkplZmZyZXkiLCJnaXZlbl9uYW1lIjoiQ2hlbiIsImlwYWRkciI6IjE2Ny4yMjAuMjMyLjUxIiwibmFtZSI6IkplZmZyZXkgQ2hlbiIsIm9pZCI6IjEyYmEwNzIwLTg0ZGEtNDZjMy05N2U1LWQ0YWU3YWNkNDMzMyIsInB1aWQiOiIxMDAzQkZGRDg3Njk1OUQyIiwic2NwIjoiQ2FsZW5kYXJzLlJlYWQgTWFpbC5SZWFkIE1haWwuU2VuZCBNeUZpbGVzLlJlYWQgTXlGaWxlcy5Xcml0ZSBvcGVuaWQiLCJzdWIiOiI3Uk9TZmluVzl0U1o5bDdZcEJpNFdrUWFHR3pKb2E0NURFUHFSaVhtTS1vIiwidGlkIjoiZTA3ZDIyMGUtZjRjMi00NDFmLWFkOWEtOWQ2ZmE1OWI0ZTUyIiwidW5pcXVlX25hbWUiOiJqZWNATWljcm9zb2Z0MzIwLm9ubWljcm9zb2Z0LmNvbSIsInVwbiI6ImplY0BNaWNyb3NvZnQzMjAub25taWNyb3NvZnQuY29tIiwidmVyIjoiMS4wIn0.d_VlBeQWsk4_FNLq6_MzgOY5OMDVT8yA9t8JcyqLJwqknJJR4zQ7Ntfu_lDcptIHpD0H8CsuNmL08xLneUEaDEFhYvIuDeJ_0rbetvnHRgHW9YWr6leWvXyWvosgCFl6jC85kppAOOpsYphZiLtLFYjUODMB1OeW2FW7YXxKSW2D2nn7O3SAkOMM62Eh30bFXUu5-lP2TbaZrsm8HZ6DLLsQYOQ-CS5wIkM4N7u_giwBBgmE3AliZ6klYec5OvwBWSyS9pjUlL3tN33cg6YwvqLg4LBsQcgGea5NwXch_DynzGQcOtfrYHrA08xGwUPKxMiGQ2Ge3vtCTUNeIqdQtw");
                });

            client.Files.GetById(""); ;

            var drive = client.Drive.ExecuteAsync().Result;
        }
    }
}
