using Xamarin.Forms;

namespace XFDraw
{
    public class SketchView : View
    {
        public static readonly BindableProperty InkColorProperty = BindableProperty.Create("InkColor", typeof(Color), typeof(SketchView), Color.Blue);

        public Color InkColor
        {
            get { return (Color)GetValue(InkColorProperty); }
            set { SetValue(InkColorProperty, value); }
        }
    }
}
