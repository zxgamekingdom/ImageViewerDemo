using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RenderTransformImageViewerDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static readonly DependencyProperty IsEllipseProperty =
            DependencyProperty.Register(nameof(IsEllipse),
                typeof(bool),
                typeof(MainWindow),
                new PropertyMetadata(default(bool)));

        public static readonly DependencyProperty IsMoveAndScaleProperty =
            DependencyProperty.Register(nameof(IsMoveAndScale),
                typeof(bool),
                typeof(MainWindow),
                new PropertyMetadata(default(bool)));

        public static readonly DependencyProperty IsRectangleProperty =
            DependencyProperty.Register(nameof(IsRectangle),
                typeof(bool),
                typeof(MainWindow),
                new PropertyMetadata(default(bool)));

        private Point _buffPosition;
        private Shape _buffShape;
        private bool _isEllipse;
        private bool _isMoveAndScale;
        private bool _isRectangle;

        public MainWindow()
        {
            InitializeComponent();
        }

        public bool IsEllipse
        {
            get => (bool) GetValue(IsEllipseProperty);
            set => SetValue(IsEllipseProperty, value);
        }

        public bool IsMoveAndScale
        {
            get => (bool) GetValue(IsMoveAndScaleProperty);
            set => SetValue(IsMoveAndScaleProperty, value);
        }

        public bool IsRectangle
        {
            get => (bool) GetValue(IsRectangleProperty);
            set => SetValue(IsRectangleProperty, value);
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Ellipse[] ellipses = Canvas.Children.OfType<Ellipse>()
                .Where(ellipse => ellipse.Fill == Brushes.Black)
                .ToArray();
            foreach (Ellipse ellipsis in ellipses)
                Canvas.Children.Remove(ellipsis);
            foreach (Rectangle rectangle in Canvas.Children.OfType<Rectangle>()
                .ToHashSet())
            {
                Point[] points =
                {
                    new Point(0, 0),
                    new Point(0, rectangle.Height),
                    new Point(rectangle.Width, 0),
                    new Point(rectangle.Width, rectangle.Height),
                };
                foreach (Point point in points)
                {
                    Point translatePoint = rectangle.TranslatePoint(point, Canvas);
                    var ellipse = new Ellipse()
                    {
                        Width = 10,
                        Height = 10,
                        Fill = Brushes.Black,
                        Stroke = Brushes.GreenYellow,
                        StrokeThickness = 2
                    };
                    Matrix matrix = ellipse.RenderTransform.Value;
                    matrix.Translate(translatePoint.X - 5, translatePoint.Y - 5);
                    ellipse.RenderTransform = new MatrixTransform(matrix);
                    Canvas.Children.Add(ellipse);
                }

                Canvas.Children.Remove(rectangle);
                Canvas.Children.Add(rectangle);
            }

            foreach (Ellipse element in Canvas.Children.OfType<Ellipse>()
                .Where(ellipse => ellipse.Fill != Brushes.Black)
                .ToHashSet())
            {
                Point[] points =
                {
                    new Point(0, 0),
                    new Point(0, element.Height),
                    new Point(element.Width, 0),
                    new Point(element.Width, element.Height),
                };
                foreach (Point point in points)
                {
                    Point translatePoint = element.TranslatePoint(point, Canvas);
                    var ellipse = new Ellipse()
                    {
                        Width = 10,
                        Height = 10,
                        Fill = Brushes.Black,
                        Stroke = Brushes.GreenYellow,
                        StrokeThickness = 2
                    };
                    Matrix matrix = ellipse.RenderTransform.Value;
                    matrix.Translate(translatePoint.X - 5, translatePoint.Y - 5);
                    ellipse.RenderTransform = new MatrixTransform(matrix);
                    Canvas.Children.Add(ellipse);
                }

                Canvas.Children.Remove(element);
                Canvas.Children.Add(element);
            }
        }

        private void Image_OnLoaded(object sender, RoutedEventArgs e)
        {
            BitmapImage bitmapImage = new BitmapImage(new Uri(
                @"C:\Users\Taurus Zhou\Pictures\Saved Pictures\2c2864bc80e702b9f3f715a390782b18.jpg",
                UriKind.Absolute));
            int pixelWidth = bitmapImage.PixelWidth;
            int pixelHeight = bitmapImage.PixelHeight;
            Image.Width = pixelWidth;
            Image.Height = pixelHeight;
            Image.Source = bitmapImage;
        }

        private void ToggleButtons_OnClick(object sender, RoutedEventArgs e)
        {
            var toggleButton = (ToggleButton) sender;
            var wrapPanel = (WrapPanel) toggleButton.Parent;
            foreach (ToggleButton button in wrapPanel.Children.OfType<ToggleButton>()
                .ToHashSet()
                .Where(button => button != toggleButton))
            {
                button.IsChecked = false;
            }
        }

        private void UIElement_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (IsMoveAndScale)
            {
                var grid = (Grid) sender;
                if (grid.CaptureMouse() == false)
                {
                    throw new Exception("未能成功捕获鼠标");
                }

                Point position = e.GetPosition(grid);
                _buffPosition = position;
                _isMoveAndScale = true;
            }

            if (IsRectangle)
            {
                var grid = (Grid) sender;
                if (grid.CaptureMouse() == false)
                {
                    throw new Exception("未能成功捕获鼠标");
                }

                Point position = e.GetPosition(Canvas);
                _buffPosition = position;
                _isRectangle = true;
            }

            if (IsEllipse)
            {
                var grid = (Grid) sender;
                if (grid.CaptureMouse() == false)
                {
                    throw new Exception("未能成功捕获鼠标");
                }

                Point position = e.GetPosition(Canvas);
                _buffPosition = position;
                _isEllipse = true;
            }
        }

        private void UIElement_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (IsMoveAndScale && _isMoveAndScale)
            {
                Matrix matrix = Canvas.RenderTransform.Value;
                Point position = e.GetPosition((IInputElement) sender);
                Vector vector = position - _buffPosition;
                matrix.Translate(vector.X, vector.Y);
                Canvas.RenderTransform = new MatrixTransform(matrix);
                _buffPosition = position;
            }

            if (IsRectangle && _isRectangle)
            {
                Point position = e.GetPosition(Canvas);
                Vector vector = position - _buffPosition;
                var rect = new Rect(_buffPosition, vector);
                var rectangle = new Rectangle()
                {
                    Width = rect.Width,
                    Height = rect.Height,
                    Stroke = Brushes.Red,
                    StrokeThickness = 3
                };
                Matrix matrix = rectangle.RenderTransform.Value;
                matrix.Translate(rect.X, rect.Y);
                rectangle.RenderTransform = new MatrixTransform(matrix);
                if (_buffShape != null)
                {
                    Canvas.Children.Remove(_buffShape);
                }

                _buffShape = rectangle;
                Canvas.Children.Add(_buffShape);
            }

            if (IsEllipse && _isEllipse)
            {
                Point position = e.GetPosition(Canvas);
                Vector vector = position - _buffPosition;
                var ellipse = new Ellipse()
                {
                    Width = vector.Length * 2,
                    Height = vector.Length * 2,
                    Stroke = Brushes.Red,
                    StrokeThickness = 3
                };
                Matrix matrix = ellipse.RenderTransform.Value;
                matrix.Translate(_buffPosition.X - ellipse.Width / 2,
                    _buffPosition.Y - ellipse.Height / 2);
                ellipse.RenderTransform = new MatrixTransform(matrix);
                if (_buffShape != null)
                {
                    Canvas.Children.Remove(_buffShape);
                }

                _buffShape = ellipse;
                Canvas.Children.Add(_buffShape);
            }
        }

        private void UIElement_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (IsMoveAndScale)
            {
                Mouse.Capture(default);
                _isMoveAndScale = false;
            }

            if (IsRectangle)
            {
                Mouse.Capture(default);
                _isRectangle = false;
                _buffShape = default;
            }

            if (IsEllipse)
            {
                Mouse.Capture(default);
                _isEllipse = false;
                _buffShape = default;
            }
        }

        private void UIElement_OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (IsMoveAndScale)
            {
                double scale = e.Delta > 0 ? 1 + 0.1 : 1 - 0.1;
                Matrix matrix = Canvas.RenderTransform.Value;
                Point position = e.GetPosition((IInputElement) sender);
                matrix.ScaleAt(scale, scale, position.X, position.Y);
                Canvas.RenderTransform = new MatrixTransform(matrix);
            }
        }
    }
}