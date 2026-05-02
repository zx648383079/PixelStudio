using System;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace ZoDream.Shared.Numerics
{
    public readonly struct Thickness : IEquatable<Thickness>
    {
        public readonly float Left;
        public readonly float Top;
        public readonly float Right;
        public readonly float Bottom;

        public Thickness()
        {
            
        }

        public Thickness(float distance)
        {
            Left = Top = Right = Bottom = distance;
        }

        public Thickness(float left, float top, float right, float bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            return obj is Thickness o && Equals(o);
        }

        public bool Equals(Thickness other)
        {
            return other.Left == Left && other.Top == Top
                && other.Right == Right && other.Bottom == Bottom;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Left, Top, Right, Bottom);
        }

        public override string ToString()
        {
            return $"{{{Left}, {Top}, {Right}, {Bottom}}}";
        }

        public static bool operator ==(Thickness left, Thickness right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Thickness left, Thickness right)
        {
            return !(left == right);
        }

        public static explicit operator Thickness(Vector4 vec)
        {
            return new(vec.X, vec.Y, vec.Z, vec.W);
        }

        public static explicit operator Vector4(Thickness rect)
        {
            return new(rect.Left, rect.Top, rect.Right, rect.Bottom);
        }

        public static explicit operator Rect(Thickness rect)
        {
            return new(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top);
        }

        public static explicit operator Thickness(Rect rect)
        {
            return new(rect.X, rect.Y, rect.X + rect.Width, rect.Y + rect.Height);
        }

        public static Rect operator -(Rect left, Thickness right)
        {
            return new(left.X + right.Left, left.Y + right.Top, 
                left.Width - right.Left - right.Right, 
                left.Height - right.Top - right.Bottom);
        }

        public static Rect operator +(Rect left, Thickness right)
        {
            return new(left.X - right.Left, left.Y - right.Top,
                left.Width + right.Left + right.Right,
                left.Height + right.Top + right.Bottom);
        }
    }
}
