using ZoDream.Shared.Interfaces;
using ZoDream.Shared.Numerics;

namespace ZoDream.Shared.ImageEditor
{
    /// <summary>
    /// 绘制图片
    /// </summary>
    public class ImageBuffer : IImageBuffer
    {
        public void Clear()
        {
        }

        public void Clear(Color color)
        {
        }

        public void Draw(IImagePixel source, Point point)
        {
        }

        public void Draw(string text, Point point, IImagePaint paint)
        {
        }

        public void Draw(IPathBuffer path, IImagePaint paint)
        {
        }

        public void Draw(IImagePixel source, Point[] sourceVertices, Point[] vertices)
        {
        }

        public void DrawCircle(Point point, float radius, IImagePaint paint)
        {
        }

        public void DrawOval(Point point, float xRadius, float yRadius, IImagePaint paint)
        {
        }

        public void DrawRect(Rect rect, IImagePaint paint)
        {
        }

        public void DrawRect(RoundRect rect, IImagePaint paint)
        {
        }
    }
}
