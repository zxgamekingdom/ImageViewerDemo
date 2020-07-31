using System.Windows.Media.Imaging;

namespace ImageViewer.ImageViewerControl.Extensions
{
    internal static class BitmapImageExtensions
    {
        public static (double width, double height) GetWH(this BitmapImage bitmapImage)
        {
            double width = bitmapImage.Width;
            double height = bitmapImage.Height;
            return (width, height);
        }
    }
}