using SkiaSharp;
using System.Collections.Generic;
using System.Linq;
using ZoDream.Shared.Numerics;

namespace ZoDream.Shared.Drawing
{
    public class PathSegment
    {
        public SKPathVerb Type { get; set; }
        public List<Point> Points { get; set; } = new(3);

        public PathSegment(SKPathVerb type, params Point[] points)
        {
            Type = type;
            Points.AddRange(points);
        }

        // 深拷贝
        public PathSegment Clone()
        {
            return new PathSegment(Type, Points.Select(p => new Point(p.X, p.Y)).ToArray());
        }
    }
}
