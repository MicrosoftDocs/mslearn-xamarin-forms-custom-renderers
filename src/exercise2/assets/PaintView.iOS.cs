using CoreGraphics;
using Foundation;
using System;
using UIKit;

namespace XFDraw.iOS
{
    class PaintView : UIImageView
    {
        UIColor inkColor = UIColor.Green;

        public event EventHandler LineDrawn;

        public PaintView()
        {
            MultipleTouchEnabled = true;
            UserInteractionEnabled = true;
        }

        public void SetInkColor (UIColor color)
        {
            inkColor = color;
        }

        public void Clear ()
        {
            Image?.Dispose();
            Image = new UIImage();
        }

        void DrawLine(CGPoint pt1, CGPoint pt2, UIColor color)
        {
            UIGraphics.BeginImageContext(Frame.Size);

            using (var g = UIGraphics.GetCurrentContext())
            {
                Layer.RenderInContext(g);

                var path = new CGPath();

                path.AddLines(new CGPoint[] { pt1, pt2 });

                g.SetLineWidth(3);
                color.SetStroke();

                g.AddPath(path);
                g.DrawPath(CGPathDrawingMode.Stroke);

                Image = UIGraphics.GetImageFromCurrentImageContext();
            }

            UIGraphics.EndImageContext();

            LineDrawn?.Invoke(this, EventArgs.Empty);
        }

        public override void TouchesMoved(NSSet touches, UIEvent evt)
        {
            foreach (UITouch touch in touches)
            {
                DrawLine(touch.PreviousLocationInView(this), touch.LocationInView(this), inkColor);
            }
        }
    }
}