using SkiaSharp;

namespace ZoDream.Shared.Interfaces
{
    public interface IImageBuffer
    {
        /// <summary>
        /// 以透明背景
        /// </summary>
        public void Clear();
        /// <summary>
        /// 以指定颜色作为背景
        /// </summary>
        /// <param name="color"></param>
        public void Clear(SKColor color);

        public void Draw(SKBitmap source, float x, float y);
        public void Draw(SKSurface? surface, float x, float y);
        public void Draw(SKPicture? picture, float x, float y);
        public void Draw(string text, float x, float y, SKTextAlign textAlign, SKFont font, SKPaint paint);

        public void Draw(SKPath path, SKPaint paint);
        /// <summary>
        /// 绘制纹理
        /// </summary>
        /// <param name="source">纹理图片</param>
        /// <param name="sourceVertices">纹理上的顶点</param>
        /// <param name="vertices">顶点对于的位置</param>
        public void Draw(SKBitmap source, SKPoint[] sourceVertices, SKPoint[] vertices);
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
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="radius"></param>
        /// <param name="paint"></param>
        public void DrawCircle(float x, float y, float radius, SKPaint paint);
        /// <summary>
        /// 画椭圆
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="xRadius"></param>
        /// <param name="yRadius"></param>
        /// <param name="paint"></param>
        public void DrawOval(float x, float y, float xRadius, float yRadius, SKPaint paint);
    }
}
