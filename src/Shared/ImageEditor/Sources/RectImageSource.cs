using SkiaSharp;
using System.Collections.Generic;
using ZoDream.Shared.Drawing;
using ZoDream.Shared.Interfaces;
using ZoDream.Shared.Numerics;

namespace ZoDream.Shared.ImageEditor.Sources
{
    public class RectImageSource(IImageEditor editor) : BaseImageSource(editor)
    {
        private readonly SKPaint _paint = new()
        {
            Color = SKColors.Black,
        };

        public SKColor FillColor { get; set; }
        public SKColor StrokeColor { get; set; }

        public bool IsFill { get; set; }
        public float StrokeWidth { get; set; }

        public float LeftRadius { get; set; }
        public float TopRadius { get; set; }
        public float RightRadius { get; set; }
        public float BottomRadius { get; set; }


        public override object? CreateThumbnail(Size size)
        {
            return Thumbnail.Mutate(size, canvas => {
                Paint(canvas);
            });
        }



        public void Paint(IImageCanvas canvas)
        {
            _paint.StrokeWidth = StrokeWidth;
            _paint.Style = SKPaintStyle.Stroke;
            _paint.ColorF = SKColors.Transparent;
            if (IsFill)
            {
                _paint.Style = SKPaintStyle.StrokeAndFill;
                _paint.ColorF = SKColors.Black;
            }
            if (LeftRadius == 0 && TopRadius == 0 && RightRadius == 0 && BottomRadius == 0)
            {
                canvas.DrawRect(Bound, _paint);
                return;
            }
            var rect = new RoundRect(Bound, LeftRadius, TopRadius, RightRadius, BottomRadius);
            canvas.DrawRect(rect, _paint);
        }

        public override void Paint(IImageCanvas canvas, IImageStyle computedStyle)
        {
        }

        public IEnumerable<PathBuilder> GetPath()
        {
            var builder = new PathBuilder();
            builder.AddRect(Bound, new SKPoint(LeftRadius, TopRadius),
                new SKPoint(RightRadius, TopRadius),
                new SKPoint(RightRadius, BottomRadius),
                new SKPoint(LeftRadius, BottomRadius));
            yield return builder;
        }

        public override void Dispose()
        {
            _paint.Dispose();
            base.Dispose();
        }

        
    }
}
