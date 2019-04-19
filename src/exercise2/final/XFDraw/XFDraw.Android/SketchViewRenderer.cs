using XFDraw;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XFDraw.Droid;
using System.ComponentModel;
using Android.Content;

[assembly: ExportRenderer(typeof(SketchView), typeof(SketchViewRenderer))]
namespace XFDraw
{
    class SketchViewRenderer : ViewRenderer<SketchView, PaintView>
    {
        Context context;

        public SketchViewRenderer(Context context) : base(context)
        {
            this.context = context;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<SketchView> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                var paintView = new PaintView(context);
                paintView.SetInkColor(Element.InkColor.ToAndroid());
                SetNativeControl(paintView);
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == SketchView.InkColorProperty.PropertyName)
            {
                Control.SetInkColor(Element.InkColor.ToAndroid());
            }
        }
    }
}