using System.Numerics;
using ZoDream.Shared.Numerics;

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
        public void Clear(Color color);

        public void Draw(IImagePixel source, Point point);
        public void Draw(string text, Point point, IImagePaint paint);

        public void Draw(IPathBuffer path, IImagePaint paint);
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
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="radius"></param>
        /// <param name="paint"></param>
        public void DrawCircle(Point point, float radius, IImagePaint paint);
        /// <summary>
        /// 画椭圆
        /// </summary>
        /// <param name="point"></param>
        /// <param name="radius"></param>
        /// <param name="paint"></param>
        public void DrawOval(Point point, Vector2 radius, IImagePaint paint);
    }
}
