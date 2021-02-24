using System;
using Android.Content;
using Android.Graphics.Drawables;
using WeatherApp.Droid;
using WeatherApp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
[assembly: ExportRenderer(typeof(RoundedEntry), typeof(RoundenEntryRenderer))]
namespace WeatherApp.Droid
{
    public class RoundenEntryRenderer : EntryRenderer
    {
        public RoundenEntryRenderer(Context context ) : base(context)
        {

        }
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null) {

                var gradientDrawable = new GradientDrawable();
                gradientDrawable.SetCornerRadius(60f);
                gradientDrawable.SetStroke(5,Android.Graphics.Color.Purple);
                gradientDrawable.SetColor(Android.Graphics.Color.White);
                Control.SetBackground(gradientDrawable);
                Control.SetPadding(50,Control.PaddingTop,Control.PaddingRight,Control.PaddingBottom);
            }
        }
    }
}
