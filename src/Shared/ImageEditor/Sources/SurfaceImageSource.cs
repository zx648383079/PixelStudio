using SkiaSharp;
using ZoDream.Shared.Drawing;
using ZoDream.Shared.Interfaces;
using ZoDream.Shared.Numerics;

namespace ZoDream.Shared.ImageEditor.Sources
{
    public class SurfaceImageSource(SKSurface image, IImageEditor editor) : BaseImageSource(editor)
    {


        private readonly SKPaint _paint = new()
        {
            Color = SKColors.White,
            IsAntialias = true
        };
        public SKSurface Source => image;


        public override Rect Bound => new(0, 0, Source.Canvas.DeviceClipBounds.Width, Source.Canvas.DeviceClipBounds.Height);

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
