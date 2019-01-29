using System.Collections.Generic;
using System.Windows.Media;
using System.Windows;
using NewPaint.Figures;
using System.Globalization;

namespace NewPaint
{
    public static class GlobalVars
    {
        public static Size sizeCanvas;

        public static CultureInfo culture = new CultureInfo("en");

        public static List<Figure> figures = new List<Figure>();
        public static List<Figure> selections = new List<Figure>();
        public static List<Figure> selected = new List<Figure>();
        public static Figure tempFigure;

        public static double thickness = 1.0;
        public static Brush outlnBrush = new SolidColorBrush(Colors.Black);
        public static Brush fillBrush = new SolidColorBrush(Colors.White);
        public static Pen selectedPen = new Pen(outlnBrush, thickness);

        public static Vector delta = new Vector(0, 0);
        public static double zoom = 1.0;
        public static double lastZoom = 1.0;
        public static double scale = 1.5;
    }
}
