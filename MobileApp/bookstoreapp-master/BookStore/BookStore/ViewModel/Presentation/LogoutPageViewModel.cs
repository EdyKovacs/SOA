using System.Net;
using System.Threading.Tasks;
using System.Windows.Input;
using BookStore.CustomViews;
using BookStore.Helpers;
using BookStore.Service;
using BookStore.View;
using Rg.Plugins.Popup.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BookStore.ViewModel
{
    public class LogoutPageViewModel : BaseViewModel
    {
        public string UserEmail
        {
            get => GetUserEmailAsync().Result;
        }
        public ICommand LogoutCommand { get; private set; }

        public LogoutPageViewModel()
        {
            LogoutCommand = new Command(async () => await LogoutAsync());
        }

        private async Task LogoutAsync()
        {
            IsBusy = true;

            await PopupNavigation.Instance.PushAsync(new CustomLoadingPopupPage(Constants.LoadingInfoStrings.LogOutString.Value));

            await RestAuthService.Instance.LogoutHttpRequestAsync();
            SecureStorage.Remove(Constants.APIStrings.TokenHeaderKeyString.Value);
            SecureStorage.Remove(Constants.APIStrings.CurrentUserMailKey.Value);

            await PopupNavigation.Instance.PopAsync();
            PageService.Instance.SetMainPage(new LoginPage());
        }

        public async Task<string> GetUserEmailAsync()
        {
            return await SecureStorage.GetAsync(Constants.APIStrings.CurrentUserMailKey.Value);
        }
    }
}
