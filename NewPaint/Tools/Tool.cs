using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace NewPaint.Tools
{
    public class Tool
    {
        protected bool pressed = false;
        public Tool()
        {

        }

        public virtual void MouseDown(Point mousePos)
        {
            pressed = true;
        }

        public virtual void MouseMove(Point mousePos)
        {

        }

        public virtual void MouseStop()
        {
            if (pressed)
                GlobalVars.figures.Last().ZIndex = 1;
            pressed = false;
        }

        public virtual void MouseUp(Point mousePos)
        {
            MouseStop();
        }

        public virtual void MouseLeave()
        {
            MouseStop();
        }

        public virtual void MouseEnter()
        {
            if (Mouse.LeftButton == MouseButtonState.Released)
                pressed = false;
        }
    }
}
