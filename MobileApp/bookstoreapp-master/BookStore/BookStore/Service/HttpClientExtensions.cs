using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace BookStore.Service
{
    //https://stackoverflow.com/posts/29772349/revisions
    public static class HttpClientExtensions
    {
        public static async Task<HttpResponseMessage> PatchAsync(this HttpClient client, string requestUriString, HttpContent iContent)
        {
            var method = new HttpMethod("PATCH");
            var request = new HttpRequestMessage(method, new Uri(requestUriString))
            {
                Content = iContent
            };

            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                response = await client.SendAsync(request);
            }
            catch (TaskCanceledException e)
            {
                Debug.WriteLine("ERROR: " + e);
            }

            return response;
        }
    }
}
