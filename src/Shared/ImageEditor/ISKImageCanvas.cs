using SkiaSharp;
using ZoDream.Shared.Interfaces;

namespace ZoDream.Shared.ImageEditor
{
    public interface ISKImageCanvas
    {
        public void Draw(SKBitmap source, SKPoint point);
        public void Draw(SKBitmap source, SKRect rect, SKPaint paint);

        public void Draw(SKBitmap source, IImageStyle style);

        public void Draw(SKImage source, SKPoint point);
        public void Draw(SKImage source, SKRect rect, SKPaint paint);
        public void Draw(SKImage source, IImageStyle style);

        public void Draw(SKSurface source, SKPoint point, SKPaint paint);
        public void Draw(SKSurface source, SKRect rect, SKPaint paint);

        public void Draw(SKSurface source, IImageStyle style);

        public void Draw(SKPicture source, SKPoint point);
        public void Draw(SKPicture source, SKRect rect, SKPaint paint);
        public void Draw(SKPicture source, IImageStyle style);

        public void DrawCircle(SKPoint center, float radius, SKPaint paint);

        public void DrawOval(SKPoint center, SKSize radius, SKPaint paint);

        public void DrawPath(SKPath path, SKPaint paint);

        public void DrawLine(SKPoint from, SKPoint to, SKPaint paint);

        public void DrawRect(SKRect rect, SKPaint paint);

        public void DrawRect(SKRoundRect rect, SKPaint paint);



        public void DrawText(string text, SKPoint point, SKTextAlign textAlign, SKFont font, SKPaint paint);

        public void Draw(SKBitmap source, SKPoint[] sourceVertices, SKPoint[] vertices);


    }
}
