using System.Threading.Tasks;
using BookStore.Service;
using BookStore.View;

namespace BookStore.ViewModel
{
    public class BookDetailsPageViewModel : DetailsPageViewModel
    {
        public bool ListEmpty => false;

        public BookDetailsPageViewModel(BookViewModel bookViewModel)
        {
            Publication = bookViewModel;
        }

        protected async override Task EditPublicationAsync()
        {
            await PageService.Instance.PushAsync(new AddPublicationPage(new AddBookPageViewModel(Publication as BookViewModel)));
        }
    }
}
