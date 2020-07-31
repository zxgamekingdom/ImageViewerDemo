using System.IO;
using System.Text.Json;
using System.Windows;
using ImageViewer.ImageViewerControl.RoiControls;
using ImageViewer.ImageViewerControl.RoiShapes;

namespace ImageViewer
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int _index;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void TestButton_OnClick(object sender, RoutedEventArgs e)
        {
            foreach (RectangleRoiControl rectangleRoi in Viewer.GetRoi()
                .OfType<RectangleRoiControl>())
            {
                var roiControl = rectangleRoi as RoiControl;
                JsonSerializer.Serialize(roiControl.GetRoiShape() as RectangleRoiShape)
                    .WriteLine();
            }
        }

        private void Viewer_OnLoaded(object sender, RoutedEventArgs e)
        {
            加载图片Button_OnClick(default, default);
        }

        private void 加载图片Button_OnClick(object sender, RoutedEventArgs e)
        {
            var directoryInfo =
                new DirectoryInfo(@"C:\Users\Taurus Zhou\Pictures\Saved Pictures");
            FileInfo[] fileInfos = directoryInfo.GetFiles("*.jpg");
            FileInfo fileInfo = fileInfos[_index % fileInfos.Length];
            Viewer.LoadImage(fileInfo.FullName);
            _index++;
        }

        private void 添加RoiButton_OnClick(object sender, RoutedEventArgs e)
        {
            Viewer.AddRoi(RoiControl.CreateRectangleRoi(0, 0, 100, 100, Viewer));
        }
    }
}