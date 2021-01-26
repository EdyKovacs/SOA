using System;
using System.Collections.Generic;
using BookStore.Helpers;
using BookStore.Model;
using Xamarin.Forms;
using static BookStore.Model.Enums;

namespace BookStore.ViewModel
{
    public class JournalViewModel : PublicationViewModel
    {
        public DateTime FirstPublishedDate { get; set; }
        public Recurrence Recurrence { get; set; }
        public List<string> ConferencePaperIds { get; set; } = new List<string>();
        public List<ConferencePaperViewModel> ConferencePapers { get; set; } = new List<ConferencePaperViewModel>();
        public override Color CorrespondingColor => Color.GreenYellow;
        public override string Type => Constants.PublicationTypeStrings.Journal.Value;

        public JournalViewModel(Publication publication)
            : base(publication)
        {
        }

        public JournalViewModel()
        {
        }
    }
}
