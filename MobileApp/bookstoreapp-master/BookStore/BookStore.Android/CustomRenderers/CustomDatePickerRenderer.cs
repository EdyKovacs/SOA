using Android.Content;
using Android.Support.V4.Content;
using BookStore.Droid.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(DatePicker), typeof(CustomDatePickerRenderer))]
namespace BookStore.Droid.CustomRenderers
{
    public class CustomDatePickerRenderer : DatePickerRenderer
    {
        public CustomDatePickerRenderer(Context context)
            :base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
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
