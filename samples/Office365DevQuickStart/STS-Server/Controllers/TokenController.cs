using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace STS_Server.Controllers
{
    public class TokenController : ApiController
    {
        public void Post([FromBody]string code)
        {

        }
    }
}
