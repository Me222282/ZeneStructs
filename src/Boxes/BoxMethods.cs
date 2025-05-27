using System;

namespace Zene.Structs
{
    public static class BoxMethods
    {
        /// <summary>
        /// Determines whether this <typeparamref name="T"/> overlaps <paramref name="box"/>.
        /// </summary>
        /// <param name="box">The <typeparamref name="T"/> to compare to.</param>
        public static bool Overlaps<T>(this T iBox, T box) where T : IBox
        {
            return (iBox.Left < box.Right) &&
                (iBox.Right > box.Left) &&
                (iBox.Bottom < box.Top) &&
                (iBox.Top > box.Bottom);
        }
        /// <summary>
        /// Determines whether <paramref name="box"/> is inside this <typeparamref name="T"/>.
        /// </summary>
        /// <param name="box">The <typeparamref name="T"/> to compare to.</param>
        public static bool Contains<T>(this T iBox, T box) where T : IBox
        {
            return (iBox.Left >= box.Right) &&
                (iBox.Right <= box.Left) &&
                (iBox.Bottom >= box.Top) &&
                (iBox.Top <= box.Bottom);
        }
        /// <summary>
        /// Determines whether <paramref name="point"/> is inside this <typeparamref name="T"/>.
        /// </summary>
        /// <param name="point">The <see cref="Vector2"/> to compare to.</param>
        public static bool Contains<T>(this T box, Vector2 point) where T : IBox
        {
            return (point.X >= box.Left) &&
                (point.X <= box.Right) &&
                (point.Y >= box.Bottom) &&
                (point.Y <= box.Top);
        }
        /// <summary>
        /// Determines whether <paramref name="point"/> is inside this <typeparamref name="T"/>.
        /// </summary>
        /// <param name="point">The <see cref="Vector2I"/> to compare to.</param>
        public static bool Contains<T>(this T box, Vector2I point) where T : IBox
        {
            return (point.X >= box.Left) &&
                (point.X <= box.Right) &&
                (point.Y >= box.Bottom) &&
                (point.Y <= box.Top);
        }

        /// <summary>
        /// Returns the smallest box possable that can contain <typeparamref name="T"/> <paramref name="box"/> and <typeparamref name="T"/> <paramref name="b"/>.
        /// </summary>
        /// <param name="box">The first box.</param>
        /// <param name="b">The second box.</param>
        /// <returns></returns>
        public static T Add<T>(this T box, T b) where T : IBox, new()
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
        /// Returns the combined volume of <paramref name="box"/> and <paramref name="b"/>.
        /// </summary>
        /// <param name="box">The first box.</param>
        /// <param name="b">The second box.</param>
        /// <returns></returns>
        public static floatv CombineVolume<T>(this T box, T b) where T : IBox
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
        public static T Clamped<T>(this T box, T bounds) where T : IBox, new()
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
        /// Extend each side of a box by a value.
        /// </summary>
        /// <param name="value">The value to extend by.</param>
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
        /// <param name="value">The value to extend by.</param>
        public static T Expanded<T>(this T box, floatv value) where T : IBox, new()
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
        /// Determines whether <paramref name="box"/> intersects the path of <see cref="Line2"/> <paramref name="line"/>.
        /// </summary>
        /// <param name="line">The line to compare to.</param>
        /// <returns></returns>
        public static bool Intersects<T>(this T box, Line2 line) where T : IBox
        {
            Vector2 dist = box.Centre.Relative(line);

            // Half of height
            floatv hh = box.Height * 0.5f;
            // Half of width
            floatv hw = box.Width * 0.5f;

            return ((dist.Y + hh >= 0) && (dist.Y - hh <= 0)) ||
                ((dist.X + hw >= 0) && (dist.X - hw <= 0));
        }
        /// <summary>
        /// Determines whether <paramref name="box"/> intersects the path of <see cref="Line2"/> <paramref name="line"/>.
        /// </summary>
        /// <param name="line">The line to compare to.</param>
        /// <param name="tolerance">Added tolerance to act as thickening the line.</param>
        /// <returns></returns>
        public static bool Intersects<T>(this T box, Line2 line, Vector2 tolerance) where T : IBox, new()
        {
            T tolBox = box.Expanded(tolerance);

            Vector2 dist = tolBox.Centre.Relative(line);

            // Half of height
            floatv hh = tolBox.Height * 0.5f;
            // Half of width
            floatv hw = tolBox.Width * 0.5f;

            return ((dist.Y + hh >= 0) && (dist.Y - hh <= 0)) ||
                ((dist.X + hw >= 0) && (dist.X - hw <= 0));
        }

        /// <summary>
        /// Determines whether <paramref name="a"/> shares a bound with <paramref name="b"/>.
        /// </summary>
        /// <param name="a">The first box.</param>
        /// <param name="b">The second box.</param>
        /// <returns></returns>
        public static bool ShareBound<T>(this T a, T b) where T : IBox
        {
            return a.Left == b.Left ||
                a.Right == b.Right ||
                a.Top == b.Top ||
                a.Bottom == b.Bottom;
        }
    }
}
