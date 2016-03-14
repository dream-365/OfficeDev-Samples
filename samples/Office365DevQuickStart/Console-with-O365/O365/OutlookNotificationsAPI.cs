using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Console_with_O365.O365
{
    public class OutlookNotificationsAPI : BaseApi
    {
        private const string ENDPOINT_URI = "https://outlook.office.com/api/v2.0";

        public OutlookNotificationsAPI(string token) : base(token)
        {

        }

        public Task<JObject> Subscribe(string resource, string changeType)
        {
            var pushNotification = new JObject
            {
                { "@odata.type", "#Microsoft.OutlookServices.PushSubscription" },
                { "Resource", resource } ,
                { "NotificationURL", "https://outlook-notify.azurewebsites.net/api/notify" } ,
                { "ChangeType",changeType },
                { "ClientState", "c75831bd-fad3-4191-9a66-280a48528679"}
            };

            var content = new StringContent(pushNotification.ToString(), Encoding.Default, "application/json");

            return PostAsync(Api("me/subscriptions"), content);
        }


        public Task<JObject> Delete(string id)
        {
            return DeleteAsync(Api(string.Format("me/subscriptions('{0}')", id)));
        }

        private string Api(string api)
        {
            return string.Format("{0}/{1}", ENDPOINT_URI, api);
        }
    }
}
