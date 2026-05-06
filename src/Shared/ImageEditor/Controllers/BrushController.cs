using ZoDream.Shared.Interfaces;

namespace ZoDream.Shared.ImageEditor.Controllers
{
    /// <summary>
    /// 画笔工具
    /// </summary>
    public class BrushController(IImageEditor editor) : ICommandController, IMouseState
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
        }

        public void Dispose()
        {
        }
    }
}
