using System;
using ZoDream.Shared.Interfaces;

namespace ZoDream.Shared.ImageEditor.Controllers
{
    internal class SelectAreaController : ICommandController, IMouseState
    {
        public bool IsEnabled => throw new NotImplementedException();

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Initialize(IImageLayer? layer)
        {
            throw new NotImplementedException();
        }

        public void Paint(IImageCanvas canvas)
        {
            throw new NotImplementedException();
        }

        public void PointerMoved(IMouseRoutedArgs args)
        {
            throw new NotImplementedException();
        }

        public void PointerPressed(IMouseRoutedArgs args)
        {
            throw new NotImplementedException();
        }

        public void PointerReleased(IMouseRoutedArgs args)
        {
            throw new NotImplementedException();
        }
    }
}
