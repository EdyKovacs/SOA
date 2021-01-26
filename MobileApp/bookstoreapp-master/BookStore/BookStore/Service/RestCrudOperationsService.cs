using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BookStore.Helpers;
using BookStore.Model;
using BookStore.ViewModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Essentials;

namespace BookStore.Service
{
    public class RestCrudOperationsService
    {
        private static RestCrudOperationsService _instance;
        public static RestCrudOperationsService Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new RestCrudOperationsService();
                }
                return _instance;
            }
        }

        private RestCrudOperationsService()
        {
        }

        private async Task<HttpResponseMessage> SendGetRequestAsync(string route)
        {
            HttpResponseMessage response;
            using (var httpClient = new HttpClient())
            {
                var token = (await SecureStorage.GetAsync(Constants.APIStrings.TokenHeaderKeyString.Value));
                httpClient.DefaultRequestHeaders.Add(Constants.APIStrings.TokenHeaderKeyString.Value, token);
                response = await httpClient.GetAsync(Constants.APIStrings.BaseUrl.Value + route);
            }
            return response;
        }

        private async Task<HttpResponseMessage> SendPostRequestAsync(string route, Publication publication)
        {
            HttpResponseMessage response = null;
            using (var httpClient = new HttpClient())
            {
                var token = (await SecureStorage.GetAsync(Constants.APIStrings.TokenHeaderKeyString.Value));
                httpClient.DefaultRequestHeaders.Add(Constants.APIStrings.TokenHeaderKeyString.Value, token);
                var json = JsonConvert.SerializeObject(publication);
                using (var httpContent = new StringContent(json, Encoding.UTF8, "application/json"))
                {
                    response = await httpClient.PostAsync(Constants.APIStrings.BaseUrl.Value + route, httpContent);
                }
            }
            return response;
        }

        private async Task<HttpResponseMessage> SendDeleteRequestAsync(string route)
        {
            HttpResponseMessage response;
            using (var httpClient = new HttpClient())
            {
                var token = (await SecureStorage.GetAsync(Constants.APIStrings.TokenHeaderKeyString.Value));
                httpClient.DefaultRequestHeaders.Add(Constants.APIStrings.TokenHeaderKeyString.Value, token);
                response = await httpClient.DeleteAsync(Constants.APIStrings.BaseUrl.Value + route);
            }
            return response;
        }

        private async Task<HttpResponseMessage> SendPatchRequestAsync(string route, Publication publication)
        {
            HttpResponseMessage response = null;
            using (var httpClient = new HttpClient())
            {
                var token = (await SecureStorage.GetAsync(Constants.APIStrings.TokenHeaderKeyString.Value));
                httpClient.DefaultRequestHeaders.Add(Constants.APIStrings.TokenHeaderKeyString.Value, token);
                var json = JsonConvert.SerializeObject(publication);
                using (var httpContent = new StringContent(json, Encoding.UTF8, "application/json"))
                {
                    response = await httpClient.PatchAsync(Constants.APIStrings.BaseUrl.Value + route, httpContent);
                }
            }
            return response;
        }

        public async Task<List<Publication>> GetPublicationsAsync()
        {
            var responseMessage = await SendGetRequestAsync(Constants.APIStrings.GetPublicationsRouteString.Value);
            var publicationListsJson = await responseMessage.Content.ReadAsStringAsync();
            var jsonPublicationLists = JsonConvert.DeserializeObject<Dictionary<string, List<JObject>>>(publicationListsJson);

            var books = jsonPublicationLists[Constants.APIStrings.BooksKeyString.Value].ConvertAll(x => x.ToObject<Book>());
            var conferencePapers = jsonPublicationLists[Constants.APIStrings.ConferencePapersKeyString.Value].ConvertAll(x => x.ToObject<ConferencePaper>());
            var journals = jsonPublicationLists[Constants.APIStrings.JournalsKeyString.Value].ConvertAll(x => x.ToObject<Journal>());

            var allPublications = books.Concat<Publication>(conferencePapers).Concat(journals).ToList();
            return allPublications;
        }

        public async Task<ConferencePaperViewModel> GetPaperById(string id)
        {
            var responseMessage = await SendGetRequestAsync($"{Constants.APIStrings.PaperRouteString.Value}/{id}");
            var paperJson = await responseMessage.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<ConferencePaperViewModel>(paperJson);
        }

        public async Task<string> AddNewPublicationAsync(Publication publication)
        {
            var responseMessage = await SendPostRequestAsync(GetPublicationRoute(publication), publication);

            switch(responseMessage.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    var json = await responseMessage.Content.ReadAsStringAsync();
                    var addedPublication = JsonConvert.DeserializeObject<Publication>(json, GetPublicationSerializerSettings());
                    publication.Id = addedPublication.Id;
                    PublicationService.Instance.AddPublication(publication);
                    return addedPublication.Id;
                case System.Net.HttpStatusCode.BadRequest:
                    await PageService.Instance.DisplayAlertAsync(Constants.ValidatorStrings.StandardErrorMessage.Value, Constants.ValidatorStrings.EmptyFieldsErrorMessage.Value, Constants.StandardStringConstants.OkString.Value);
                    return null;
                default:
                    await PageService.Instance.DisplayAlertAsync(Constants.ValidatorStrings.StandardErrorMessage.Value, Constants.ValidatorStrings.StandardErrorMessage.Value, Constants.StandardStringConstants.OkString.Value);
                    return null;
            }
        }

        public async Task<bool> DeletePublicationAsync(string publicationToDeleteId)
        {
            var publicationToDelete = PublicationService.Instance.GetById(publicationToDeleteId);
            var responseMessage = await SendDeleteRequestAsync($"{GetPublicationRoute(publicationToDelete)}/{publicationToDeleteId}");

            switch (responseMessage.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    PublicationService.Instance.DeletePublication(publicationToDelete);
                    return true;
                default:
                    await PageService.Instance.DisplayAlertAsync(Constants.ValidatorStrings.StandardErrorMessage.Value, Constants.ValidatorStrings.StandardErrorMessage.Value, Constants.StandardStringConstants.OkString.Value);
                    return false;
            }
        }

        public async Task<bool> UpdatePublicationAsync(Publication publication)
        {
            var responseMessage = await SendPatchRequestAsync($"{GetPublicationRoute(publication)}/{publication.Id}", publication);

            switch (responseMessage.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    PublicationService.Instance.UpdatePublication(publication);
                    return true;
                case System.Net.HttpStatusCode.BadRequest:
                    await PageService.Instance.DisplayAlertAsync(Constants.ValidatorStrings.StandardErrorMessage.Value, Constants.ValidatorStrings.EmptyFieldsErrorMessage.Value, Constants.StandardStringConstants.OkString.Value);
                    return false;
                default:
                    await PageService.Instance.DisplayAlertAsync(Constants.ValidatorStrings.StandardErrorMessage.Value, Constants.ValidatorStrings.StandardErrorMessage.Value, Constants.StandardStringConstants.OkString.Value);
                    return false;
            }
        }

        public JsonSerializerSettings GetPublicationSerializerSettings()
        {
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new PublicationConverter<Publication, Book>());
            settings.Converters.Add(new PublicationConverter<Publication, Journal>());
            settings.Converters.Add(new PublicationConverter<Publication, ConferencePaper>());
            return settings;
        }

        public string GetPublicationRoute(Publication publication)
        {
            if (publication is Book)
            {
                return Constants.APIStrings.BookRouteString.Value;
            }
            if (publication is Journal)
            {
                return Constants.APIStrings.JournalRouteString.Value;
            }
            if (publication is ConferencePaper)
            {
                return Constants.APIStrings.PaperRouteString.Value;
            }

            return null;
        }
    }
}