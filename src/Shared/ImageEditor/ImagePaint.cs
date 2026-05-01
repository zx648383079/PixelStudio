using SkiaSharp;
using ZoDream.Shared.Interfaces;

namespace ZoDream.Shared.ImageEditor
{
    public class ImagePaint(SKPaint paint) : IImagePaint
    {
        public virtual void Dispose()
        {
            paint.Dispose();
        }
    }

    public class FontImagePaint(SKFont font, SKTextAlign align,  SKPaint paint) : ImagePaint(paint)
    {
        public override void Dispose()
        {
            base.Dispose();
            font.Dispose();
        }
    }
}
