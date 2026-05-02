using SkiaSharp;
using System.Collections.Generic;
using ZoDream.Shared.Interfaces;
using ZoDream.Shared.Numerics;

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
        private ImageBuffer? _surface;

        public IList<int> HorizontalLines = [];
        public IList<int> VerticalLines = [];
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
            _surface.Clear();
        }

        public void Dispose()
        {
            _surface?.Dispose();
        }


    }
}
