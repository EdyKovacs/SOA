using BookStore.ViewModel;
using Xamarin.Forms;

namespace BookStore.View
{
    public partial class LoginPage : ContentPage
    {
        public LoginPageViewModel ViewModel
        {
            get => BindingContext as LoginPageViewModel;
            set => BindingContext = value;
        }

        public LoginPage()
        {
            ViewModel = new LoginPageViewModel();

            InitializeComponent();
            EmailEntry.ReturnCommand = new Command(() => PasswordEntry.Focus());
            PasswordEntry.ReturnCommand = new Command(async () =>
            {
                ConfirmPasswordEntry.Focus();
                await ViewModel.OnPasswordEnteredAsync();
            });
            ConfirmPasswordEntry.ReturnCommand = new Command(async () => await ViewModel.LoginAsync());
        }
    }
}
