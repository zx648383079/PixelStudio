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
    }
}
