using Android.App;
using Android.Content;
using Android.Widget;
using BookStore.Droid.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android.AppCompat;

[assembly: ExportRenderer(typeof(NavigationPage), typeof(CustomToolbarTitleRenderer))]
namespace BookStore.Droid.CustomRenderers
{
    public class CustomToolbarTitleRenderer : NavigationPageRenderer
    {
        public CustomToolbarTitleRenderer(Context context)
            :base(context)
        {
        }

        //https://stackoverflow.com/a/47726843
        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);
            var activity = this.Context as Activity;
            var toolbar = activity.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);

            if (toolbar != null)
            {
                for (int index = 0; index < toolbar.ChildCount; index++)
                {
                    if (toolbar.GetChildAt(index) is TextView)
                    {
                        var title = toolbar.GetChildAt(index) as TextView;
                        float toolbarCenter = toolbar.MeasuredWidth / 2;
                        float titleCenter = title.MeasuredWidth / 2;
                        title.SetX(toolbarCenter - titleCenter);
                    }
                }
            }
        }
    }
}