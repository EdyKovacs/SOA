using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace BookStore.Model
{
    public class ConferencePaper : Publication
    {
        [JsonProperty(PropertyName = "abstract")]
        public string Abstract { get; set; }
        [JsonProperty(PropertyName = "firstConferenceName")]
        public string FirstConference { get; set; }
        [JsonProperty(PropertyName = "conferenceLocation")]
        public string ConferenceLocation { get; set; }
        [JsonProperty(PropertyName = "references")]
        public List<string> References { get; set; }

        public ConferencePaper(string id, string title, string author, string genre, long publishedDate, string abstractString, string firstConference, string conferenceLocation, List<String> references, ImageSource coverImageSource = null)
             : base(id, title, author, genre, publishedDate, coverImageSource)
        {
            Abstract = abstractString;
            FirstConference = firstConference;
            ConferenceLocation = conferenceLocation;
            References = references;
        }

        public ConferencePaper()
        {
        }

        public override string ToString()
        {
            return string.Format("{0} by {1}", Title, Author);
        }
    }
}
