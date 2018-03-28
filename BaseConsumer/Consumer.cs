using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BaseConsumer
{
    public class Consumer
    {
        private string _url => BaseUrl + DestinationUrl;

        public string BaseUrl { get; set; }
        public string DestinationUrl { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }

        protected async Task<string> AuthenticationToken()
        {
            using (var httpClient = new HttpClient())
            {
                var clientToken = new HttpClient();
                clientToken.BaseAddress = new Uri(BaseUrl);
                var request = new HttpRequestMessage(HttpMethod.Post, "/token");

                var keyValues = new List<KeyValuePair<string, string>>();
                keyValues.Add(new KeyValuePair<string, string>("grant_type", "password"));
                keyValues.Add(new KeyValuePair<string, string>("Username", Username));
                keyValues.Add(new KeyValuePair<string, string>("Password", Password));

                request.Content = new FormUrlEncodedContent(keyValues);
                var tokenResponse = await clientToken.SendAsync(request);
                var tokenDetails = JsonConvert.DeserializeObject<Dictionary<string, string>>(tokenResponse.Content.ReadAsStringAsync().Result);
                var token = tokenDetails.FirstOrDefault().Value;
                return token;
            }
        }

        protected async Task<T> Post<T>(T model) where T : class
        {
            using (var httpClient = new HttpClient())
            {
                var myContent = JsonConvert.SerializeObject(model);
                var buffer = Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = await httpClient.PostAsync(new Uri(_url), byteContent);
                model = response.Content.ReadAsAsync<T>(new[] { new JsonMediaTypeFormatter() }).Result;

                return model;
            }
        }

        protected async Task<T> PostWithAuthentication<T>(T model) where T : class
        {
            using (var httpClient = new HttpClient())
            {
                var token = await AuthenticationToken();

                var myContent = JsonConvert.SerializeObject(model);
                var buffer = Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await httpClient.PostAsync(new Uri(_url), byteContent);
                model = response.Content.ReadAsAsync<T>(new[] { new JsonMediaTypeFormatter() }).Result;

                return model;
            }
        }
    }
}
