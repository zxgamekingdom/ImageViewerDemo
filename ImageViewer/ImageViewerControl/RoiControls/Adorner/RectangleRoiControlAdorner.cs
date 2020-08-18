using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace ImageViewer.ImageViewerControl.RoiControls.Adorner
{
    public class RectangleRoiControlAdorner : RoiControlAdorner
    {
       
        public RectangleRoiControlAdorner(RectangleRoiControl adornedElement) : base(
            adornedElement)
        {
           
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
        }
    }
}