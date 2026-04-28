
using ZoDream.Shared.Interfaces;

namespace ZoDream.Shared.ImageEditor.Sources
{
    public class FolderImageSource(IImageEditor editor) : BaseImageSource(editor)
    {
        public IImageLayer? Host {  get; set; }

        public override void Paint(IImageCanvas canvas, IImageStyle computedStyle)
        {
            if (Host is null || !Host.IsVisible)
            {
                return; 
            }
            var c = canvas.Transform(X, Y);
            foreach (var item in Host.Children)
            {
                item.Paint(c);
            }
        }
    }
}
