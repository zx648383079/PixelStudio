using System;
using ZoDream.Shared.Interfaces;

namespace ZoDream.Shared.ImageEditor.Controllers
{
    public class SelectAreaController(IImageEditor editor) : ICommandController, IMouseState
    {
        public bool IsEnabled => true;



        public void Initialize(IImageLayer? layer)
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
