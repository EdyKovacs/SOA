using BookStore.Helpers;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms.Xaml;

namespace BookStore.CustomViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomLoadingPopupPage : PopupPage
    {
        public string LoadingMessage { get; set; } = Constants.LoadingInfoStrings.LoadingString.Value;

        public CustomLoadingPopupPage(string loadingMessage = null)
        {
            InitializeComponent();

            if(loadingMessage != null)
            {
                LoadingMessage = loadingMessage;
            }
            CloseWhenBackgroundIsClicked = false;

            BindingContext = this;
        }
    }
}
