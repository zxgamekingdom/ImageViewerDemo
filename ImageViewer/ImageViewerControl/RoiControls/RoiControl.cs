using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using ImageViewer.ImageViewerControl.RoiControls.Adorner;
using ImageViewer.ImageViewerControl.RoiControls.Extensions;
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

        internal static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register(nameof(IsSelected),
                typeof(bool),
                typeof(RoiControl),
                new PropertyMetadata(default(bool), IsSelectedPropertyChangedCallback));

        private static void IsSelectedPropertyChangedCallback(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            var roiControl = (RoiControl) d;
            AdornerLayer layer = roiControl.AdornerLayer;
            System.Windows.Documents.Adorner[] adorners = layer.GetAdorners(roiControl);
            if (roiControl.IsSelected)
            {
                foreach (System.Windows.Documents.Adorner adorner in adorners)
                {
                    adorner.Visibility = Visibility.Visible;
                }
            }
            else
            {
                foreach (System.Windows.Documents.Adorner adorner in adorners)
                {
                    adorner.Visibility = Visibility.Collapsed;
                }
            }

            roiControl.IsSelected.WriteLine();
        }

        protected AdornerLayer AdornerLayer;

        internal bool IsSelected
        {
            get => (bool) GetValue(IsSelectedProperty);
            set => SetValue(IsSelectedProperty, value);
        }

        protected RoiControl(ShapeType shapeType)
        {
            Type = shapeType;
        }

        protected abstract RoiControlAdorner GetRoiControlAdorner();

        protected override void OnRender(DrawingContext drawingContext)
        {
            if (AdornerLayer == null)
            {
                AdornerLayer layer = this.GetAdornerLayer();
                layer.Add(GetRoiControlAdorner());
                foreach (System.Windows.Documents.Adorner adorner in layer.GetAdorners(
                    this))
                    adorner.Visibility = Visibility.Collapsed;
                AdornerLayer = layer;
            }

            base.OnRender(drawingContext);
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

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            if (ImageViewer.IsModifyRoi)
            {
                foreach (RoiControl roiControl in ImageViewer.GetRoi())
                    roiControl.IsSelected = false;
                IsSelected = true;
            }

            base.OnMouseDown(e);
        }

        public Path ContentPath => Content as Path;
        public abstract RoiShape GetRoiShape();
    }
}