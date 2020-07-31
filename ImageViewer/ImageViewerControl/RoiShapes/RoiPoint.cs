namespace ImageViewer.ImageViewerControl.RoiShapes
{
    public class RoiPoint
    {
        public RoiPoint(double x, double y)
        {
            X = x;
            Y = y;
        }

        public RoiPoint()
        {
        }

        public double X { get; set; }
        public double Y { get; set; }
    }
}