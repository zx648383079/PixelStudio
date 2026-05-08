using SkiaSharp;
using ZoDream.Shared.Drawing;
using ZoDream.Shared.Interfaces;
using ZoDream.Shared.Numerics;

namespace ZoDream.Shared.ImageEditor.Sources
{
    public class FillImageSource(IImageProperty editor) : IImageSource
    {
        public FillImageSource(SKColor color, IImageProperty editor)
            : this(editor)
        {
            Color = color.ToColor();
        }

        public FillImageSource(Color color, IImageProperty editor)
            : this(editor)
        {
            Color = color;
        }
        private readonly BitmapBuilder _thumbnail = new();
        public Color Color { get; set; }

        public Rect Bound => new(new Point(), editor.Size);

        public bool Contains(Point point)
        {
            return Bound.Contains(point);
        }

        public object? CreateThumbnail(Size size)
        {
            return _thumbnail.Mutate(size, canvas => {
                canvas.Clear(Color);
            });
        }



        public void Paint(IImageCanvas canvas)
        {
            canvas.Clear(Color);
        }

        public void Dispose()
        {
            _thumbnail.Dispose();
        }
    }
}
