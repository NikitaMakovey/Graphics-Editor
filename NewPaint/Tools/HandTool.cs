using System.Windows;

namespace NewPaint.Tools
{
    public class HandTool : Tool
    {
        private Point lastPos;

        public override void MouseDown(Point mousePos)
        {
            
            base.MouseDown(mousePos);
            lastPos = mousePos;
            GlobalVars.delta = Point.Subtract(lastPos, mousePos);
        }

        public override void MouseMove(Point mousePos)
        {
            if (pressed)
            {   
                GlobalVars.delta = Point.Subtract(lastPos, mousePos);
                MainWindow.appWindow.Set_Offset(GlobalVars.delta.X / 500, GlobalVars.delta.Y / 500);
                lastPos = mousePos;
            }
        }

        public override void MouseStop()
        {
            base.MouseStop();
            GlobalVars.delta = new Vector(0, 0);
        }
    }
}
