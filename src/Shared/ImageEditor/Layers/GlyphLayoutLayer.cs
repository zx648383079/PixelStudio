using SkiaSharp;
using System;
using ZoDream.Shared.Drawing;
using ZoDream.Shared.Interfaces;
using ZoDream.Shared.Numerics;

namespace ZoDream.Shared.ImageEditor.Layers
{
    /// <summary>
    /// 显示模具
    /// </summary>
    public class GlyphLayoutLayer : IImageSource, ICommandLayer
    {
        public GlyphLayoutLayer(IImageEditor editor)
        {
            _editor = editor;
            Invalidate();
        }

        private readonly IImageEditor _editor;
        private ImageBuffer? _surface;
        public bool IsVisible { get; set; } = true;
        public Rect Bound => new();

        public bool Contains(Point point)
        {
            return false;
        }

        public object? CreateThumbnail(Size size)
        {
            return null;
        }

        public void Resize(Size size)
        {
            Invalidate();
        }

        public void Initialize(IImageLayer[] items)
        {
        }


        public void Invalidate()
        {

            _surface?.Dispose();
            _surface = null;
        }

        public void Paint(IImageCanvas canvas)
        {
            if (_surface == null)
            {
                RenderSurface();
            }
            if (_surface == null)
            {
                return;
            }
            canvas.Draw(_surface);
        }

        private void RenderSurface()
        {
            var size = _editor.Size;
            if (size.Width == 0 || size.Height == 0)
            {
                return;
            }
            _surface = new(size);
            _surface.Clear(Color.Transparent);

            var width = Math.Min(400, Math.Min(size.Width, size.Height));
            Paint(_surface, new((size.Width - width) / 2, (size.Height - width) / 2, width, width));
        }

        private void Paint(IImageCanvas canvas, Rect rect)
        {
            var options = _editor.Options;
            var vBearingY = rect.Height / 5;
            var hBearingX = rect.Width / 8;

            var glyphWidth = rect.Width - hBearingX * 3;
            var glyphHeight = rect.Height - vBearingY * 1.5f;

            var hBearingY = glyphHeight / 1.5f;
            using var outlinePaint = new ImagePaint(new SKPaint()
            {
                IsStroke = true,
                StrokeWidth = 2,
                Color = options.Foreground.ToColor(), 
            });
            using var linePaint = new ImagePaint(new SKPaint()
            {
                IsStroke = true,
                StrokeWidth = 2,
                Color = options.Foreground.WithAlpha(40).ToColor(),
                PathEffect = SKPathEffect.CreateDash([10, 5], 0)
            });
            DrawVLine(canvas, new Point(rect.Left + hBearingX, rect.Top), rect.Height, linePaint);
            DrawVLine(canvas, new Point(rect.Right - hBearingX * 2, rect.Top), rect.Height, linePaint);

            DrawHLine(canvas, new Point(rect.Left, rect.Top + vBearingY + hBearingY), rect.Width, outlinePaint);
            DrawVLine(canvas, new Point(rect.Left + hBearingX + glyphWidth / 2, rect.Top), rect.Width, outlinePaint);


            DrawHLine(canvas, new Point(rect.Left, rect.Top + vBearingY), rect.Width, linePaint);
            DrawHLine(canvas, new Point(rect.Left, rect.Bottom - vBearingY / 2), rect.Width, linePaint);




            canvas.DrawRect(rect, outlinePaint);
        }

        private void DrawHLine(IImageCanvas canvas, Point point, float length, IImagePaint paint)
        {
            canvas.DrawLine(point, new Point(point.X + length, point.Y), paint);
        }

        private void DrawVLine(IImageCanvas canvas, Point point, float length, IImagePaint paint)
        {
            canvas.DrawLine(point, new Point(point.X, point.Y + length), paint);
        }

        public void Dispose()
        {
            _surface?.Dispose();
        }
    }
}
