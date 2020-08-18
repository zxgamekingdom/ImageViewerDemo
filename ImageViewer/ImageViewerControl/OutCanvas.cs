using ImageViewer.ImageViewerControl.Extensions;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ImageViewer.ImageViewerControl
{
    internal class OutCanvas : Canvas
    {
        private readonly ImageViewer _imageViewer;
        private readonly InCanvas _inCanvas;
        private Point _buffPoint;

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            if (IsMoveAndScaleConditionOk(e))
                _buffPoint = e.GetPosition(this);
            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (IsMoveAndScaleConditionOk(e))
            {
                Point position = e.GetPosition(this);
                Vector vector = position - _buffPoint;
                (double x, double y) = _inCanvas.GetCanvasXY();
                (double newX, double newY) = (x + vector.X, y + vector.Y);
                _inCanvas.SetCanvasXY(newX, newY);
                _buffPoint = position;
            }

            base.OnMouseMove(e);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Readability",
            "RCS1123:Add parentheses when necessary.",
            Justification = "<挂起>")]
        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            if (_imageViewer.IsMoveAndScale)
            {
                (double scale, double scaleFactor, double minScale, double maxScale) =
                    _imageViewer.GetScaleInfo();
                int i = e.Delta > 0 ? 1 : -1;
                double d = scale + scaleFactor * i;
                if (d > minScale && d < maxScale)
                {
                    _imageViewer.Scale = d;
                    _inCanvas.RenderTransformOrigin = new Point(0.5, 0.5);
                    _inCanvas.RenderTransform = new ScaleTransform(d, d);
                }
            }

            base.OnMouseWheel(e);
        }

        private bool IsMoveAndScaleConditionOk(MouseButtonEventArgs e)
        {
            return _imageViewer.IsMoveAndScale &&
                   (e.LeftButton == MouseButtonState.Pressed ||
                    e.RightButton == MouseButtonState.Pressed);
        }

        private bool IsMoveAndScaleConditionOk(MouseEventArgs e)
        {
            return _imageViewer.IsMoveAndScale &&
                   (e.LeftButton == MouseButtonState.Pressed ||
                    e.RightButton == MouseButtonState.Pressed);
        }

        public  OutCanvas(ImageViewer imageViewer)
        {
            _imageViewer = imageViewer ??
                           throw new ArgumentNullException(nameof(imageViewer));
            _inCanvas = imageViewer.InCanvas;
        }
    }
}