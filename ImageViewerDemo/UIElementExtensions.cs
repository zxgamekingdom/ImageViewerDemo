using System.Windows;
using System.Windows.Controls;

namespace ImageViewerDemo
{
    public static class UIElementExtensions
    {
        public static double GetX(this UIElement uiElement)
        {
            double d = Canvas.GetLeft(uiElement);
            return double.IsNaN(d) ? 0 : d;
        }

        public static (double x, double y) GetXY(this UIElement uiElement)
        {
            double x = uiElement.GetX();
            double y = uiElement.GetY();
            return (x, y);
        }

        public static double GetY(this UIElement uiElement)
        {
            double d = Canvas.GetTop(uiElement);
            return double.IsNaN(d) ? 0 : d;
        }

        public static void SetX(this UIElement uiElement, double length)
        {
            Canvas.SetLeft(uiElement, length);
        }

        public static void SetXY(this UIElement uiElement, double x, double y)
        {
            uiElement.SetX(x);
            uiElement.SetY(y);
        }

        public static void SetY(this UIElement uiElement, double length)
        {
            Canvas.SetTop(uiElement, length);
        }
    }
}