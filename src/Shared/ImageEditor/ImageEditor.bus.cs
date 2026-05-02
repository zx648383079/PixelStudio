using ZoDream.Shared.Interfaces;
using ZoDream.Shared.Numerics;

namespace ZoDream.Shared.ImageEditor
{
    public partial class ImageEditor : IImageShellEventBus
    {
        public void OnPainting(ICanvasShell canvas)
        {
            Paint(canvas);
        }

        public void OnSizeChanged(Size size)
        {
            Resize(size);
        }

        public void OnTapped(Point point)
        {
            Touch(point);
        }

        public void OnPointerPressed(IMouseRoutedArgs args)
        {
            if (Commander is IMouseState t)
            {
                t.PointerPressed(args);
            }
        }

        public void OnPointerMoved(IMouseRoutedArgs args)
        {
            if (Commander is IMouseState t)
            {
                t.PointerMoved(args);
            }
        }

        public void OnPointerReleased(IMouseRoutedArgs args)
        {
            if (Commander is IMouseState t)
            {
                t.PointerReleased(args);
            }
        }

        public void OnKeyPressed(IKeyboardRoutedArgs args)
        {
            if (Commander is IKeyboardState t)
            {
                t.KeyPressed(args);
            }
        }

        public void OnKeyReleased(IKeyboardRoutedArgs args)
        {
            if (Commander is IKeyboardState t)
            {
                t.KeyReleased(args);
            }
        }
    }
}
