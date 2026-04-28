using ZoDream.Shared.Interfaces;

namespace ZoDream.Shared.ImageEditor
{
    public class RealImageStyler : IImageStyler
    {
        public string Name => ImageStyleManager.RealName;

        public IImageStyle Compute(IImageLayer layer)
        {
            return layer.Source is IImageStyle style ? style : new ImageComputedStyle();
        }
    }
}
