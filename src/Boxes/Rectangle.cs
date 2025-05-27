using System;

namespace Zene.Structs
{
    /// <summary>
    /// A box stored by the top left point, <see cref="Width"/> and <see cref="Height"/> values.
    /// </summary>
    public struct Rectangle : IBox
    {
        /// <summary>
        /// Creates a rectangle box from a location and size.
        /// </summary>
        /// <param name="left">The x value of the top left location.</param>
        /// <param name="top">The y value of the top left location.</param>
        /// <param name="w">The width value of the size.</param>
        /// <param name="h">The height value of the size.</param>
        public Rectangle(floatv left, floatv top, floatv w, floatv h)
        {
            X = left;
            Y = top;
            Width = w;
            Height = h;
        }
        /// <summary>
        /// Creates a rectangle box from a location and size.
        /// </summary>
        /// <param name="location">The location of the rectangle.</param>
        /// <param name="size">The size of the rectangle.</param>
        public Rectangle(Vector2 location, Vector2 size)
        {
            X = location.X;
            Y = location.Y;
            Width = size.X;
            Height = size.Y;
        }
        /// <summary>
        /// Creates a rectangle box from a <see cref="Box"/>.
        /// </summary>
        /// <param name="box">The basic box.</param>
        public Rectangle(Box box)
        {
            X = box.Left;
            Y = box.Top;
            Width = box.Width;
            Height = box.Height;
        }
        /// <summary>
        /// Creates a rectangle box from a <see cref="Bounds"/> box.
        /// </summary>
        /// <param name="box">The bounding box.</param>
        public Rectangle(Bounds box)
        {
            X = box.Left;
            Y = box.Top;
            Width = box.Width;
            Height = box.Height;
        }
        /// <summary>
        /// Creates a rectangle box from a <see cref="RectangleI"/> box.
        /// </summary>
        /// <param name="rect">The rectangle..</param>
        public Rectangle(RectangleI rect)
        {
            X = rect.X;
            Y = rect.Y;
            Width = rect.Width;
            Height = rect.Height;
        }

        /// <summary>
        /// The left x coordinate of the box.
        /// </summary>
        public floatv X { readonly get; set; }
        /// <summary>
        /// The top y coordinate of the box.
        /// </summary>
        public floatv Y { readonly get; set; }
        /// <summary>
        /// The width of the box.
        /// </summary>
        public floatv Width { readonly get; set; }
        /// <summary>
        /// The height of the box.
        /// </summary>
        public floatv Height { readonly get; set; }

        /// <summary>
        /// The centre location of the box.
        /// </summary>
        public Vector2 Centre
        {
            readonly get => new Vector2(X + (Width * 0.5f), Y - (Height * 0.5f));
            set
            {
                X = value.X - (Width * 0.5f);
                Y = value.Y + (Height * 0.5f);
            }
        }

        /// <summary>
        /// The top-left location of the box.
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
        /// The x coordinate of the left side of the box.
        /// </summary>
        public floatv Left
        {
            readonly get => X;
            set
            {
                Width += X - value;
                //if (Width < 0)
                //{
                //    Width = 0;
                //}
                X = value;
            }
        }
        /// <summary>
        /// The x coordinate of the right side of the box.
        /// </summary>
        public floatv Right
        {
            readonly get => X + Width;
            set
            {
                Width = value - X;
                //if (Width < 0)
                //{
                //    Width = 0;
                //}
            }
        }
        /// <summary>
        /// The y coordinate of the bottom side of the box.
        /// </summary>
        public floatv Bottom
        {
            readonly get => Y - Height;
            set
            {
                Height = Y - value;
                //if (Height < 0)
                //{
                //    Height = 0;
                //}
                Y = value;
            }
        }
        /// <summary>
        /// The y coordinate of the bottom top of the box.
        /// </summary>
        public floatv Top
        {
            readonly get => Y;
            set
            {
                Height += value - Y;
                //if (Height < 0)
                //{
                //    Height = 0;
                //}
            }
        }

        ///// <summary>
        ///// The top-left point.
        ///// </summary>
        //public Vector2 TopLeft => Location;
        /// <summary>
        /// The top-right point.
        /// </summary>
        public Vector2 TopRight
        {
            readonly get => new Vector2(Right, Y);
            set => (Right, Y) = value;
        }
        /// <summary>
        /// The bottom-left point.
        /// </summary>
        public Vector2 BottomLeft
        {
            readonly get => new Vector2(X, Bottom);
            set => (X, Bottom) = value;
        }
        /// <summary>
        /// The bottom-right point.
        /// </summary>
        public Vector2 BottomRight
        {
            readonly get => new Vector2(Right, Bottom);
            set => (Right, Bottom) = value;
        }

        ///// <summary>
        ///// Determines whether this box overlaps <paramref name="box"/>.
        ///// </summary>
        ///// <param name="box">The box to compare to.</param>
        //public readonly bool Overlaps(Rectangle box)
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
        //public readonly bool Contains(Rectangle box)
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
        //public readonly Rectangle Add(Rectangle b) => ((Bounds)this).Add(b);
        //// {
        ////     Vector2 diff = Location - b.Location;
        ////     Vector2 loc = 0;

        ////     if (diff.X < 0)
        ////     {
        ////         diff.X = -diff.X;
        ////         diff.X += b.Width;
        ////         if (diff.X < Width) { diff.X = Width; }
        ////         loc.X = X;
        ////     }
        ////     else
        ////     {
        ////         diff.X += Width;
        ////         if (diff.X < b.Width) { diff.X = b.Width; }
        ////         loc.X = b.X;
        ////     }
        ////     if (diff.Y < 0)
        ////     {
        ////         diff.Y = -diff.Y;
        ////         diff.Y += Height;
        ////         if (diff.Y < b.Height) { diff.Y = b.Height; }
        ////         loc.Y = b.Y;
        ////     }
        ////     else
        ////     {
        ////         diff.Y += b.Height;
        ////         if (diff.Y < Height) { diff.Y = Height; }
        ////         loc.Y = Y;
        ////     }

        ////     return new Rectangle(loc, diff);
        //// }

        ///// <summary>
        ///// Returns the combined volume of <paramref name="b"/> and this box.
        ///// </summary>
        ///// <param name="b">The second box.</param>
        ///// <returns></returns>
        //public readonly floatv CombinedVolume(Rectangle b) => ((Bounds)this).CombinedVolume(b);

        ///// <summary>
        ///// Returns a box clamped to the bounds of <paramref name="bounds"/>.
        ///// </summary>
        ///// <param name="bounds">The constricting bounds.</param>
        ///// <returns></returns>
        //public readonly Rectangle Clamped(Rectangle bounds) => ((Bounds)this).Clamped(bounds);

        ///// <summary>
        ///// Returns a rectangle with each side of the box extended by a value.
        ///// </summary>
        ///// <param name="value">The value to extend by.</param>
        //public readonly Rectangle Expanded(Vector2 value)
        //{
        //    return new Rectangle(
        //        X - value.X,
        //        Y + value.Y,
        //        Width + value.X * 2,
        //        Height + value.Y * 2);
        //}
        ///// <summary>
        ///// Extend each side of the box by a value.
        ///// </summary>
        ///// <param name="value">The value to extend by.</param>
        //public void Expand(Vector2 value)
        //{
        //    X -= value.X;
        //    Right += value.X;
        //    Bottom -= value.Y;
        //    Y += value.Y;
        //}

        ///// <summary>
        ///// Determines whether this box intersects the path of <see cref="Line2"/> <paramref name="line"/>.
        ///// </summary>
        ///// <param name="line">The line to compare to.</param>
        ///// <param name="tolerance">Added tolerance to act as thickening the line.</param>
        ///// <returns></returns>
        //public readonly bool Intersects(Line2 line, Vector2 tolerance)
        //{
        //    Rectangle tolBox = Expanded(tolerance);

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
        //public readonly bool ShareBound(Rectangle b)
        //{
        //    return X == b.X ||
        //        Right == b.Right ||
        //        Y == b.Y ||
        //        Bottom == b.Bottom;
        //}

        /// <summary>
        /// Creates a <see cref="Rectangle"/> from a reference to its centre location.
        /// </summary>
        /// <param name="x">The centre x position.</param>
        /// <param name="y">The centre y position.</param>
        /// <param name="width">The width of the box.</param>
        /// <param name="height">The height of the box.</param>
        /// <returns></returns>
        public static Rectangle FromCoords(floatv x, floatv y, floatv width, floatv height)
        {
            floatv hw = width * 0.5f;
            floatv hh = height * 0.5f;
            return new Rectangle(x - hw, y + hh, width, height);
        }
        /// <summary>
        /// Creates a <see cref="Rectangle"/> from its limiting bounds.
        /// </summary>
        /// <param name="left">The left side location.</param>
        /// <param name="right">The right side location.</param>
        /// <param name="top">The top side location.</param>
        /// <param name="bottom">The bottom side location.</param>
        /// <returns></returns>
        public static Rectangle FromBounds(floatv left, floatv right, floatv top, floatv bottom)
        {
            return new Rectangle(left, top, right - left, top - bottom);
        }

#nullable enable
        public readonly override string ToString()
        {
            return $"X:{X}, Y:{Y}, Width:{Width}, Height:{Height}";
        }
        public readonly string ToString(string? format)
        {
            return $"X:{X.ToString(format)}, Y:{Y.ToString(format)}, Width:{Width.ToString(format)}, Height:{Height.ToString(format)}";
        }
#nullable disable

        public readonly override bool Equals(object obj)
        {
            return obj is Rectangle b &&
                    X == b.Left && Width == b.Width &&
                    Y == b.Top && Height == b.Height;
        }
        public readonly override int GetHashCode() => HashCode.Combine(X, Y, Width, Height);

        public static bool operator ==(Rectangle l, Rectangle r) => l.Equals(r);
        public static bool operator !=(Rectangle l, Rectangle r) => !l.Equals(r);

        public static Rectangle operator *(Rectangle box, floatv scale) => new Rectangle(box.X * scale, box.Y * scale, box.Width * scale, box.Height * scale);
        public static Rectangle operator *(Rectangle box, Vector2 scale) => new Rectangle(box.X * scale.X, box.Y * scale.Y, box.Width * scale.X, box.Height * scale.Y);
        public static Rectangle operator /(Rectangle box, floatv scale)
        {
            floatv div = 1 / scale;
            return new Rectangle(box.X * div, box.Y * div, box.Width * div, box.Height * div);
        }
        public static Rectangle operator /(Rectangle box, Vector2 scale)
        {
            Vector2 div = 1 / scale;
            return new Rectangle(box.X * div.X, box.Y * div.Y, box.Width * div.X, box.Height * div.Y);
        }

        public static Rectangle operator +(Rectangle box, Vector2 offset) => new Rectangle(box.X + offset.X, box.Y + offset.Y, box.Width, box.Height);
        public static Rectangle operator -(Rectangle box, Vector2 offset) => new Rectangle(box.X - offset.X, box.Y - offset.Y, box.Width, box.Height);
        public static Rectangle operator +(Rectangle a, Rectangle b) => a.Add(b);
        public static Rectangle operator -(Rectangle a, Rectangle b) => a.Clamped(b);

        public static implicit operator Rectangle(Box box) => new Rectangle(box);
        public static implicit operator Rectangle(Bounds box) => new Rectangle(box);
        public static implicit operator Rectangle(RectangleI box) => new Rectangle(box);

        /// <summary>
        /// A <see cref="Rectangle"/> that spans from negative to positive infinity.
        /// </summary>
        public static Rectangle Infinity { get; } = new Rectangle(floatv.NegativeInfinity, floatv.NegativeInfinity, floatv.PositiveInfinity, floatv.PositiveInfinity);
        /// <summary>
        /// A <see cref="Rectangle"/> with <see cref="X"/>, <see cref="Y"/>, <see cref="Width"/> and <see cref="Height"/> all set to 0.
        /// </summary>
        public static Rectangle Zero { get; } = new Rectangle(0, 0, 0, 0);
        /// <summary>
        /// A <see cref="Rectangle"/> with a <see cref="Width"/> and <see cref="Height"/> of 1 centred around origin.
        /// </summary>
        public static Rectangle One { get; } = new Rectangle(-0.5f, 0.5f, 1, 1);
    }
}
