using Microsoft.Exchange.WebServices.Data;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_with_O365.Scenarios
{
    public class EWSOAuthDemo : Scenario
    {
        private static void UploadMIMEEmail(ExchangeService service)
        {
            EmailMessage email = new EmailMessage(service);

            string emlFileName = @"C:\Workbench\temp\tests\test-sample-message.eml";

            using (FileStream fs = new FileStream(emlFileName, FileMode.Open, FileAccess.Read))
            {
                byte[] bytes = new byte[fs.Length];
                int numBytesToRead = (int)fs.Length;
                int numBytesRead = 0;

                while (numBytesToRead > 0)
                {
                    int n = fs.Read(bytes, numBytesRead, numBytesToRead);

                    if (n == 0)
                        break;

                    numBytesRead += n;
                    numBytesToRead -= n;
                }

                // Set the contents of the .eml file to the MimeContent property.
                email.MimeContent = new MimeContent("UTF-8", bytes);
            }

            // Indicate that this email is not a draft. Otherwise, the email will appear as a 
            // draft to clients.
            ExtendedPropertyDefinition PR_MESSAGE_FLAGS_msgflag_read = new ExtendedPropertyDefinition(3591, MapiPropertyType.Integer);
            email.SetExtendedProperty(PR_MESSAGE_FLAGS_msgflag_read, 1);

            // This results in a CreateItem call to EWS. The email will be saved in the Inbox folder.
            email.Save(WellKnownFolderName.Inbox);
        }


        public override void Run()
        {
            string authority = "https://login.windows.net/MOD878411.onmicrosoft.com";
            string clientId = "dcd68e75-54d4-451c-9dfb-dbc43833ec1a";
            Uri redirectUri = new Uri("http://localhost");

            // string serverName = "outlook.office365.com";

            string resource = "https://outlook.office365.com/";

            AuthenticationResult authenticationResult = null;

            AuthenticationContext authenticationContext = new AuthenticationContext(authority, false);

            authenticationResult = authenticationContext.AcquireToken(resource, clientId, redirectUri);

            ExchangeService exchangeService = new ExchangeService(ExchangeVersion.Exchange2007_SP1);

            exchangeService.Url = new Uri(resource + "ews/exchange.asmx");
            exchangeService.TraceEnabled = true;
            exchangeService.TraceFlags = TraceFlags.All;
            exchangeService.Credentials = new OAuthCredentials(authenticationResult.AccessToken);

            UploadMIMEEmail(exchangeService);
        }
    }
}
