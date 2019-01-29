using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace NewPaint.Figures
{
    [Serializable]
    public class Pencil : Figure
    {
        public Pencil()
        {

        }

        public Pencil(Pen pen, int ZIndex)
        {
            points = new List<Point>();
            drawPen = pen;
            br = new SolidColorBrush(Colors.Transparent);
            this.ZIndex = ZIndex;
        }

        public override void Draw(DrawingContext drawingContext)
        {
            for (int i = 0; i < points.Count - 1; i++)
                drawingContext.DrawLine(drawPen, points[i], points[i+1]);
        }

        public override void SetSelection()
        {
            double maxX = 0, maxY = 0, minX = Double.MaxValue, minY = Double.MaxValue;
            for (int i = 0; i < points.Count(); i++)
            {
                if (maxX < points[i].X)
                    maxX = points[i].X;
                if (maxY < points[i].Y)
                    maxY = points[i].Y;
                if (minX > points[i].X)
                    minX = points[i].X;
                if (minY > points[i].Y)
                    minY = points[i].Y;
            }
            Pen dashPen = new Pen(new SolidColorBrush(Colors.DarkCyan), 3.0)
            {
                DashStyle = DashStyles.Dash
            };
            GlobalVars.selections.Add(new Rectangle(new SolidColorBrush(Colors.Transparent), dashPen, new Point(maxX, maxY), new Point(minX, minY), int.MaxValue, false));
            GlobalVars.selected.Add(this);
        }

        public override Figure Clone()
        {
            return new Pencil(drawPen, ZIndex)
            {
                points = new List<Point>(points),
                br = br,
                Thickness = Thickness,
            };
        }

        public override string ConvertToSVG()
        {
            var svg_points = string.Empty;

            for (var i = 0; i < points.Count - 1; i++)
                svg_points +=  points[i].X.ToString(GlobalVars.culture) + "," + points[i].Y.ToString(GlobalVars.culture) + " ";
            svg_points += points[points.Count - 1].X.ToString(GlobalVars.culture) + "," + points[points.Count - 1].Y.ToString(GlobalVars.culture);

            var stroke = ((SolidColorBrush)drawPen.Brush).Color.ToString().Remove(1, 2);

            return "<polyline points=\"" + svg_points + "\" style=\"fill:none;stroke:" + stroke+";stroke-width:"+Thickness.ToString(GlobalVars.culture)+"\"/>";
        }
    }
}
