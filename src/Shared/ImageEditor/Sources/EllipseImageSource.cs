using SkiaSharp;
using System.Collections.Generic;
using ZoDream.Shared.Drawing;
using ZoDream.Shared.Interfaces;

namespace ZoDream.Shared.ImageEditor.Sources
{
    public class EllipseImageSource(IImageEditor editor) : BaseImageSource(editor)
    {
        private readonly SKPaint _paint = new()
        {
            Color = SKColors.Black,
        };
        public bool IsFill { get; set; }

        public SKColor FillColor { get; set; }
        public SKColor StrokeColor { get; set; }

        public float StrokeWidth { get; set; }
        public float XRadius => Width / 2;
        public float YRadius => Height / 2;


        public override SKBitmap? CreateThumbnail(SKSize size)
        {
            return Thumbnail.Mutate(size, canvas => 
            {
                Paint(canvas);
            });
        }



        public override void Paint(IImageCanvas canvas)
        {
            _paint.StrokeWidth = StrokeWidth;
            _paint.Style = SKPaintStyle.Stroke;
            _paint.ColorF = SKColors.Transparent;
            if (IsFill)
            {
                _paint.Style = SKPaintStyle.StrokeAndFill;
                _paint.ColorF = SKColors.Black;
            }
            var center = new SKPoint(X + XRadius, Y + YRadius);
            if (XRadius == YRadius)
            {
                canvas.DrawCircle(center, XRadius, _paint);
                return;
            }
            canvas.DrawOval(center, new SKSize(XRadius, YRadius), _paint);
        }

        public IEnumerable<PathBuilder> GetPath()
        {
            var builder = new PathBuilder();
            builder.AddEllipse(Bound);
            yield return builder;
        }

        public override void Dispose()
        {
            _paint.Dispose();
            base.Dispose();
        }

        public override void Paint(IImageCanvas canvas, IImageStyle computedStyle)
        {
        }
    }
}
