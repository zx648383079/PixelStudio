using ZoDream.Shared.Numerics;

namespace ZoDream.Shared.Interfaces
{
    public interface ICanvasOption
    {
        public int Width { get; }

        public int Height { get; }

        public Color Foreground { get; }
        public Color Background { get; }

        public int StrokeWidth { get; }

        public IImagePaint CreateBorder(Color color, float strokeWidth = 1);
        public IImagePaint CreateFill(Color color);
        public IFontPaint CreateFont(Color color, int fontSize);
    }
}
