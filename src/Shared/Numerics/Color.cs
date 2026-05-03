using System;
using System.Globalization;
using System.Numerics;

namespace ZoDream.Shared.Numerics
{
    public struct Color : IEquatable<Color>
    {
        public static Color White => new(255, 255, 255, 255);
        public static Color Black => new(0, 0, 0, 255);

        public static Color Blue = new(0, 0, 255, 255);
        public static Color Red = new(255, 0, 0, 255);
        public static Color Green = new(0, 255, 0, 255);
        public static Color Transparent => new(16777215u);


        public byte A;
        public byte B;
        public byte G;
        public byte R;

        

        public Color()
        {
            
        }

        public Color(uint value)
        {
            R = (byte)((value >> 16) & 0xFF);
            G = (byte)((value >> 8) & 0xFF);
            B = (byte)(value & 0xFF);
            A = (byte)((value >> 24) & 0xFF);
        }

        public Color(byte r, byte g, byte b, byte a = 255)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public Color(float r, float g, float b, float a = 1)
        {
            R = (byte)(r * 255);
            G = (byte)(g * 255);
            B = (byte)(b * 255);
            A = (byte)(a * 255);
        }

        public override readonly string ToString()
        {
            return $"[{R},{G},{B},{A}]";
        }

        public override readonly int GetHashCode()
        {
            return HashCode.Combine(R, G, B, A);
        }

        public override readonly bool Equals(object? obj)
        {
            if (obj is Color o)
            {
                return Equals(o);
            }
            return base.Equals(obj);
        }

        public readonly bool Equals(Color other)
        {
            return other.R == R && other.G == G && other.B == B && other.A == A;
        }

        public static bool TryParse(string text, out Color color)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                color = new();
                return false;
            }

            var s = text.AsSpan().Trim().TrimStart('#');
            int length = s.Length;
            switch (length)
            {
                case 3:
                case 4:
                    {
                        byte result2;
                        if (length == 4)
                        {
                            if (!byte.TryParse(s.Slice(0, 1), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out result2))
                            {
                                color = new();
                                return false;
                            }

                            result2 = (byte)((result2 << 4) | result2);
                        }
                        else
                        {
                            result2 = byte.MaxValue;
                        }

                        if (!byte.TryParse(s.Slice(length - 3, 1), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var result3) || !byte.TryParse(s.Slice(length - 2, 1), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var result4) || !byte.TryParse(s.Slice(length - 1, 1), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var result5))
                        {
                            color = new();
                            return false;
                        }

                        color = new Color((byte)((result3 << 4) | result3), (byte)((result4 << 4) | result4), (byte)((result5 << 4) | result5), result2);
                        return true;
                    }
                case 6:
                case 8:
                    {
                        if (!uint.TryParse(s, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var result))
                        {
                            color = new();
                            return false;
                        }

                        color = new Color(result);
                        if (length == 6)
                        {
                            color.A = byte.MaxValue;
                        }

                        return true;
                    }
                default:
                    color = new();
                    return false;
            }
        }

        public readonly Color WithAlpha(byte a)
        {
            return new(R, G, B, a);
        }

        public static explicit operator Color(Vector3 vec)
        {
            return new(vec.X, vec.Y, vec.Z);
        }

        public static explicit operator Color(Vector4 vec)
        {
            return new(vec.X, vec.Y, vec.Z, vec.W);
        }

        public static explicit operator uint(Color color)
        {
            return (uint)((color.A << 24) | (color.R << 16) | (color.G << 8) | color.B);
        }

        public static explicit operator Color(uint value)
        {
            return new(value);
        }

        public static explicit operator Vector4(Color color)
        {
            return new((float)color.R / 255, (float)color.G / 255, (float)color.B / 255, (float)color.A / 255);
        }

        public static explicit operator Color(Vector3Int vec)
        {
            return new((byte)vec.X, (byte)vec.Y, (byte)vec.Z);
        }
    }
}
