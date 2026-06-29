using SkiaSharp;
using System;
using ZoDream.Shared.Interfaces;

namespace ZoDream.Shared.ImageEditor
{
    public class SKPathBuffer(SKPath path) : IPathBuffer, ISKImagePixel, IDisposable
    {
  

        public void Paint(SKCanvas canvas, SKPoint point, SKPaint? paint = null)
        {
            canvas.DrawPath(path, paint);
        }

        public void Paint(SKCanvas canvas, SKRect rect, SKPaint? paint = null)
        {
            canvas.DrawPath(path, paint);
        }

        public void Paint(SKCanvas canvas, SKPoint[] sourceVertices, SKPoint[] vertices)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            path.Dispose();
        }
    }
}
