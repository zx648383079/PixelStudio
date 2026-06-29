using SkiaSharp;
using System;
using ZoDream.Shared.Drawing;
using ZoDream.Shared.Interfaces;

namespace ZoDream.Shared.ImageEditor
{
    public class PathBuffer(PathBuilder builder) : IPathBuffer, ISKImagePixel
    {
        public void Paint(SKCanvas canvas, SKPoint point, SKPaint? paint = null)
        {
            canvas.DrawPath(builder.Build(), paint);
        }

        public void Paint(SKCanvas canvas, SKRect rect, SKPaint? paint = null)
        {
            canvas.DrawPath(builder.Build(), paint);
        }

        public void Paint(SKCanvas canvas, SKPoint[] sourceVertices, SKPoint[] vertices)
        {
            throw new NotImplementedException();
        }
    }
}
