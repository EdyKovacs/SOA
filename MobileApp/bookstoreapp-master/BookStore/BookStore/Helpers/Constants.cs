using System;
using Xamarin.Forms;

namespace BookStore.Helpers
{
    public static class Constants
    {
        public static class APIStrings
        {
            public static readonly Lazy<string> BaseUrl = new Lazy<string>(() => "http://10.0.2.2:3000");
            public static readonly Lazy<string> SignInRouteString = new Lazy<string>(() => "/auth");
            public static readonly Lazy<string> SignUpRouteString = new Lazy<string>(() => "/signup");
            public static readonly Lazy<string> SignOutRouteString = new Lazy<string>(() => "/signout");
            public static readonly Lazy<string> BookRouteString = new Lazy<string>(() => "/book");
            public static readonly Lazy<string> JournalRouteString = new Lazy<string>(() => "/journal");
            public static readonly Lazy<string> PaperRouteString = new Lazy<string>(() => "/conferencePaper");
            public static readonly Lazy<string> GetPublicationsRouteString = new Lazy<string>(() => "/publishables");
            public static readonly Lazy<string> TokenHeaderKeyString = new Lazy<string>(() => "x-auth");
            public static readonly Lazy<string> BooksKeyString = new Lazy<string>(() => "books");
            public static readonly Lazy<string> JournalsKeyString = new Lazy<string>(() => "journals");
            public static readonly Lazy<string> ConferencePapersKeyString = new Lazy<string>(() => "conferencePapers");
            public static readonly Lazy<string> CurrentUserMailKey = new Lazy<string>(() => "CurrentMail");
        }

        public static class Colors
        {
            public static readonly Lazy<Color> PrimaryBlue = new Lazy<Color>(() => (Color)Application.Current.Resources["PrimaryBlue"]);
            public static readonly Lazy<Color> StandardTextGray = new Lazy<Color>(() => (Color)Application.Current.Resources["StandardTextGray"]);
            public static readonly Lazy<Color> StandardTextDarkGray = new Lazy<Color>(() => (Color)Application.Current.Resources["StandardTextDarkGray"]);
            public static readonly Lazy<Color> GenreTextGray = new Lazy<Color>(() => (Color)Application.Current.Resources["GenreTextGray"]);
            public static readonly Lazy<Color> GenreBoxGray = new Lazy<Color>(() => (Color)Application.Current.Resources["GenreBoxGray"]);
        }

        public static class ValidatorStrings
        {
            public static readonly Lazy<string> StandardErrorMessage = new Lazy<string>(() => "Error");
            public static readonly Lazy<string> StandardWarningMessage = new Lazy<string>(() => "Warning");
            public static readonly Lazy<string> TitleOrAuthorValidationErrorMessage = new Lazy<string>(() => "Title/Author fields cannot be empty.");
            public static readonly Lazy<string> EmailOrPasswordValidationErrorMessage = new Lazy<string>(() => "Email/Password fields cannot be empty.");
            public static readonly Lazy<string> NoCameraAvailableErrorMessage = new Lazy<string>(() => "No camera available.");
            public static readonly Lazy<string> NotSupportedErrorMessage = new Lazy<string>(() => "Not supported.");
            public static readonly Lazy<string> PermissionsErrorMessage = new Lazy<string>(() => "Permissions needed to perform this action.");
            public static readonly Lazy<string> PasswordMatchErrorMessage = new Lazy<string>(() => "Passwords have to match!");
            public static readonly Lazy<string> InvalidCredentialsErrorMessage = new Lazy<string>(() => "Invalid credentials.");
            public static readonly Lazy<string> SomethingWrongErrorMessage = new Lazy<string>(() => "Something went wrong.");
            public static readonly Lazy<string> InvalidEmailErrorMessage = new Lazy<string>(() => "Invalid email.");
            public static readonly Lazy<string> EmptyFieldsErrorMessage = new Lazy<string>(() => "You left some fields empty.");
            public static readonly Lazy<string> NoPapersAvailableWarningMessage = new Lazy<string>(() => "There are no more conference papers available.");
            public static readonly Lazy<string> EmptyReferenceErrorMessage = new Lazy<string>(() => "Reference cannot be empty.");
        }

        public static class IconSources
        {
            public static readonly Lazy<ImageSource> BookIconSource = new Lazy<ImageSource>(() => ImageSource.FromFile("book.png"));
            public static readonly Lazy<ImageSource> ActiveBookIconSource = new Lazy<ImageSource>(() => ImageSource.FromFile("book_active.png"));
            public static readonly Lazy<ImageSource> JournalIconSource = new Lazy<ImageSource>(() => ImageSource.FromFile("journal.png"));
            public static readonly Lazy<ImageSource> ActiveJournalIconSource = new Lazy<ImageSource>(() => ImageSource.FromFile("journal_active.png"));
            public static readonly Lazy<ImageSource> PaperIconSource = new Lazy<ImageSource>(() => ImageSource.FromFile("paper.png"));
            public static readonly Lazy<ImageSource> ActivePaperIconSource = new Lazy<ImageSource>(() => ImageSource.FromFile("paper_active.png"));
            public static readonly Lazy<ImageSource> UploadIconSource = new Lazy<ImageSource>(() => ImageSource.FromFile("upload_icon.png"));
            public static readonly Lazy<ImageSource> CoverIconSource = new Lazy<ImageSource>(() => ImageSource.FromFile("cover_icon.png"));
            public static readonly Lazy<ImageSource> RemoveIconSource = new Lazy<ImageSource>(() => ImageSource.FromFile("remove_icon.png"));
        }

        public static class PublicationTypeStrings
        {
            public static readonly Lazy<string> Book = new Lazy<string>(() => "Book");
            public static readonly Lazy<string> Journal = new Lazy<string>(() => "Journal");
            public static readonly Lazy<string> ConferencePaper = new Lazy<string>(() => "Paper");
        }

        public static class StandardStringConstants
        {
            public static readonly Lazy<string> OkString = new Lazy<string>(() => "Ok");
            public static readonly Lazy<string> CancelString = new Lazy<string>(() => "Cancel");
            public static readonly Lazy<string> DeleteString = new Lazy<string>(() => "Delete");
            public static readonly Lazy<string> TakePhotoString = new Lazy<string>(() => "Take Photo");
            public static readonly Lazy<string> ChoosePhotoString = new Lazy<string>(() => "Choose Photo");
            public static readonly Lazy<string> UploadImageString = new Lazy<string>(() => "Upload Image");
            public static readonly Lazy<string> RemoveImageString = new Lazy<string>(() => "Remove Image");
            public static readonly Lazy<string> ChoosePaperString = new Lazy<string>(() => "Choose conference paper");
            public static readonly Lazy<string> AreYouSureString = new Lazy<string>(() => "Are you sure?");
            public static readonly Lazy<string> LoginString = new Lazy<string>(() => "Login");
            public static readonly Lazy<string> RegisterString = new Lazy<string>(() => "Register");
            public static readonly Lazy<string> NeedsAccountString = new Lazy<string>(() => "Already have an account? ");
            public static readonly Lazy<string> HasAccountString = new Lazy<string>(() => "Don't have an account? ");
            public static readonly Lazy<string> DeletePublicationWarningString = new Lazy<string>(() => "Doing this will permanently delete the publicaiton.");
        }

        public static class SubscriptionMessageString
        {
            public static readonly Lazy<string> BookAdded = new Lazy<string>(() => "BookAdded");
            public static readonly Lazy<string> JournalAdded = new Lazy<string>(() => "JournalAdded");
            public static readonly Lazy<string> PaperAdded = new Lazy<string>(() => "PaperAdded");
            public static readonly Lazy<string> BookUpdated = new Lazy<string>(() => "BookUpdated");
            public static readonly Lazy<string> JournalUpdated = new Lazy<string>(() => "JournalUpdated");
            public static readonly Lazy<string> PaperUpdated = new Lazy<string>(() => "PaperUpdated");
        }

        public static class LoadingInfoStrings
        {
            public static readonly Lazy<string> LoadingString = new Lazy<string>(() => "Loading");
            public static readonly Lazy<string> LoadDataString = new Lazy<string>(() => "Loading data");
            public static readonly Lazy<string> DeletePublicationString = new Lazy<string>(() => "Deleting");
            public static readonly Lazy<string> CreateAccountString = new Lazy<string>(() => "Creating account");
            public static readonly Lazy<string> AccountCreatedString = new Lazy<string>(() => "Account created!\nSigning in");
            public static readonly Lazy<string> LogOutString = new Lazy<string>(() => "Logging out");
            public static readonly Lazy<string> LogInString = new Lazy<string>(() => "Logging in");
            public static readonly Lazy<string> UpdateBookString = new Lazy<string>(() => "Updating book");
            public static readonly Lazy<string> AddBookString = new Lazy<string>(() => "Adding book");
            public static readonly Lazy<string> UpdateJournalString = new Lazy<string>(() => "Updating journal");
            public static readonly Lazy<string> AddJournalString = new Lazy<string>(() => "Adding journal");
            public static readonly Lazy<string> UpdatePaperString = new Lazy<string>(() => "Updating paper");
            public static readonly Lazy<string> AddPaperString = new Lazy<string>(() => "Adding paper");
        }

        public static class DefaultValues
        {
            public static readonly Lazy<int> EntryImageHeight = new Lazy<int>(() => 40);
            public static readonly Lazy<int> EntryImageWidth = new Lazy<int>(() => 40);
            public static readonly Lazy<int> EntryHeight = new Lazy<int>(() => 44);
        }

        public static class PageTitles
        {
            public static readonly Lazy<string> AddPublicationTitle = new Lazy<string>(() => "Add new {0}");
            public static readonly Lazy<string> UpdatePublicationTitle = new Lazy<string>(() => "Update {0}");
        }
    }
}
