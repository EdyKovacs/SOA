using System.Drawing;
using BookStore.CustomViews;
using BookStore.iOS.CustomRenderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ImageEntry), typeof(ImageEntryRenderer))]
namespace BookStore.iOS.CustomRenderers
{
    public class ImageEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || e.NewElement == null)
            {
                return;
            }

            var element = (ImageEntry)this.Element;
            var textField = this.Control;

            if (!string.IsNullOrEmpty(element.Image))
            {
                textField.LeftViewMode = UITextFieldViewMode.Always;
                textField.LeftView = GetImageView(element.Image, element.ImageHeight, element.ImageWidth);
            }
        }

        private UIView GetImageView(string imagePath, int height, int width)
        {
            var uiImageView = new UIImageView(UIImage.FromBundle(imagePath))
            {
                Frame = new RectangleF(24, 0, width, height)
            };
            UIView objLeftView = new UIView(new System.Drawing.Rectangle(0, 0, 58, height));
            objLeftView.AddSubview(uiImageView);

            return objLeftView;
        }
    }
}
