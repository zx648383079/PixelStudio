using ZoDream.Shared.Numerics;

namespace ZoDream.Shared.Interfaces
{
    public interface ICanvasShell
    {
        public Size Size { get; }

        public void Clear(Color color);

        public IImageCanvas ToCanvas();

        public IImageStyleCanvas ToCanvas(IImageStyler styler);
    }
}
