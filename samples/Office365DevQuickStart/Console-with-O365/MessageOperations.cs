using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Console_with_O365
{
    public class MessageOperations
    {
        private string _token;

        public MessageOperations(string token)
        {
            _token = token;
        }

        public async Task GetMessagesAsync()
        {
            var uri = "https://graph.microsoft.com/v1.0/me/mailFolders/Inbox/messages";

            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + _token);

            var result = await httpClient.GetAsync(uri);

            var text = await result.Content.ReadAsStringAsync();

            Console.WriteLine(text);
        }
    }
}
