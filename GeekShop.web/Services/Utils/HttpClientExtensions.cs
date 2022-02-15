using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;

namespace GeekShop.web.Services.Utils
{
    public static class HttpClientExtensions
    {
        private static MediaTypeHeaderValue _contentType = new MediaTypeHeaderValue("application/json");

        public static async Task<T> ReadContentAs<T>(this HttpResponseMessage response)
        {
            Console.WriteLine(response);

            // Verify the status code
            if (!response.IsSuccessStatusCode) throw new ApplicationException($"Something went wrong calling the API: {response.ReasonPhrase}");

            // Read as Json Stringfied
            var dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            
            // Convert from JSON to Object Entity
            return JsonSerializer.Deserialize<T>(dataAsString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true } );
        }

        public static bool HandleNoContent(this HttpResponseMessage response)
        {
            // Verify the status code
            if (!response.IsSuccessStatusCode) throw new ApplicationException($"Something went wrong calling the API: {response.ReasonPhrase}");

            if (response.StatusCode == HttpStatusCode.NoContent) return true;

            return false;
        }

        public static Task<HttpResponseMessage> PostAsJson<T>(this HttpClient httpClient, string url, T data)
        {
            var dataAsString = JsonSerializer.Serialize(data);

            var content = new StringContent(dataAsString);

            content.Headers.ContentType = _contentType;

            return httpClient.PostAsync(url, content);
        }

        public static Task<HttpResponseMessage> PutAsJson<T>(this HttpClient httpClient, string url, T data)
        {
            var dataAsString = JsonSerializer.Serialize(data);

            var content = new StringContent(dataAsString);

            content.Headers.ContentType = _contentType;

            return httpClient.PutAsync(url, content);
        }
    }
}
