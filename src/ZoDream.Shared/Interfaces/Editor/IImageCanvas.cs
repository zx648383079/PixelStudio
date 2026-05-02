using System;
using System.Numerics;
using ZoDream.Shared.Numerics;

namespace ZoDream.Shared.Interfaces
{
    public interface IImageCanvas : IDisposable
    {
        #region 基本功能

        /// <summary>
        /// 以透明背景
        /// </summary>
        public void Clear();
        /// <summary>
        /// 以指定颜色作为背景
        /// </summary>
        /// <param name="color"></param>
        public void Clear(Color color);
        public void Draw(IImagePixel source);
        public void Draw(IImagePixel source, Point point);
        public void Draw(IImagePixel source, Rect rect);
        public void Draw(IImagePixel source, Rect rect, IImagePaint paint);
        public void Draw(IImageBuffer source, IImagePaint paint);
        public void Draw(string text, Point point, IImagePaint paint);

        public void Draw(IPathBuffer path, IImagePaint paint);
        public void DrawLine(Point from, Point to, IImagePaint paint);
        /// <summary>
        /// 绘制纹理
        /// </summary>
        /// <param name="source">纹理图片</param>
        /// <param name="sourceVertices">纹理上的顶点</param>
        /// <param name="vertices">顶点对于的位置</param>
        public void Draw(IImagePixel source, Point[] sourceVertices, Point[] vertices);
        /// <summary>
        /// 画矩形
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="paint"></param>
        public void DrawRect(Rect rect, IImagePaint paint);
        /// <summary>
        /// 画圆角矩形
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="paint"></param>
        public void DrawRect(RoundRect rect, IImagePaint paint);
        /// <summary>
        /// 画圆
        /// </summary>
        /// <param name="center"></param>
        /// <param name="radius"></param>
        /// <param name="paint"></param>
        public void DrawCircle(Point center, float radius, IImagePaint paint);
        /// <summary>
        /// 画椭圆
        /// </summary>
        /// <param name="center"></param>
        /// <param name="radius"></param>
        /// <param name="paint"></param>
        public void DrawOval(Point center, Vector2 radius, IImagePaint paint);

        #endregion

        #region 与 Style 联动
        public IImageCanvas Transform(Vector2 offset);
        public void Mutate(IImageStyle style, Action<IImageCanvas> cb);
        public IImageStyle Compute(IImageLayer layer);
        public void Draw(IImagePixel source, IImageStyle style);
        public void Draw(string text, IImageStyle style, IImagePaint paint);
        #endregion
    }
}
