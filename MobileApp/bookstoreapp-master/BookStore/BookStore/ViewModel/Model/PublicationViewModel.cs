using System;
using BookStore.Helpers;
using BookStore.Model;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace BookStore.ViewModel
{
    public abstract class PublicationViewModel : BaseViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public DateTime PublishedDate { get; set; }
        [JsonIgnore]
        public ImageSource CoverImageSource { get; set; }
        public abstract Color CorrespondingColor { get; }
        public abstract string Type { get; }

        public PublicationViewModel()
        {
            Id = null;
        }

        public PublicationViewModel(Publication publication)
        {
            if (String.IsNullOrWhiteSpace(publication.Title) || String.IsNullOrWhiteSpace(publication.Author))
            {
                throw new ArgumentNullException(Constants.ValidatorStrings.TitleOrAuthorValidationErrorMessage.Value);
            }

            Id = publication.Id;
            Title = publication.Title;
            Author = publication.Author;
            Genre = publication.Genre;
            PublishedDate = UnixTimestampConverter.UnixTimestampToDateTime(publication.PublishedDate);
            CoverImageSource = publication.CoverImageSource;
        }

        public virtual bool IsValid()
        {
            if (string.IsNullOrWhiteSpace(Title) || string.IsNullOrWhiteSpace(Author) || string.IsNullOrWhiteSpace(Genre))
            {
                return false;
            }

            return true;
        }
    }
}
