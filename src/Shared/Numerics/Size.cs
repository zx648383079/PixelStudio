using System;
using System.Numerics;

namespace ZoDream.Shared.Numerics
{
    public readonly struct Size : IEquatable<Size>
    {
        public readonly float Width;
        public readonly float Height;

        public readonly bool IsEmpty => Width == 0 || Height == 0;

        public Size()
        {

        }

        public Size(float width, float height)
        {
            Width = width;
            Height = height;
        }

        public override readonly bool Equals(object? obj)
        {
            return obj is Size rect && Equals(rect);
        }
        public readonly bool Equals(Size other)
        {
            return other.Width == Width && other.Height == Height;
        }

        public override readonly int GetHashCode()
        {
            return HashCode.Combine(Width, Height);
        }

        public override readonly string ToString()
        {
            return $"{{{Width},{Height}}}";
        }

        public static bool operator ==(Size left, Size right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Size left, Size right)
        {
            return !(left == right);
        }

        public static explicit operator Size(Vector2 vec)
        {
            return new(vec.X, vec.Y);
        }

        public static explicit operator Vector2(Size rect)
        {
            return new(rect.Width, rect.Height);
        }
    }
}
