using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using BookStore.CustomViews;
using BookStore.Helpers;
using BookStore.Model;
using BookStore.Model.Helpers;
using BookStore.Service;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using static BookStore.Model.Enums;

namespace BookStore.ViewModel
{
    public class AddJournalPageViewModel : AddPublicationPageViewModel
    {
        #region UI Control Properties

        public new bool JournalPicked => true;
        public int ListHeight => 44 * PaperTitles.Count;
        public ImageSource ImageSourceClone { get; set; }
        public List<Recurrence> RecurenceTypes => Enum.GetValues(typeof(Recurrence)).Cast<Recurrence>().ToList();
        public List<ConferencePaper> AvailableConferencePapers { get; set; } = PublicationService.Instance.GetAllConferencePapers();
        public List<string> ConferencePapers { get; set; } = new List<string>();
        public ObservableCollection<StringWithPropertyChangedViewModel> PaperTitles { get; set; } = new ObservableCollection<StringWithPropertyChangedViewModel>();

        #endregion

        #region Command Properties

        public ICommand AddPaperCommand { get; private set; }
        public ICommand DeletePaperCommand { get; private set; }

        #endregion

        public AddJournalPageViewModel()
        {
            NewPublication = new JournalViewModel();
            AddPaperCommand = new Command(async () => await AddPaperAsync());
            DeletePaperCommand = new Command<StringWithPropertyChangedViewModel>(DeletePaper);
            PageTitle = string.Format(Constants.PageTitles.AddPublicationTitle.Value, NewPublication.Type);
        }

        public AddJournalPageViewModel(JournalViewModel journalViewModel)
            : this()
        {
            var clone = journalViewModel.CloneJson();
            NewPublication = clone;
            ConferencePapers = clone.ConferencePapers.ConvertAll(x => x.Id);
            PaperTitles = new ObservableCollection<StringWithPropertyChangedViewModel>(clone.ConferencePapers.ConvertAll(x => new StringWithPropertyChangedViewModel(x.ToString())).ToList());
            ImageSourceClone = journalViewModel.CoverImageSource.CloneJson();
            PageTitle = string.Format(Constants.PageTitles.UpdatePublicationTitle.Value, NewPublication.Title);
        }

        private async Task AddPaperAsync()
        {
            if (ConferencePapers.Count == AvailableConferencePapers.Count)
            {
                await PageService.Instance.DisplayAlertAsync(Constants.ValidatorStrings.StandardWarningMessage.Value, Constants.ValidatorStrings.NoPapersAvailableWarningMessage.Value, Constants.StandardStringConstants.OkString.Value);
                return;
            }

            var availablePapers = AvailableConferencePapers.Where(paper => !ConferencePapers.Contains(paper.Id)).ToList();
            var selectedPaperTitle = await PageService.Instance.DisplayActionSheetAsync(Constants.StandardStringConstants.ChoosePaperString.Value, Constants.StandardStringConstants.CancelString.Value, availablePapers.ConvertAll(x => x.ToString()).ToArray());

            if (selectedPaperTitle == Constants.StandardStringConstants.CancelString.Value) 
            {
                return;
            }

            var selectedPaper = AvailableConferencePapers.First(x => x.ToString().Equals(selectedPaperTitle));
            ConferencePapers.Add(selectedPaper.Id);
            PaperTitles.Add(new StringWithPropertyChangedViewModel { Text = selectedPaperTitle });
            OnPropertyChanged(nameof(ListHeight));
        }

        private void DeletePaper(StringWithPropertyChangedViewModel publicationToDelete)
        {
            var paperToDelete = AvailableConferencePapers.First(x => x.ToString().Equals(publicationToDelete.Text));
            ConferencePapers.Remove(paperToDelete.Id);
            PaperTitles.Remove(publicationToDelete);
            OnPropertyChanged(nameof(ListHeight));
        }

        protected override async Task SavePublicationAsync()
        {
            if(NewPublication.IsValid())
            {
                if (NewPublication.Id == null)
                {
                    await PopupNavigation.Instance.PushAsync(new CustomLoadingPopupPage(Constants.LoadingInfoStrings.AddJournalString.Value));

                    var newJournal = new Journal
                    {
                        Title = NewPublication.Title,
                        Author = NewPublication.Author,
                        Genre = NewPublication.Genre,
                        PublishedDate = UnixTimestampConverter.DateTimeToUnixTimestamp(NewPublication.PublishedDate),
                        CoverImageSource = NewPublication.CoverImageSource,
                        FirstPublishedDate = UnixTimestampConverter.DateTimeToUnixTimestamp(((JournalViewModel)NewPublication).FirstPublishedDate),
                        Recurrence = ((JournalViewModel)NewPublication).Recurrence.ToString().ToLower(),
                        ConferencePapers = ConferencePapers
                    };
                    var newId = await RestCrudOperationsService.Instance.AddNewPublicationAsync(newJournal);

                    if (newId != null)
                    {
                        var newJournalViewModel = PublicationModelToViewModelConverter.Convert(newJournal) as JournalViewModel;
                        newJournalViewModel.Id = newId;
                        MessagingCenter.Send(this, Constants.SubscriptionMessageString.JournalAdded.Value, newJournalViewModel);

                        await PageService.Instance.PopToRootAsync();
                    }

                    await PopupNavigation.Instance.PopAsync();
                }
                else
                {
                    if (!WasUpdated)
                    {
                        await PageService.Instance.PopToRootAsync();
                        return;
                    }
                    await PopupNavigation.Instance.PushAsync(new CustomLoadingPopupPage(Constants.LoadingInfoStrings.UpdateJournalString.Value));

                    var newJournal = new Journal
                    {
                        Id = NewPublication.Id,
                        Title = NewPublication.Title,
                        Author = NewPublication.Author,
                        Genre = NewPublication.Genre,
                        PublishedDate = UnixTimestampConverter.DateTimeToUnixTimestamp(NewPublication.PublishedDate),
                        CoverImageSource = NewPublication.CoverImageSource,
                        FirstPublishedDate = UnixTimestampConverter.DateTimeToUnixTimestamp(((JournalViewModel)NewPublication).FirstPublishedDate),
                        Recurrence = ((JournalViewModel)NewPublication).Recurrence.ToString().ToLower(),
                        ConferencePapers = ConferencePapers
                    };
                    if (newJournal.CoverImageSource == null)
                    {
                        newJournal.CoverImageSource = ImageSourceClone;
                    }

                    var wasUpdated = await RestCrudOperationsService.Instance.UpdatePublicationAsync(newJournal);
                    if (wasUpdated)
                    {
                        MessagingCenter.Send(this, Constants.SubscriptionMessageString.JournalUpdated.Value, PublicationModelToViewModelConverter.Convert(newJournal) as JournalViewModel);
                        await PageService.Instance.PopToRootAsync();
                    }

                    await PopupNavigation.Instance.PopAsync();
                }
            }
            else
            {
                await PageService.Instance.DisplayAlertAsync(Constants.ValidatorStrings.StandardErrorMessage.Value, Constants.ValidatorStrings.EmptyFieldsErrorMessage.Value, Constants.StandardStringConstants.OkString.Value);
            }
        }

        public override AddPublicationPageViewModel Reset()
        {
            return new AddJournalPageViewModel();
        }
    }
}
