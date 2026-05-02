using System;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace ZoDream.Shared.Numerics
{
    public readonly struct RoundRect : IEquatable<RoundRect>
    {
        public readonly float X;
        public readonly float Y;
        public readonly float Width;
        public readonly float Height;

        public readonly Vector2 LeftTopRadius;
        public readonly Vector2 LeftBottomRadius;
        public readonly Vector2 RightTopRadius;
        public readonly Vector2 RightBottomRadius;


        public RoundRect(float x, float y, float width, float height, float radius)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            RightBottomRadius = RightTopRadius = LeftBottomRadius = LeftTopRadius = new(radius, radius);
        }

        public RoundRect(Rect rect, float radius)
            : this(rect.X, rect.Y, rect.Width, rect.Height, radius)
        {
            
        }

        public RoundRect(Rect rect, float xRadius, float yRadius)
        {
            X = rect.X;
            Y = rect.Y;
            Width = rect.Width;
            Height = rect.Height;
            RightBottomRadius = RightTopRadius = LeftBottomRadius = LeftTopRadius = new(xRadius, yRadius);
        }

        public RoundRect(Rect rect, float leftTopRadius, float rightTopRadius,
            float rightBottomRadius, float leftBottomRadius)
        {
            X = rect.X;
            Y = rect.Y;
            Width = rect.Width;
            Height = rect.Height;
            RightBottomRadius = new(rightBottomRadius, rightBottomRadius);
            RightTopRadius = new(rightTopRadius, rightTopRadius);
            LeftBottomRadius = new(leftBottomRadius, leftBottomRadius);
            LeftTopRadius = new(leftTopRadius, leftTopRadius);
        }

        public override readonly bool Equals([NotNullWhen(true)] object? obj)
        {
            return obj is RoundRect rect && Equals(rect);
        }

        public readonly bool Equals(RoundRect other)
        {
            return other.X == X && other.Y == Y && other.Width == Width && other.Height == Height
                && other.LeftTopRadius == LeftTopRadius 
                && other.RightTopRadius == RightTopRadius
                && other.RightBottomRadius == RightBottomRadius
                && other.LeftBottomRadius == LeftBottomRadius;
        }

        public override readonly int GetHashCode()
        {
            var hash = new HashCode();
            hash.Add(X);
            hash.Add(Y);
            hash.Add(Width);
            hash.Add(Height);
            hash.Add(LeftTopRadius);
            hash.Add(RightTopRadius);
            hash.Add(RightBottomRadius);
            hash.Add(LeftBottomRadius);
            return hash.ToHashCode();
        }

        public override readonly string? ToString()
        {
            return base.ToString();
        }

        /// <summary>
        /// 判断半径是否有效
        /// </summary>
        /// <param name="radius"></param>
        public static bool IsRadius(Vector2 radius)
        {
            return radius.X > 0 && radius.Y > 0;
        }

        public static bool operator ==(RoundRect left, RoundRect right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(RoundRect left, RoundRect right)
        {
            return !(left == right);
        }

        public static explicit operator Rect(RoundRect rect)
        {
            return new(rect.X, rect.Y, rect.Width, rect.Height);
        }
    }
}
