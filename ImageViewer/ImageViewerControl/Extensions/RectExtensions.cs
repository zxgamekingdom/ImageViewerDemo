using System.Windows;

namespace ImageViewer.ImageViewerControl.Extensions
{
    internal static class RectExtensions
    {
        public static (double x, double y, double width, double height)
            GetPositionAndSize(this Rect rect)
        {
            return (rect.X, rect.Y, rect.Width, rect.Height);
        }
    }
}