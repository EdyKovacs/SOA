using System.Threading.Tasks;
using BookStore.Helpers;
using Xamarin.Forms;

namespace BookStore.Service
{
    public class PageService
    { 
        private static PageService _instance;
        public static PageService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PageService();
                }

                return _instance;
            }
        }

        private PageService()
        {
        }

        private Page MainPage
        {
            get { return Application.Current.MainPage; }
        }

        public void SetMainPage(Page page)
        {
            ConfigureiOSStatusBar(Color.Black);
            Application.Current.MainPage = page;
        }

        public void SetMainPageAsNavigation(Page page)
        {
            Application.Current.MainPage = new NavigationPage(page);
            ConfigureNavbar();
        }

        public void ConfigureNavbar()
        {
            ((NavigationPage)MainPage).BarBackgroundColor = Constants.Colors.PrimaryBlue.Value;
            ((NavigationPage)MainPage).BarTextColor = Color.White;
        }

        public void ConfigureiOSStatusBar(Color textColor)
        {
            if (Device.RuntimePlatform == Device.iOS)
            {
                if (MainPage is NavigationPage navigationPage)
                {
                    navigationPage.BarTextColor = textColor;
                }
            }
        }

        public async Task PushAsync(Page page)
        {
            await MainPage.Navigation.PushAsync(page);
        }

        public async Task<Page> PopAsync()
        {
            return await MainPage.Navigation.PopAsync();
        }

        public async Task PopToRootAsync()
        {
            await MainPage.Navigation.PopToRootAsync();
        }

        public async Task DisplayAlertAsync(string title, string message, string ok)
        {
            await MainPage.DisplayAlert(title, message, ok);
        }

        public async Task<bool> DisplayAlertAsync(string title, string message, string ok, string cancel)
        {
            return await MainPage.DisplayAlert(title, message, ok, cancel);
        }

        public async Task<string> DisplayActionSheetAsync(string title, string cancel, params string[] buttons)
        {
            return await MainPage.DisplayActionSheet(title, cancel, null, buttons);
        }
    }
}
