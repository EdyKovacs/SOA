using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BookStore.ViewModel
{
    public abstract class DetailsPageViewModel : BaseViewModel
    {
        public PublicationViewModel Publication { get; set; }
        public bool JournalPicked { get; }
        public bool PaperPicked { get; }
        public ICommand EditPublicationCommand { get; protected set; }

        public DetailsPageViewModel()
        {
            EditPublicationCommand = new Command(async () => await EditPublicationAsync());
        }

        protected abstract Task EditPublicationAsync();
    }
}
