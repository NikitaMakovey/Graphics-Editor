using System.Linq;
using System.Windows;
using NewPaint.Figures;
using System.Windows.Media;
using System.Windows.Input;

namespace NewPaint.Tools
{
    public class RectangleTool : Tool
    {
        public override void MouseDown(Point mousePos)
        {
            base.MouseDown(mousePos);
            Brush br;
            bool isFilled = false;
            if (Mouse.RightButton == MouseButtonState.Pressed)
            {
                isFilled = true;
                br = GlobalVars.fillBrush;
            }
            else
                br = new SolidColorBrush(Colors.Transparent);
            GlobalVars.selectedPen.Brush = GlobalVars.outlnBrush;
            GlobalVars.figures.Add(new Rectangle(br, GlobalVars.selectedPen.Clone(), mousePos, mousePos, int.MaxValue, isFilled));
        }

        public override void MouseMove(Point mousePos)
        {
            base.MouseMove(mousePos);
            if (pressed)
                GlobalVars.figures.Last().SetPoint(1, mousePos);
        }
    }
}
