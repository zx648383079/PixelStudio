using ZoDream.Shared.Interfaces;

namespace ZoDream.Shared.ImageEditor.Controllers
{
    /// <summary>
    /// 文字工具
    /// </summary>
    public class TextController : ICommandController, IMouseState, IKeyboardState
    {
        public bool IsEnabled => throw new System.NotImplementedException();

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public void Initialize(IImageLayer? layer)
        {
            throw new System.NotImplementedException();
        }

        public void KeyPressed(IKeyboardRoutedArgs args)
        {
            throw new System.NotImplementedException();
        }

        public void KeyReleased(IKeyboardRoutedArgs args)
        {
            throw new System.NotImplementedException();
        }

        public void Paint(IImageCanvas canvas)
        {
            throw new System.NotImplementedException();
        }

        public void PointerMoved(IMouseRoutedArgs args)
        {
            throw new System.NotImplementedException();
        }

        public void PointerPressed(IMouseRoutedArgs args)
        {
            throw new System.NotImplementedException();
        }

        public void PointerReleased(IMouseRoutedArgs args)
        {
            throw new System.NotImplementedException();
        }
    }
}
