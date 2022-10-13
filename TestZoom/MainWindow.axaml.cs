using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.Threading;

namespace TestZoom
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Button adjustWidthAndHeightButton = this.Find<Button>("AdjustWidthAndHeightButton");
            Button fixedWidthAndHeight = this.Find<Button>("FixedWidthAndHeightButton");
            Button fixedWidth = this.Find<Button>("FixedWidthButton");

            adjustWidthAndHeightButton.Click += AdjustWidthAndHeightButton_Click;
            fixedWidthAndHeight.Click += FixedWidthAndHeight_Click;
            fixedWidth.Click += FixedWidth_Click;
        }

        private void FixedWidth_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            FixedWidthDialog dialog = new FixedWidthDialog();
            dialog.Show();
        }

        private void FixedWidthAndHeight_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            FixedWidthAndHeightDialog dialog = new FixedWidthAndHeightDialog();
            dialog.Show();
        }

        private void AdjustWidthAndHeightButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            AdjustWidthAndHeightDialog dialog = new AdjustWidthAndHeightDialog();
            dialog.Show();
        }

        class BaseDialog : Window
        {
            internal BaseDialog()
            {
                RenderTransformOrigin = new RelativePoint(0, 0, RelativeUnit.Absolute);

                Content = CreateContent();

                CanResize = true;

                Dispatcher.UIThread.Post(() =>
                {
                    SetZoomLevel(1.5);
                }, DispatcherPriority.Layout - 1);
            }

            protected override Size MeasureOverride(Size availableSize)
            {
                if (!mIsScalingEnabled)
                    return base.MeasureOverride(availableSize);

                mIsScalingEnabled = false;
                Size unscaledResult = base.MeasureOverride(availableSize / mPrevZoomLevel);
                return unscaledResult * mZoomLevel;
            }

            protected override void OnKeyDown(KeyEventArgs e)
            {
                base.OnKeyDown(e);

                if (e.Key == Key.P)
                {
                    SetZoomLevel(mZoomLevel + 0.1);
                }
                else if (e.Key == Key.O)
                {
                    SetZoomLevel(mZoomLevel - 0.1);
                }
            }

            void SetZoomLevel(double zoomLevel)
            {
                mPrevZoomLevel = mZoomLevel;
                mZoomLevel = zoomLevel;
                this.RenderTransform = new ScaleTransform(mZoomLevel, mZoomLevel);

                mIsScalingEnabled = true;
                InvalidateMeasure();
            }

            private bool mIsScalingEnabled = false;
            protected double mPrevZoomLevel = 1;
            protected double mZoomLevel = 1;
        }

        class AdjustWidthAndHeightDialog : BaseDialog
        {
            internal AdjustWidthAndHeightDialog() : base()
            {
                SizeToContent = SizeToContent.WidthAndHeight;
            }
        }

        class FixedWidthAndHeightDialog : BaseDialog
        {
            internal FixedWidthAndHeightDialog() : base()
            {
                SizeToContent = SizeToContent.Manual;

                Width = 340;
                Height = 180;
            }
        }

        class FixedWidthDialog : BaseDialog
        {
            internal FixedWidthDialog()
            {
                SizeToContent = SizeToContent.Height;

                Width = 340;
            }
        }

        static LayoutTransformControl CreateLayout(Window window)
        {
            LayoutTransformControl result = new LayoutTransformControl();

            result.Child = CreateContent();

            return result;
        }

        static IControl CreateContent()
        {
            StackPanel content = new StackPanel();
            content.Margin = new Avalonia.Thickness(20);
            content.Spacing = 5;

            StackPanel panel1 = new StackPanel();
            panel1.Orientation = Avalonia.Layout.Orientation.Horizontal;
            panel1.Children.Add(new Button() { Content = "Button1" });
            panel1.Children.Add(new Button() { Content = "Button2" });
            panel1.Children.Add(new Button() { Content = "Button3" });
            panel1.Children.Add(new Button() { Content = "Button4" });
            panel1.Spacing = 5;

            StackPanel panel2 = new StackPanel();
            panel2.Orientation = Avalonia.Layout.Orientation.Horizontal;
            panel2.Children.Add(new Button() { Content = "Button1" });
            panel2.Children.Add(new Button() { Content = "Button2" });
            panel2.Children.Add(new Button() { Content = "Button3" });
            panel2.Children.Add(new Button() { Content = "Button4" });
            panel2.Spacing = 5;

            StackPanel panel3 = new StackPanel();
            panel3.Orientation = Avalonia.Layout.Orientation.Horizontal;
            panel3.Children.Add(new Button() { Content = "Button1" });
            panel3.Children.Add(new Button() { Content = "Button2" });
            panel3.Children.Add(new Button() { Content = "Button3" });
            panel3.Children.Add(new Button() { Content = "Button4" });
            panel3.Spacing = 5;

            StackPanel panel4 = new StackPanel();
            panel4.Orientation = Avalonia.Layout.Orientation.Horizontal;
            panel4.Children.Add(new Button() { Content = "Button1" });
            panel4.Children.Add(new Button() { Content = "Button2" });
            panel4.Children.Add(new Button() { Content = "Button3" });
            panel4.Children.Add(new Button() { Content = "Button4" });
            panel4.Spacing = 5;

            content.Children.Add(panel1);
            content.Children.Add(panel2);
            content.Children.Add(panel3);
            content.Children.Add(panel4);

            return content;
        }
    }
}
