using BookStore.Helpers;
using Xamarin.Forms;

namespace BookStore.View
{
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();
            Master = new LogoutPage();
            Detail = new NavigationPage(new HomePage())
            {
                BarBackgroundColor = Constants.Colors.PrimaryBlue.Value,
                BarTextColor = Color.White
            };
            NavigationPage.SetBackButtonTitle(this, string.Empty);
        }
    }
}
