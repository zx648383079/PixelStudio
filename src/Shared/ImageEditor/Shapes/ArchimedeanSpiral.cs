using System;
using System.Collections.Generic;
using System.Text;
using ZoDream.Shared.Drawing;
using ZoDream.Shared.Interfaces;
using ZoDream.Shared.Numerics;

namespace ZoDream.Shared.ImageEditor.Shapes
{
    public class ArchimedeanSpiral(IImageProperty option) : IImageSource
    {
        private readonly IThumbnailBuilder _thumbnail = option.Options.CreateThumbnail();
        public Rect Bound => throw new System.NotImplementedException();

        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public int Angle { get; set; }

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
            var radius = Math.Min(center.X, center.Y);
            var path = new PathBuilder();
            for (float angle = 0; angle < Angle; angle += 1)
            {
                float scaledRadius = radius * angle / Angle;
                double radians = Math.PI * angle / 180;
                float x = center.X + scaledRadius * (float)Math.Cos(radians);
                float y = center.Y + scaledRadius * (float)Math.Sin(radians);
                var point = new Point(x, y);

                if (angle == 0)
                {
                    path.MoveTo(point);
                }
                else
                {
                    path.LineTo(point);
                }
            }
            canvas.Draw(new PathBuffer(path), option.Options.JointPaint);
        }

        public void Dispose()
        {
        }
    }
}
