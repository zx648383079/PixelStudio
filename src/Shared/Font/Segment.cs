using System;
using System.Numerics;

namespace ZoDream.Shared.Font
{
    public interface IGlyphSegment : ICloneable
    {

    }

    public class LinearSegment(Vector2 point) : IGlyphSegment
    {
        public Vector2 Point { get; set; } = point;

        

        public static explicit operator LinearSegment(Vector2 point)
        {
            return new LinearSegment(point);
        }

        public object Clone()
        {
            return new LinearSegment(Point);
        }
    }

    public class QuadraticBezierSegment(Vector2 controlPoint, Vector2 toPoint) : IGlyphSegment
    {
        public Vector2 ControlPoint { get; set; } = controlPoint;
        public Vector2 ToPoint { get; set; } = toPoint;

        public object Clone()
        {
            return new QuadraticBezierSegment(ControlPoint, ToPoint);
        }

        public static explicit operator QuadraticBezierSegment(Vector4 rect)
        {
            return new QuadraticBezierSegment(
                new Vector2(rect.X, rect.Y), 
                new Vector2(rect.Z, rect.W));
        }
    }
    public class CubicBezierSegment(Vector2 controlPoint1, Vector2 controlPoint2, Vector2 toPoint) : IGlyphSegment
    {
        public Vector2 ControlPoint1 { get; set; } = controlPoint1;
        public Vector2 ControlPoint2 { get; set; } = controlPoint2;
        public Vector2 ToPoint { get; set; } = toPoint;

        public object Clone()
        {
            return new CubicBezierSegment(ControlPoint1, ControlPoint2, ToPoint);
        }

    }
}
