using BookStore.Helpers;
using BookStore.Model;
using Xamarin.Forms;

namespace BookStore.ViewModel
{
    public class BookViewModel : PublicationViewModel
    {
        public override Color CorrespondingColor => Color.HotPink;
        public override string Type => Constants.PublicationTypeStrings.Book.Value;

        public BookViewModel(Publication publication)
            : base(publication)
        {
        }

        public BookViewModel()
        {
        }
    }
}
