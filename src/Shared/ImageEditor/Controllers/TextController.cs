using ZoDream.Shared.Interfaces;

namespace ZoDream.Shared.ImageEditor.Controllers
{
    /// <summary>
    /// 文字工具
    /// </summary>
    public class TextController(IImageEditor editor) : ICommandController, IMouseState, IKeyboardState
    {
        public bool IsEnabled => true;


        public void Initialize(IImageLayer[] items)
        {
        }

        public void Paint(IImageCanvas canvas)
        {
        }

        public void PointerMoved(IMouseRoutedArgs args)
        {
        }

        public void PointerPressed(IMouseRoutedArgs args)
        {
        }

        public void PointerReleased(IMouseRoutedArgs args)
        {
            editor.Service.AskTextAsync();
        }

        public void KeyPressed(IKeyboardRoutedArgs args)
        {
        }

        public void KeyReleased(IKeyboardRoutedArgs args)
        {
        }

        public void Dispose()
        {
        }


    }
}
