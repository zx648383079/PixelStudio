using SkiaSharp;
using System.Numerics;
using ZoDream.Shared.Interfaces;
using ZoDream.Shared.Numerics;

namespace ZoDream.Shared.ImageEditor
{
    /// <summary>
    /// 绘制图片
    /// </summary>
    public class ImageBuffer : IImageBuffer
    {

        public ImageBuffer(Size size)
        {
            var info = new SKImageInfo((int)size.Width, (int)size.Height);
            _surface = SKSurface.Create(info);
            _canvas = new ImageCanvas(_surface.Canvas);
        }

        private readonly SKSurface _surface;
        private readonly IImageCanvas _canvas;

        public void Clear()
        {
            _canvas.Clear();
        }

        public void Clear(Color color)
        {
            _canvas.Clear(color);
        }

        public void Dispose()
        {
            _canvas.Dispose();
            _surface.Dispose();
        }

        public void Draw(IImagePixel source, Point point)
        {
            _canvas.Draw(source, point);
        }

        public void Draw(string text, Point point, IImagePaint paint)
        {
            _canvas.Draw(text, point, paint);
        }

        public void Draw(IPathBuffer path, IImagePaint paint)
        {
            _canvas.Draw(path, paint);
        }

        public void Draw(IImagePixel source, Point[] sourceVertices, Point[] vertices)
        {
            _canvas.Draw(source, sourceVertices, vertices);
        }

        public void DrawCircle(Point point, float radius, IImagePaint paint)
        {
            _canvas.DrawCircle(point, radius, paint);
        }

        public void DrawOval(Point point, Vector2 radius, IImagePaint paint)
        {
            _canvas.DrawOval(point, radius, paint);
        }

        public void DrawRect(Rect rect, IImagePaint paint)
        {
            _canvas.DrawRect(rect, paint);
        }

        public void DrawRect(RoundRect rect, IImagePaint paint)
        {
            _canvas.DrawRect(rect, paint);
        }
    }
}
