using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace NewPaint
{
    public static class Converter
    {
        public static void ConvertToSVG(string fileName)
        {
            var culture = GlobalVars.culture;
            var svg = "<svg width=" + GlobalVars.sizeCanvas.Width.ToString(culture) + " height=" + GlobalVars.sizeCanvas.Height.ToString(culture) + ">\n" + Environment.NewLine;
            foreach (var figure in GlobalVars.figures)
            {
                svg += " " + figure.ConvertToSVG() + Environment.NewLine;
            }
            svg += "</svg>";
            File.WriteAllText(fileName, svg);
        }

        public static void ConvertToPNG(string fileName)
        {
            MemoryStream ms = SaveCanvasAsStream();
            File.WriteAllBytes(fileName, ms.ToArray());
        }

        public static MemoryStream SaveCanvasAsStream()
        {
            Rect rect = new Rect(MainWindow.appWindow.canvas.RenderSize);
            RenderTargetBitmap rtb = new RenderTargetBitmap((int)rect.Right,
              (int)rect.Bottom, 96d, 96d, System.Windows.Media.PixelFormats.Default);
            rtb.Render(MainWindow.appWindow.canvas);

            BitmapEncoder pngEncoder = new PngBitmapEncoder();
            pngEncoder.Frames.Add(BitmapFrame.Create(rtb));

            MemoryStream ms = new MemoryStream();

            pngEncoder.Save(ms);
            ms.Close();
            return ms;
        }
    }
}
