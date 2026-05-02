using System;

namespace ZoDream.Shared.Numerics
{
    public readonly struct Vector4Int : IEquatable<Vector4Int>
    {
        public readonly int X;
        public readonly int Y;
        public readonly int Z;
        public readonly int W;

        public Vector4Int()
        {
            
        }

        public Vector4Int(int x, int y, int z, int w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }


        public override readonly string ToString()
        {
            return $"{{{X},{Y},{Z},{W}}}";
        }

        public override readonly int GetHashCode()
        {
            return HashCode.Combine(X, Y, Z, W);
        }

        public override readonly bool Equals(object? obj)
        {
            if (obj is Vector4Int o)
            {
                return Equals(o);
            }
            return base.Equals(obj);
        }

        public readonly bool Equals(Vector4Int other)
        {
            return other.X == X && other.Y == Y && other.Z == Z;
        }
        public static bool operator ==(Vector4Int left, Vector4Int right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Vector4Int left, Vector4Int right)
        {
            return !(left == right);
        }
    }
}
