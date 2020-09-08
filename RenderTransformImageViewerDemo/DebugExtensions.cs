using System.Diagnostics;

namespace RenderTransformImageViewerDemo
{
    public static class DebugExtensions
    {
        public static void WriteLine<T>(this T t)
        {
            Debug.WriteLine(t);
        }
    }
}