using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BookStore.Helpers;
using BookStore.Model;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace BookStore.Service
{
    public class RestAuthService
    {
        private static RestAuthService _instance;
        public static RestAuthService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new RestAuthService();
                }

                return _instance;
            }
        }

        private RestAuthService()
        {
        }

        public async Task<HttpResponseMessage> AuthHttpRequestAsync(string email, string password, string requestString)
        {
            var user = new User
            {
                Email = email,
                Password = password
            };
            var json = JsonConvert.SerializeObject(user);

            HttpResponseMessage response;
            using (var httpClient = new HttpClient())
            {
                using (var httpContent = new StringContent(json, Encoding.UTF8, "application/json"))
                {
                    response = await httpClient.PostAsync(Constants.APIStrings.BaseUrl.Value + requestString, httpContent);
                    System.Diagnostics.Debug.WriteLine($"=============================================");
                    System.Diagnostics.Debug.WriteLine($"BaseUrl: {Constants.APIStrings.BaseUrl.Value}");
                    System.Diagnostics.Debug.WriteLine($"Request string: {requestString}");
                    System.Diagnostics.Debug.WriteLine($"Response status: {response.StatusCode}");
                    System.Diagnostics.Debug.WriteLine($"Response content: {response.Content.ToString()}");
                    System.Diagnostics.Debug.WriteLine($"Response headers: {response.Headers}");
                    System.Diagnostics.Debug.WriteLine($"=============================================");
                }
            }
            return response;
        }

        public async Task<HttpResponseMessage> LogoutHttpRequestAsync()
        {
            HttpResponseMessage response;
            using (var httpClient = new HttpClient())
            {
                var token = (await SecureStorage.GetAsync(Constants.APIStrings.TokenHeaderKeyString.Value));
                httpClient.DefaultRequestHeaders.Add(Constants.APIStrings.TokenHeaderKeyString.Value, token);
                response = await httpClient.DeleteAsync(Constants.APIStrings.BaseUrl.Value + Constants.APIStrings.SignOutRouteString.Value);
            }
            return response;
        }
    }
}
