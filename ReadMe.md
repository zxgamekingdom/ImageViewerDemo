# 使用仿射变换实现Image Viewer控件原理

WPF控件的属性`RenderTransform`为`System.Windows.Media.Transform`类型，该类的基本功能为实现二维平面的仿射变换（平移、旋转、缩放、倾斜的组合变换）。

`System.Windows.Media.Transform`的底层包含了一个`System.Windows.Media.Matrix`的结构体，该结构体表示的结构为3*3的仿射变换矩阵，并且该结构体还提供了一些方法，用于计算进行仿射变换后新的仿射变换矩阵。例如：平移`public void Translate (double offsetX, double offsetY);`、旋转`public void Rotate (double angle);`、倾斜`public void Skew (double skewX, double skewY);`、缩放`public void Scale (double scaleX, double scaleY);`。

首先通过调用WPF控件的`RenderTransform`的`Value`获取控件当前用于渲染的`System.Windows.Media.Matrix`，调用该`System.Windows.Media.Matrix`的各种仿射变换方法，然后构造一个新的`System.Windows.Media.MatrixTransform`类，并在`System.Windows.Media.MatrixTransform`类的构造函数中填入刚刚的`System.Windows.Media.Matrix`对象。并且将这个新构造的`System.Windows.Media.MatrixTransform`对象赋值给WPF控件的`RenderTransform`的属性。

通过调用控件的`public System.Windows.Point TranslatePoint (System.Windows.Point point, System.Windows.UIElement relativeTo);`方法得到本控件的某个坐标在其他控件的坐标是多少。