using System;
using System.Windows;
using System.Windows.Media;
using NewPaint.Figures;

namespace NewPaint.Tools
{
    class SelectionTool: Tool
    {
        public override void MouseDown(Point mousePos)
        {
            base.MouseDown(mousePos);
            Brush br = new SolidColorBrush(Colors.Transparent);
            GlobalVars.selectedPen.Brush = GlobalVars.outlnBrush;
            Pen dashPen = new Pen(new SolidColorBrush(Colors.Black), 1.0);
            dashPen.DashStyle = DashStyles.Dash;
            GlobalVars.tempFigure = new Rectangle(br, dashPen, mousePos, mousePos, int.MaxValue, false);
        }

        public override void MouseMove(Point mousePos)
        {
            base.MouseMove(mousePos);
            if (pressed)
                GlobalVars.tempFigure.SetPoint(1, mousePos);
        }

        public override void MouseStop()
        {
            if (pressed)
            {
                GlobalVars.selections.Clear();
                GlobalVars.selected.Clear();
                foreach (var prop in MainWindow.appWindow.propPanels)
                {
                    prop.Visibility = Visibility.Collapsed;
                }
                MainWindow.appWindow.applyProp.Visibility = Visibility.Collapsed;

                for (int i = 0; i < GlobalVars.figures.Count; i++)
                {
                    if (GlobalVars.figures[i].CheckPoints((Rectangle)GlobalVars.tempFigure))
                    {
                        MainWindow.appWindow.applyProp.Visibility = Visibility.Visible;
                        MainWindow.appWindow.standartProps.Visibility = Visibility.Visible;
                        GlobalVars.figures[i].SetSelection();
                        Type figType = GlobalVars.figures[i].GetType();
                        foreach (var property in figType.GetProperties())
                        {
                            if (property.Name == "FillColor")
                            {
                                MainWindow.appWindow.fillProp.Visibility = Visibility.Visible;
                            }

                            if (property.Name == "RadiusX")
                            {
                                MainWindow.appWindow.roundsProp.Visibility = Visibility.Visible;
                            }
                        }
                    }
                }
                GlobalVars.tempFigure = null;
            }
            pressed = false;
        }
    }
}
