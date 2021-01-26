using System.Collections.Generic;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace BookStore.Model
{
    public class Journal : Publication
    {
        [JsonProperty(PropertyName = "firstPublished")]
        public long FirstPublishedDate { get; set; }
        [JsonProperty(PropertyName = "recurrence")]
        public string Recurrence { get; set; }
        [JsonProperty(PropertyName = "conferencePapers")]
        public List<string> ConferencePapers { get; set; } = new List<string>();

        public Journal(string id, string title, string author, string genre, long publishedDate, long firstPublishedDate, string recurrence, List<string> conferencePapers, ImageSource coverImageSource = null)
            : base(id, title, author, genre, publishedDate, coverImageSource)
        {
            FirstPublishedDate = FirstPublishedDate;
            Recurrence = recurrence;
            ConferencePapers = conferencePapers;
        }

        public Journal()
        {
        }
    }
}
