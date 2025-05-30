﻿#pragma warning disable CS8981
global using floatv =
#if DOUBLE
    System.Double;
#else
	System.Single;
#endif

global using Maths =
#if DOUBLE
    System.Math;
#else
    System.MathF;
#endif
#pragma warning restore CS8981

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
                throw new ArgumentException($"{nameof(max)} must be greater than or equal to {nameof(min)}.", nameof(min));
            }

            return (random.NextDouble() * (max - min)) + min;
        }

        /// <summary>
        /// Returns a random floating-point number that is greater than or equal to <paramref name="min"/>, and less than <paramref name="max"/>.
        /// </summary>
        /// <param name="random"></param>
        /// <param name="min">The inclusive lower bound of the random number returned.</param>
        /// <param name="max">The exclusive upper bound of the random number returned. <paramref name="max"/> must be greater than or equal to <paramref name="min"/>.</param>
        public static float NextFloat(this Random random, float min, float max)
        {
            if (max < min)
            {
                throw new ArgumentException($"{nameof(max)} must be greater than or equal to {nameof(min)}.", nameof(min));
            }

            return ((float)random.NextDouble() * (max - min)) + min;
        }

        /// <summary>
        /// Returns a random floating-point number that is greater than or equal to <paramref name="min"/>, and less than <paramref name="max"/>.
        /// </summary>
        /// <param name="random"></param>
        /// <param name="min">The inclusive lower bound of the random number returned.</param>
        /// <param name="max">The exclusive upper bound of the random number returned. <paramref name="max"/> must be greater than or equal to <paramref name="min"/>.</param>
        public static floatv NextNumber(this Random random, floatv min, floatv max)
        {
            if (max < min)
            {
                throw new ArgumentException($"{nameof(max)} must be greater than or equal to {nameof(min)}.", nameof(min));
            }

            return ((floatv)random.NextDouble() * (max - min)) + min;
        }

        /// <summary>
        /// Returns a random floating-point number that is greater than or equal to 0.0, and less than 1.0.
        /// </summary>
        /// <param name="random"></param>
        public static float NextFloat(this Random random) => (float)random.NextDouble();

        /// <summary>
        /// Returns a random floating-point number that is greater than or equal to 0.0, and less than 1.0.
        /// </summary>
        /// <param name="random"></param>
        public static floatv NextNumber(this Random random) => (floatv)random.NextDouble();

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
        public static Vector2 NextVector2(this Random random, floatv scale)
        {
            return new Vector2(
                random.NextDouble() * scale,
                random.NextDouble() * scale);
        }
        public static Vector2 NextVector2(this Random random, Vector2 scale)
        {
            return new Vector2(
                random.NextDouble() * scale.X,
                random.NextDouble() * scale.Y);
        }
        public static Vector2 NextVector2(this Random random, floatv min, floatv max)
        {
            return new Vector2(
                (random.NextDouble() * (max - min)) + min,
                (random.NextDouble() * (max - min)) + min);
        }
        public static Vector2 NextVector2(this Random random, floatv minX, floatv maxX, floatv minY, floatv maxY)
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
        public static Vector3 NextVector3(this Random random, floatv scale)
        {
            return new Vector3(
                random.NextDouble() * scale,
                random.NextDouble() * scale,
                random.NextDouble() * scale);
        }
        public static Vector3 NextVector3(this Random random, Vector3 scale)
        {
            return new Vector3(
                random.NextDouble() * scale.X,
                random.NextDouble() * scale.Y,
                random.NextDouble() * scale.Z);
        }
        public static Vector3 NextVector3(this Random random, floatv min, floatv max)
        {
            return new Vector3(
                (random.NextDouble() * (max - min)) + min,
                (random.NextDouble() * (max - min)) + min,
                (random.NextDouble() * (max - min)) + min);
        }
        public static Vector3 NextVector3(this Random random, floatv minX, floatv maxX, floatv minY, floatv maxY, floatv minZ, floatv maxZ)
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
        public static Vector4 NextVector4(this Random random, floatv scale)
        {
            return new Vector4(
                random.NextDouble() * scale,
                random.NextDouble() * scale,
                random.NextDouble() * scale,
                random.NextDouble() * scale);
        }
        public static Vector4 NextVector4(this Random random, Vector4 scale)
        {
            return new Vector4(
                random.NextDouble() * scale.X,
                random.NextDouble() * scale.Y,
                random.NextDouble() * scale.Z,
                random.NextDouble() * scale.W);
        }
        public static Vector4 NextVector4(this Random random, floatv min, floatv max)
        {
            return new Vector4(
                (random.NextDouble() * (max - min)) + min,
                (random.NextDouble() * (max - min)) + min,
                (random.NextDouble() * (max - min)) + min,
                (random.NextDouble() * (max - min)) + min);
        }
        public static Vector4 NextVector4(this Random random, floatv minX, floatv maxX, floatv minY, floatv maxY, floatv minZ, floatv maxZ, floatv minW, floatv maxW)
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
        public static int Lerp(this int a, int b, floatv blend) => (int)(blend * (b - a)) + a;
        public static uint Lerp(this uint a, uint b, floatv blend) => (uint)(blend * (b - a)) + a;
        public static short Lerp(this short a, short b, floatv blend) => (short)((blend * (b - a)) + a);
        public static ushort Lerp(this ushort a, ushort b, floatv blend) => (ushort)((blend * (b - a)) + a);
        public static byte Lerp(this byte a, byte b, floatv blend) => (byte)((blend * (b - a)) + a);
        public static long Lerp(this long a, long b, floatv blend) => (long)(blend * (b - a)) + a;
        public static ulong Lerp(this ulong a, ulong b, floatv blend) => (ulong)(blend * (b - a)) + a;

        public static double InvLerp(this double value, double a, double b) => (value - a) / (b - a);
        public static float InvLerp(this float value, float a, float b) => (value - a) / (b - a);
        public static floatv InvLerp(this int value, int a, int b) => (floatv)(value - a) / (b - a);
        public static floatv InvLerp(this uint value, uint a, uint b) => (floatv)(value - a) / (b - a);
        public static floatv InvLerp(this short value, short a, short b) => (floatv)(value - a) / (b - a);
        public static floatv InvLerp(this ushort value, ushort a, ushort b) => (floatv)(value - a) / (b - a);
        public static floatv InvLerp(this byte value, byte a, byte b) => (floatv)(value - a) / (b - a);
        public static floatv InvLerp(this long value, long a, long b) => (floatv)(value - a) / (b - a);
        public static floatv InvLerp(this ulong value, ulong a, ulong b) => (floatv)(value - a) / (b - a);

        public static Colour Lerp(this Colour a, Colour b, floatv blend)
        {
            return new Colour(
                (byte)((blend * (b.R - a.R)) + a.R),
                (byte)((blend * (b.G - a.G)) + a.G),
                (byte)((blend * (b.B - a.B)) + a.B),
                (byte)((blend * (b.A - a.A)) + a.A));
        }
        public static Colour3 Lerp(this Colour3 a, Colour3 b, floatv blend)
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
