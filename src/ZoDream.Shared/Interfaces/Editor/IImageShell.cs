using ZoDream.Shared.Numerics;

namespace ZoDream.Shared.Interfaces
{
    public interface IImageShell
    {
        public Size Size { get; }
        /// <summary>
        /// 0-60/s
        /// </summary>
        public byte FPS { get; set; }

        public IImageShellEventBus Bus { set; }
        /// <summary>
        /// 当 FPS > 0, 手动无效
        /// </summary>
        public void Invalidate();
        public void Resize(Size size);
    }

    public interface IImageShellEventBus
    {
        public void OnPainting(ICanvasShell canvas, float delta);
        public void OnSizeChanged(Size size);
        public void OnTapped(Point point);

        public void OnPointerPressed(IMouseRoutedArgs args);

        public void OnPointerMoved(IMouseRoutedArgs args);

        public void OnPointerReleased(IMouseRoutedArgs args);
        public void OnKeyPressed(IKeyboardRoutedArgs args);

        public void OnKeyReleased(IKeyboardRoutedArgs args);
    }
}
