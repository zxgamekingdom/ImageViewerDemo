using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using ImageViewer.ImageViewerControl.Extensions;

namespace ImageViewer.ImageViewerControl.RoiControls.Adorner
{
    public abstract class RoiControlAdorner : System.Windows.Documents.Adorner
    {
        public static readonly DependencyPropertyKey ChildrenPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(Children),
                typeof(VisualCollection),
                typeof(RoiControlAdorner),
                new PropertyMetadata(default(VisualCollection)));

        public static readonly DependencyProperty ChildrenProperty =
            ChildrenPropertyKey.DependencyProperty;

        public VisualCollection Children => (VisualCollection) GetValue(ChildrenProperty);

        VisualCollection ChildrenKey
        {
            get => (VisualCollection) GetValue(ChildrenPropertyKey.DependencyProperty);
            set => SetValue(ChildrenPropertyKey, value);
        }

        protected override Visual GetVisualChild(int index)
        {
            return Children[index];
        }

        protected override int VisualChildrenCount => Children.Count;

        protected virtual Thumb MoveThumb { get; set; } = new Thumb()
        {
            Background = Brushes.DarkRed, Height = 25, Width = 25,
        };

        protected RoiControlAdorner(RoiControl adornedElement) : base(adornedElement)
        {
            ChildrenKey = new VisualCollection(this);
            MoveThumb.DragDelta += OnMoveThumbDragDelta;
            Children.Add(MoveThumb);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            Size desiredSize = AdornedElement.DesiredSize;
            MoveThumb.Arrange(new Rect(desiredSize.Width / 2 - MoveThumb.Width / 2,
                desiredSize.Height / 2 - MoveThumb.Height / 2,
                MoveThumb.Width,
                MoveThumb.Height));
            return base.ArrangeOverride(finalSize);
        }

        protected override Size MeasureOverride(Size constraint)
        {
            MoveThumb.Measure(constraint);
            return base.MeasureOverride(constraint);
        }

        protected virtual void OnMoveThumbDragDelta(object sender, DragDeltaEventArgs e)
        {
            (double x, double y) = AdornedElement.GetCanvasXY();
            double horizontalChange = e.HorizontalChange;
            double verticalChange = e.VerticalChange;
            (double newX, double newY) = (x + horizontalChange, y + verticalChange);
            AdornedElement.SetCanvasXY(newX, newY);
        }
    }
}