using SkiaSharp;
using ZoDream.Shared.Interfaces;
using ZoDream.Shared.Numerics;

namespace ZoDream.Shared.ImageEditor.Layers
{
    /// <summary>
    /// 参考图层
    /// </summary>
    public class ReferLayer : IImageSource, ICommandLayer
    {
        public ReferLayer(IImageEditor editor)
        {
            _editor = editor;
            Invalidate();
        }

        private readonly IImageEditor _editor;
        private readonly SKPaint _paint = new()
        {
            Color = SKColors.White,
            IsAntialias = true
        };
        private float _opacity = 1;

        public SKBitmap Source { get; set; }

        public float X { get; set; }
        public float Y { get; set; }
        public float Scale { get; set; } = 1;

        public float Opacity { get => _opacity;
            set {
                _opacity = value;
                _paint.Color = SKColors.White.WithAlpha((byte)(255 * value));
            }
        }
        public bool IsVisible { get; set; } = true;

        public Rect Bound => new();

        public void Resize(Size size)
        {
            Invalidate();
        }

        public void Initialize(IImageLayer[] items)
        {

        }

        public void Invalidate()
        {
        }


        public void Paint(IImageCanvas canvas)
        {
            if (Source is null)
            {
                return;
            }
            (canvas as ISKImageCanvas)?.Draw(Source, 
                SKRect.Create(X, Y, Source.Width * Scale, Source.Height * Scale),
                _paint);
        }

        public bool Contains(Point point)
        {
            return false;
        }

        public object? CreateThumbnail(Size size)
        {
            return Source?.CreateThumbnail(new SKSizeI((int)size.Width, (int)size.Height));
        }

        public void Dispose()
        {
            _paint.Dispose();
            Source?.Dispose();
        }
    }
}
