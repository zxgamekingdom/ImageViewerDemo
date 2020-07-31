using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;

namespace ImageViewer.ImageViewerControl.Extensions
{
   
    internal static class ControlExtensions
    {
        public static (double x, double y) GetCanvasXY(
            this FrameworkElement frameworkElement)
        {
            double x = Canvas.GetLeft(frameworkElement);
            double y = Canvas.GetTop(frameworkElement);
            x = double.IsNaN(x) ? 0 : x;
            y = double.IsNaN(y) ? 0 : y;
            return (x, y);
        }

        public static (double width, double height) GetWH(
            this FrameworkElement frameworkElement)
        {
            double width = frameworkElement.Width;
            double height = frameworkElement.Height;
            return (width, height);
        }

        public static void SetCanvasXY(this FrameworkElement frameworkElement,
            double left,
            double top)
        {
            Canvas.SetLeft(frameworkElement, left);
            Canvas.SetTop(frameworkElement, top);
        }

        public static void SetWH(this FrameworkElement frameworkElement,
            double width,
            double height)
        {
            frameworkElement.Width = width;
            frameworkElement.Height = height;
        }
    }
}