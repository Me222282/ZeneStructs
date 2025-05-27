using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Zene.Structs
{
    /// <summary>
    /// A box stored by the <see cref="Left"/>, <see cref="Right"/>, <see cref="Top"/> and <see cref="Bottom"/> values.
    /// </summary>
    public struct Bounds : IBox
    {
        /// <summary>
        /// Creates a box from its left, right, top and bottom locations.
        /// </summary>
        /// <param name="l">The x loction of the left side.</param>
        /// <param name="r">The x loction of the right side.</param>
        /// <param name="t">The y loction of the top side.</param>
        /// <param name="b">The y loction of the bottom side.</param>
        public Bounds(floatv l, floatv r, floatv t, floatv b)
        {
            Left = l;
            Right = r;
            Top = t;
            Bottom = b;
        }
        /// <summary>
        /// Creates a box from a location and size.
        /// </summary>
        /// <param name="location">The location of the centre of the box.</param>
        /// <param name="size">The size of the box.</param>
        public Bounds(Vector2 location, Vector2 size)
        {
            Left = 0;
            Right = 0;
            Bottom = 0;
            Top = 0;

            Centre = location;
            Size = size;
        }
        /// <summary>
        /// Creates a box from a <see cref="Rectangle"/>.
        /// </summary>
        /// <param name="rect">The rectanle.</param>
        public Bounds(Rectangle rect)
        {
            Left = rect.Left;
            Right = rect.Right;
            Bottom = rect.Bottom;
            Top = rect.Top;
        }
        /// <summary>
        /// Creates a box from a <see cref="Box"/>.
        /// </summary>
        /// <param name="box">The basic box.</param>
        public Bounds(Box box)
        {
            floatv hw = box.Width * 0.5f;
            floatv hh = box.Height * 0.5f;

            Left = box.X - hw;
            Right = box.X + hw;
            Bottom = box.Y - hh;
            Top = box.Y + hh;
        }
        /// <summary>
        /// Creates a box from a <see cref="RectangleI"/>.
        /// </summary>
        /// <param name="rect">The rectanle.</param>
        public Bounds(RectangleI rect)
        {
            Left = rect.Left;
            Right = rect.Right;
            Bottom = rect.Bottom;
            Top = rect.Top;
        }

        /// <summary>
        /// The x coordinate of the left side of the box.
        /// </summary>
        public floatv Left { readonly get; set; }
        /// <summary>
        /// The x coordinate of the right side of the box.
        /// </summary>
        public floatv Right { readonly get; set; }
        /// <summary>
        /// The y coordinate of the bottom of the box.
        /// </summary>
        public floatv Bottom { readonly get; set; }
        /// <summary>
        /// The y coordinate of the top of the box.
        /// </summary>
        public floatv Top { readonly get; set; }

        /// <summary>
        /// The center location of the box.
        /// </summary>
        public Vector2 Centre
        {
            readonly get => new Vector2(X, Y);
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }
        /// <summary>
        /// The center location of the box.
        /// </summary>
        public Vector2 Location
        {
            readonly get => new Vector2(X, Y);
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }
        /// <summary>
        /// The width and height of the box.
        /// </summary>
        public Vector2 Size
        {
            readonly get => new Vector2(Width, Height);
            set
            {
                Width = value.X;
                Height = value.Y;
            }
        }

        /// <summary>
        /// The centre x coordinate of the box.
        /// </summary>
        public floatv X
        {
            readonly get => Left + (Width * 0.5f);
            set
            {
                floatv offset = value - X;

                Left += offset;
                Right += offset;
            }
        }
        /// <summary>
        /// The centre y coordinate of the box.
        /// </summary>
        public floatv Y
        {
            readonly get => Bottom + (Height * 0.5f);
            set
            {
                floatv offset = value - Y;

                Bottom += offset;
                Top += offset;
            }
        }
        /// <summary>
        /// The width of the box.
        /// </summary>
        public floatv Width
        {
            readonly get => Right - Left;
            set
            {
                floatv offset = (value - Width) * 0.5f;

                Left -= offset;
                Right += offset;
            }
        }
        /// <summary>
        /// The height of the box.
        /// </summary>
        public floatv Height
        {
            readonly get => Top - Bottom;
            set
            {
                floatv offset = (value - Height) * 0.5f;

                Bottom -= offset;
                Top += offset;
            }
        }

        /// <summary>
        /// The top-left point.
        /// </summary>
        public readonly Vector2 TopLeft => new Vector2(Left, Top);
        /// <summary>
        /// The top-right point.
        /// </summary>
        public readonly Vector2 TopRight => new Vector2(Right, Top);
        /// <summary>
        /// The bottom-left point.
        /// </summary>
        public readonly Vector2 BottomLeft => new Vector2(Left, Bottom);
        /// <summary>
        /// The bottom-right point.
        /// </summary>
        public readonly Vector2 BottomRight => new Vector2(Right, Bottom);

        /// <summary>
        /// Sets the top left corner of the box whilst keeping the size the same.
        /// </summary>
        /// <param name="value"></param>
        public void SetTopLeft(Vector2 value)
        {
            Vector2 size = Size;

            Top = value.Y;
            Left = value.X;
            Bottom = value.Y - size.Y;
            Right = value.X + size.X;
        }

        /// <summary>
        /// Shifts this box by <paramref name="offset"/>.
        /// </summary>
        /// <param name="offset">The distance to offset.</param>
        /// <returns></returns>
        public void Shift(Vector2 offset)
        {
            Left += offset.X;
            Right += offset.X;
            Top += offset.Y;
            Bottom += offset.Y;
        }
        /// <summary>
        /// Shifts this box by <paramref name="offset"/>.
        /// </summary>
        /// <param name="offset">The distance to offset.</param>
        /// <returns></returns>
        public readonly Bounds Shifted(Vector2 offset)
        {
            return new Bounds(
                Left + offset.X,
                Right + offset.X,
                Top + offset.Y,
                Bottom + offset.Y);
        }

        ///// <summary>
        ///// Determines whether this box overlaps <paramref name="box"/>.
        ///// </summary>
        ///// <param name="box">The box to compare to.</param>
        //public readonly bool Overlaps(Bounds box)
        //{
        //    return (Left < box.Right) &&
        //        (Right > box.Left) &&
        //        (Bottom < box.Top) &&
        //        (Top > box.Bottom);
        //}
        ///// <summary>
        ///// Determines whether <paramref name="box"/> is inside this.
        ///// </summary>
        ///// <param name="box">The box to compare to.</param>
        //public readonly bool Contains(Bounds box)
        //{
        //    return (Left >= box.Right) &&
        //        (Right <= box.Left) &&
        //        (Bottom >= box.Top) &&
        //        (Top <= box.Bottom);
        //}
        ///// <summary>
        ///// Determines whether <paramref name="point"/> is inside this.
        ///// </summary>
        ///// <param name="point">The <see cref="Vector2"/> to compare to.</param>
        //public readonly bool Contains(Vector2 point)
        //{
        //    return (point.X >= Left) &&
        //        (point.X <= Right) &&
        //        (point.Y >= Bottom) &&
        //        (point.Y <= Top);
        //}
        ///// <summary>
        ///// Determines whether <paramref name="point"/> is inside this.
        ///// </summary>
        ///// <param name="point">The <see cref="Vector2I"/> to compare to.</param>
        //public readonly bool Contains(Vector2I point) => Contains((Vector2)point);

        ///// <summary>
        ///// Returns the smallest box possible to contain <paramref name="b"/> and this.
        ///// </summary>
        ///// <param name="b">The second box.</param>
        ///// <returns></returns>
        //public readonly Bounds Add(Bounds b)
        //{
        //    Bounds r = this;
        //    if (r.Left > b.Left)
        //    {
        //        r.Left = b.Left;
        //    }
        //    if (r.Right < b.Right)
        //    {
        //        r.Right = b.Right;
        //    }
        //    if (r.Bottom > b.Bottom)
        //    {
        //        r.Bottom = b.Bottom;
        //    }
        //    if (r.Top < b.Top)
        //    {
        //        r.Top = b.Top;
        //    }

        //    return r;
        //}

        ///// <summary>
        ///// Returns the combined volume of <paramref name="b"/> and this box.
        ///// </summary>
        ///// <param name="b">The second box.</param>
        ///// <returns></returns>
        //public readonly floatv CombinedVolume(Bounds b)
        //{
        //    Bounds ex = Add(b);
        //    return ex.Width * ex.Height;
        //}

        ///// <summary>
        ///// Returns a box clamped to the bounds of <paramref name="bounds"/>.
        ///// </summary>
        ///// <param name="bounds">The constricting bounds.</param>
        ///// <returns></returns>
        //public readonly Bounds Clamped(Bounds bounds)
        //{
        //    Bounds r = this;
        //    if (r.Left < bounds.Left)
        //    {
        //        r.Left = bounds.Left;
        //    }
        //    if (r.Right > bounds.Right)
        //    {
        //        r.Right = bounds.Right;
        //    }
        //    if (r.Bottom < bounds.Bottom)
        //    {
        //        r.Bottom = bounds.Bottom;
        //    }
        //    if (r.Top > bounds.Top)
        //    {
        //        r.Top = bounds.Top;
        //    }

        //    return r;
        //}

        ///// <summary>
        ///// Returns a box with each side of the box extended by a value.
        ///// </summary>
        ///// <param name="value">The value to extend by</param>
        //public readonly Bounds Expanded(Vector2 value)
        //{
        //    return new Bounds(
        //        Left - value.X,
        //        Right + value.X,
        //        Bottom - value.Y,
        //        Top + value.Y);
        //}
        ///// <summary>
        ///// Extend each side of the box by a value.
        ///// </summary>
        ///// <param name="value">The value to extend by</param>
        //public void Expand(Vector2 value)
        //{
        //    Left -= value.X;
        //    Right += value.X;
        //    Bottom -= value.Y;
        //    Top += value.Y;
        //}

        ///// <summary>
        ///// Determines whether this box intersects the path of <see cref="Line2"/> <paramref name="line"/>.
        ///// </summary>
        ///// <param name="line">The line to compare to</param>
        ///// <param name="tolerance">Added tolerance to act as thickening the line.</param>
        ///// <returns></returns>
        //public readonly bool Intersects(Line2 line, Vector2 tolerance)
        //{
        //    Bounds tolBox = Expanded(tolerance);

        //    Vector2 dist = tolBox.Centre.Relative(line);

        //    // Half of height
        //    floatv hh = tolBox.Height * 0.5f;
        //    // Half of width
        //    floatv hw = tolBox.Width * 0.5f;

        //    return ((dist.Y + hh >= 0) && (dist.Y - hh <= 0)) ||
        //        ((dist.X + hw >= 0) && (dist.X - hw <= 0));
        //}

        ///// <summary>
        ///// Determines whether <paramref name="b"/> shares a bound with this box.
        ///// </summary>
        ///// <param name="b">The second box.</param>
        ///// <returns></returns>
        //public readonly bool ShareBound(Bounds b)
        //{
        //    return X == b.X ||
        //        Right == b.Right ||
        //        Y == b.Y ||
        //        Bottom == b.Bottom;
        //}

        /// <summary>
        /// Creates a <see cref="Bounds"/> from a reference to its centre location.
        /// </summary>
        /// <param name="x">The centre x position.</param>
        /// <param name="y">The centre y position.</param>
        /// <param name="width">The width of the box.</param>
        /// <param name="height">The height of the box.</param>
        /// <returns></returns>
        public static Bounds FromCoords(floatv x, floatv y, floatv width, floatv height)
        {
            floatv hw = width * 0.5f;
            floatv hh = height * 0.5f;
            return new Bounds(x - hw, x + hw, y + hh, y - hh);
        }
        /// <summary>
        /// Creates a <see cref="Bounds"/> from a reference to its top-left location.
        /// </summary>
        /// <param name="left">The left side location.</param>
        /// <param name="top">The top side location.</param>
        /// <param name="width">The width of the box.</param>
        /// <param name="height">The height of the box.</param>
        /// <returns></returns>
        public static Bounds FromRect(floatv left, floatv top, floatv width, floatv height)
        {
            return new Bounds(left, left + width, top, top - height);
        }

#nullable enable
        public readonly override string ToString()
        {
            return $"Left:{Left}, Right:{Right}, Top:{Top}, Bottom:{Bottom}";
        }
        public readonly string ToString(string? format)
        {
            return $"Left:{Left.ToString(format)}, Right:{Right.ToString(format)}, Top:{Top.ToString(format)}, Bottom:{Bottom.ToString(format)}";
        }
#nullable disable

        public readonly override bool Equals(object obj)
        {
            return obj is Bounds b &&
                    Left == b.Left && Right == b.Right &&
                    Top == b.Top && Bottom == b.Bottom;
        }
        public readonly override int GetHashCode()
        {
            return HashCode.Combine(Left, Right, Top, Bottom);
        }

        public static bool operator ==(Bounds l, Bounds r) => l.Equals(r);
        public static bool operator !=(Bounds l, Bounds r) => !l.Equals(r);

        public static Bounds operator *(Bounds box, floatv scale) => new Bounds(box.Left * scale, box.Right * scale, box.Top * scale, box.Bottom * scale);
        public static Bounds operator *(Bounds box, Vector2 scale) => new Bounds(box.Left * scale.X, box.Right * scale.X, box.Top * scale.Y, box.Bottom * scale.Y);
        public static Bounds operator /(Bounds box, floatv scale)
        {
            floatv div = 1 / scale;
            return new Bounds(box.Left * div, box.Right * div, box.Top * div, box.Bottom * div);
        }
        public static Bounds operator /(Bounds box, Vector2 scale)
        {
            Vector2 div = 1 / scale;
            return new Bounds(box.Left * div.X, box.Right * div.X, box.Top * div.Y, box.Bottom * div.Y);
        }

        public static Bounds operator +(Bounds box, Vector2 offset) => box.Shifted(offset);
        public static Bounds operator -(Bounds box, Vector2 offset) => box.Shifted(-offset);
        public static Bounds operator +(Bounds a, Bounds b) => a.Add(b);
        public static Bounds operator -(Bounds a, Bounds b) => a.Clamped(b);

        public static implicit operator Bounds(Rectangle box) => new Bounds(box);
        public static implicit operator Bounds(Box box) => new Bounds(box);
        public static implicit operator Bounds(RectangleI box) => new Bounds(box);

        /// <summary>
        /// A <see cref="Bounds"/> that spans from negative to positive infinity.
        /// </summary>
        public static Bounds Infinity { get; } = new Bounds(floatv.NegativeInfinity, floatv.PositiveInfinity, floatv.PositiveInfinity, floatv.NegativeInfinity);
        /// <summary>
        /// A <see cref="Bounds"/> with <see cref="Left"/>, <see cref="Right"/>, <see cref="Top"/> and <see cref="Bottom"/> all set to 0.
        /// </summary>
        public static Bounds Zero { get; } = new Bounds(0, 0, 0, 0);
        /// <summary>
        /// A <see cref="Bounds"/> with a <see cref="Width"/> and <see cref="Height"/> of 1 centred around origin.
        /// </summary>
        public static Bounds One { get; } = new Bounds(-0.5f, 0.5f, 0.5f, -0.5f);
    }
}
