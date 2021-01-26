using BookStore.Helpers;
using Xamarin.Forms;

namespace BookStore.CustomViews
{
    public class ImageEntry : Entry
    {
        #region Bindable Properties

        public static readonly BindableProperty ImageProperty =
            BindableProperty.Create(nameof(Image), typeof(string), typeof(ImageEntry), string.Empty);

        public static readonly BindableProperty ImageHeightProperty =
            BindableProperty.Create(nameof(ImageHeight), typeof(int), typeof(ImageEntry), Constants.DefaultValues.EntryImageHeight.Value);

        public static readonly BindableProperty ImageWidthProperty =
            BindableProperty.Create(nameof(ImageWidth), typeof(int), typeof(ImageEntry), Constants.DefaultValues.EntryImageWidth.Value);

        #endregion

        #region Properties

        public string Image
        {
            get { return (string)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        public int ImageWidth
        {
            get { return (int)GetValue(ImageWidthProperty); }
            set { SetValue(ImageWidthProperty, value); }
        }

        public int ImageHeight
        {
            get { return (int)GetValue(ImageHeightProperty); }
            set { SetValue(ImageHeightProperty, value); }
        }

        #endregion

        public ImageEntry()
        {
            HeightRequest = Constants.DefaultValues.EntryHeight.Value;
        }
    }
}
