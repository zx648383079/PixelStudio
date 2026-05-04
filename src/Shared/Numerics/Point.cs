using System;
using System.Numerics;

namespace ZoDream.Shared.Numerics
{
    public readonly struct Point : IEquatable<Point>
    {
        public readonly float X;
        public readonly float Y;

        public readonly bool IsEmpty => X == 0 || Y == 0;

        public Point()
        {

        }

        public Point(float x, float y)
        {
            X = x;
            Y = y;
        }
        public override readonly bool Equals(object? obj)
        {
            return obj is Point rect && Equals(rect);
        }
        public readonly bool Equals(Point other)
        {
            return other.X == X && other.Y == Y;
        }

        public override readonly int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public override readonly string ToString()
        {
            return $"{{{X},{Y}}}";
        }

        public static bool operator ==(Point left, Point right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Point left, Point right)
        {
            return !(left == right);
        }

        public static Vector2 operator -(Point left, Point right)
        {
            return new(left.X - right.X, left.Y - right.Y);
        }

        public static Point operator +(Point left, Vector2 right)
        {
            return new(left.X + right.X, left.Y + right.Y);
        }

        public static explicit operator Point(Vector2 vec)
        {
            return new(vec.X, vec.Y);
        }

        public static explicit operator Vector2(Point rect)
        {
            return new(rect.X, rect.Y);
        }
    }
}
