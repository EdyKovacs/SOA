using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using BookStore.Helpers;
using BookStore.Model;
using BookStore.Service;
using Xamarin.Forms;
using BookStore.Model.Helpers;
using BookStore.CustomViews;
using Rg.Plugins.Popup.Services;

namespace BookStore.ViewModel
{
    public class AddPaperPageViewModel : AddPublicationPageViewModel
    {
        #region UI Control Properties

        public new bool PaperPicked => true;
        public int ListHeight => 48 * References.Count;
        public ImageSource ImageSourceClone { get; set; }
        public StringWithPropertyChangedViewModel NewReference { get; set; } = new StringWithPropertyChangedViewModel(string.Empty);
        public ObservableCollection<StringWithPropertyChangedViewModel> References { get; set; } = new ObservableCollection<StringWithPropertyChangedViewModel>();

        #endregion

        #region Command Properties

        public ICommand AddReferenceCommand { get; private set; }
        public ICommand DeleteReferenceCommand { get; private set; }

        #endregion

        public AddPaperPageViewModel()
        {
            NewPublication = new ConferencePaperViewModel();
            AddReferenceCommand = new Command(async () => await AddReferenceAsync());
            DeleteReferenceCommand = new Command<StringWithPropertyChangedViewModel>(DeleteReference);
            PageTitle = string.Format(Constants.PageTitles.AddPublicationTitle.Value, NewPublication.Type);
        }

        public AddPaperPageViewModel(ConferencePaperViewModel paperViewModel)
            :this()
        {
            var clone = paperViewModel.CloneJson();
            NewPublication = clone;
            References = new ObservableCollection<StringWithPropertyChangedViewModel>(paperViewModel.References.ToList().ConvertAll(x => new StringWithPropertyChangedViewModel(x)));
            ImageSourceClone = paperViewModel.CoverImageSource.CloneJson();
            PageTitle = string.Format(Constants.PageTitles.UpdatePublicationTitle.Value, NewPublication.Title);
            WasUpdated = false;
        }

        public async Task AddReferenceAsync()
        {
            if(string.IsNullOrWhiteSpace(NewReference.Text))
            {
                await PageService.Instance.DisplayAlertAsync(Constants.ValidatorStrings.StandardErrorMessage.Value, Constants.ValidatorStrings.EmptyReferenceErrorMessage.Value, Constants.StandardStringConstants.OkString.Value);
                return;
            }

            References.Add(new StringWithPropertyChangedViewModel(NewReference.Text));
            NewReference = new StringWithPropertyChangedViewModel(string.Empty);
            OnPropertyChanged(nameof(NewReference));
            OnPropertyChanged(nameof(ListHeight));
        }

        void DeleteReference(StringWithPropertyChangedViewModel referenceToDelete)
        {
            References.Remove(referenceToDelete);
            OnPropertyChanged(nameof(ListHeight));
        }

        protected override async Task SavePublicationAsync()
        {
            if (NewPublication.IsValid())
            {
                if (NewPublication.Id == null)
                {
                    await PopupNavigation.Instance.PushAsync(new CustomLoadingPopupPage(Constants.LoadingInfoStrings.AddPaperString.Value));

                    var newPaper = new ConferencePaper
                    {
                        Title = NewPublication.Title,
                        Author = NewPublication.Author,
                        Genre = NewPublication.Genre,
                        PublishedDate = UnixTimestampConverter.DateTimeToUnixTimestamp(NewPublication.PublishedDate),
                        CoverImageSource = NewPublication.CoverImageSource,
                        Abstract = ((ConferencePaperViewModel)NewPublication).Abstract,
                        FirstConference = ((ConferencePaperViewModel)NewPublication).FirstConference,
                        ConferenceLocation = ((ConferencePaperViewModel)NewPublication).ConferenceLocation,
                        References = new List<string>(References.ToList().ConvertAll(i => i.Text))
                    };
                    var newId = await RestCrudOperationsService.Instance.AddNewPublicationAsync(newPaper);

                    if (newId != null)
                    {
                        var newPaperViewModel = PublicationModelToViewModelConverter.Convert(newPaper) as ConferencePaperViewModel;
                        newPaperViewModel.Id = newId;
                        MessagingCenter.Send(this, Constants.SubscriptionMessageString.PaperAdded.Value, newPaperViewModel);

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
                    await PopupNavigation.Instance.PushAsync(new CustomLoadingPopupPage(Constants.LoadingInfoStrings.UpdatePaperString.Value));

                    var newPaper = new ConferencePaper
                    {
                        Id = NewPublication.Id,
                        Title = NewPublication.Title,
                        Author = NewPublication.Author,
                        Genre = NewPublication.Genre,
                        PublishedDate = UnixTimestampConverter.DateTimeToUnixTimestamp(NewPublication.PublishedDate),
                        CoverImageSource = NewPublication.CoverImageSource,
                        Abstract = ((ConferencePaperViewModel)NewPublication).Abstract,
                        FirstConference = ((ConferencePaperViewModel)NewPublication).FirstConference,
                        ConferenceLocation = ((ConferencePaperViewModel)NewPublication).ConferenceLocation,
                        References = new List<string>(References.ToList().ConvertAll(i => i.Text))
                    };
                    if (newPaper.CoverImageSource == null)
                    {
                        newPaper.CoverImageSource = ImageSourceClone;
                    }

                    var wasUpdated = await RestCrudOperationsService.Instance.UpdatePublicationAsync(newPaper);
                    if(wasUpdated)
                    {
                        MessagingCenter.Send(this, Constants.SubscriptionMessageString.PaperUpdated.Value, PublicationModelToViewModelConverter.Convert(newPaper) as ConferencePaperViewModel);
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
            return new AddPaperPageViewModel();
        }
    }
}
