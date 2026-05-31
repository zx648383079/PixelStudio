using System;
using ZoDream.Shared.Interfaces;
using ZoDream.Shared.Numerics;

namespace ZoDream.Shared.PixelArt
{
    public class PointCommand(IImageProperty option) : IImageSource
    {
        private readonly IThumbnailBuilder _thumbnail = option.Options.CreateThumbnail();
        public Rect Bound => throw new System.NotImplementedException();

        public bool Contains(Point point)
        {
            return Bound.Contains(point);
        }

        public object? CreateThumbnail(Size size)
        {
            return _thumbnail.Mutate(size, canvas => {

            });
        }



        public void Paint(IImageCanvas canvas)
        {
        }

        public void Dispose()
        {
        }

        public static void DrawLine(IImageCanvas canvas,
            Point point,
            IImagePaint paint)
        {
            canvas.DrawRect(new Rect(point, new(0, 0)), paint);
        }

        public static Point Floor(Point point, int blockSize)
        {
            return Floor(point, blockSize, Point.Empty);
        }

        public static Point Floor(Point point, int blockSize, Point origin)
        {
            var x = (int)((point.X - origin.X) / blockSize) * blockSize + origin.X;
            var y = (int)((point.Y - origin.Y) / blockSize) * blockSize + origin.Y;
            if (x == point.X && y == point.Y)
            {
                return point;
            }
            return new Point(x, y);
        }

        public static Point Ceiling(Point point, int blockSize)
        {
            return Ceiling(point, blockSize, Point.Empty);
        }
        public static Point Ceiling(Point point, int blockSize, Point origin)
        {
            var x = (int)((point.X - origin.X + blockSize - 1) / blockSize) * blockSize + origin.X;
            var y = (int)((point.Y - origin.Y + blockSize - 1) / blockSize) * blockSize + origin.Y;
            if (x == point.X && y == point.Y)
            {
                return point;
            }
            return new Point(x, y);
        }
    }
}
