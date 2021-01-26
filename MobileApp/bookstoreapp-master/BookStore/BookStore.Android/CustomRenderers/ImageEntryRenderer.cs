using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Support.V4.Content;
using BookStore.CustomViews;
using BookStore.Droid.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ImageEntry), typeof(ImageEntryRenderer))]
namespace BookStore.Droid.CustomRenderers
{
    public class ImageEntryRenderer : EntryRenderer
    {
        ImageEntry element;

        public ImageEntryRenderer(Context context)
            : base(context)
        {  
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || e.NewElement == null)
            {
                return;
            }

            element = (ImageEntry)Element;
            var editText = Control;

            if (!string.IsNullOrEmpty(element.Image))
            {
                editText.SetCompoundDrawablesWithIntrinsicBounds(GetDrawable(element.Image), null, null, null);
                editText.CompoundDrawablePadding = 24 * 4;
            }
            editText.SetPadding(24 * 4, 0, 0, 0);
            editText.Gravity = Android.Views.GravityFlags.CenterVertical;

            var shape = new ShapeDrawable(new Android.Graphics.Drawables.Shapes.RectShape());
            shape.Paint.Color = Xamarin.Forms.Color.Black.ToAndroid();
            shape.Paint.SetStyle(Paint.Style.Stroke);
            editText.Background = shape;

            GradientDrawable gd = new GradientDrawable();
            gd.SetColor(Android.Graphics.Color.White);
            gd.SetCornerRadius(30);
            gd.SetStroke(2, Android.Graphics.Color.LightGray);
            editText.SetBackground(gd);
        }

        private BitmapDrawable GetDrawable(string imageEntryImage)
        {
            int resID = Resources.GetIdentifier(imageEntryImage, "drawable", Context.PackageName);
            var drawable = ContextCompat.GetDrawable(Context, resID);
            var bitmap = ((BitmapDrawable)drawable).Bitmap;

            return new BitmapDrawable(Resources, Bitmap.CreateScaledBitmap(bitmap, element.ImageWidth * 4, element.ImageHeight * 4, true));
        }
    }
}
