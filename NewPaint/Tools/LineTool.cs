using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using NewPaint.Figures;

namespace NewPaint.Tools
{
    public class LineTool : Tool
    {
        public override void MouseDown(Point mousePos)
        {
            base.MouseDown(mousePos);
            
            GlobalVars.figures.Add(new Line(GlobalVars.selectedPen.Clone(), mousePos, mousePos, int.MaxValue));

            GlobalVars.selectedPen.Brush = GlobalVars.outlnBrush;
        }

        public override void MouseMove(Point mousePos)
        {
            base.MouseMove(mousePos);
            if (pressed)
                GlobalVars.figures.Last().SetPoint(1, mousePos);
        }
    }
}
