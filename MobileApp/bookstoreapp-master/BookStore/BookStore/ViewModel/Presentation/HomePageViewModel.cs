using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using BookStore.Helpers;
using BookStore.Service;
using BookStore.View;
using Xamarin.Forms;
using BookStore.Model.Helpers;
using System.Linq;
using BookStore.CustomViews;
using Rg.Plugins.Popup.Services;

namespace BookStore.ViewModel
{
    public class HomePageViewModel : BaseViewModel
    {
        #region UI Control Properties

        private PublicationViewModel _selectedPublication;
        public PublicationViewModel SelectedPublication
        {
            get => _selectedPublication;
            set => SetValue(ref _selectedPublication, value);
        }
        public ObservableCollection<PublicationViewModel> Publications { get; private set; } = new ObservableCollection<PublicationViewModel>();
        public bool IsListEmpty => Publications.Count == 0;

        #endregion

        #region Command Properties

        public ICommand AddPublicationCommand { get; private set; }
        public ICommand DeletePublicationCommand { get; private set; }
        public ICommand LoadDataCommand { get; private set; }

        #endregion

        public HomePageViewModel()
        {
            LoadDataCommand = new Command(async () => await LoadDataAsync());
            AddPublicationCommand = new Command(async () => await AddPublicationAsync());
            DeletePublicationCommand = new Command<PublicationViewModel>(async (p) => await DeletePublicationAsync(p));
            SubscribeToNewPublicationEvents();
            SubscribeToUpdatePublicationEvents();
        }

        public async Task LoadDataAsync()
        {
            await PopupNavigation.Instance.PushAsync(new CustomLoadingPopupPage(Constants.LoadingInfoStrings.LoadDataString.Value));

            var allPublications = await PublicationService.Instance.FetchAllPublicationsAsync();
            Publications = new ObservableCollection<PublicationViewModel>(allPublications.Select(p => PublicationModelToViewModelConverter.Convert(p)));
            OnPropertyChanged(nameof(Publications));
            OnPropertyChanged(nameof(IsListEmpty));

            await PopupNavigation.Instance.PopAsync();
        }

        private async Task AddPublicationAsync()
        {
            IsBusy = true;

            var viewModel = new AddNewPageViewModel();
            await PageService.Instance.PushAsync(new AddNewPage(viewModel));

            IsBusy = false;
        }

        private async Task DeletePublicationAsync(PublicationViewModel publicationToDelete)
        {
            var canDelete = await PageService.Instance.DisplayAlertAsync(Constants.StandardStringConstants.AreYouSureString.Value, Constants.StandardStringConstants.DeletePublicationWarningString.Value, Constants.StandardStringConstants.DeleteString.Value, Constants.StandardStringConstants.CancelString.Value);
            if (canDelete)
            {
                await PopupNavigation.Instance.PushAsync(new CustomLoadingPopupPage(Constants.LoadingInfoStrings.DeletePublicationString.Value));

                if (await RestCrudOperationsService.Instance.DeletePublicationAsync(publicationToDelete.Id))
                {
                    Publications.Remove(publicationToDelete);
                    OnPropertyChanged(nameof(IsListEmpty));
                }

                await PopupNavigation.Instance.PopAsync();
            }
        }

        public async Task SelectPublicationAsync(PublicationViewModel selectedPublication)
        {
            if (selectedPublication == null)
            {
                return;
            }
            SelectedPublication = null;

            DetailsPageViewModel viewModel;
            switch (selectedPublication)
            {
                case BookViewModel bookViewModel:
                    viewModel = new BookDetailsPageViewModel(bookViewModel);
                    break;
                case JournalViewModel journalViewModel:
                    viewModel = new JournalDetailsPageViewModel(journalViewModel);
                    break;
                case ConferencePaperViewModel paperViewModel:
                    viewModel = new PaperDetailsPageViewModel(paperViewModel);
                    break;
                default:
                    viewModel = null;
                    break;
            }

            await PageService.Instance.PushAsync(new DetailsPage(viewModel));
        }

        private void SubscribeToNewPublicationEvents()
        {
            MessagingCenter.Subscribe<AddBookPageViewModel, BookViewModel>(this, Constants.SubscriptionMessageString.BookAdded.Value, HandlePublicationAdded);
            MessagingCenter.Subscribe<AddJournalPageViewModel, JournalViewModel>(this, Constants.SubscriptionMessageString.JournalAdded.Value, HandlePublicationAdded);
            MessagingCenter.Subscribe<AddPaperPageViewModel, ConferencePaperViewModel>(this, Constants.SubscriptionMessageString.PaperAdded.Value, HandlePublicationAdded);
        }
        
        private void SubscribeToUpdatePublicationEvents()
        {
            MessagingCenter.Subscribe<AddBookPageViewModel, BookViewModel>(this, Constants.SubscriptionMessageString.BookUpdated.Value, HandlePublicationUpdated);
            MessagingCenter.Subscribe<AddJournalPageViewModel, JournalViewModel>(this, Constants.SubscriptionMessageString.JournalUpdated.Value, HandlePublicationUpdated);
            MessagingCenter.Subscribe<AddPaperPageViewModel, ConferencePaperViewModel>(this, Constants.SubscriptionMessageString.PaperUpdated.Value, HandlePublicationUpdated);
        }

        private void HandlePublicationUpdated(object sender, PublicationViewModel updatedPublication)
        {
            Publications.Remove(Publications.First(x => x.Id == updatedPublication.Id));
            HandlePublicationAdded(sender, updatedPublication);
        }

        private void HandlePublicationAdded(object sender, PublicationViewModel newPublication)
        {
            Publications.Add(newPublication);
            OnPropertyChanged(nameof(IsListEmpty));
        }
    }
}