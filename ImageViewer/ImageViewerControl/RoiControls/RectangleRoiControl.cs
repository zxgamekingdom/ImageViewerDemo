using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using ImageViewer.ImageViewerControl.Extensions;
using ImageViewer.ImageViewerControl.RoiShapes;

namespace ImageViewer.ImageViewerControl.RoiControls
{
    public class RectangleRoiControl : RoiControl
    {
        internal RectangleRoiControl(double x, double y, double width, double height) :
            base(ShapeType.Rectangle)
        {
            SetPositionAndSize(x, y, width, height);
        }

        public static RectangleRoiControl CreateRoi(double x,
            double y,
            double width,
            double height,
            ImageViewer imageViewer)
        {
            if (imageViewer.IsImageLoadedKey == false)
            {
                throw new ImageNotLoadedException();
            }

            Image image = imageViewer.Image;
            (double dx, double dy) = image.GetCanvasXY();
            return new RectangleRoiControl(dx + x, dy + y, width, height);
        }

        /// <summary>
        ///     获取此Roi相对于图片坐标系的左上角的坐标
        /// </summary>
        /// <returns></returns>
        public override RoiShape GetRoiShape()
        {
            Image image = ImageViewer.Image;
            Point point = TranslatePoint(new Point(0, 0), image);
            return new RectangleRoiShape()
            {
                Width = Width,
                Height = Height,
                LeftTopPoint = new RoiPoint(point.X, point.Y)
            };
        }

        /// <summary>
        /// 设置此Roi相对于画布坐标系的位置与尺寸
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        internal void SetPositionAndSize(double x,
            double y,
            double width,
            double height)
        {
            this.SetWH(width, height);
            RenderRectangle(width, height);
            this.SetCanvasXY(x, y);
        }

        private void RenderRectangle(double width, double height)
        {
            Content = new Path
            {
                Data = new RectangleGeometry(new Rect(0, 0, width, height)),
                StrokeThickness = 5,
                Stroke = Brushes.DarkRed
            };
        }
    }
}