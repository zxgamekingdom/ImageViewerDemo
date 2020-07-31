using ImageViewer.ImageViewerControl.Extensions;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ImageViewer.ImageViewerControl.RoiControls;

namespace ImageViewer.ImageViewerControl
{
    internal class InCanvas : Canvas
    {
        private readonly ImageViewer _imageViewer;
        private Point _buffPoint;

        private RoiControl _buffRoiControl;
        public InCanvas(ImageViewer imageViewer)
        {
            _imageViewer = imageViewer;
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            if (IsRectangleConditionOk(e))
            {
                _buffPoint = e.GetPosition(this);
                _buffRoiControl = default;
            }
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (IsRectangleConditionOk(e))
            {
                Point position = e.GetPosition(this);
                Vector vector = position - _buffPoint;
                var rect = new Rect(_buffPoint, vector);
                (double x, double y, double width, double height) =
                    rect.GetPositionAndSize();
                if (_buffRoiControl == null)
                {
                    _buffRoiControl = new RectangleRoiControl(x, y, width, height);
                    _imageViewer.AddRoi(_buffRoiControl);
                }

                RectangleRoiControl rectangleRoiControl = Children.OfType<RectangleRoiControl>()
                    .Single(roi => roi == _buffRoiControl);
                rectangleRoiControl.SetPositionAndSize(x, y, width, height);
            }
        }

        private bool IsRectangleConditionOk(MouseEventArgs e)
        {
            return _imageViewer.IsRectangle &&
                   (e.LeftButton == MouseButtonState.Pressed ||
                    e.RightButton == MouseButtonState.Pressed);
        }

        private bool IsRectangleConditionOk(MouseButtonEventArgs e)
        {
            return _imageViewer.IsRectangle &&
                   (e.LeftButton == MouseButtonState.Pressed ||
                    e.RightButton == MouseButtonState.Pressed);
        }
    }
}