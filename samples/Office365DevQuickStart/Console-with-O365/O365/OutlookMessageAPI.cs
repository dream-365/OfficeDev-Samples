using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_with_O365.O365
{
    public class OutlookMessageAPI : BaseApi
    {
        private const string ENDPOINT_URI = "https://outlook.office.com/api/v2.0";

        public OutlookMessageAPI(string token) : base(token)
        {
        }

        public Task<JObject> GetMessagesAsync()
        {
            return GetAsync(Api("me/messages"));
        }

        private string Api(string api)
        {
            return string.Format("{0}/{1}", ENDPOINT_URI, api);
        }
    }
}
