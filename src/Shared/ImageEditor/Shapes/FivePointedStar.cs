using SkiaSharp;
using System;
using ZoDream.Shared.Drawing;
using ZoDream.Shared.Interfaces;
using ZoDream.Shared.Numerics;

namespace ZoDream.Shared.ImageEditor.Shapes
{
    public class FivePointedStar(IImageProperty option) : IImageSource
    {
        private readonly IThumbnailBuilder _thumbnail = option.Options.CreateThumbnail();
        public Rect Bound => throw new System.NotImplementedException();

        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

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
            var center = new Point(Width / 2, Height / 2);
            var radius = 0.45f * Math.Min(Width, Height);
            var path = new PathBuilder();
            path.MoveTo(Width / 2, Height / 2 - radius);

            for (int i = 1; i < 5; i++)
            {
                // angle from vertical
                double angle = i * 4 * Math.PI / 5;
                path.LineTo(center + new SKPoint(radius * (float)Math.Sin(angle),
                                                -radius * (float)Math.Cos(angle)));
            }
            path.Close();
            canvas.Draw(new PathBuffer(path), option.Options.JointPaint);
        }

        public void Dispose()
        {
        }
    }
}
