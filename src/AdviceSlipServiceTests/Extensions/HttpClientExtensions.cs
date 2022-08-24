using AdviceSlipService.Models;
using Newtonsoft.Json;
using System.Text;

namespace AdviceSlipServiceTests.Extensions
{
    public static class HttpClientExtensions
    {
        public static async Task<T> PostAndDeserialize<T>(this HttpClient client, string requestUri, string jsonBody)
        {
            HttpContent httpContent = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(requestUri, httpContent);

            var result = await response.Content.ReadAsStringAsync();
            var adviceResponse = JsonConvert.DeserializeObject<T>(result);
            
            return adviceResponse;
        }

        public static async Task<HttpResponseMessage> PostAsync(this HttpClient client, string requestUri, string jsonBody)
        {
            HttpContent httpContent = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(requestUri, httpContent);

            return response;
        }
    }
}
