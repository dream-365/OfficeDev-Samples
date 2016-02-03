using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Console_with_O365
{
    class Program
    {
        static void Main(string[] args)
        {
            var tmgr = new TokenManagement();

            var token = tmgr.GetTokenForUser();

            var operatinos = new MessageOperations(token);

            var task = operatinos.GetMessagesAsync();

            task.Wait();
        }
    }
}
