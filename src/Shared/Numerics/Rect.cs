using System;
using System.Collections.Generic;
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

        public readonly bool IsEmpty => Width == 0 || Height == 0;

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

        public static Rect Create(Point start, Point end)
        {
            var left = start.X;
            var right = end.X;
            if (end.X < left)
            {
                left = end.X;
                right = start.X;
            }
            var top = start.Y;
            var bottom = end.Y;
            if (end.Y < top)
            {
                top = end.Y;
                bottom = start.Y;
            }
            return Create(left, top, right, bottom);
        }

        public static Rect Create(float left, float top, float right, float bottom)
        {
            return new(left, top, right - left, bottom - top);
        }

        public static Rect Create(IEnumerable<Rect> items)
        {
            var left = 0f;
            var right = 0f;
            var top = 0f;
            var bottom = 0f;
            var first = true;
            foreach (var item in items)
            {
                if (first)
                {
                    left = item.Left;
                    right = item.Right;
                    top = item.Top;
                    bottom = item.Bottom;
                    first = false;
                    continue;
                }
                left = Math.Min(left, item.Left);
                top = Math.Min(top, item.Top);
                right = Math.Max(right, item.Right);
                bottom = Math.Max(bottom, item.Bottom);
            }
            return Create(left, top, right, bottom);
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

        public static Rect operator +(Rect left, Vector2 right)
        {
            return new(left.X + right.X, left.Y + right.Y, left.Width, left.Height);
        }

        public static Rect operator -(Rect left, Vector2 right)
        {
            return new(left.X - right.X, left.Y - right.Y, left.Width, left.Height);
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
