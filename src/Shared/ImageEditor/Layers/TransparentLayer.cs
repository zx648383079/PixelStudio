using SkiaSharp;
using ZoDream.Shared.Interfaces;
using ZoDream.Shared.Numerics;

namespace ZoDream.Shared.ImageEditor.Layers
{
    public class TransparentLayer : IImageSource, ICommandLayer
    {
        public TransparentLayer(IImageEditor editor)
        {
            _editor = editor;
            Invalidate();
        }

        private readonly IImageEditor _editor;
        private readonly int _gridSize = 10;
        private readonly ImagePaint _grayPaint = new(new SKPaint()
        {
            Color = SKColors.LightGray,
            Style = SKPaintStyle.Fill,
            StrokeWidth = 0,
        });

        private ImageBuffer? _surface;

        public bool IsVisible { get; set; } = true;

        public Rect Bound => new();

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

        private void RenderSurface()
        {
            var size = _editor.Size;
            if (size.IsEmpty)
            {
                return;
            }
            _surface = new(size);
            _surface.Clear(Color.White);
            var columnCount = (int)(size.Width / _gridSize) + 1;
            var rowCount = (int)(size.Height / _gridSize) + 1;
            for (var i = 0; i < columnCount; i++)
            {
                for (var j = 0; j < rowCount; j++)
                {
                    if ((i + j) % 2 == 0)
                    {
                        continue;
                    }
                    _surface.DrawRect(new Rect(i * _gridSize, j * _gridSize, _gridSize, _gridSize), _grayPaint);
                }
            }
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


        public void Dispose()
        {
            _surface?.Dispose();
            _grayPaint?.Dispose();
        }

        public bool Contains(Point point)
        {
            return false;
        }

        public object? CreateThumbnail(Size size)
        {
            return null;
        }
    }
}
