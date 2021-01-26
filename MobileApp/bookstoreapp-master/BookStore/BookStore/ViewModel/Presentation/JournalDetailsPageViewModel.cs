using System.Threading.Tasks;
using BookStore.Service;
using BookStore.View;

namespace BookStore.ViewModel
{
    public class JournalDetailsPageViewModel : DetailsPageViewModel
    {
        public new bool JournalPicked => true;
        public int ListHeight => 18 * (Publication as JournalViewModel).ConferencePapers.Count;
        public bool ListEmpty => (Publication as JournalViewModel).ConferencePapers.Count == 0;

        public JournalDetailsPageViewModel(JournalViewModel journalViewModel)
        {
            Publication = journalViewModel;
        }

        protected async override Task EditPublicationAsync()
        {
            await PageService.Instance.PushAsync(new AddPublicationPage(new AddJournalPageViewModel(Publication as JournalViewModel)));
        }
    }
}
