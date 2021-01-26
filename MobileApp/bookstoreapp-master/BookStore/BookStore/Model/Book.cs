using Xamarin.Forms;

namespace BookStore.Model
{
    public class Book : Publication
    {
        public Book(string id, string title, string author, string genre, long publishedDate, ImageSource coverImageSource = null)
            : base(id, title, author, genre, publishedDate, coverImageSource)
        {
        }

        public Book()
        {
        }
    }
}
