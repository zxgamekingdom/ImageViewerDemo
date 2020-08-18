using System.Windows.Documents;

namespace ImageViewer.ImageViewerControl.RoiControls.Extensions
{
    internal static class RoiControlExtensions
    {
        public static AdornerLayer GetAdornerLayer(this RoiControl roiControl)
        {
            return AdornerLayer.GetAdornerLayer(roiControl);
        }
    }
}