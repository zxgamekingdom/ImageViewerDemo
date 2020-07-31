using ImageViewer.ImageViewerControl.RoiControls;

namespace ImageViewer.ImageViewerControl.RoiShapes
{
    public class RectangleRoiShape : RoiShape
    {
        public RoiPoint LeftTopPoint { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }

        public RectangleRoiShape() : base(ShapeType.Rectangle)
        {
        }
    }
}