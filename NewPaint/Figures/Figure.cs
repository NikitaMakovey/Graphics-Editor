using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Xml.Serialization;

namespace NewPaint.Figures
{
    [XmlInclude(typeof(Ellipse))]
    [XmlInclude(typeof(Line))]
    [XmlInclude(typeof(Pencil))]
    [XmlInclude(typeof(Rectangle))]
    [XmlInclude(typeof(RoundRectangle))]
    [XmlInclude(typeof(SolidColorBrush))]
    [XmlInclude(typeof(MatrixTransform))]

    [Serializable]
    public class Figure
    {
        public double Thickness
        {
            get => drawPen.Thickness;
            set => drawPen.Thickness = value;
        }

        public Brush Color
        {
            set => drawPen = new Pen(value, Thickness);
        }

        public DashStyle DashStyle
        {
            get => drawPen.DashStyle;
            set => drawPen.DashStyle = value;
        }
        public int ZIndex { get; set; }

        public List<Point> points;
        public Brush br;
        public Pen drawPen = new Pen(GlobalVars.outlnBrush, 1.0);

        public Figure()
        {
            points = new List<Point>();
            br = new SolidColorBrush(Colors.Transparent);
            drawPen = new Pen(GlobalVars.outlnBrush, Thickness);
            ZIndex = 0;
        }

        public virtual void Draw(DrawingContext drawingContext)
        {

        }

        public virtual void AddPoint(Point point)
        {
            points.Add(point);
        }

        public virtual Point getPoint(int index)
        {
            return (points[index]);
        }

        public virtual void SetPoint(int index, Point point)
        {
            points[index] = point;
        }

        public virtual bool CheckPoints(Rectangle searchField)
        {
            for (int i = 0; i < this.points.Count; i++)
            {
                if (!((this.points[i].X > searchField.getPoint(0).X != this.points[i].X > searchField.getPoint(1).X) &&
                    (this.points[i].Y > searchField.getPoint(0).Y != this.points[i].Y > searchField.getPoint(1).Y)))
                {
                    return false;
                }
            }
            return true;
        }

        public virtual void SetSelection()
        {
            Pen dashPen = new Pen(new SolidColorBrush(Colors.DarkCyan), 3.0);
            dashPen.DashStyle = DashStyles.Dash;
            GlobalVars.selections.Add(new Rectangle(new SolidColorBrush(Colors.Transparent), dashPen, points[0], points[1], int.MaxValue, false));
            GlobalVars.selected.Add(this);
            MainWindow.appWindow.thicknessSelector.Text = Thickness.ToString();
            MainWindow.appWindow.zSelector.Text = ZIndex.ToString();
        }

        public virtual Figure SetConvertedFigure()
        {
            var figure = this;
            for (int i = 0; i < figure.points.Count; i++)
            {
                figure.points[i] -= GlobalVars.delta;
                if (GlobalVars.lastZoom != GlobalVars.zoom)
                {
                    figure.points[i] = new Point(figure.points[i].X * GlobalVars.zoom / GlobalVars.lastZoom, figure.points[i].Y * GlobalVars.zoom / GlobalVars.lastZoom);
                }
            }
            return figure;
        }

        public virtual Figure Clone()
        {
            var figureClone = new Figure();
            figureClone.points = points;
            figureClone.ZIndex = ZIndex;
            figureClone.drawPen = drawPen;
            figureClone.br = br;
            return figureClone;
        }

        public virtual string ConvertToSVG()
        {
            return "";
        }
    }
}
