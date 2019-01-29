using System;
using System.Windows;
using System.Windows.Media;
using NewPaint.Figures;

namespace NewPaint.Tools
{
    public class ZoomTool : Tool
    {
        public override void MouseDown(Point mousePos)
        {
            base.MouseDown(mousePos);
            Brush br = new SolidColorBrush(Colors.Transparent);
            Pen dashPen = new Pen(new SolidColorBrush(Colors.Black), 1.0);
            dashPen.DashStyle = DashStyles.Dash;
            GlobalVars.tempFigure = new Rectangle(br, dashPen, mousePos, mousePos, int.MaxValue, false);
        }

        public override void MouseMove(Point mousePos)
        {
            GlobalVars.delta = new Vector(0, 0);
            GlobalVars.lastZoom = GlobalVars.zoom;
            base.MouseMove(mousePos);
            if (pressed)
                GlobalVars.tempFigure.SetPoint(1, mousePos);
        }

        public override void MouseStop()
        {
            if (pressed)
            {
                GlobalVars.zoom *= Math.Min(GlobalVars.sizeCanvas.Width / Math.Abs(GlobalVars.tempFigure.getPoint(1).X - GlobalVars.tempFigure.getPoint(0).X),
                                           GlobalVars.sizeCanvas.Height / Math.Abs(GlobalVars.tempFigure.getPoint(1).Y - GlobalVars.tempFigure.getPoint(0).Y));

                GlobalVars.delta = new Vector(Math.Min(GlobalVars.tempFigure.getPoint(1).X,
                                                       GlobalVars.tempFigure.getPoint(0).X),
                                              Math.Min(GlobalVars.tempFigure.getPoint(1).Y,
                                                       GlobalVars.tempFigure.getPoint(0).Y));
                GlobalVars.tempFigure = null;
            }

            pressed = false;
        }
    }
}