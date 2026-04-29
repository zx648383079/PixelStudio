using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using SkiaSharp;
using System.Windows.Input;
using ZoDream.Shared;
using ZoDream.Shared.Interfaces;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ZoDream.PixelStudio.Controls
{
    public sealed partial class ImageCanvasShell : UserControl, IImageShell
    {
        public ImageCanvasShell()
        {
            this.InitializeComponent();
            Loaded += ImageEditor_Loaded;
        }

        private readonly double _dpiScale = App.ViewModel.GetDpiScaleFactorFromWindow();
        private bool _booted = false;
        private bool _isPointerPressed;
        private bool _isPointerMoved;
        public IImageShellEventBus? Bus { set; private get; }

        public SKSize Size => new((int)(ActualWidth * _dpiScale), (int)(ActualHeight * _dpiScale));

        private void ImageEditor_Loaded(object sender, RoutedEventArgs e)
        {
            _booted = true;
            Bus?.OnSizeChanged(Size);
        }

        public ICommand SelectedCommand {
            get { return (ICommand)GetValue(SelectedCommandProperty); }
            set { SetValue(SelectedCommandProperty, value); }
        }

        

        // Using a DependencyProperty as the backing store for SelectedCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedCommandProperty =
            DependencyProperty.Register("SelectedCommand", typeof(ICommand), typeof(ImageCanvasShell), new PropertyMetadata(null));


        private void PART_Canvas_PaintSurface(object sender, SkiaSharp.Views.Windows.SKPaintSurfaceEventArgs e)
        {
            if (!_booted)
            {
                ImageEditor_Loaded(this, null);
            }
            Bus?.OnPainting(e.Surface.Canvas, e.Info);
        }


        private void PART_Canvas_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            var target = (UIElement)sender;
            var point = e.GetCurrentPoint(target);
            _isPointerPressed = true;
            _isPointerMoved = false;
            Bus?.OnPointerPressed(new ImageMouseRoutedArgs(
                new((float)(point.Position.X * _dpiScale), (float)(point.Position.Y * _dpiScale)),
                PointerState.Pressed,
                point,
                e.KeyModifiers
                ));
        }

        private void PART_Canvas_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            var target = (UIElement)sender;
            var point = e.GetCurrentPoint(target);
            _isPointerMoved = true;
            Bus?.OnPointerMoved(new ImageMouseRoutedArgs(
                new((float)(point.Position.X * _dpiScale), (float)(point.Position.Y * _dpiScale)),
                _isPointerPressed ? PointerState.PressedMoved : PointerState.ReleasedMoved,
                point,
                e.KeyModifiers
                ));

        }

        private void PART_Canvas_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            var target = (UIElement)sender;
            var point = e.GetCurrentPoint(target);

            Bus?.OnPointerReleased(new ImageMouseRoutedArgs(
                new((float)(point.Position.X * _dpiScale), (float)(point.Position.Y * _dpiScale)),
                _isPointerMoved ? PointerState.Released : PointerState.NotMovedReleased,
                point,
                e.KeyModifiers
                ));
            _isPointerPressed = false;
            _isPointerMoved = false;
        }

        private void PART_Canvas_ManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
        {
            _isPointerPressed = true;
            _isPointerMoved = false;
            Bus?.OnPointerPressed(
                new ImageMouseRoutedArgs(
                new((float)(e.Position.X * _dpiScale), (float)(e.Position.Y * _dpiScale)),
                PointerState.Pressed));
        }

        private void PART_Canvas_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            _isPointerMoved = true;
            Bus?.OnPointerPressed(new ImageMouseRoutedArgs(
                new((float)(e.Position.X * _dpiScale), (float)(e.Position.Y * _dpiScale)),
                _isPointerPressed ? PointerState.PressedMoved : PointerState.ReleasedMoved));
        }

        private void PART_Canvas_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {

            Bus?.OnPointerReleased(new ImageMouseRoutedArgs(
                new((float)(e.Position.X * _dpiScale), (float)(e.Position.Y * _dpiScale)),
                _isPointerMoved ? PointerState.Released : PointerState.NotMovedReleased
                ));
            _isPointerPressed = false;
            _isPointerMoved = false;
        }

        private void PART_Canvas_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            Bus?.OnKeyPressed(new ImageKeyboardRoutedArgs(e));
        }

        private void PART_Canvas_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            Bus?.OnKeyReleased(new ImageKeyboardRoutedArgs(e));
        }


        public void Invalidate()
        {
            PART_Canvas.Invalidate();
        }

        public void Resize(SKSize size)
        {
            ResizeWithControl((int)size.Width, (int)size.Height);
        }
        private void ResizeWithControl(int width, int height)
        {
            PART_Canvas.Width = width / _dpiScale;
            PART_Canvas.Height = height / _dpiScale;
            //Canvas.SetLeft(CanvasTarget, 0);
            //Canvas.SetTop(CanvasTarget, 0);
            //HScrollBar.Value = 0;
            //VScrollBar.Value = 0;
            //HScrollBar.Maximum = CanvasTarget.ActualWidth;
            //VScrollBar.Maximum = CanvasTarget.ActualHeight;
            //HScrollBar.Visibility = CanvasTarget.ActualWidth > ActualWidth ? Visibility.Visible : Visibility.Collapsed;
            //VScrollBar.Visibility = CanvasTarget.ActualHeight > ActualHeight ? Visibility.Visible : Visibility.Collapsed;
        }

    }
}
