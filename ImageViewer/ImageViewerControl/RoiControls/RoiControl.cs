using System.Windows;
using System.Windows.Controls;
using ImageViewer.ImageViewerControl.RoiShapes;

namespace ImageViewer.ImageViewerControl.RoiControls
{
    public abstract class RoiControl : ContentControl
    {
        public static readonly DependencyProperty TypeProperty =
            DependencyProperty.Register(nameof(Type),
                typeof(ShapeType),
                typeof(RoiControl),
                new PropertyMetadata(default(ShapeType)));

        protected internal ImageViewer ImageViewer;

        protected RoiControl(ShapeType shapeType)
        {
            Type = shapeType;
        }

        public ShapeType Type
        {
            get => (ShapeType) GetValue(TypeProperty);
            set => SetValue(TypeProperty, value);
        }

        public static RectangleRoiControl CreateRectangleRoi(double x,
            double y,
            double width,
            double height,
            ImageViewer imageViewer)
        {
            return RectangleRoiControl.CreateRoi(x, y, width, height, imageViewer);
        }

        public abstract RoiShape GetRoiShape();
    }
}