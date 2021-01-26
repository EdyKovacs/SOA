using System;
using BookStore.Helpers;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace BookStore.Model
{
    public abstract class Publication
    {
        [JsonProperty(PropertyName = "_id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
        [JsonProperty(PropertyName = "author")]
        public string Author { get; set; }
        [JsonProperty(PropertyName = "genre")]
        public string Genre { get; set; }
        [JsonProperty(PropertyName = "publishedDate")]
        public long PublishedDate { get; set; }
        public ImageSource CoverImageSource { get; set; }

        protected Publication(string id, string title, string author, string genre, long publishedDate, ImageSource coverImageSource = null)
        {
            if (String.IsNullOrWhiteSpace(title) || String.IsNullOrWhiteSpace(author))
            {
                throw new ArgumentNullException(Constants.ValidatorStrings.TitleOrAuthorValidationErrorMessage.Value);
            }

            Id = id;
            Title = title;
            Author = author;
            Genre = genre;
            PublishedDate = publishedDate;
            CoverImageSource = coverImageSource;
        }

        protected Publication()
        {
        }
    }
}
