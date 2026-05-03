using SkiaSharp;
using System.Collections.Generic;
using System.Numerics;
using ZoDream.Shared.Drawing;
using ZoDream.Shared.Interfaces;
using ZoDream.Shared.Numerics;

namespace ZoDream.Shared.ImageEditor.Sources
{
    public class EllipseImageSource(IImageEditor editor) : BaseImageSource(editor)
    {
        private readonly ImagePaint _paint = ImagePaint.CreateBorder(Color.Black);
        public bool IsFill { get; set; }

        public SKColor FillColor { get; set; }
        public SKColor StrokeColor { get; set; }

        public float StrokeWidth { get; set; }
        public float XRadius => Width / 2;
        public float YRadius => Height / 2;


        public override object? CreateThumbnail(Size size)
        {
            return Thumbnail.Mutate(size, canvas => 
            {
                Paint(canvas);
            });
        }



        public override void Paint(IImageCanvas canvas)
        {
            _paint.Mutate(paint => {
                paint.StrokeWidth = StrokeWidth;
                paint.Style = SKPaintStyle.Stroke;
                paint.ColorF = SKColors.Transparent;
                if (IsFill)
                {
                    paint.Style = SKPaintStyle.StrokeAndFill;
                    paint.ColorF = SKColors.Black;
                }
            });
            
            var center = new Point(X + XRadius, Y + YRadius);
            if (XRadius == YRadius)
            {
                canvas.DrawCircle(center, XRadius, _paint);
                return;
            }
            canvas.DrawOval(center, new Vector2(XRadius, YRadius), _paint);
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

        public override void Paint(IImageStyleCanvas canvas, IImageStyle computedStyle)
        {
        }
    }
}
