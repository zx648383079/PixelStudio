using System;
using System.Numerics;

namespace ZoDream.Shared.Numerics
{
    public readonly struct Rect : IEquatable<Rect>
    {
        public readonly float X;
        public readonly float Y;
        public readonly float Width;
        public readonly float Height;

        public readonly Point Center => new(X + Width / 2, Y + Height / 2);

        public readonly float Left => X;
        public readonly float Top => Y;
        public readonly float Right => X + Width;
        public readonly float Bottom => Y + Height;

        public Rect()
        {
            
        }

        public Rect(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public Rect(Point point, Size size)
            : this(point.X, point.Y, size.Width, size.Height)
        {
            
        }
        public override readonly bool Equals(object? obj)
        {
            return obj is Rect rect && Equals(rect);
        }
        public readonly bool Equals(Rect other)
        {
            return other.X == X && other.Y == Y && other.Width == Width && other.Height == Height;
        }

        public override readonly int GetHashCode()
        {
            return HashCode.Combine(X, Y, Width, Height);
        }

        public override readonly string ToString()
        {
            return $"{{{X},{Y},{Width},{Height}}}";
        }

        public bool Contains(Point point)
        {
            if (point.X >= X && point.X < Right && point.Y>= Y)
            {
                return point.Y < Bottom;
            }

            return false;
        }
        /// <summary>
        /// 是否与矩形相交
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public bool IsIntersect(Rect rect)
        {
            if (X < rect.Right && Right > rect.X && Y < rect.Bottom)
            {
                return Bottom > rect.Y;
            }

            return false;
        }

        public static bool operator ==(Rect left, Rect right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Rect left, Rect right)
        {
            return !(left == right);
        }

        public static explicit operator Rect(Vector4 vec)
        {
            return new(vec.X, vec.Y, vec.Z, vec.W);
        }

        public static Thickness operator -(Rect left, Rect right)
        {
            var x = right.X - left.X;
            var y = right.Y - left.Y;
            return new(x, y,
                left.Width - right.Width - x,
                left.Height - right.Height - y);
        }

        public static explicit operator Vector4(Rect rect)
        {
            return new(rect.X, rect.Y, rect.Width, rect.Height);
        }

        public static explicit operator Rect(Quaternion vec)
        {
            return new(vec.X, vec.Y, vec.Z, vec.W);
        }

        public static explicit operator Quaternion(Rect rect)
        {
            return new(rect.X, rect.Y, rect.Width, rect.Height);
        }

    }
}
