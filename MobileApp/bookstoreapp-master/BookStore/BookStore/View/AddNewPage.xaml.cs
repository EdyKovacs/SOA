using BookStore.ViewModel;
using Xamarin.Forms;

namespace BookStore.View
{
    public partial class AddNewPage : ContentPage
    {
        public AddNewPage(AddNewPageViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = viewModel;
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }
            PublicationTypesList.SelectedItem = null;
        }
    }
}