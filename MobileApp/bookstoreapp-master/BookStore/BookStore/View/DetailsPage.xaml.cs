using BookStore.ViewModel;
using Xamarin.Forms;

namespace BookStore.View
{
    public partial class DetailsPage : ContentPage
    {
        public DetailsPage(DetailsPageViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = viewModel;
        }
    }
}
