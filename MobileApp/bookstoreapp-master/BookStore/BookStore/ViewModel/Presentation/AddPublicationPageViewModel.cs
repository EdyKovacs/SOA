using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BookStore.ViewModel
{
    public abstract class AddPublicationPageViewModel : BaseViewModel
    {
        public PublicationViewModel NewPublication { get; set; }
        public bool WasUpdated { get; set; }
        public bool JournalPicked { get; }
        public bool PaperPicked { get; }
        public string PageTitle { get; set; }
        public ICommand SavePublicationCommand { get; protected set; }

        public AddPublicationPageViewModel()
        {
            SavePublicationCommand = new Command(async () => await SavePublicationAsync());
        }

        protected abstract Task SavePublicationAsync();
        public abstract AddPublicationPageViewModel Reset();
    }
}
