using SkiaSharp;
using System;
using ZoDream.Shared.Interfaces;
using ZoDream.Shared.Numerics;

namespace ZoDream.Shared.ImageEditor
{
    public class BitmapBuilder : IDisposable
    {
        private SKBitmap? _instance;

        public SKBitmap Mutate(Size size, Action<IImageCanvas> action)
        {
            return Mutate(new SKSizeI((int)size.Width, (int)size.Height), action);
        }

        public SKBitmap Mutate(SKSizeI size, Action<IImageCanvas> action)
        {
            if (_instance is null || _instance.Width != size.Width || _instance.Height != size.Height)
            {
                _instance?.Dispose();
                _instance = new SKBitmap(size.Width, size.Height);
            }
            using var canvas = new SKCanvas(_instance);
            canvas.Clear(SKColors.Transparent);
            action?.Invoke(new ImageCanvas(canvas));
            return _instance;
        }

        public SKBitmap Snapshot(Size size, SKBitmap source)
        {
            return Mutate(size, canvas => {
                var scale = Math.Min(size.Width / source.Width, size.Height / source.Height);
                var w = source.Width * scale;
                var h = source.Height * scale;
                (canvas as ISKImageCanvas)?.DrawBitmap(source, new SKRect((size.Width - w) / 2, (size.Height - h) / 2, w, h));
            });
        }

        public SKBitmap Snapshot(Size size, SKPicture source)
        {
            return Mutate(size, canvas => {
                var scale = Math.Min(size.Width / source.CullRect.Width, size.Height / source.CullRect.Height);
                var w = source.CullRect.Width * scale;
                var h = source.CullRect.Height * scale;
                (canvas as ISKImageCanvas)?.DrawPicture(source, SKRect.Create((size.Width - w) / 2, (size.Height - h) / 2, w, h));
            });
        }

        public void Dispose()
        {
            _instance?.Dispose();
        }
    }
}
