using ZoDream.Shared.ImageEditor.Layers;
using ZoDream.Shared.Interfaces;

namespace ZoDream.Shared.ImageEditor.Controllers
{
    public class ViewController(IImageEditor editor) : ICommandController
    {
        private readonly HighlightLayer _layer = new(editor) 
        {
            IsVisible = false
        };
        public bool IsEnabled => true;

        public void Initialize(IImageLayer[] items)
        {
            _layer.IsVisible = items.Length > 0;
            if (items.Length == 0)
            {
                return;
            }
            _layer.Initialize(items);
        }


        public void Paint(IImageCanvas canvas)
        {
            if (_layer.IsVisible)
            {
                _layer.Paint(canvas);
            }
        }

        public void Dispose()
        {
            _layer.Dispose();
        }


    }
}
