using ZoDream.Shared.Numerics;

namespace ZoDream.Shared.Interfaces
{
    public interface IImageShell
    {
        public Size Size { get; }

        public IImageShellEventBus Bus { set; }

        public void Invalidate();
        public void Resize(Size size);
    }

    public interface IImageShellEventBus
    {
        public void OnPainting(ICanvasShell canvas);
        public void OnSizeChanged(Size size);
        public void OnTapped(Point point);

        public void OnPointerPressed(IMouseRoutedArgs args);

        public void OnPointerMoved(IMouseRoutedArgs args);

        public void OnPointerReleased(IMouseRoutedArgs args);
        public void OnKeyPressed(IKeyboardRoutedArgs args);

        public void OnKeyReleased(IKeyboardRoutedArgs args);
    }
}
