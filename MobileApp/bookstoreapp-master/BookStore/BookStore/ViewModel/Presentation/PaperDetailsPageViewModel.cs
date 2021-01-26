using System.Threading.Tasks;
using BookStore.Service;
using BookStore.View;

namespace BookStore.ViewModel
{
    public class PaperDetailsPageViewModel : DetailsPageViewModel
    {
        public new bool PaperPicked => true;
        public int ListHeight => 17 * (Publication as ConferencePaperViewModel).References.Count;
        public bool ListEmpty => (Publication as ConferencePaperViewModel).References.Count == 0;

        public PaperDetailsPageViewModel(ConferencePaperViewModel paperViewModel)
        {
            Publication = paperViewModel;
        }

        protected async override Task EditPublicationAsync()
        {
            await PageService.Instance.PushAsync(new AddPublicationPage(new AddPaperPageViewModel(Publication as ConferencePaperViewModel)));
        }
    }
}
