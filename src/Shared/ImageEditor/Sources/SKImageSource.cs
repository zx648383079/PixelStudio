using SkiaSharp;
using System.Threading;
using System.Threading.Tasks;
using ZoDream.Shared.Drawing;
using ZoDream.Shared.Interfaces;
using ZoDream.Shared.Numerics;

namespace ZoDream.Shared.ImageEditor.Sources
{
    public class SKImageSource(SKImage image) : BaseImageSource, ISplittableImage
    {


        private readonly SKPaint _paint = new()
        {
            Color = SKColors.White,
            IsAntialias = true
        };
        public SKImage Source => image;


        public override Rect Bound => new(X, Y, Source.Width, Source.Height);

        public async Task<SKPath[]> GetContourAsync(CancellationToken token = default)
        {
            return await new ImageContourTrace(true).GetContourAsync(Source, token);
        }

        public IImageSource? Split(SKPath path)
        {
            var bound = path.Bounds;
            if (bound.IsEmpty || bound.Width < 1 || bound.Height < 1)
            {
                return null;
            }
            var kid = SkiaExtension.Mutate((int)bound.Width, (int)bound.Height, canvas => {
                canvas.DrawImage(image, bound,
                   SKRect.Create(0, 0, bound.Width, bound.Height), SKSamplingOptions.Default);
                path.Offset(-bound.Left, -bound.Top);
                canvas.ClipPath(path, SKClipOperation.Difference);
                canvas.Clear();
            }); ;
            if (kid is null)
            {
                return null;
            }
            return new BitmapImageSource(
                kid)
            {
                X = (int)bound.Left,
                Y = (int)bound.Top
            };
        }

        public void CopyTo(SKCanvas canvas, SKRect source, SKRect dest, SKPaint? paint = null)
        {
            canvas.DrawImage(Source, source, dest, SKSamplingOptions.Default, paint);
        }

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
