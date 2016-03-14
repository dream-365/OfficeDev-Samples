using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Outlook_Notification_Service.Models
{
    public class Notification
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime ReceivedOn { get; set; }
    }
}