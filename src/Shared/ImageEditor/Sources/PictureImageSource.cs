using SkiaSharp;
using ZoDream.Shared.Drawing;
using ZoDream.Shared.Interfaces;
using ZoDream.Shared.Numerics;

namespace ZoDream.Shared.ImageEditor.Sources
{
    public class PictureImageSource(SKPicture image) : BaseImageSource
    {


        private readonly SKPaint _paint = new()
        {
            Color = SKColors.White,
            IsAntialias = true
        };
        public SKPicture Source => image;


        public override Rect Bound => new(X, Y, Source.CullRect.Width, Source.CullRect.Height);

        public override void Paint(IImageCanvas canvas)
        {
            if (Source is null)
            {
                return;
            }
            (canvas as ISKImageCanvas)?.Draw(Source,
                Bound.ToRect(),
                _paint);
        }

        public override void Paint(IImageStyleCanvas canvas, IImageStyle computedStyle)
        {
            (canvas as ISKImageCanvas)?.Draw(Source, computedStyle);
        }


        public override object? CreateThumbnail(Size size)
        {
            return Thumbnail.Snapshot(size, Source);
        }

        public override void Dispose()
        {
            Thumbnail.Dispose();
            _paint.Dispose();
            Source?.Dispose();
        }

  
    }
}
