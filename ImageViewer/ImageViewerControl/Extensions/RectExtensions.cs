using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ImageViewer.ImageViewerControl.Extensions
{
    internal static class PathExtensions
    {
        public static void SetRoiPathFill(this Path path)
        {
            path.Fill = new SolidColorBrush(Color.FromArgb(1, 0, 0, 0));
        }

        public static void SetRoiPathNullFill(this Path path)
        {
            path.Fill = default;
        }
    }

    internal static class RectExtensions
    {
        public static (double x, double y, double width, double height)
            GetPositionAndSize(this Rect rect)
        {
            return (rect.X, rect.Y, rect.Width, rect.Height);
        }
    }
}