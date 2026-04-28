using SkiaSharp;
using System;

namespace ZoDream.Shared.Interfaces
{
    public interface IImageCanvas
    {
        #region 基本功能
        public void DrawBitmap(SKBitmap source);
        public void DrawBitmap(SKBitmap source, SKPoint point);
        public void DrawBitmap(SKBitmap source, SKRect rect);
        public void DrawBitmap(SKBitmap source, SKRect rect, SKPaint paint);
        public void DrawSurface(SKSurface surface);
        public void DrawSurface(SKSurface surface, SKPoint point);

        public void DrawSurface(SKSurface surface, SKRect rect);

        public void DrawPicture(SKPicture picture, SKPoint point);
        public void DrawPicture(SKPicture picture, SKRect rect);
        public void DrawPicture(SKPicture picture, SKRect rect, SKPaint paint);
        public void DrawText(string text, SKPoint point, SKTextAlign textAlign, SKFont font, SKPaint paint);

        public void DrawPath(SKPath path, SKPaint paint);
        public void DrawLine(SKPoint from, SKPoint to, SKPaint paint);
        /// <summary>
        /// 绘制纹理
        /// </summary>
        /// <param name="source">纹理图片</param>
        /// <param name="sourceVertices">纹理上的顶点</param>
        /// <param name="vertices">顶点对于的位置</param>
        public void DrawTexture(SKBitmap source, SKPoint[] sourceVertices, SKPoint[] vertices);
        /// <summary>
        /// 画矩形
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="paint"></param>
        public void DrawRect(SKRect rect, SKPaint paint);
        /// <summary>
        /// 画圆角矩形
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="paint"></param>
        public void DrawRect(SKRoundRect rect, SKPaint paint);
        /// <summary>
        /// 画圆
        /// </summary>
        /// <param name="center"></param>
        /// <param name="radius"></param>
        /// <param name="paint"></param>
        public void DrawCircle(SKPoint center, float radius, SKPaint paint);
        /// <summary>
        /// 画椭圆
        /// </summary>
        /// <param name="center"></param>
        /// <param name="radius"></param>
        /// <param name="paint"></param>
        public void DrawOval(SKPoint center, SKSize radius, SKPaint paint);

        #endregion

        #region 与 Style 联动
        public IImageCanvas Transform(float x, float y);
        public void Mutate(IImageStyle style, Action<IImageCanvas> cb);
        public IImageStyle Compute(IImageLayer layer);
        public void DrawBitmap(SKBitmap source, IImageStyle style);
        public void DrawSurface(SKSurface surface, IImageStyle style);
        public void DrawPicture(SKPicture picture, IImageStyle style);
        public void DrawText(string text, IImageStyle style, SKTextAlign textAlign, SKFont font, SKPaint paint);
        #endregion
    }
}
