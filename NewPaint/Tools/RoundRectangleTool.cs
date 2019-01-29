using System.Linq;
using System.Windows;
using NewPaint.Figures;
using System.Windows.Media;
using System.Windows.Input;

namespace NewPaint.Tools
{
    public class RoundRectangleTool : Tool
    {
        public override void MouseDown(Point mousePos)
        {
            bool isFilled = false;
            base.MouseDown(mousePos);
            Brush br;
            if (Mouse.RightButton == MouseButtonState.Pressed)
            {
                br = GlobalVars.fillBrush;
                isFilled = true;
            }
            else
                br = new SolidColorBrush(Colors.Transparent);
            GlobalVars.selectedPen.Brush = GlobalVars.outlnBrush;
            GlobalVars.figures.Add(new RoundRectangle(br, GlobalVars.selectedPen.Clone(), mousePos, mousePos, int.MaxValue, isFilled));
        }

        public override void MouseMove(Point mousePos)
        {
            base.MouseMove(mousePos);
            if (pressed)
                GlobalVars.figures.Last().SetPoint(1, mousePos);
        }
    }
}
