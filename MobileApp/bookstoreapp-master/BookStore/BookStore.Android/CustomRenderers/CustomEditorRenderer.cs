using Android.Content;
using Android.Support.V4.Content;
using BookStore.Droid.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Editor), typeof(CustomEditorRenderer))]
namespace BookStore.Droid.CustomRenderers
{
    public class CustomEditorRenderer : EditorRenderer
    {
        public CustomEditorRenderer(Context context)
            : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
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
