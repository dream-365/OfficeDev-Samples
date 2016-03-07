using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace using_CSOM_with_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var webUrl = "[your_site_url]";

            var userEmailAddress = "[your_email_address]";

            using (var context = new ClientContext(webUrl))
            {
                Console.WriteLine("input your password and click enter");

                context.Credentials = new SharePointOnlineCredentials(userEmailAddress, GetPasswordFromConsoleInput());
                context.Load(context.Web, w => w.Title);
                context.ExecuteQuery();

                Console.WriteLine("Your site title is: " + context.Web.Title);

                //Retrieve site users             
                var users = context.LoadQuery(context.Web.SiteUsers);

                context.ExecuteQuery();

                foreach(var user in users)
                {
                    Console.WriteLine(user.Email);
                }
            }
        }

        private static SecureString GetPasswordFromConsoleInput()
        {
            ConsoleKeyInfo info;

            //Get the user's password as a SecureString
            SecureString securePassword = new SecureString();
            do
            {
                info = Console.ReadKey(true);
                if (info.Key != ConsoleKey.Enter)
                {
                    securePassword.AppendChar(info.KeyChar);
                }
            }
            while (info.Key != ConsoleKey.Enter);

            return securePassword;
        }
    }
}
