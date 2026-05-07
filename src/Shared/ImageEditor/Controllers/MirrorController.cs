using System;
using System.Collections.Generic;
using System.Text;
using ZoDream.Shared.Interfaces;

namespace ZoDream.Shared.ImageEditor.Controllers
{
    public class MirrorController(IImageEditor editor) : ICommandController, IMouseState
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
