using System.Windows.Shapes;

namespace ImageViewerDemo
{
    public static class PathExtensions
    {
        public static void SetFrom(this Path source, Path path)
        {
            source.Data = path.Data;
            source.Stroke = path.Stroke;
            source.StrokeThickness = path.StrokeThickness;
        }
    }
}