using SkiaSharp;
using ZoDream.Shared.Drawing;
using ZoDream.Shared.Interfaces;
using ZoDream.Shared.Numerics;

namespace ZoDream.Shared.ImageEditor
{
    public class DefaultImageOptions : IImageOptions
    {

        private readonly Color _background = SKColors.White.ToColor();
        private readonly Color _foreground = SKColors.Black.ToColor();
        private readonly Color _hovered = SKColors.Blue.WithAlpha(50).ToColor();
        private readonly Color _activated = SKColors.Blue.ToColor();
        private readonly SKFont _font = new(SKTypeface.Default, 16);

        private readonly SKPaint _jointStrokePaint = new()
        {
            Color = SKColors.Blue,
            StrokeWidth = 2,
            IsStroke = true,
            IsAntialias = true,
        };
        private readonly SKPaint _jointPaint = new()
        {
            ColorF = SKColors.White,
            Style = SKPaintStyle.Fill,
            IsAntialias = true,
        };
        private readonly SKPaint _jointHoveredPaint = new()
        {
            ColorF = SKColors.Blue.WithAlpha(50),
            Style = SKPaintStyle.Fill,
            IsAntialias = true,
        };

        private readonly SKPaint _backgroundPaint = new()
        {
            IsStroke = false,
            ColorF = SKColors.White,
            IsAntialias = true,
        };
        private readonly SKPaint _foregroundPaint = new()
        {
            IsStroke = false,
            ColorF = SKColors.Black,
            IsAntialias = true,
        };
        public IImagePaint JointStrokePaint => new ImagePaint(_jointStrokePaint);

        public IImagePaint JointPaint => new ImagePaint(_jointPaint);

        public IImagePaint JointHoveredPaint => new ImagePaint(_jointHoveredPaint);

        public float JointSize => 16;

        public float PixelSize => 1;

        public Color Hovered => _hovered;
        public Color Activated => _activated;

        public Color Background => _background;

        public IImagePaint BackgroundPaint => new ImagePaint(_backgroundPaint);

        public Color Foreground => _foreground;

        public IImagePaint ForegroundPaint => new ImagePaint(_foregroundPaint);

        public IImagePaint TitlePaint => new FontImagePaint(_font, SKTextAlign.Left, _foregroundPaint);

        public IImagePaint TextPaint => new FontImagePaint(_font, SKTextAlign.Left, _foregroundPaint);


        public IImagePaint CreateBorder(Color color, float strokeWidth = 1)
        {
            return ImagePaint.CreateBorder(color, strokeWidth);
        }
        public IImagePaint CreateFill(Color color)
        {
            return ImagePaint.CreateFill(color);
        }
        public IFontPaint CreateFont(Color color, int fontSize)
        {
            return new FontImagePaint(new(SKTypeface.Default, fontSize), 
                SKTextAlign.Left, new()
                {
                    IsStroke = false,
                    ColorF = color.ToColor(),
                    IsAntialias = true,
                });
        }

        public IThumbnailBuilder CreateThumbnail()
        {
            return new BitmapBuilder();
        }

        public void Dispose()
        {
            _font.Dispose();
            _jointPaint.Dispose();
            _jointHoveredPaint.Dispose();
            _jointStrokePaint.Dispose();
            _backgroundPaint.Dispose();
            _foregroundPaint.Dispose();
        }
    }
}
