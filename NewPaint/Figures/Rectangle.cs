using System.Collections.Generic;
using System.Windows.Media;
using System.Windows;
using System;

namespace NewPaint.Figures
{
    [Serializable]
    public class Rectangle : Figure
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

        public Rectangle()
        {

        }

        public Rectangle(Brush brush, Pen pen, Point point0, Point point1, int ZIndex,  bool isFilled)
        {
            points = new List<Point>() { point0, point1 };
            br = brush;
            drawPen = pen;
            IsFilled = isFilled;
            this.ZIndex = ZIndex;
        }

        public override void Draw(DrawingContext drawingContext)
        {
            var size = Point.Subtract(points[0], points[1]);
            if (IsFilled)
                drawingContext.DrawRectangle(br, drawPen, new Rect(points[1], size));
            else
                drawingContext.DrawRectangle(new SolidColorBrush(Colors.Transparent), drawPen, new Rect(points[1], size));
        }

        public override void SetSelection()
        {
            base.SetSelection();
            MainWindow.appWindow.fillSelector.IsChecked = IsFilled;
        }

        public override Figure Clone()
        {
            return new Rectangle(br, drawPen, points[0], points[1], ZIndex, IsFilled)
            {
                Thickness = Thickness
            };
        }

        public override string ConvertToSVG()
        {
            var point1 = points[0];
            var point2 = points[1];

            var size = Point.Subtract(point2, point1);

            var fill = ((SolidColorBrush)br).Color.ToString().Remove(1, 2);
            var stroke = ((SolidColorBrush)drawPen.Brush).Color.ToString().Remove(1, 2);
            var alpha = ((SolidColorBrush)br).Color.A / 255.0;

            var svg = "<rect x=" + point1.X.ToString(GlobalVars.culture) + " y=" + point1.Y.ToString(GlobalVars.culture) + " fill-opacity=" + alpha.ToString(GlobalVars.culture) + " width=" + size.X.ToString(GlobalVars.culture) + " height=" + size.Y.ToString(GlobalVars.culture) + " style=\"fill:" + fill + ";stroke:" + stroke + ";stroke-width:" + Thickness.ToString(GlobalVars.culture) + "\" />";

            return svg;
        }
    }
}
