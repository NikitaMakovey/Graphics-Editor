using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace NewPaint
{
    public class PlaneHost : FrameworkElement
    {
        private VisualCollection visualCollection;
        
        protected override Visual GetVisualChild(int index)
        {
           return visualCollection[index];
        }

        protected override int VisualChildrenCount => visualCollection.Count;

        public PlaneHost()
        {
            visualCollection = new VisualCollection(this);
        }

        public void Redraw()
        {
            visualCollection.Clear();

            DrawingVisual drawingVisual = new DrawingVisual();
            DrawingContext drawingContext = drawingVisual.RenderOpen();

            GlobalVars.figures = GlobalVars.figures.OrderBy(o => o.ZIndex).ToList();
            foreach (Figures.Figure figure in GlobalVars.figures)
            {
                var newFigure = figure.SetConvertedFigure();
                newFigure.Draw(drawingContext);
            }

            foreach (var figure in GlobalVars.selections)
            {
                var newFigure = figure.SetConvertedFigure();
                newFigure.Draw(drawingContext);
            }

            if (GlobalVars.tempFigure != null)
            {
                GlobalVars.tempFigure = GlobalVars.tempFigure.SetConvertedFigure();
                GlobalVars.tempFigure.Draw(drawingContext);
            }

            drawingContext.Close();
            visualCollection.Add(drawingVisual);
        }
    }
}
