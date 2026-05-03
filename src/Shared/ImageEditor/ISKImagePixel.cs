using SkiaSharp;

namespace ZoDream.Shared.ImageEditor
{
    public interface ISKImagePixel
    {
        public void Paint(SKCanvas canvas, SKPoint point, SKPaint? paint = null);
        public void Paint(SKCanvas canvas, SKRect rect, SKPaint? paint = null);
        public void Paint(SKCanvas canvas, SKPoint[] sourceVertices, SKPoint[] vertices);
    }
}
