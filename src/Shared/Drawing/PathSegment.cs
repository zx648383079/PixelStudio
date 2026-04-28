using SkiaSharp;
using System.Collections.Generic;
using System.Linq;

namespace ZoDream.Shared.Drawing
{
    public class PathSegment
    {
        public SKPathVerb Type { get; set; }
        public List<SKPoint> Points { get; set; } = new(3);

        public PathSegment(SKPathVerb type, params SKPoint[] points)
        {
            Type = type;
            Points.AddRange(points);
        }

        // 深拷贝
        public PathSegment Clone()
        {
            return new PathSegment(Type, Points.Select(p => new SKPoint(p.X, p.Y)).ToArray());
        }
    }
}
