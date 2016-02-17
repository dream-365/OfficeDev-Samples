using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Upload_file_through_HTTP_Client
{
    public class FileStreamOperations
    {
        public async Task Upload()
        {
            Console.WriteLine("start to upload file...");

            using (var memoryStream = new MemoryStream())
            using (var sw = new StreamWriter(memoryStream))
            {
                sw.WriteLine("Hello World");

                sw.Flush();

                memoryStream.Position = 0;

                var multipartFormDataContent = new MultipartFormDataContent();

                multipartFormDataContent.Headers.Add("x-encoding", "utf8");

                var streamContent = new StreamContent(memoryStream);

                multipartFormDataContent.Add(streamContent);

                var client = new HttpClient();

                var response = await client.PostAsync("http://localhost:20184/api/files", multipartFormDataContent);

                var text = await response.Content.ReadAsStringAsync();

                Console.WriteLine("response: {0}", text);
            }

            Console.WriteLine("end of uploading file!");
        }
    }
}
