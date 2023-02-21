using System;

namespace Zene.Structs
{
    /// <summary>
    /// The class containing extensions included in the library.
    /// </summary>
    public static class StructExt
    {
        /// <summary>
        /// Returns a random floating-point number that is greater than or equal to <paramref name="min"/>, and less than <paramref name="max"/>.
        /// </summary>
        /// <param name="random"></param>
        /// <param name="min">The inclusive lower bound of the random number returned.</param>
        /// <param name="max">The exclusive upper bound of the random number returned. <paramref name="max"/> must be greater than or equal to <paramref name="min"/>.</param>
        public static double NextDouble(this Random random, double min, double max)
        {
            if (max < min)
            {
                throw new Exception($"{nameof(max)} must be greater than or equal to {nameof(min)}.");
            }

            return (random.NextDouble() * (max - min)) + min;
        }

        /// <summary>
        /// Returns a colour with random values for the RGB components.
        /// </summary>
        /// <remarks>
        /// Alpha has a value of 255.
        /// </remarks>
        /// <param name="random"></param>
        public static Colour NextColour(this Random random)
        {
            return new Colour(
                (byte)random.Next(0, 256),
                (byte)random.Next(0, 256),
                (byte)random.Next(0, 256));
        }
        /// <summary>
        /// Returns a colour with random values for the RGBA components.
        /// </summary>
        /// <param name="random"></param>
        public static Colour NextColourA(this Random random)
        {
            return new Colour(
                (byte)random.Next(0, 256),
                (byte)random.Next(0, 256),
                (byte)random.Next(0, 256),
                (byte)random.Next(0, 256));
        }

        /// <summary>
        /// Returns a colour with random values for the RGB components.
        /// </summary>
        /// <param name="random"></param>
        public static Colour3 NextColour3(this Random random)
        {
            return new Colour3(
                (byte)random.Next(0, 256),
                (byte)random.Next(0, 256),
                (byte)random.Next(0, 256));
        }
        /// <summary>
        /// Returns a colour in SDR stored as floats with random values for the RGB components.
        /// </summary>
        /// <param name="random"></param>
        public static ColourF3 NextColourF3(this Random random)
        {
            return new ColourF3(
                (float)random.NextDouble(),
                (float)random.NextDouble(),
                (float)random.NextDouble());
        }

        /// <summary>
        /// Returns a colour in SDR stored as floats with random values for the RGB components.
        /// </summary>
        /// <remarks>
        /// Alpha has a value of 1.0f.
        /// </remarks>
        /// <param name="random"></param>
        public static ColourF NextColourF(this Random random)
        {
            return new ColourF(
                (float)random.NextDouble(),
                (float)random.NextDouble(),
                (float)random.NextDouble());
        }
        /// <summary>
        /// Returns a colour in SDR stored as floats with random values for the RGBA components.
        /// </summary>
        /// <param name="random"></param>
        public static ColourF NextColourFA(this Random random)
        {
            return new ColourF(
                (float)random.NextDouble(),
                (float)random.NextDouble(),
                (float)random.NextDouble(),
                (float)random.NextDouble());
        }

        public static Vector2 NextVector2(this Random random)
        {
            return new Vector2(
                random.NextDouble(),
                random.NextDouble());
        }
        public static Vector2 NextVector2(this Random random, double scale)
        {
            return new Vector2(
                random.NextDouble() * scale,
                random.NextDouble() * scale);
        }
        public static Vector2 NextVector2(this Random random, double min, double max)
        {
            return new Vector2(
                (random.NextDouble() * (max - min)) + min,
                (random.NextDouble() * (max - min)) + min);
        }
        public static Vector2 NextVector2(this Random random, double minX, double maxX, double minY, double maxY)
        {
            return new Vector2(
                (random.NextDouble() * (maxX - minX)) + minX,
                (random.NextDouble() * (maxY - minY)) + minY);
        }

        public static Vector2I NextVector2I(this Random random)
        {
            return new Vector2I(
                random.Next(),
                random.Next());
        }
        public static Vector2I NextVector2I(this Random random, int min, int max)
        {
            return new Vector2I(
                random.Next(min, max),
                random.Next(min, max));
        }
        public static Vector2I NextVector2I(this Random random, int minX, int maxX, int minY, int maxY)
        {
            return new Vector2I(
                random.Next(minX, maxX),
                random.Next(minY, maxY));
        }

        public static Vector3 NextVector3(this Random random)
        {
            return new Vector3(
                random.NextDouble(),
                random.NextDouble(),
                random.NextDouble());
        }
        public static Vector3 NextVector3(this Random random, double scale)
        {
            return new Vector3(
                random.NextDouble() * scale,
                random.NextDouble() * scale,
                random.NextDouble() * scale);
        }
        public static Vector3 NextVector3(this Random random, double min, double max)
        {
            return new Vector3(
                (random.NextDouble() * (max - min)) + min,
                (random.NextDouble() * (max - min)) + min,
                (random.NextDouble() * (max - min)) + min);
        }
        public static Vector3 NextVector3(this Random random, double minX, double maxX, double minY, double maxY, double minZ, double maxZ)
        {
            return new Vector3(
                (random.NextDouble() * (maxX - minX)) + minX,
                (random.NextDouble() * (maxY - minY)) + minY,
                (random.NextDouble() * (maxZ - minZ)) + minZ);
        }

        public static Vector3I NextVector3I(this Random random)
        {
            return new Vector3I(
                random.Next(),
                random.Next(),
                random.Next());
        }
        public static Vector3I NextVector3I(this Random random, int min, int max)
        {
            return new Vector3I(
                random.Next(min, max),
                random.Next(min, max),
                random.Next(min, max));
        }
        public static Vector3I NextVector3I(this Random random, int minX, int maxX, int minY, int maxY, int minZ, int maxZ)
        {
            return new Vector3I(
                random.Next(minX, maxX),
                random.Next(minY, maxY),
                random.Next(minZ, maxZ));
        }

        public static Vector4 NextVector4(this Random random)
        {
            return new Vector4(
                random.NextDouble(),
                random.NextDouble(),
                random.NextDouble(),
                random.NextDouble());
        }
        public static Vector4 NextVector4(this Random random, double scale)
        {
            return new Vector4(
                random.NextDouble() * scale,
                random.NextDouble() * scale,
                random.NextDouble() * scale,
                random.NextDouble() * scale);
        }
        public static Vector4 NextVector4(this Random random, double min, double max)
        {
            return new Vector4(
                (random.NextDouble() * (max - min)) + min,
                (random.NextDouble() * (max - min)) + min,
                (random.NextDouble() * (max - min)) + min,
                (random.NextDouble() * (max - min)) + min);
        }
        public static Vector4 NextVector4(this Random random, double minX, double maxX, double minY, double maxY, double minZ, double maxZ, double minW, double maxW)
        {
            return new Vector4(
                (random.NextDouble() * (maxX - minX)) + minX,
                (random.NextDouble() * (maxY - minY)) + minY,
                (random.NextDouble() * (maxZ - minZ)) + minZ,
                (random.NextDouble() * (maxW - minW)) + minW);
        }

        public static Vector4I NextVector4I(this Random random)
        {
            return new Vector4I(
                random.Next(),
                random.Next(),
                random.Next(),
                random.Next());
        }
        public static Vector4I NextVector4I(this Random random, int min, int max)
        {
            return new Vector4I(
                random.Next(min, max),
                random.Next(min, max),
                random.Next(min, max),
                random.Next(min, max));
        }
        public static Vector4I NextVector4I(this Random random, int minX, int maxX, int minY, int maxY, int minZ, int maxZ, int minW, int maxW)
        {
            return new Vector4I(
                random.Next(minX, maxX),
                random.Next(minY, maxY),
                random.Next(minZ, maxZ),
                random.Next(minW, maxW));
        }

        public static double Lerp(this double a, double b, double blend) => (blend * (b - a)) + a;
        public static float Lerp(this float a, float b, float blend) => (blend * (b - a)) + a;
        public static int Lerp(this int a, int b, double blend) => (int)(blend * (b - a)) + a;
        public static uint Lerp(this uint a, uint b, double blend) => (uint)(blend * (b - a)) + a;
        public static short Lerp(this short a, short b, double blend) => (short)((blend * (b - a)) + a);
        public static ushort Lerp(this ushort a, ushort b, double blend) => (ushort)((blend * (b - a)) + a);
        public static byte Lerp(this byte a, byte b, double blend) => (byte)((blend * (b - a)) + a);
        public static long Lerp(this long a, long b, double blend) => (long)(blend * (b - a)) + a;
        public static ulong Lerp(this ulong a, ulong b, double blend) => (ulong)(blend * (b - a)) + a;

        public static Colour Lerp(this Colour a, Colour b, double blend)
        {
            return new Colour(
                (byte)((blend * (b.R - a.R)) + a.R),
                (byte)((blend * (b.G - a.G)) + a.G),
                (byte)((blend * (b.B - a.B)) + a.B),
                (byte)((blend * (b.A - a.A)) + a.A));
        }
        public static Colour3 Lerp(this Colour3 a, Colour3 b, double blend)
        {
            return new Colour3(
                (byte)((blend * (b.R - a.R)) + a.R),
                (byte)((blend * (b.G - a.G)) + a.G),
                (byte)((blend * (b.B - a.B)) + a.B));
        }
        public static ColourF Lerp(this ColourF a, ColourF b, float blend)
        {
            return new ColourF(
                (blend * (b.R - a.R)) + a.R,
                (blend * (b.G - a.G)) + a.G,
                (blend * (b.B - a.B)) + a.B,
                (blend * (b.A - a.A)) + a.A);
        }
        public static ColourF3 Lerp(this ColourF3 a, ColourF3 b, float blend)
        {
            return new ColourF3(
                (blend * (b.R - a.R)) + a.R,
                (blend * (b.G - a.G)) + a.G,
                (blend * (b.B - a.B)) + a.B);
        }
    }
}
