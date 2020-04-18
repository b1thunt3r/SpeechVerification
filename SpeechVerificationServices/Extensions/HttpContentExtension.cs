using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace SpeechVerificationServcies.Extensions
{
    public static class HttpContentExtension
    {
        public static async Task<T> ReadAsJsonAsync<T>(this HttpContent content)
        {
            return await JsonSerializer.DeserializeAsync<T>(await content.ReadAsStreamAsync());
        }
    }
}
