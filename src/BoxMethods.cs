using System;

namespace Zene.Structs
{
    public static class BoxMethods
    {
        /// <summary>
        /// Determines whether this <see cref="IBox"/> overlaps <paramref name="box"/>.
        /// </summary>
        /// <param name="box">The <see cref="IBox"/> to compare to.</param>
        public static bool Overlaps(this IBox iBox, IBox box)
        {
            return (iBox.Left < box.Right) &&
                (iBox.Right > box.Left) &&
                (iBox.Bottom < box.Top) &&
                (iBox.Top > box.Bottom);
        }
        /// <summary>
        /// Determines whether <paramref name="box"/> is inside this <see cref="IBox"/>.
        /// </summary>
        /// <param name="box">The <see cref="IBox"/> to compare to.</param>
        public static bool Contains(this IBox iBox, IBox box)
        {
            return (iBox.Left >= box.Right) &&
                (iBox.Right <= box.Left) &&
                (iBox.Bottom >= box.Top) &&
                (iBox.Top <= box.Bottom);
        }
        /// <summary>
        /// Determines whether <paramref name="point"/> is inside this <see cref="IBox"/>.
        /// </summary>
        /// <param name="point">The <see cref="Vector2"/> to compare to.</param>
        public static bool Contains(this IBox box, Vector2 point)
        {
            return (point.X >= box.Left) &&
                (point.X <= box.Right) &&
                (point.Y >= box.Bottom) &&
                (point.Y <= box.Top);
        }
        /// <summary>
        /// Determines whether <paramref name="point"/> is inside this <see cref="IBox"/>.
        /// </summary>
        /// <param name="point">The <see cref="Vector2I"/> to compare to.</param>
        public static bool Contains(this IBox box, Vector2I point)
        {
            return (point.X >= box.Left) &&
                (point.X <= box.Right) &&
                (point.Y >= box.Bottom) &&
                (point.Y <= box.Top);
        }

        /// <summary>
        /// Returns the smallest box possable that can contain <see cref="IBox"/> <paramref name="box"/> and <see cref="IBox"/> <paramref name="b"/>.
        /// </summary>
        /// <param name="box">The first box.</param>
        /// <param name="b">The second box.</param>
        /// <returns></returns>
        public static T Combine<T>(this T box, IBox b) where T : IBox, new()
        {
            return new T()
            {
                Left = Math.Min(box.Left, b.Left),
                Right = Math.Max(box.Right, b.Right),
                Bottom = Math.Min(box.Bottom, b.Bottom),
                Top = Math.Max(box.Top, b.Top)
            };
        }

        /// <summary>
        /// Sets <paramref name="box"/> to the smallest box possable that can contain <see cref="IBox"/> <paramref name="box"/> and <see cref="IBox"/> <paramref name="b"/>.
        /// </summary>
        /// <param name="box">The first box.</param>
        /// <param name="b">The second box.</param>
        /// <returns></returns>
        public static void Add(this IBox box, IBox b)
        {
            box.Left = Math.Min(box.Left, b.Left);
            box.Right = Math.Max(box.Right, b.Right);
            box.Bottom = Math.Min(box.Bottom, b.Bottom);
            box.Top = Math.Max(box.Top, b.Top);
        }

        /// <summary>
        /// Returns the combined volume of <paramref name="box"/> and <paramref name="b"/>.
        /// </summary>
        /// <param name="box">The first box.</param>
        /// <param name="b">The second box.</param>
        /// <returns></returns>
        public static double CombineVolume(this IBox box, IBox b)
        {
            return (Math.Max(box.Right, b.Right) - Math.Min(box.Left, b.Left)) *
                (Math.Max(box.Top, b.Top) - Math.Min(box.Bottom, b.Bottom));
        }

        /// <summary>
        /// Clamps <paramref name="box"/> to the bounds of <paramref name="bounds"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="box">The box to clamp.</param>
        /// <param name="bounds">The constricting bounds.</param>
        /// <returns></returns>
        public static T Clamp<T>(this T box, IBox bounds) where T : IBox, new()
        {
            return new T()
            {
                Left = Math.Max(box.Left, bounds.Left),
                Right = Math.Min(box.Right, bounds.Right),
                Bottom = Math.Max(box.Bottom, bounds.Bottom),
                Top = Math.Min(box.Top, bounds.Top)
            };
        }

        /// <summary>
        /// Shifts <see cref="IBox"/> <paramref name="box"/> by <paramref name="offset"/>.
        /// </summary>
        /// <param name="box">The box to offset.</param>
        /// <param name="offset">The distance to offset.</param>
        /// <returns></returns>
        public static T Shifted<T>(this T box, Vector2 offset) where T : IBox, new()
        {
            return new T()
            {
                Left = box.Left + offset.X,
                Right = box.Right + offset.X,
                Bottom = box.Bottom + offset.Y,
                Top = box.Top + offset.Y
            };
        }
        /// <summary>
        /// Shifts <see cref="IBox"/> <paramref name="box"/> by <paramref name="offset"/>.
        /// </summary>
        /// <param name="box">The box to offset.</param>
        /// <param name="offset">The distance to offset.</param>
        /// <returns></returns>
        public static void Shift<T>(this T box, Vector2 offset) where T : class, IBox
        {
            box.Left += offset.X;
            box.Right += offset.X;
            box.Top += offset.Y;
            box.Bottom += offset.Y;
        }

        /// <summary>
        /// Shifts <see cref="IBox"/> <paramref name="box"/> by <paramref name="offset"/>.
        /// </summary>
        /// <param name="box">The box to offset.</param>
        /// <param name="offset">The distance to offset.</param>
        /// <returns></returns>
        public static void Shift<T>(this ref T box, Vector2 offset) where T : struct, IBox
        {
            box.Left += offset.X;
            box.Right += offset.X;
            box.Top += offset.Y;
            box.Bottom += offset.Y;
        }

        /// <summary>
        /// Extend each side of a box by a value.
        /// </summary>
        /// <param name="value">The value to extend by</param>
        public static T Expanded<T>(this T box, Vector2 value) where T : IBox, new()
        {
            return new T()
            {
                Left = box.Left - value.X,
                Right = box.Right + value.X,
                Bottom = box.Bottom - value.Y,
                Top = box.Top + value.Y
            };
        }
        /// <summary>
        /// Extend each side of a box by a value.
        /// </summary>
        /// <param name="value">The value to extend by</param>
        public static void Expand<T>(this T box, Vector2 value) where T : class, IBox
        {
            box.Left -= value.X;
            box.Right += value.X;
            box.Bottom -= value.Y;
            box.Top += value.Y;
        }
        /// <summary>
        /// Extend each side of a box by a value.
        /// </summary>
        /// <param name="value">The value to extend by</param>
        public static void Expand<T>(this ref T box, Vector2 value) where T : struct, IBox
        {
            box.Left -= value.X;
            box.Right += value.X;
            box.Bottom -= value.Y;
            box.Top += value.Y;
        }

        /// <summary>
        /// Extend each side of a box by a value.
        /// </summary>
        /// <param name="value">The value to extend by</param>
        public static T Expanded<T>(this T box, double value) where T : IBox, new()
        {
            return new T()
            {
                Left = box.Left - value,
                Right = box.Right + value,
                Bottom = box.Bottom - value,
                Top = box.Top + value
            };
        }
        /// <summary>
        /// Extend each side of a box by a value.
        /// </summary>
        /// <param name="value">The value to extend by</param>
        public static void Expand<T>(this T box, double value) where T : class, IBox
        {
            box.Left -= value;
            box.Right += value;
            box.Bottom -= value;
            box.Top += value;
        }
        /// <summary>
        /// Extend each side of a box by a value.
        /// </summary>
        /// <param name="value">The value to extend by</param>
        public static void Expand<T>(this ref T box, double value) where T : struct, IBox
        {
            box.Left -= value;
            box.Right += value;
            box.Bottom -= value;
            box.Top += value;
        }

        /// <summary>
        /// Determines whether <paramref name="box"/> intersects the path of <see cref="Line2"/> <paramref name="line"/>.
        /// </summary>
        /// <param name="line">The line to compare to</param>
        /// <returns></returns>
        public static bool Intersects(this IBox box, Line2 line)
        {
            Vector2 dist = box.Centre.Relative(line);

            // Half of height
            double hh = box.Height / 2;
            // Half of width
            double hw = box.Width / 2;

            return ((dist.Y + hh >= 0) && (dist.Y - hh <= 0)) ||
                ((dist.X + hw >= 0) && (dist.X - hw <= 0));
        }
        /// <summary>
        /// Determines whether <paramref name="box"/> intersects the path of <see cref="Line2"/> <paramref name="line"/>.
        /// </summary>
        /// <param name="line">The line to compare to</param>
        /// <param name="tolerance">Added tolerance to act as thickening the line.</param>
        /// <returns></returns>
        public static bool Intersects<T>(this T box, Line2 line, Vector2 tolerance) where T : IBox, new()
        {
            T tolBox = box.Expanded(tolerance);

            Vector2 dist = tolBox.Centre.Relative(line);

            // Half of height
            double hh = tolBox.Height / 2;
            // Half of width
            double hw = tolBox.Width / 2;

            return ((dist.Y + hh >= 0) && (dist.Y - hh <= 0)) ||
                ((dist.X + hw >= 0) && (dist.X - hw <= 0));
        }

        /// <summary>
        /// Determines whether <paramref name="a"/> shares a bound with <paramref name="b"/>.
        /// </summary>
        /// <param name="a">The first box.</param>
        /// <param name="b">THe second box.</param>
        /// <returns></returns>
        public static bool ShareBound(this IBox a, IBox b)
        {
            return a.Left == b.Left ||
                a.Right == b.Right ||
                a.Top == b.Top ||
                a.Bottom == b.Bottom;
        }
    }
}
