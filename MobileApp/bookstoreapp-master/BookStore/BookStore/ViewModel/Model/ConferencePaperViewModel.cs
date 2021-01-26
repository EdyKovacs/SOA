using System.Collections.Generic;
using BookStore.Helpers;
using BookStore.Model;
using Xamarin.Forms;

namespace BookStore.ViewModel
{
    public class ConferencePaperViewModel : PublicationViewModel
    {
        public string Abstract { get; set; }
        public string FirstConference { get; set; }
        public string ConferenceLocation { get; set; }
        public List<string> References { get; set; }
        public override Color CorrespondingColor => Color.OrangeRed;
        public override string Type => Constants.PublicationTypeStrings.ConferencePaper.Value;

        public ConferencePaperViewModel(Publication publication)
            : base(publication)
        {
        }

        public ConferencePaperViewModel(ConferencePaper conferencePaper)
            :base(conferencePaper)
        {
            Abstract = conferencePaper.Abstract;
            FirstConference = conferencePaper.FirstConference;
            ConferenceLocation = conferencePaper.ConferenceLocation;
            References = conferencePaper.References;
        }

        public ConferencePaperViewModel()
        {
        }

        public override bool IsValid()
        {
            if (string.IsNullOrWhiteSpace(Title) || string.IsNullOrWhiteSpace(Author) || string.IsNullOrWhiteSpace(Genre) || string.IsNullOrWhiteSpace(Abstract) || string.IsNullOrWhiteSpace(FirstConference) || string.IsNullOrWhiteSpace(ConferenceLocation))
            {
                return false;
            }

            return true;
        }

        public override string ToString()
        {
            return string.Format("{0} by {1}", Title, Author);
        }
    }
}
