using SkiaSharp;
using System.Numerics;
using ZoDream.Shared.Interfaces;
using ZoDream.Shared.Numerics;

namespace ZoDream.Shared.ImageEditor
{
    /// <summary>
    /// 绘制图片
    /// </summary>
    public class ImageBuffer : IImageBuffer, ISKImagePixel
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

        public void Draw(IImagePixel source)
        {
            _canvas.Draw(source);
        }

        public void Draw(IImagePixel source, Rect rect)
        {
            _canvas.Draw(source, rect);
        }

        public void Draw(IImagePixel source, Rect rect, IImagePaint paint)
        {
            _canvas.Draw(source, rect, paint);
        }

        public void Draw(IImageBuffer source)
        {
            _canvas.Draw(source);
        }

        public void Draw(IImageBuffer source, IImagePaint paint)
        {
            _canvas.Draw(source, paint);
        }

        public void DrawLine(Point from, Point to, IImagePaint paint)
        {
            _canvas.DrawLine(from, to, paint);
        }

        public void Paint(SKCanvas canvas, SKPoint point, SKPaint? paint = null)
        {
            canvas.DrawSurface(_surface, point, paint);
        }

        public void Paint(SKCanvas canvas, SKRect rect, SKPaint? paint = null)
        {
            Paint(canvas, new SKPoint(rect.Left, rect.Top), paint);
        }

        public void Paint(SKCanvas canvas, SKPoint[] sourceVertices, SKPoint[] vertices)
        {
            using var image = _surface.Snapshot();
            using var paint = new SKPaint()
            {
                IsAntialias = true,
                Shader = SKShader.CreateImage(image, SKShaderTileMode.Clamp, SKShaderTileMode.Clamp)
            };
            canvas.DrawVertices(SKVertexMode.TriangleFan,
                vertices,
                sourceVertices, null, paint);
        }
    }
}
