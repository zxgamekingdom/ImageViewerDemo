using System;
using System.Runtime.Serialization;

namespace ImageViewer.ImageViewerControl.RoiControls
{
    public class ImageNotLoadedException : Exception
    {
        public ImageNotLoadedException() : base("还未加载任何图片，无法执行此操作！")
        {
        }

        protected ImageNotLoadedException(SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }

        public ImageNotLoadedException(string message) : base(message)
        {
        }

        public ImageNotLoadedException(string message, Exception innerException) : base(
            message,
            innerException)
        {
        }
    }
}