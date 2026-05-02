using SkiaSharp;
using ZoDream.Shared.Drawing;
using ZoDream.Shared.ImageEditor;
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
        public IImageCanvas ToCanvas()
        {
            return new ImageCanvas(canvas);
        }
        public IImageCanvas ToCanvas(IImageStyler styler)
        {
            return new ImageStyleCanvas(canvas, styler);
        }
    }
}
