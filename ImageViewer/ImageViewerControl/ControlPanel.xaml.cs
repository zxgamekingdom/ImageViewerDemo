using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace ImageViewer.ImageViewerControl
{
    /// <summary>
    ///     ControlPanel.xaml 的交互逻辑
    /// </summary>
    internal partial class ControlPanel : UserControl
    {
        public ControlPanel(ImageViewer imageViewer)
        {
            InitializeComponent();
            DataContext = imageViewer;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style",
            "RCS1001:Add braces (when expression spans over multiple lines).",
            Justification = "<挂起>")]
        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        private void ToggleButtons_OnChecked(object sender, RoutedEventArgs e)
        {
            var toggleButton = sender as ToggleButton;
            var wrapPanel = toggleButton.Parent as WrapPanel;
            foreach (ToggleButton button in wrapPanel.Children.OfType<ToggleButton>())
                if (button != toggleButton)
                    button.IsChecked = false;
        }
    }
}