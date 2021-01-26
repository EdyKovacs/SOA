using Android.Content;
using Android.Support.V4.Content;
using BookStore.Droid.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Picker), typeof(CustomPickerRenderer))]
namespace BookStore.Droid.CustomRenderers
{
    public class CustomPickerRenderer : PickerRenderer
    {
        public CustomPickerRenderer(Context context)
            : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.TextSize = 14;
                Control.Background = ContextCompat.GetDrawable(this.Context, Resource.Drawable.RoundedCornerEntry);
                Control.SetPadding(10, 10, 10, 3);
            }
        }
    }
}
