namespace BookStore.ViewModel
{
    public class StringWithPropertyChangedViewModel : BaseViewModel
    {
        public string Text { get; set; }

        public StringWithPropertyChangedViewModel(string text)
        {
            Text = text;
        }

        public StringWithPropertyChangedViewModel()
        {
        }
    }
}