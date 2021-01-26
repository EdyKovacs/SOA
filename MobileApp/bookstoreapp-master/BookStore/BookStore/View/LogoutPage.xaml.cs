using BookStore.ViewModel;
using Xamarin.Forms;

namespace BookStore.View
{
    public partial class LogoutPage : ContentPage
    {
        public LogoutPageViewModel ViewModel
        {
            get => BindingContext as LogoutPageViewModel;
            set => BindingContext = value;
        }

        public LogoutPage()
        {
            ViewModel = new LogoutPageViewModel();

            InitializeComponent();
        }
    }
}
