using System.Net;
using System.Net.Mail;

namespace SmtpClient_with_O365
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new SmtpClient();

            var sendFrom = "send_from@email.com";

            var sendTo = "send_to@email.com";

            var pass = "pass_word";

            client.UseDefaultCredentials = false;

            client.Credentials = new NetworkCredential(sendFrom, pass);

            client.Host = "smtp.office365.com";

            // optional
            client.TargetName = "STARTTLS/smtp.office365.com";

            client.Port = 587;

            client.EnableSsl = true;

            var message = new MailMessage();

            message.Sender = new MailAddress(sendFrom);

            message.From = message.Sender;

            message.To.Add(sendTo);

            message.Subject = "Message from SMTP Submission";

            message.Body = "Message from SMTP Submission";

            client.Send(message);
        }
    }
}
