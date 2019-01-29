using System.Collections.Generic;
using System.Windows.Media;
using System.Windows;
using System;

namespace NewPaint.Figures
{
    [Serializable]
    public class Ellipse : Figure
    {
        public bool IsFilled
        {
            get;
            set;
        } = false;

        public Brush FillColor
        {
            get => br;
            set => br = value;
        }


        public Ellipse()
        {

        }

        public Ellipse(Brush brush, Pen pen, Point point0, Point point1, int ZIndex, bool isFilled) : base()
        {
            points = new List<Point>() { point0, point1 };
            br = brush;
            drawPen = pen;
            IsFilled = isFilled;
            this.ZIndex = ZIndex;
        }

        public override void Draw(DrawingContext drawingContext)
        {
            var size = Point.Subtract(points[1], points[0]);
            if (IsFilled)
            {
                drawingContext.DrawEllipse(br, drawPen, Point.Subtract(points[1], size / 2), size.X / 2, size.Y / 2);
            }
            else
            {
                drawingContext.DrawEllipse(new SolidColorBrush(Colors.Transparent), drawPen, Point.Subtract(points[1], size / 2), size.X / 2, size.Y / 2);
            }
            
        }

        public override void SetSelection()
        {
            base.SetSelection();
            MainWindow.appWindow.fillSelector.IsChecked = IsFilled;
        }

        public override Figure Clone()
        {
            return new Ellipse(br, drawPen, points[0], points[1], ZIndex, IsFilled)
            {
                Thickness = Thickness
            };
        }

        public override string ConvertToSVG()
        {
            var culture = GlobalVars.culture;
            var size = Point.Subtract(points[1], points[0]);
            var point0 = Point.Subtract(points[1], size / 2);
            var opacity = ((SolidColorBrush)br).Color.A / 255.0;
            var fill = ((SolidColorBrush)br).Color.ToString(culture).Remove(1, 2);
            var stroke = ((SolidColorBrush)drawPen.Brush).Color.ToString(culture).Remove(1, 2);
            return "<ellipse cx=" + point0.X.ToString(culture) + " cy=" + point0.Y.ToString(culture) + " fill-opacity=" + opacity.ToString(culture) + " rx=" + size.X.ToString(culture) + " ry=" + size.Y.ToString(culture) + " style=\"fill:" + fill + ";stroke:" + stroke + ";stroke-width:\"" + Thickness.ToString(culture) + " />";
        }
    }
}
