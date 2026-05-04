using System;
using System.Collections.Generic;
using System.Text;
using ZoDream.Shared.Interfaces;

namespace ZoDream.Shared.ImageEditor.Controllers
{
    /// <summary>
    /// 擦除功能
    /// </summary>
    public class EraseController(IImageEditor editor) : ICommandController, IMouseState
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
