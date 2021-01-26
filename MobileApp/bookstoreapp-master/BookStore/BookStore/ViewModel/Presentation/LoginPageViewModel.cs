using System.Text.RegularExpressions;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Input;
using BookStore.Helpers;
using BookStore.Service;
using BookStore.View;
using Xamarin.Essentials;
using Xamarin.Forms;
using Rg.Plugins.Popup.Services;
using BookStore.CustomViews;

namespace BookStore.ViewModel
{
    public class LoginPageViewModel : BaseViewModel
    {
        #region User data

        public string Email { get; set; } = "a@2a.com";
        public string Password { get; set; } = "password!";
        public string ConfirmedPassword { get; set; } = string.Empty;

        #endregion

        #region UI Control Properties

        public bool NeedsAccount { get; set; }
        public string LoginButtonText => NeedsAccount ? Constants.StandardStringConstants.RegisterString.Value : Constants.StandardStringConstants.LoginString.Value;
        public string SwitchButtonText => NeedsAccount ? Constants.StandardStringConstants.LoginString.Value : Constants.StandardStringConstants.RegisterString.Value;
        public string UserAccountStatusLabelText => NeedsAccount ? Constants.StandardStringConstants.NeedsAccountString.Value : Constants.StandardStringConstants.HasAccountString.Value;

        #endregion

        #region Commands
        
        public ICommand LoginCommand { get; private set; }
        public ICommand SwitchModeCommand { get; private set; }

        #endregion

        public LoginPageViewModel()
        {
            LoginCommand = new Command(async () => await LoginAsync());
            SwitchModeCommand = new Command(SwitchMode);
        }

        private void SwitchMode()
        {
            NeedsAccount = !NeedsAccount;

            OnPropertyChanged(nameof(NeedsAccount));
            OnPropertyChanged(nameof(LoginButtonText));
            OnPropertyChanged(nameof(SwitchButtonText));
            OnPropertyChanged(nameof(UserAccountStatusLabelText));
        }

        public async Task LoginAsync()
        {
            IsBusy = true;

            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                await PageService.Instance.DisplayAlertAsync(Constants.ValidatorStrings.StandardErrorMessage.Value, Constants.ValidatorStrings.EmailOrPasswordValidationErrorMessage.Value, Constants.StandardStringConstants.OkString.Value);
                IsBusy = false;
                return;
            }
            if(!Regex.IsMatch(Email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"))
            {
                await PageService.Instance.DisplayAlertAsync(Constants.ValidatorStrings.StandardErrorMessage.Value, Constants.ValidatorStrings.InvalidEmailErrorMessage.Value, Constants.StandardStringConstants.OkString.Value);
                IsBusy = false;
                return;
            }
            if(NeedsAccount)
            {
                if (string.IsNullOrWhiteSpace(ConfirmedPassword) || !ConfirmedPassword.Equals(Password))
                {
                    await PageService.Instance.DisplayAlertAsync(Constants.ValidatorStrings.StandardErrorMessage.Value, Constants.ValidatorStrings.PasswordMatchErrorMessage.Value, Constants.StandardStringConstants.OkString.Value);
                    IsBusy = false;
                    return;
                }
                await PopupNavigation.Instance.PushAsync(new CustomLoadingPopupPage(Constants.LoadingInfoStrings.CreateAccountString.Value));

                var response = await RestAuthService.Instance.AuthHttpRequestAsync(Email, Password, Constants.APIStrings.SignUpRouteString.Value);
                switch(response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        SwitchMode();
                        await PopupNavigation.Instance.PopAsync();
                        await LoginAsync();
                        break;
                    case HttpStatusCode.BadRequest:
                        await PopupNavigation.Instance.PopAsync();
                        await PageService.Instance.DisplayAlertAsync(Constants.ValidatorStrings.StandardErrorMessage.Value, Constants.ValidatorStrings.InvalidEmailErrorMessage.Value, Constants.StandardStringConstants.OkString.Value);
                        IsBusy = false;
                        break;
                    default:
                        await PopupNavigation.Instance.PopAsync();
                        await PageService.Instance.DisplayAlertAsync(Constants.ValidatorStrings.StandardErrorMessage.Value, Constants.ValidatorStrings.SomethingWrongErrorMessage.Value, Constants.StandardStringConstants.OkString.Value);
                        IsBusy = false;
                        break;
                }
            } 
            else
            {
                await PopupNavigation.Instance.PushAsync(new CustomLoadingPopupPage(Constants.LoadingInfoStrings.LogInString.Value));

                var response = await RestAuthService.Instance.AuthHttpRequestAsync(Email, Password, Constants.APIStrings.SignInRouteString.Value);
                switch (response.StatusCode)
                {       
                    case HttpStatusCode.OK:
                        var token = response.Headers.TryGetValues(Constants.APIStrings.TokenHeaderKeyString.Value, out var values) ? values.FirstOrDefault() : null;
                        await SecureStorage.SetAsync(Constants.APIStrings.TokenHeaderKeyString.Value, token);
                        await SecureStorage.SetAsync(Constants.APIStrings.CurrentUserMailKey.Value, Email);
                        await PopupNavigation.Instance.PopAsync();
                        PageService.Instance.SetMainPageAsNavigation(new MainPage());
                        break;
                    case HttpStatusCode.BadRequest:
                        await PopupNavigation.Instance.PopAsync();
                        await PageService.Instance.DisplayAlertAsync(Constants.ValidatorStrings.StandardErrorMessage.Value, Constants.ValidatorStrings.InvalidCredentialsErrorMessage.Value, Constants.StandardStringConstants.OkString.Value);
                        IsBusy = false;
                        break;
                    default:
                        await PopupNavigation.Instance.PopAsync();
                        await PageService.Instance.DisplayAlertAsync(Constants.ValidatorStrings.StandardErrorMessage.Value, Constants.ValidatorStrings.SomethingWrongErrorMessage.Value, Constants.StandardStringConstants.OkString.Value);
                        IsBusy = false;
                        break;
                }
            }
        }

        public async Task OnPasswordEnteredAsync()
        {
            if(!NeedsAccount)
            {
                IsBusy = false;

                await LoginAsync();
            }
        }
    }
}
