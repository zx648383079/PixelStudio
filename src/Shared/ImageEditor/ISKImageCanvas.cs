using SkiaSharp;
using ZoDream.Shared.Interfaces;

namespace ZoDream.Shared.ImageEditor
{
    public interface ISKImageCanvas
    {
        public void DrawBitmap(SKBitmap source);
        public void DrawBitmap(SKBitmap source, SKPoint point);

        public void DrawBitmap(SKBitmap source, SKRect rect);

        public void DrawBitmap(SKBitmap source, SKRect rect, SKPaint paint);

        public void DrawBitmap(SKBitmap source, IImageStyle style);

        public void DrawCircle(SKPoint center, float radius, SKPaint paint);

        public void DrawOval(SKPoint center, SKSize radius, SKPaint paint);

        public void DrawPath(SKPath path, SKPaint paint);

        public void DrawLine(SKPoint from, SKPoint to, SKPaint paint);

        public void DrawRect(SKRect rect, SKPaint paint);

        public void DrawRect(SKRoundRect rect, SKPaint paint);

        public void DrawSurface(SKSurface surface);

        public void DrawSurface(SKSurface surface, SKPoint point);

        public void DrawSurface(SKSurface surface, SKRect rect);

        public void DrawPicture(SKPicture picture, SKPoint point);

        public void DrawPicture(SKPicture picture, SKRect rect);

        public void DrawPicture(SKPicture picture, SKRect rect, SKPaint paint);
        public void DrawPicture(SKPicture picture, IImageStyle style);

        public void DrawText(string text, SKPoint point, SKTextAlign textAlign, SKFont font, SKPaint paint);

        public void DrawTexture(SKBitmap source, SKPoint[] sourceVertices, SKPoint[] vertices);


    }
}
