using Microsoft.Office365.OutlookServices;
using System;
using System.ComponentModel.DataAnnotations;

namespace AspNetMvc_with_O365.Models
{
    public class MailItem
    {
        public string Id;

        public string Subject { get; set; }

        public string Sender { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM-dd-yyyy HH:mm tt}")]
        public DateTimeOffset? Received { get; set; }


        public MailItem(IMessage mailitem)
        {
            Id = mailitem.Id;

            Subject = mailitem.Subject;

            if (mailitem.Sender != null)
            {
                Sender = mailitem.Sender.EmailAddress.Address;
            }
            else
            {
                Sender = string.Empty;
            }

            if (mailitem.DateTimeReceived != null)
            {
                Received = mailitem.DateTimeReceived;
            }
        }
    }
}