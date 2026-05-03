using SkiaSharp;
using System.IO;
using System.Numerics;
using ZoDream.Shared.Interfaces;
using ZoDream.Shared.Numerics;

namespace ZoDream.Shared.ImageEditor
{
    public class BitmapBuffer(SKBitmap bitmap) : IImagePixel, IImageBuffer
    {

        private IImageCanvas? _canvas;

        protected IImageCanvas Canvas {
            get {
                _canvas ??= new ImageCanvas(new SKCanvas(bitmap));
                return _canvas;
            }
        }
        public Size Size => new(bitmap.Width, bitmap.Height);

        public object Picture {
            get {
                var recorder = new SKPictureRecorder();
                var canvas = recorder.BeginRecording(new SKRect(0, 0, bitmap.Width, bitmap.Height));
                canvas.DrawBitmap(bitmap, 0, 0);
                var picture = recorder.EndRecording();
                recorder.Dispose();
                return picture;
            }
        }

        public void Clear()
        {
            Canvas.Clear();
        }

        public void Clear(Color color)
        {
            Canvas.Clear(color);
        }

        public void Dispose()
        {
            _canvas?.Dispose();
            bitmap.Dispose();
        }

        public void Draw(IImagePixel source, Point point)
        {
            Canvas.Draw(source, point);
        }

        public void Draw(string text, Point point, IImagePaint paint)
        {
            Canvas.Draw(text, point, paint);
        }

        public void Draw(IPathBuffer path, IImagePaint paint)
        {
            Canvas.Draw(path, paint);
        }

        public void Draw(IImagePixel source, Point[] sourceVertices, Point[] vertices)
        {
            Canvas.Draw(source, sourceVertices, vertices);
        }

        public void DrawCircle(Point point, float radius, IImagePaint paint)
        {
            Canvas.DrawCircle(point, radius, paint);
        }

        public void DrawOval(Point point, Vector2 radius, IImagePaint paint)
        {
            Canvas.DrawOval(point, radius, paint);
        }

        public void DrawRect(Rect rect, IImagePaint paint)
        {
            Canvas.DrawRect(rect, paint);
        }

        public void DrawRect(RoundRect rect, IImagePaint paint)
        {
            Canvas.DrawRect(rect, paint);
        }

        public void SaveAs(Stream output)
        {
            bitmap.Encode(output, SKEncodedImageFormat.Png, 100);
        }

        public void SaveAs(string fileName)
        {
            using var fs = File.Create(fileName);
            SaveAs(fs);
        }

        public void Draw(IImagePixel source)
        {
            Canvas.Draw(source);
        }

        public void Draw(IImagePixel source, Rect rect)
        {
            Canvas.Draw(source, rect);
        }

        public void Draw(IImagePixel source, Rect rect, IImagePaint paint)
        {
            Canvas.Draw(source, rect, paint);
        }

        public void Draw(IImageBuffer source)
        {
            Canvas.Draw(source);
        }

        public void Draw(IImageBuffer source, IImagePaint paint)
        {
            Canvas.Draw(source, paint);
        }

        public void DrawLine(Point from, Point to, IImagePaint paint)
        {
            Canvas.DrawLine(from, to, paint);
        }
    }
}
