using SkiaSharp;
using ZoDream.Shared.Drawing;
using ZoDream.Shared.Interfaces;
using ZoDream.Shared.Numerics;

namespace ZoDream.PixelStudio.Controls
{
    public class ImageCanvasControl(SKCanvas canvas, SKImageInfo info) : ICanvasShell
    {
        public Size Size => new(info.Width, info.Height);

        public void Clear(Color color)
        {
            canvas.Clear(color.ToColor());
        }
    }
}
