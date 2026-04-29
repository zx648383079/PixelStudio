using SkiaSharp;

namespace ZoDream.Shared.Interfaces
{
    public interface IImageShell
    {
        public SKSize Size { get; }

        public IImageShellEventBus Bus { set; }

        public void Invalidate();
        public void Resize(SKSize size);
    }

    public interface IImageShellEventBus
    {
        public void OnPainting(SKCanvas canvas, SKImageInfo info);
        public void OnSizeChanged(SKSize size);
        public void OnTapped(SKPoint point);

        public void OnPointerPressed(IMouseRoutedArgs args);

        public void OnPointerMoved(IMouseRoutedArgs args);

        public void OnPointerReleased(IMouseRoutedArgs args);
        public void OnKeyPressed(IKeyboardRoutedArgs args);

        public void OnKeyReleased(IKeyboardRoutedArgs args);
    }
}
