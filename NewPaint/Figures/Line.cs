using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows;

namespace NewPaint.Figures
{
    [Serializable]
    public class Line : Figure
    {
        public Line()
        {

        }

        public Line(Pen pen, Point point0, Point point1, int ZIndex)
        {
            points = new List<Point>() { point0, point1 };
            drawPen = pen;
            br = new SolidColorBrush(Colors.Transparent);
            this.ZIndex = ZIndex;
        }

        public override void Draw(DrawingContext drawingContext)
        {
            drawingContext.DrawLine(drawPen, points[0], points[1]);
        }

        public override Figure Clone()
        {
            return new Line(drawPen, points[0], points[1], ZIndex)
            {
                br = br,
                Thickness = Thickness
            };
        }

        public override string ConvertToSVG()
        {
            var point1 = points[0];
            var point2 = points[1];

            var stroke = ((SolidColorBrush)drawPen.Brush).Color.ToString().Remove(1, 2);

            return "<line x1=" + point1.X.ToString(GlobalVars.culture) + " y1=" + point1.Y.ToString(GlobalVars.culture) + " x2=" + point2.X.ToString(GlobalVars.culture) + " y2=" + point2.Y.ToString(GlobalVars.culture) + " style=\"stroke:" + stroke + ";stroke-width:" + Thickness.ToString(GlobalVars.culture) + "\"/>";
        }
    }
}
