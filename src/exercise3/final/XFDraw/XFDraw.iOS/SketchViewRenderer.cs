using System.ComponentModel;
using XFDraw;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XFDraw.iOS;

[assembly: ExportRenderer(typeof(SketchView), typeof(SketchViewRenderer))]
namespace XFDraw.iOS
{
    class SketchViewRenderer : ViewRenderer<SketchView, PaintView>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<SketchView> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                var paintView = new PaintView();
                paintView.SetInkColor(this.Element.InkColor.ToUIColor());
                SetNativeControl(paintView);

                MessagingCenter.Subscribe<SketchView>(this, "Clear", OnMessageClear);
                paintView.LineDrawn += PaintViewLineDrawn;
            }
        }
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == SketchView.InkColorProperty.PropertyName)
            {
                Control.SetInkColor(Element.InkColor.ToUIColor());
            }
        }

        void OnMessageClear(SketchView sender)
        {
            if (sender == Element)
                Control.Clear();
        }

        private void PaintViewLineDrawn(object sender, System.EventArgs e)
        {
            var sketchCon = (ISketchController)Element;

            if (sketchCon == null)
                return;

            sketchCon.SendSketchUpdated();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                MessagingCenter.Unsubscribe<SketchView>(this, "Clear");
            }

            base.Dispose(disposing);
        }
    }
}