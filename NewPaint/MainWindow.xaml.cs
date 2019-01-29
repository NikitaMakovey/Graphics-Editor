using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using NewPaint.Tools;
using System.Reflection;
using NewPaint.Figures;

namespace NewPaint
{
    public partial class MainWindow : Window
    {

        public PlaneHost planeHost;
        public static MainWindow appWindow;

        private static LineTool lineTool = new LineTool();
        private static PencilTool pencilTool = new PencilTool();
        private static RectangleTool rectangleTool = new RectangleTool();
        private static RoundRectangleTool roundRectangleTool = new RoundRectangleTool();
        private static EllipseTool ellipseTool = new EllipseTool();
        private static HandTool handTool = new HandTool();
        private static ZoomTool zoomTool = new ZoomTool();
        private static SelectionTool selectionTool = new SelectionTool();

        private Button selectedColor = new Button();
        private Tool selectedTool = pencilTool;
        private Button lastBtn = new Button();
        private Thickness ActiveThick = new Thickness(4.0);
        private Thickness UnactiveThick = new Thickness(1.0);

        private double lastScrollX = 0;
        private double lastScrollY = 0;

        public List<Grid> propPanels;

        private List<Figure> copyFigures = new List<Figure>();

        public MainWindow()
        {
            InitializeComponent();
            appWindow = this;

            planeHost = new PlaneHost
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch
            };

            selectedColor = Color1;            

            var baseUri = new Uri("C:/Users/АДМИН/source/repos/GrafRedactorMakovei/NewPaint/Resourses/");
            Resources = new ResourceDictionary() { Source = new Uri(baseUri, "ResourceDictionary.xaml") };

            Dictionary<string, string> btns = new Dictionary<string, string> {
                { "Undo_Button", "icons/Undo.bmp"},
                { "Redo_Button", "icons/Redo.bmp"},
                { "ClearAll_Button", "icons/ClearAllTool.bmp"},
                { "Pencil_Button", "icons/PencilTool.bmp"},
                { "Line_Button", "icons/LineTool.bmp"},
                { "Rectangle_Button", "icons/RectangleTool.bmp"},
                { "RoundRectangle_Button", "icons/RoundRectangleTool.bmp"},
                { "Ellipse_Button", "icons/EllipseTool.bmp"},
                { "Hand_Button", "icons/HandTool.bmp"},
                { "Zoom_Button", "icons/ZoomTool.bmp"},
                { "Emphasize_Button", "icons/EmphasizeTool.bmp"}
            };

            Color[] colors = new Color[] {
                Colors.Black, Colors.White, Colors.Brown, Colors.Yellow,
                Colors.Blue, Colors.DarkSlateBlue, Colors.LightBlue, Colors.SlateBlue,
                Colors.Pink, Colors.Purple, Colors.MediumPurple, Colors.Red,
                Colors.Green, Colors.Goldenrod, Colors.LightYellow, Colors.LightGreen
            };

            int j = 0;

            foreach (string key in btns.Keys)
            {
                Create_Button(MainGrid, key, j, new Uri(baseUri, btns[key]));
                j++;
            }

            int i = 0;
            j = 0;

            foreach (Color color in colors)
            {
                Create_Button(Palette, new SolidColorBrush(color), i, j);
                j++;
                if (j > 7)
                {
                    j = 0; i++;
                }
            }

            canvas.Children.Add(planeHost);

            Loaded += delegate
            {
                GlobalVars.sizeCanvas = new Size(canvas.ActualWidth, canvas.ActualHeight);
                ScrollBarX.Maximum = canvas.ActualWidth / 100;
                ScrollBarY.Maximum = canvas.ActualHeight / 100;

                propPanels = new List<Grid> {
                    standartProps, fillProp, roundsProp
                };
            };

            Serializator.Deserialize("last_session.xml");
            planeHost.Redraw();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            ExportWindow exportWindow = new ExportWindow();
            exportWindow.Show();
        }
        private void Load_Click(object sender, RoutedEventArgs e)
        {
            ExportWindow exportWindow = new ExportWindow();
            exportWindow.Show();
        }
        private void Export_Click(object sender, RoutedEventArgs e)
        {
            ExportWindow exportWindow = new ExportWindow();
            exportWindow.Show();
        }

        public void Set_Offset(double X, double Y)
        {
            lastScrollX += X;
            lastScrollY += Y;
            ScrollBarX.Value += X;
            ScrollBarY.Value += Y;
        }

        private void Create_Button(Grid parent, string btnName, int col, Uri imagePath)
        {
            Button button1 = new Button
            {
                Name = btnName,
                Content = new Image { Source = new BitmapImage(imagePath) },
                Tag = btnName
            };
            button1.Click += new RoutedEventHandler(ToolBtn_Click);
            parent.Children.Add(button1);
            button1.SetValue(Grid.ColumnProperty, col);
        }

        private void Create_Button(Grid parent, Brush color, int row, int col)
        {
            Button button1 = new Button
            {
                Background = color
            };
            button1.Click += new RoutedEventHandler(Color_Click);
            Palette.Children.Add(button1);
            button1.SetValue(Grid.RowProperty, row);
            button1.SetValue(Grid.ColumnProperty, col);
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            var mousePos = e.GetPosition(canvas);
            selectedTool.MouseMove(mousePos);
            planeHost.Redraw();
        }

        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var mousePos = e.GetPosition(canvas);
            if (e.LeftButton == MouseButtonState.Released)
                selectedTool.MouseUp(mousePos);
            planeHost.Redraw();
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var mousePos = e.GetPosition(canvas);
            if (e.LeftButton == MouseButtonState.Pressed)
                selectedTool.MouseDown(mousePos);
            planeHost.Redraw();
        }

        private void ToolBtn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if ((string)btn.Tag != "ClearAll_Button")
            {
                GlobalVars.selections.Clear();
                GlobalVars.selected.Clear();
                lastBtn.BorderThickness = UnactiveThick;
                lastBtn = btn;
                lastBtn.BorderThickness = ActiveThick;
                applyProp.Visibility = Visibility.Collapsed;
                foreach (var panel in propPanels)
                {
                    panel.Visibility = Visibility.Collapsed;
                }
                switch (btn.Tag)
                {
                    case "Pencil_Button":
                        selectedTool = pencilTool;
                        break;
                    case "Line_Button":
                        selectedTool = lineTool;
                        break;
                    case "Rectangle_Button":
                        selectedTool = rectangleTool;
                        break;
                    case "RoundRectangle_Button":
                        selectedTool = roundRectangleTool;
                        break;
                    case "Ellipse_Button":
                        selectedTool = ellipseTool;
                        break;
                    case "Hand_Button":
                        selectedTool = handTool;
                        break;
                    case "Zoom_Button":
                        selectedTool = zoomTool;
                        break;
                    case "Emphasize_Button":
                        selectedTool = selectionTool;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (btn.Tag)
                {
                    case "ClearAll_Button":
                        if (GlobalVars.selected.Count == 0)
                        {
                            GlobalVars.figures.Clear();
                            GlobalVars.selected.Clear();
                        }
                        else
                        {
                            for (int i = 0; i < GlobalVars.figures.Count; i++)
                            {
                                for (int j = 0; j < GlobalVars.selected.Count; j++)
                                {
                                    if (GlobalVars.figures[i] == GlobalVars.selected[j])
                                    {
                                        GlobalVars.figures.RemoveAt(i);
                                        GlobalVars.selected.RemoveAt(j);
                                        j--;
                                        if (i > 0)
                                            i--;
                                    }
                                }
                            }
                        }

                        GlobalVars.selections.Clear();
                        planeHost.Redraw();
                        break;
                }
            }
            planeHost.Redraw();
        }

        private void Color_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(e.GetType());
            Button btn = sender as Button;

            selectedColor.Background = btn.Background;
            GlobalVars.outlnBrush = Color1.Background;
            GlobalVars.fillBrush = Color2.Background;

            foreach (var figure in GlobalVars.selected)
            {
                figure.Color = GlobalVars.outlnBrush;
            }
            planeHost.Redraw();
            
        }

        private void ChosenColor_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            Color1.BorderBrush = Brushes.DarkGray;
            Color2.BorderBrush = Brushes.DarkGray;
            btn.BorderBrush = Brushes.LightSteelBlue;
            selectedColor = btn;
        }

        private void Canvas_MouseLeave(object sender, MouseEventArgs e)
        {
            selectedTool.MouseLeave();
        }

        private void Canvas_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var mousePos = e.GetPosition(canvas);
            if (e.RightButton == MouseButtonState.Pressed)
                selectedTool.MouseDown(mousePos);
            planeHost.Redraw();
        }

        public static bool IsValid(string str)
        {
            return double.TryParse(str, out double i) && i >= 1 && i <= 10000;
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox tb = sender as TextBox;
            e.Handled = !IsValid(tb.Text + e.Text);
            double.TryParse(tb.Text + e.Text, out double i);
            if (i <= 0)
                tb.Text = "1";
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            for (int j = 0; j < GlobalVars.selected.Count; j++)
            {
                Figure figure = GlobalVars.selected[j];
                try
                {
                    figure.Thickness = double.Parse(thicknessSelector.Text);
                }
                catch
                {
                    MessageBox.Show("Invalid Thickness");
                    figure.Thickness = 1;
                }

                switch (dashSelector.SelectedIndex)
                {
                    case 0:
                        figure.DashStyle = DashStyles.Solid;
                        break;
                    case 1:
                        figure.DashStyle = DashStyles.Dash;
                        break;
                    case 2:
                        figure.DashStyle = DashStyles.Dot;
                        break;
                    case 3:
                        figure.DashStyle = DashStyles.DashDot;
                        break;
                }
                try
                {
                    figure.ZIndex = int.Parse(zSelector.Text);
                }
                catch
                {
                    MessageBox.Show("Invalid ZIndex");
                    figure.ZIndex = 1;
                }

                Type figType = figure.GetType();
                foreach (var property in figType.GetProperties())
                {
                    var transparentBr = new SolidColorBrush(Colors.Transparent);
                    if (property.Name == "IsFilled" && fillSelector.IsChecked == true)
                    {
                        object obj = true;
                        object[] objs = new object[] { obj };
                        figType.InvokeMember("IsFilled", BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty, Type.DefaultBinder, figure, objs);
                    }
                    else if (property.Name == "IsFilled" && fillSelector.IsChecked == false)
                    {
                        object obj = false;
                        object[] objs = new object[] { obj };
                        figType.InvokeMember("IsFilled", BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty, Type.DefaultBinder, figure, objs);
                    }
                    

                    if (property.Name == "RadiusX")
                    {
                        var a = 1;
                        try
                        {
                            a = int.Parse(roundsSelectorX.Text);
                        }
                        catch
                        {
                            a = 1;
                        }
                        object[] objs = new object[] { a };
                        figType.InvokeMember("RadiusX", BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty, Type.DefaultBinder, figure, objs);
                    }

                    if (property.Name == "RadiusY")
                    {
                        var a = 1;
                        try
                        {
                            a = int.Parse(roundsSelectorY.Text);
                        }
                        catch
                        {
                            a = 1;
                        }
                        object[] objs = new object[] { a };
                        figType.InvokeMember("RadiusY", BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty, Type.DefaultBinder, figure, objs);
                    }
                }
            }
            var tempList = new List<Figure>(GlobalVars.selected);
            GlobalVars.selected.Clear();
            GlobalVars.selections.Clear();
            foreach (var figure in tempList)
                figure.SetSelection();

            planeHost.Redraw();
        }

        private void Canvas_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            e.Handled = true;
            
            if (e.Delta > 0)
            {
                GlobalVars.zoom *= GlobalVars.scale;
            }
            else
            {
                GlobalVars.zoom /= GlobalVars.scale;
                
            }
            planeHost.Redraw();
            GlobalVars.lastZoom = GlobalVars.zoom;
        }

        private void ScrollBarY_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {
            GlobalVars.delta.Y = (ScrollBarY.Value - lastScrollY) * 50 * GlobalVars.zoom;
            planeHost.Redraw();
            GlobalVars.delta.Y = 0;
            lastScrollY = ScrollBarY.Value;
        }

        private void ScrollBarX_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {
            GlobalVars.delta.X = (ScrollBarX.Value - lastScrollX) * 50 * GlobalVars.zoom;
            planeHost.Redraw();
            GlobalVars.delta.X = 0;
            lastScrollX = ScrollBarX.Value;
        }

        private void FillColor_Click(object sender, RoutedEventArgs e)
        {
            foreach (var figure in GlobalVars.selected)
            {
                Type figType = figure.GetType();
                foreach (var property in figType.GetProperties())
                {
                    if (property.Name == "FillColor")
                    {
                        object obj = GlobalVars.fillBrush;
                        object[] objs = new object[] { obj };
                        figType.InvokeMember("FillColor", BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty, Type.DefaultBinder, figure, objs);
                    }
                }
            }
            planeHost.Redraw();
        }

        private void Form_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Serializator.Serialize("last_session.xml");
        }

        private void Form_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.X)
            {
                copyFigures.Clear();
                for (int i = 0; i < GlobalVars.figures.Count; i++)
                {
                    for (int k = 0; k < GlobalVars.selected.Count; k++)
                    {
                        if (GlobalVars.figures[i] == GlobalVars.selected[k])
                        {
                            copyFigures.Add(GlobalVars.selected[k]);
                            GlobalVars.figures.RemoveAt(i);
                            GlobalVars.selected.RemoveAt(k);
                            GlobalVars.selections.Clear();
                            k--;
                            if (i > 0)
                                i--;
                        }   
                    }
                }
                planeHost.Redraw();
            }
            else if (e.Key == Key.C)
            {
                copyFigures.Clear();
                for (int i = 0; i < GlobalVars.figures.Count; i++)
                {
                    for (int k = 0; k < GlobalVars.selected.Count; k++)
                    {
                        if (GlobalVars.figures[i] == GlobalVars.selected[k])
                        {
                            copyFigures.Add(GlobalVars.selected[k]);
                        }
                    }
                }
            }
            else if (e.Key == Key.V)
            {
                var center = new Point(0, 0);
                int length = 0;

                foreach (var figure in copyFigures)
                {
                    for (int i = 0; i < figure.points.Count; i++)
                        center = center + new Vector(figure.points[i].X, figure.points[i].Y);
                    length += figure.points.Count;
                }
                center = new Point(center.X / length, center.Y / length);

                var mousePosition = Mouse.GetPosition(canvas);

                foreach (var figure in copyFigures)
                {
                    dynamic cloneFig = figure.Clone();

                    for (int i = 0; i < figure.points.Count; i++)
                        cloneFig.points[i] += mousePosition - center;

                    GlobalVars.figures.Add(cloneFig);
                }

                planeHost.Redraw();
            }
        }
    }
}
