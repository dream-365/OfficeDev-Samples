using AspNetMvc_MultiTenant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AspNetMvc_MultiTenant.Controllers
{

    [Authorize]
    [HandleError(ExceptionType = typeof(Office365AssertFailedException))]
    public class EmailController : Controller
    {
        private ServiceClientFactory _factory;

        public EmailController()
        {
            _factory = new ServiceClientFactory();
        }

        public async Task<ActionResult> Index()
        {
            var outlookServicesClient = await _factory.CreateOutlookServicesClientWithAsync("Mail");

            Assert.ThrowExceptionIfNull(outlookServicesClient, "Failed to create outlook service client, please ensure you have capbility to access mails");

            var result = await (from i in outlookServicesClient.Me.Folders.GetById("Inbox").Messages
                                     orderby i.DateTimeReceived descending
                                     select i).Take(10).ExecuteAsync();

            var mailItems = new List<MailItem>();

            foreach(var mail in result.CurrentPage)
            {
                mailItems.Add(new MailItem(mail));
            }

            return View(mailItems);
        }
    }
}