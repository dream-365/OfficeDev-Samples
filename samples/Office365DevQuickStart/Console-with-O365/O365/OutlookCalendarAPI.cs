using System;
using System.Threading.Tasks;

namespace Console_with_O365.O365
{
    public class OutlookCalendarAPI : BaseApi
    {
        public OutlookCalendarAPI(string token) : base(token)
        {

        }

        public async Task GetCalendarsAsync()
        {
            var uri = "https://outlook.office.com/api/v2.0/me/calendars";

            var result = await _httpClient.GetAsync(uri);

            var text = await result.Content.ReadAsStringAsync();

            Console.WriteLine(text);
        }
    }
}
