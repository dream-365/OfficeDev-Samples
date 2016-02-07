using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Upload_file_through_HTTP_Server.Controllers
{
    public class FilesController : ApiController
    {
        public async Task<string> Post()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            var provider = await Request.Content.ReadAsMultipartAsync();

            var encoding = Request.Headers.GetValues("x-encoding").FirstOrDefault();

            var content = provider.Contents.FirstOrDefault();

            using (var stream = await content.ReadAsStreamAsync())
            using (var sr = new StreamReader(stream))
            {
                var text = sr.ReadToEnd();

                return text.ToLower();
            }
        }
    }
}
