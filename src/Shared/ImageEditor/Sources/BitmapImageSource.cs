using BitmapToVector;
using BitmapToVector.SkiaSharp;
using SkiaSharp;
using System.Collections.Generic;
using System.Linq;
using ZoDream.Shared.Drawing;
using ZoDream.Shared.Interfaces;
using ZoDream.Shared.Numerics;

namespace ZoDream.Shared.ImageEditor.Sources
{
    public class BitmapImageSource(SKBitmap bitmap) : BaseImageSource
    {


        private readonly SKPaint _paint = new()
        {
            Color = SKColors.White,
            IsAntialias = true
        };
        public SKBitmap Source => bitmap;


        public override Rect Bound => new(X, Y, Source.Width, Source.Height);

        public IList<IImageSource> Split(IEnumerable<ISpriteLayer> items)
        {
            using var paint = new SKPaint();
            return items.Select(item => {
                var bitmap = new SKBitmap((int)item.Width, (int)item.Height);
                using var canvas = new SKCanvas(bitmap);
                // canvas.Clear(SKColors.Transparent);
                canvas.DrawBitmap(Source, SKRect.Create(item.X, item.Y, item.Width, item.Height), SKRect.Create(0, 0, item.Width, item.Height), paint);
                return new BitmapImageSource(bitmap)
                {
                    X = item.X,
                    Y = item.Y,
                };
            }).ToArray();
        }

        public BitmapImageSource? Split(ISpriteLayer item)
        {
            if (item.Y < 0)
            {
                item.Y += Source.Height - item.Height;
            }
            if (item.X < 0)
            {
                item.X += Source.Width - item.Width;
            }
            var bitmap = Source.Clip(item);
            if (bitmap == null)
            {
                return null;
            }
            return new BitmapImageSource(bitmap)
            {
                X = item.X,
                Y = item.Y,
                Rotate = item.Rotate
            };
        }

        public override void Paint(IImageCanvas canvas)
        {
            if (Source is null)
            {
                return;
            }
            _paint.Color = SKColors.White.WithAlpha((byte)(255 * Opacity));
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

        public IEnumerable<PathBuilder> GetPath()
        {
            return PotraceSkiaSharp.Trace(new PotraceParam(), bitmap)
                .Select(i => PathBuilder.FromPath(i));
        }

        public override void Dispose()
        {
            Thumbnail.Dispose();
            _paint.Dispose();
            Source?.Dispose();
        }

  
    }
}
