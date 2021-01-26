using System.Threading.Tasks;
using BookStore.CustomViews;
using BookStore.Helpers;
using BookStore.Model;
using BookStore.Model.Helpers;
using BookStore.Service;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace BookStore.ViewModel
{
    public class AddBookPageViewModel : AddPublicationPageViewModel
    {
        public ImageSource ImageSourceClone { get; set; }

        public AddBookPageViewModel()
        {
            NewPublication = new BookViewModel();
            PageTitle = string.Format(Constants.PageTitles.AddPublicationTitle.Value, NewPublication.Type);
        }

        public AddBookPageViewModel(BookViewModel bookViewModel)
        {
            NewPublication = bookViewModel.CloneJson();
            ImageSourceClone = bookViewModel.CoverImageSource.CloneJson();
            PageTitle = string.Format(Constants.PageTitles.UpdatePublicationTitle.Value, NewPublication.Title);
        }

        protected override async Task SavePublicationAsync()
        {
            if (NewPublication.IsValid())
            {
                if (NewPublication.Id == null)
                {
                    await PopupNavigation.Instance.PushAsync(new CustomLoadingPopupPage(Constants.LoadingInfoStrings.AddBookString.Value));

                    var newBook = new Book
                    {
                        Title = NewPublication.Title,
                        Author = NewPublication.Author,
                        Genre = NewPublication.Genre,
                        PublishedDate = UnixTimestampConverter.DateTimeToUnixTimestamp(NewPublication.PublishedDate),
                        CoverImageSource = PublicationService.Instance.SetPublicationCover()
                    };
                    var newId = await RestCrudOperationsService.Instance.AddNewPublicationAsync(newBook);

                    if (newId != null)
                    {
                        var newBookViewModel = PublicationModelToViewModelConverter.Convert(newBook) as BookViewModel;
                        newBookViewModel.Id = newId;
                        MessagingCenter.Send(this, Constants.SubscriptionMessageString.BookAdded.Value, newBookViewModel);

                        await PageService.Instance.PopToRootAsync();
                    }

                    await PopupNavigation.Instance.PopAsync();
                }
                else
                {
                    if(!WasUpdated)
                    {
                        await PageService.Instance.PopToRootAsync();
                        return;
                    }
                    await PopupNavigation.Instance.PushAsync(new CustomLoadingPopupPage(Constants.LoadingInfoStrings.UpdateBookString.Value));

                    var newBook = new Book
                    {
                        Id = NewPublication.Id,
                        Title = NewPublication.Title,
                        Author = NewPublication.Author,
                        Genre = NewPublication.Genre,
                        PublishedDate = UnixTimestampConverter.DateTimeToUnixTimestamp(NewPublication.PublishedDate),
                        CoverImageSource = NewPublication.CoverImageSource
                    };
                    if (newBook.CoverImageSource == null)
                    {
                        newBook.CoverImageSource = ImageSourceClone;
                    }

                    var wasUpdated = await RestCrudOperationsService.Instance.UpdatePublicationAsync(newBook);
                    if(wasUpdated)
                    {
                        MessagingCenter.Send(this, Constants.SubscriptionMessageString.BookUpdated.Value, PublicationModelToViewModelConverter.Convert(newBook) as BookViewModel);
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
            return new AddBookPageViewModel();
        }
    }
}
