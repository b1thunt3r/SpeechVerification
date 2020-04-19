using SpeechVerificationServcies.Extensions;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SpeechVerificationServcies
{
    public class ApiClient
    {
        private readonly HttpClient _client = new HttpClient();
        private static readonly String _apiPrefix = "/spid/v1.0";

        public ApiClient(String speechApiKey)
        {
            _client.BaseAddress = new Uri("https://westus.api.cognitive.microsoft.com/");

            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            _client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", speechApiKey);

            _client.DefaultRequestHeaders.UserAgent.Clear();
            _client.DefaultRequestHeaders.UserAgent.ParseAdd("Super Secret Authenticaton Client 1.0");
        }

        public async Task<T> PostAsync<T>(String url, HttpContent content, String contentType = null)
        {
            HttpResponseMessage response;

            using (content)
            {
                if (!String.IsNullOrWhiteSpace(contentType))
                {
                    content.Headers.ContentType = new MediaTypeHeaderValue(contentType);
                }

                response = await _client.PostAsync(_apiPrefix + url, content);
            }

            var result = await response.Content.ReadAsJsonAsync<T>();
            return result;
        }

        public async Task<T> GetAsync<T>(String url)
        {
            var response = await _client.GetAsync(_apiPrefix + url);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsJsonAsync<T>();
            return result;
        }

        public async Task DeleteAsync(String url)
        {
            var response = await _client.DeleteAsync(_apiPrefix + url);
            response.EnsureSuccessStatusCode();
        }
    }
}