using SkiaSharp;
using System;
using ZoDream.Shared.Interfaces;
using ZoDream.Shared.Numerics;

namespace ZoDream.Shared.ImageEditor
{
    public class BitmapBuilder : IThumbnailBuilder
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
                (canvas as ISKImageCanvas)?.Draw(source, Compute(source.Width, source.Height, size), null);
            });
        }

        public SKBitmap Snapshot(Size size, SKPicture source)
        {
            return Mutate(size, canvas => {
                (canvas as ISKImageCanvas)?.Draw(source, Compute(source.CullRect.Width, source.CullRect.Height, size), null);
            });
        }

        public SKBitmap Snapshot(Size size, SKImage source)
        {
            return Mutate(size, canvas => {
                (canvas as ISKImageCanvas)?.Draw(source, Compute(source.Width, source.Height, size), null);
            });
        }

        public SKBitmap Snapshot(Size size, SKSurface source)
        {
            return Mutate(size, canvas => {
                (canvas as ISKImageCanvas)?.Draw(source, Compute(source.Canvas.DeviceClipBounds.Width, source.Canvas.DeviceClipBounds.Height, size), null);
            });
        }

        private static SKRect Compute(float width, float height, Size toSize)
        {
            var scale = Math.Min(toSize.Width / width, toSize.Height / height);
            var w = width * scale;
            var h = height * scale;
            return SKRect.Create((toSize.Width - w) / 2, (toSize.Height - h) / 2, w, h);
        }

        public void Dispose()
        {
            _instance?.Dispose();
        }

        object IThumbnailBuilder.Mutate(Size size, Action<IImageCanvas> action)
        {
            return Mutate(size, action);
        }

    }
}
