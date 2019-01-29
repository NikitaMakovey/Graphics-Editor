using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using NewPaint.Figures;
using System.Windows.Media;

namespace NewPaint.Tools
{
    public class PencilTool : Tool
    {
        public override void MouseDown(Point mousePos)
        {
            base.MouseDown(mousePos);

            GlobalVars.selectedPen.Brush = GlobalVars.outlnBrush;

            GlobalVars.figures.Add(new Pencil(GlobalVars.selectedPen.Clone(), int.MaxValue));
        }

        public override void MouseMove(Point mousePos)
        {
            if (pressed)
            {
                GlobalVars.figures.Last().AddPoint(mousePos);
            }   
        }
    }
}
