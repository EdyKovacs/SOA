using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using BookStore.Helpers;
using BookStore.Service;
using BookStore.View;
using Xamarin.Forms;

namespace BookStore.ViewModel
{
    public class AddNewPageViewModel : BaseViewModel
    {
        #region Fields

        private AddPublicationPageViewModel _selectedPublication;

        #endregion

        #region UI Control Properties

        public List<ImageCheckBoxViewModel> Selections { get; set; }

        private bool _isButtonVisible;
        public bool IsButtonVisible
        {
            get => _isButtonVisible;
            set => SetValue(ref _isButtonVisible, value);
        }

        #endregion

        #region Command Properties

        public ICommand SelectCommand { get; private set; }
        public ICommand SelectionDoneCommand { get; private set; }

        #endregion

        public AddNewPageViewModel()
        {
            InitializeSelections();

            SelectCommand = new Command<ImageCheckBoxViewModel>(HandleCheckBoxClicked);
            SelectionDoneCommand = new Command(async () => await HandleNextButtonClickedAsync());
        }

        void InitializeSelections()
        {
            Selections = new List<ImageCheckBoxViewModel>
            {
                new ImageCheckBoxViewModel(Constants.PublicationTypeStrings.Book.Value, Constants.IconSources.BookIconSource.Value, Constants.IconSources.ActiveBookIconSource.Value, new AddBookPageViewModel()),
                new ImageCheckBoxViewModel(Constants.PublicationTypeStrings.Journal.Value, Constants.IconSources.JournalIconSource.Value, Constants.IconSources.ActiveJournalIconSource.Value, new AddJournalPageViewModel()),
                new ImageCheckBoxViewModel(Constants.PublicationTypeStrings.ConferencePaper.Value, Constants.IconSources.PaperIconSource.Value, Constants.IconSources.ActivePaperIconSource.Value, new AddPaperPageViewModel())
            };
        }

        private void HandleCheckBoxClicked(ImageCheckBoxViewModel parameter)
        {
            bool newState = parameter.IsActive;
            if (newState)
            {
                foreach (ImageCheckBoxViewModel viewModel in Selections)
                {
                    if (viewModel != parameter)
                    {
                        viewModel.IsActive = false;
                    }
                }
            }

            IsButtonVisible = newState;
            _selectedPublication = parameter.Publication;
        }

        private async Task HandleNextButtonClickedAsync()
        {
            IsBusy = true;

            _selectedPublication = _selectedPublication.Reset();
            await PageService.Instance.PushAsync(new AddPublicationPage(_selectedPublication));

            IsBusy = false;
        }
    }
}
