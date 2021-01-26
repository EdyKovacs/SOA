using Android.Content;
using Android.Support.V4.Content;
using BookStore.Droid.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Entry), typeof(CustomEntryRenderer))]
namespace BookStore.Droid.CustomRenderers
{
    public class CustomEntryRenderer : EntryRenderer
    {
        public CustomEntryRenderer(Context context)
            :base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.Background = ContextCompat.GetDrawable(this.Context, Resource.Drawable.RoundedCornerEntry);
                Control.Gravity = Android.Views.GravityFlags.CenterVertical;
                Control.SetPadding(24, 0, 0, 0);
            }
        }
    }
}
