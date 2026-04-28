using ZoDream.Shared.Interfaces;

namespace ZoDream.Shared.ImageEditor
{
    internal class NormalImageStyler : IImageStyler
    {
        public string Name => ImageStyleManager.DefaultName;

        public IImageStyle Compute(IImageLayer layer)
        {
            if (layer.Source is IImageStyle style)
            {
                return new ImageComputedStyle(style, 0);
            }
            return new ImageComputedStyle();
        }
    }
}
