
using ZoDream.Shared.Interfaces;

namespace ZoDream.Shared.ImageEditor.Sources
{
    public class FolderImageSource : BaseImageSource
    {
        public IImageLayer? Host {  get; set; }

        public override void Paint(IImageStyleCanvas canvas, IImageStyle computedStyle)
        {
            if (Host is null || !Host.IsVisible)
            {
                return; 
            }
            var c = canvas.Transform(new(X, Y));
            foreach (var item in Host.Children)
            {
                item.Paint(c);
            }
        }
    }
}
