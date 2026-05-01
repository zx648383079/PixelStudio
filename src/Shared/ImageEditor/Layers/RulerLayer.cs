using SkiaSharp;
using System.Collections.Generic;
using ZoDream.Shared.Interfaces;

namespace ZoDream.Shared.ImageEditor.Layers
{
    /// <summary>
    /// 标尺
    /// </summary>
    /// <param name="editor"></param>
    public class RulerLayer : IImageSource, ICommandLayer
    {
        public RulerLayer(IImageEditor editor)
        {
            _editor = editor;
            Invalidate();
        }
        private readonly IImageEditor _editor;
        private SKSurface? _surface;

        public IList<int> HorizontalLines = [];
        public IList<int> VerticalLines = [];
        public bool IsVisible { get; set; } = true;
        public SKRect Bound => SKRect.Empty;

        public bool Contains(SKPoint point)
        {
            return false;
        }

        public SKBitmap? CreateThumbnail(SKSize size)
        {
            return null;
        }

        public void Resize(SKSize size)
        {
            Invalidate();
        }

        public void Invalidate()
        {
            _surface?.Dispose();
            _surface = null;
        }

        public void With(IImageLayer layer)
        {
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
            canvas.DrawSurface(_surface);
        }

        private void RenderSurface()
        {
            var size = _editor.Size;
            if (size.Width == 0 || size.Height == 0)
            {
                return;
            }
            var info = new SKImageInfo((int)size.Width, (int)size.Height);
            _surface = SKSurface.Create(info);
            var canvas = _surface.Canvas;
            canvas.Clear(SKColors.Transparent);

        }

        public void Dispose()
        {
            _surface?.Dispose();
        }


    }
}
