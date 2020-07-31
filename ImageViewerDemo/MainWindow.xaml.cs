using System;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using JetBrains.Annotations;

namespace ImageViewerDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
    public partial class MainWindow : INotifyPropertyChanged
    {
        private Path _buffPath;
        private Point _buffPosition;
        private Image _image;

        public MainWindow()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public bool IsMove { get; set; }
        public bool IsRectangle { get; set; }
        public bool IsScale { get; set; }
        public double Scale { get; private set; } = 1;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(
            [CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private static bool IsMouseButtonDown(MouseEventArgs e)
        {
            return e.LeftButton == MouseButtonState.Pressed ||
                   e.RightButton == MouseButtonState.Pressed;
        }

        private void InCanvas_OnLoaded(object sender, RoutedEventArgs e)
        {
            var canvas = sender as Canvas;
            var bitmapImage = new BitmapImage(new Uri(
                @"C:\Users\Taurus Zhou\Downloads\b4979074ad8058e0e7d2c89a6e8d930b.jpg"));
            _image = new Image {Source = bitmapImage};
            canvas.Children.Add(_image);
            canvas.Width = bitmapImage.Width * 1000;
            canvas.Height = bitmapImage.Height * 1000;
            double x = canvas.Width / 2 - bitmapImage.Width / 2;
            double y = canvas.Height / 2 - bitmapImage.Height / 2;
            _image.SetXY(x, y);
            canvas.SetXY(-x, -y);
        }

        private void InCanvas_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (IsRectangle && IsMouseButtonDown(e))
            {
                _buffPosition = e.GetPosition((IInputElement) sender);
                _buffPath = default;
            }
        }

        private void InCanvas_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (IsRectangle && IsMouseButtonDown(e))
            {
                var canvas = sender as Canvas;
                Point position = e.GetPosition(canvas);
                Vector vector = position - _buffPosition;
                var buffRect = new Rect(_buffPosition, vector);
                double x = buffRect.X;
                double y = buffRect.Y;
                var rect = new Rect(buffRect.Size);
                var rectangleGeometry = new RectangleGeometry(rect);
                var path = new Path
                {
                    Data = rectangleGeometry,
                    Stroke = Brushes.DarkRed,
                    StrokeThickness = 5
                };
                if (_buffPath == null)
                {
                    _buffPath = path;
                    canvas.Children.Add(_buffPath);
                }
                else
                {
                    Path single = canvas.Children.OfType<Path>()
                        .Single(path1 => path1 == _buffPath);
                    single.SetFrom(path);
                }

                _buffPath.SetXY(x, y);
            }
        }

        private void OutCanvas_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (IsMove && IsMouseButtonDown(e))
                _buffPosition = e.GetPosition((IInputElement) sender);
        }

        private void OutCanvas_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (IsMove && IsMouseButtonDown(e))
            {
                Point position = e.GetPosition((IInputElement) sender);
                Vector vector = position - _buffPosition;
                (double x, double y) = InCanvas.GetXY();
                InCanvas.SetXY(vector.X + x, vector.Y + y);
                _buffPosition = position;
            }
        }

        private void OutCanvas_OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (IsScale)
            {
                const double constScale = 0.005;
                if (e.Delta > 0)
                {
                    Scale += constScale;
                }
                else
                {
                    double d = Scale - constScale;
                    if (d > 0)
                        Scale = d;
                }

                InCanvas.RenderTransform = new ScaleTransform(Scale, Scale);
            }
        }

        private void ToggleButtons_OnClick(object sender, RoutedEventArgs e)
        {
            var toggleButton = sender as ToggleButton;
            WrapPanel wrapPanel = toggleButton.Parent as WrapPanel;
            foreach (ToggleButton button in wrapPanel.Children.OfType<ToggleButton>())
                if (button != toggleButton)
                    button.IsChecked = false;
        }

        private void 获取Image相对于InCanvas的位置Button_OnClick(object sender,
            RoutedEventArgs e)
        {
            double x = _image.RenderSize.Width;
            double y = _image.RenderSize.Height;
            ConsoleExtensions.ConsoleSplitLine(foregroundColor: ConsoleColor.DarkRed);
            _image.TranslatePoint(new Point(0, 0), InCanvas).WriteLine();
            _image.TranslatePoint(new Point(x, 0), InCanvas).WriteLine();
            _image.TranslatePoint(new Point(0, y), InCanvas).WriteLine();
            _image.TranslatePoint(new Point(x, y), InCanvas).WriteLine();
            ConsoleExtensions.ConsoleSplitLine(foregroundColor: ConsoleColor.DarkRed);
        }

        private void 获取InCanvas相对于OutCanvas的位置Button_OnClick(object sender,
            RoutedEventArgs e)
        {
            double x = InCanvas.RenderSize.Width;
            double y = InCanvas.RenderSize.Height;
            ConsoleExtensions.ConsoleSplitLine(foregroundColor: ConsoleColor.DarkRed);
            InCanvas.TranslatePoint(new Point(0, 0), OutCanvas).WriteLine();
            InCanvas.TranslatePoint(new Point(x, 0), OutCanvas).WriteLine();
            InCanvas.TranslatePoint(new Point(0, y), OutCanvas).WriteLine();
            InCanvas.TranslatePoint(new Point(x, y), OutCanvas).WriteLine();
            ConsoleExtensions.ConsoleSplitLine(foregroundColor: ConsoleColor.DarkRed);
        }

        private void 删除所有的roiButton_OnClick(object sender, RoutedEventArgs e)
        {
            foreach (Path path in InCanvas.Children.OfType<Path>().ToImmutableArray())
            {
                InCanvas.Children.Remove(path);
            }
        }

        private void 获取所有的roi与Image的相对位置Button_OnClick(object sender, RoutedEventArgs e)
        {
            ConsoleExtensions.ConsoleSplitLine(foregroundColor: ConsoleColor.Red);
            foreach (Path path in InCanvas.Children.OfType<Path>())
            {
                path.TranslatePoint(new Point(0, 0), _image).WriteLine();
            }

            ConsoleExtensions.ConsoleSplitLine(foregroundColor: ConsoleColor.Red);
        }

        private void 旋转所有的roiButton_OnClick(object sender, RoutedEventArgs e)
        {
            foreach (Path path in InCanvas.Children.OfType<Path>())
            {
                {
                    (double x, double y) valueTuple = path.GetXY();
                    valueTuple.WriteLine();
                }
                path.RenderTransformOrigin = new Point(0.5, 0.5);
                path.RenderTransform = new RotateTransform(30);
                {
                    (double x, double y) valueTuple = path.GetXY();
                    valueTuple.WriteLine();
                }
            }
        }
    }
}