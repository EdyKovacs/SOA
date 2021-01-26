using System;
using BookStore.ViewModel;
using Xamarin.Forms;

namespace BookStore.View
{
    public partial class AddPublicationPage : ContentPage
    {
        public AddPublicationPageViewModel ViewModel
        {
            get => BindingContext as AddPublicationPageViewModel;
            set => BindingContext = value;
        }

        public AddPublicationPage(AddPublicationPageViewModel viewModel)
        {
            ViewModel = viewModel;

            InitializeComponent();
            RecurrencePicker.SelectedIndex = 0;
            SetEntryFocus();
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if(e.SelectedItem == null)
            {
                return;
            }
            ReferencesList.SelectedItem = null;
            PapersList.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ViewModel.WasUpdated = false;
        }

        void HandleTextChanged(object sender, EventArgs e)
        {
            if(ViewModel != null)
            {
                ViewModel.WasUpdated = true;
            }
        }

        void SetEntryFocus()
        {
            TitleEntry.ReturnCommand = new Command(() => AuthorEntry.Focus());
            AuthorEntry.ReturnCommand = new Command(() => GenreEntry.Focus());
            GenreEntry.ReturnCommand = new Command(() => DatePublishedPicker.Focus());
            FirstConferenceEntry.ReturnCommand = new Command(() => ConferenceLocationEntry.Focus());
            ConferenceLocationEntry.ReturnCommand = new Command(() => AbstractEditor.Focus());

            if(ViewModel is AddPaperPageViewModel addPaperPageViewModel)
            {
                NewReferenceEntry.ReturnCommand = new Command(async () => await addPaperPageViewModel.AddReferenceAsync());
            }
        }
    }
}
