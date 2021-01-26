using BookStore.Helpers;
using Xamarin.Forms;

namespace BookStore.ViewModel
{
    public class ImageCheckBoxViewModel : BaseViewModel
    {
        private bool _isActive;
        public bool IsActive
        {
            get => _isActive;
            set => SetValue(ref _isActive, value);
        }
        public string Text { get; set; }
        public Color DefaultTextColor { get; set; }
        public Color ActiveTextColor { get; set; }
        public ImageSource DefaultImage { get; set; }
        public ImageSource ActiveImage { get; set; }
        public Color DefaultBackgroundColor { get; set; }
        public Color ActiveBackgroundColor { get; set; }
        public Color DefaultFrameBorderColor { get; set; }
        public Color ActiveFrameBorderColor { get; set; }
        public AddPublicationPageViewModel Publication { get; set; }

        public ImageCheckBoxViewModel(string text, ImageSource defaultImage, ImageSource activeImage, AddPublicationPageViewModel publication)
        {
            Text = text;
            DefaultTextColor = Constants.Colors.StandardTextGray.Value;
            ActiveTextColor = Color.White;
            DefaultImage = defaultImage;
            ActiveImage = activeImage;
            DefaultBackgroundColor = Color.White;
            ActiveBackgroundColor = Constants.Colors.PrimaryBlue.Value;
            Publication = publication;
            DefaultFrameBorderColor = Constants.Colors.StandardTextGray.Value;
            ActiveFrameBorderColor = Constants.Colors.PrimaryBlue.Value;
        }
    }
}
