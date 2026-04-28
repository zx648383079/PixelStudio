using SkiaSharp;
using System.Collections.Generic;
using ZoDream.Shared.Drawing;
using ZoDream.Shared.Interfaces;

namespace ZoDream.Shared.ImageEditor.Sources
{
    public class TextImageSource(string text) : IImageSource
    {
        private readonly BitmapBuilder _thumbnail = new();
        private readonly SKPaint _paint = new()
        {
            Color = SKColors.Black,
            IsAntialias = true,
            IsStroke = false
        };

        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get; private set; }
        public float Height { get; private set; }
        public string Text { get; set; } = text;

        public SKTypeface? FontFamily { get; set; }

        public int FontSize { get; set; } = 16;
        public SKRect Bound => SKRect.Create(X, Y, Width, Height);

        public bool Contains(SKPoint point)
        {
            return Bound.Contains(point);
        }

        public SKBitmap? CreateThumbnail(SKSize size)
        {
            return _thumbnail.Mutate(size, canvas => 
            {
                Paint(canvas);
            });
        }



        public void Paint(IImageCanvas canvas)
        {
            using var font = new SKFont(FontFamily ?? SKTypeface.Default, FontSize);
            if (!font.ContainsGlyphs(Text))
            {
                // 乱码
                return;
            }
            var r = font.MeasureText(Text, out var bound, _paint);
            Width = bound.Width;
            Height = bound.Height + 3;
            canvas.DrawText(Text, new SKPoint(X, Y), SKTextAlign.Left, font, _paint);
        }

        public IEnumerable<PathBuilder> GetPath()
        {
            using var font = new SKFont(FontFamily ?? SKTypeface.Default, FontSize);
            if (!font.ContainsGlyphs(Text))
            {
                yield break;
            } else
            {
                yield return PathBuilder.FromPath(font.GetTextPath(Text));
            }
        }

        public void Dispose()
        {
            _paint.Dispose();
            _thumbnail.Dispose();
        }
    }
}
