﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Console_with_O365
{
    public abstract class BaseApi
    {
        private string _token;

        protected HttpClient _httpClient;

        public BaseApi(string token)
        {
            _token = token;

            _httpClient = new HttpClient();

            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + _token);
        }


        protected async Task<JObject> GetAsync(string url, IDictionary<string, string> headers = null)
        {
            if(headers != null)
            {
                foreach(var header in headers)
                {
                    _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }

            var result = await _httpClient.GetAsync(url);

            var text = await result.Content.ReadAsStringAsync();

            foreach(var header in result.Headers)
            {
                Console.WriteLine("{0}:{1}", header.Key, string.Join(";", header.Value));
            }

            return Deserialize(text);
        }

        protected async Task<JObject> PostAsync(string url, HttpContent content)
        {
            var result = await _httpClient.PostAsync(url, content);

            var text = await result.Content.ReadAsStringAsync();

            return Deserialize(text);
        }

        protected async Task<JObject> DeleteAsync(string url)
        {
            var result = await _httpClient.DeleteAsync(url);

            var text = await result.Content.ReadAsStringAsync();

            return Deserialize(text);
        }

        protected JObject Deserialize(string text)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject(text) as JObject;
        }
    }
}
