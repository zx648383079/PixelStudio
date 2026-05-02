using SkiaSharp;
using ZoDream.Shared.Interfaces;
using ZoDream.Shared.Numerics;

namespace ZoDream.Shared.ImageEditor.Layers
{
    public class HighlightLayer : IImageSource, ICommandLayer
    {
        public HighlightLayer(IImageEditor editor)
        {
            _editor = editor;
        }

        private readonly IImageEditor _editor;
        private ImageBuffer? _surface;
        private IImageLayer? _target;

        public bool IsVisible { get; set; }

        public Rect Bound => new();

        public void Resize(Size size)
        {
            Invalidate();
        }

        public void With(IImageLayer layer)
        {
            _target = layer;
            SyncSize();
            Invalidate();
        }

        public void Invalidate()
        {
            _surface?.Dispose();
            _surface = null;
        }

        private void SyncSize()
        {
            if (_target is null)
            {
                return;
            }
        }

        private void RenderSurface()
        {
            SyncSize();
            var size = _editor.Size;
            _surface = new(size);
            _surface.Clear();
            using var paint = new ImagePaint(new SKPaint()
            {
                Color = new SKColor(0, 0, 0, 150),
                Style = SKPaintStyle.Fill,
                StrokeWidth = 0,
            });
            var bound = _target.Source.Bound;
            if (bound.Left > 0)
            {
                _surface.DrawRect(new Rect(0, 0, bound.Left, size.Height), paint);
            }
            if (bound.Right < size.Width)
            {
                _surface.DrawRect(new Rect(bound.Right, 0, size.Width - bound.Right, size.Height), paint);
            }
            if (bound.Top > 0)
            {
                _surface.DrawRect(new Rect(bound.Left, 0, bound.Width, bound.Top), paint);
            }
            if (bound.Bottom < size.Height)
            {
                _surface.DrawRect(new Rect(bound.Left, bound.Bottom, bound.Width, size.Height - bound.Bottom), paint);
            }
        }

        public void Paint(IImageCanvas canvas)
        {
            if (_surface == null)
            {
                RenderSurface();
            }
            canvas.Draw(_surface);
        }

        public void Dispose()
        {
            _surface?.Dispose();
        }

        public bool Contains(SKPoint point)
        {
            return false;
        }

        public SKBitmap? CreateThumbnail(SKSize size)
        {
            return null;
        }
    }
}
