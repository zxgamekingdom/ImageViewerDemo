using ImageViewer.ImageViewerControl.RoiControls;

namespace ImageViewer.ImageViewerControl.RoiShapes
{
    public abstract class RoiShape
    {
        protected RoiShape(ShapeType shapeType)
        {
            Type = shapeType;
        }

        public ShapeType Type { get; }
    }
}