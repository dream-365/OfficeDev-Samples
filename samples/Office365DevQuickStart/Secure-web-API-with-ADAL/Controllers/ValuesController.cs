using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace Secure_web_API_with_ADAL.Controllers
{
    [Authorize]
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            // You can use the ClaimsPrincipal to access information about the
            // user making the call.  In this case, we use the 'sub' or
            // NameIdentifier claim to serve as a key for the tasks in the data store.

            Claim subject = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier);

            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
