using Microsoft.Exchange.WebServices.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exchange_managed_api
{
    class Program
    {
        static void Main(string[] args)
        {
            ExchangeService service = new ExchangeService();

            service.UseDefaultCredentials = false;

            service.Credentials = new WebCredentials("user@your-domain.onmicrosoft.com", "");

            service.Url = new Uri("https://outlook.office365.com/EWS/Exchange.asmx");

            Folder inbox = Folder.Bind(service, WellKnownFolderName.Inbox);

            SearchFilter sf = new SearchFilter.SearchFilterCollection(LogicalOperator.And, new SearchFilter.IsEqualTo(EmailMessageSchema.IsRead, true));
            ItemView view = new ItemView(1);

            FindItemsResults<Item> findResults = service.FindItems(WellKnownFolderName.Inbox, sf, view);
        }
    }
}
