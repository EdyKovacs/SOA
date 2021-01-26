using BookStore.ViewModel;
using Xamarin.Forms;

namespace BookStore.View
{
    public partial class HomePage : ContentPage
    {
        private bool _wasLoaded;
        public HomePageViewModel ViewModel
        {
            get => BindingContext as HomePageViewModel;
            set => BindingContext = value;
        }
        
        public HomePage()
        {
            ViewModel = new HomePageViewModel();
            NavigationPage.SetBackButtonTitle(this, string.Empty);
            InitializeComponent();
            PublicationList.RefreshCommand = new Command(async () =>
            {
                PublicationList.IsRefreshing = false;
                await ViewModel.LoadDataAsync();
            });
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await (BindingContext as HomePageViewModel).SelectPublicationAsync(e.SelectedItem as PublicationViewModel);
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            if (!_wasLoaded)
            {
                await ViewModel.LoadDataAsync();
                _wasLoaded = true;
            }
        }
    }
}
