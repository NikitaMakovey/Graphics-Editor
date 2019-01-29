using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows;

namespace NewPaint.Figures
{
    [Serializable]
    public class RoundRectangle : Figure
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

        public double RadiusX
        {
            get;
            set;
        } = 30;

        public double RadiusY
        {
            get;
            set;
        } = 30;

        public RoundRectangle()
        {

        }

        public RoundRectangle(Brush brush, Pen pen, Point point0, Point point1, int ZIndex, bool isFilled)
        {
            points = new List<Point>() { point0, point1 };
            br = brush;
            drawPen = pen;
            IsFilled = isFilled;
            this.ZIndex = ZIndex;
            var size = Point.Subtract(points[0], points[1]);
        }

        public override void Draw(DrawingContext drawingContext)
        {
            var size = Point.Subtract(points[0], points[1]);
            if (IsFilled)
                drawingContext.DrawRoundedRectangle(br, drawPen, new Rect(points[1], size), RadiusX, RadiusY);
            else
                drawingContext.DrawRoundedRectangle(new SolidColorBrush(Colors.Transparent), drawPen, new Rect(points[1], size), RadiusX, RadiusY);
        }

        public override void SetSelection()
        {
            base.SetSelection();
            MainWindow.appWindow.fillSelector.IsChecked = IsFilled;
            MainWindow.appWindow.roundsSelectorX.Text = RadiusX.ToString();
            MainWindow.appWindow.roundsSelectorY.Text = RadiusY.ToString();
        }

        public override Figure Clone()
        {
            return new RoundRectangle(br, drawPen, points[0], points[1], ZIndex, IsFilled)
            {
                Thickness = Thickness,
            };
        }

        public override string ConvertToSVG()
        {
            var point1 = points[0];

            var size = Point.Subtract(points[1], point1);

            var fill = ((SolidColorBrush)br).Color.ToString().Remove(1, 2);
            var stroke = ((SolidColorBrush)drawPen.Brush).Color.ToString().Remove(1, 2);
            var alpha = ((SolidColorBrush)br).Color.A / 255.0;

            return "<rect x=" + point1.X.ToString(GlobalVars.culture) + " y=" + point1.Y.ToString(GlobalVars.culture) + " rx=" + RadiusX.ToString(GlobalVars.culture) + " ry =" + RadiusY.ToString(GlobalVars.culture) + " width=" + size.X.ToString(GlobalVars.culture) + " height=" + size.Y.ToString(GlobalVars.culture) + " fill-opacity=" + alpha.ToString(GlobalVars.culture) + " style=\"fill:" + fill + ";stroke:" + stroke + ";stroke-width:" + Thickness.ToString(GlobalVars.culture) + "\" />";
        }
    }
}