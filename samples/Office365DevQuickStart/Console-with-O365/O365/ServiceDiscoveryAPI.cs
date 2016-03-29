using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_with_O365.O365
{
    public class ServiceDiscoveryAPI : BaseApi
    {
        private const string END_POINT = "https://api.office.com/discovery/v2.0";

        public ServiceDiscoveryAPI(string token) : base(token)
        {

        }

        public Task<JObject> GetServicesAsync()
        {
            return GetAsync(string.Format("{0}/{1}", END_POINT, "me/services"));
        }
    }
}
