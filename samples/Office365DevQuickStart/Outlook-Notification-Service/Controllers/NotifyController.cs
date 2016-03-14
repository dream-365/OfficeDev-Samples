using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Outlook_Notification_Service.Controllers
{
    public class NotifyController : ApiController
    {
        private DefaultDbConext _context;

        public NotifyController()
        {
            _context = new DefaultDbConext();
        }

        public HttpResponseMessage Post(string validationToken)
        {
            var response = Request.CreateResponse(HttpStatusCode.OK);

            response.Content = new StringContent(validationToken, Encoding.Default, "text/plain");

            _context.Notifications.Add(new Models.Notification { Content = string.Format("validation token: {0}", validationToken), ReceivedOn = DateTime.Now });

            _context.SaveChanges();

            return response;
        }

        public async Task<HttpResponseMessage> Post()
        {
            var content = await Request.Content.ReadAsStringAsync();

            _context.Notifications.Add(new Models.Notification { Content = content, ReceivedOn = DateTime.Now });

            _context.SaveChanges();

            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
