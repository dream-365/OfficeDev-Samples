using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace Console_with_O365
{
    public class GraphAuthenticationProvider : IAuthenticationProvider
    {
        private string _token = null;

        public GraphAuthenticationProvider()
        {

        }

        public Task AuthenticateRequestAsync(HttpRequestMessage request)
        {            
            throw new NotImplementedException();
        }
    }
}
