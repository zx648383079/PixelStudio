using SkiaSharp;
using System.Collections.Generic;
using ZoDream.Shared.Drawing;
using ZoDream.Shared.Interfaces;

namespace ZoDream.Shared.ImageEditor.Sources
{
    public class TextImageSource(string text, IImageEditor editor) : BaseImageSource(editor)
    {
        private readonly SKPaint _paint = new()
        {
            Color = SKColors.Black,
            IsAntialias = true,
            IsStroke = false
        };

        public string Text { get; set; } = text;

        public SKColor Color { get; set; }
        public SKTypeface? FontFamily { get; set; }

        public int FontSize { get; set; } = 16;

        public override SKBitmap? CreateThumbnail(SKSize size)
        {
            return Thumbnail.Mutate(size, canvas => 
            {
                Paint(canvas);
            });
        }



        public override void Paint(IImageCanvas canvas)
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

        public override void Paint(IImageCanvas canvas, IImageStyle computedStyle)
        {
            Paint(canvas);
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

        public override void Dispose()
        {
            _paint.Dispose();
            base.Dispose();
        }

 
    }
}
