//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Zene.Structs
//{
//    /// <summary>
//    /// A box stored by the <see cref="Left"/>, <see cref="Right"/>, <see cref="Top"/> and <see cref="Bottom"/> values.
//    /// </summary>
//    public struct BoundsI
//    {
//        /// <summary>
//        /// Creates a box from its left, right, top and bottom locations.
//        /// </summary>
//        /// <param name="l">The x loction of the left side.</param>
//        /// <param name="r">The x loction of the right side.</param>
//        /// <param name="t">The y loction of the top side.</param>
//        /// <param name="b">The y loction of the bottom side.</param>
//        public BoundsI(int l, int r, int t, int b)
//        {
//            Left = l;
//            Right = r;
//            Top = t;
//            Bottom = b;
//        }
//        /// <summary>
//        /// Creates a box from a location and size.
//        /// </summary>
//        /// <param name="location">The location of the centre of the box.</param>
//        /// <param name="size">The size of the box.</param>
//        public BoundsI(Vector2I location, Vector2I size)
//        {
//            Left = 0;
//            Right = 0;
//            Bottom = 0;
//            Top = 0;

//            Centre = location;
//            Size = size;
//        }
//        /// <summary>
//        /// Creates a box from a <see cref="Rectangle"/>.
//        /// </summary>
//        /// <param name="rect">The rectanle.</param>
//        public BoundsI(Rectangle rect)
//        {
//            Left = rect.Left;
//            Right = rect.Right;
//            Bottom = rect.Bottom;
//            Top = rect.Top;
//        }
//        /// <summary>
//        /// Creates a box from a <see cref="Box"/>.
//        /// </summary>
//        /// <param name="box">The basic box.</param>
//        public BoundsI(Box box)
//        {
//            int hw = box.Width * 0.5f;
//            int hh = box.Height * 0.5f;

//            Left = box.X - hw;
//            Right = box.X + hw;
//            Bottom = box.Y - hh;
//            Top = box.Y + hh;
//        }
//        /// <summary>
//        /// Creates a box from a <see cref="RectangleI"/>.
//        /// </summary>
//        /// <param name="rect">The rectanle.</param>
//        public BoundsI(RectangleI rect)
//        {
//            Left = rect.Left;
//            Right = rect.Right;
//            Bottom = rect.Bottom;
//            Top = rect.Top;
//        }

//        /// <summary>
//        /// The x coordinate of the left side of the box.
//        /// </summary>
//        public int Left { readonly get; set; }
//        /// <summary>
//        /// The x coordinate of the right side of the box.
//        /// </summary>
//        public int Right { readonly get; set; }
//        /// <summary>
//        /// The y coordinate of the bottom of the box.
//        /// </summary>
//        public int Bottom { readonly get; set; }
//        /// <summary>
//        /// The y coordinate of the top of the box.
//        /// </summary>
//        public int Top { readonly get; set; }

//        /// <summary>
//        /// The center location of the box.
//        /// </summary>
//        public Vector2I Centre
//        {
//            readonly get => new Vector2I(X, Y);
//            set
//            {
//                X = value.X;
//                Y = value.Y;
//            }
//        }
//        /// <summary>
//        /// The center location of the box.
//        /// </summary>
//        public Vector2I Location
//        {
//            readonly get => new Vector2I(X, Y);
//            set
//            {
//                X = value.X;
//                Y = value.Y;
//            }
//        }
//        /// <summary>
//        /// The width and height of the box.
//        /// </summary>
//        public Vector2I Size
//        {
//            readonly get => new Vector2I(Width, Height);
//            set
//            {
//                Width = value.X;
//                Height = value.Y;
//            }
//        }

//        /// <summary>
//        /// The centre x coordinate of the box.
//        /// </summary>
//        public floatv X
//        {
//            readonly get => Left + (Width * 0.5f);
//            set
//            {
//                int offset = value - X;

//                Left += offset;
//                Right += offset;
//            }
//        }
//        /// <summary>
//        /// The centre y coordinate of the box.
//        /// </summary>
//        public floatv Y
//        {
//            readonly get => Bottom + (Height * 0.5f);
//            set
//            {
//                floatv offset = value - Y;

//                Bottom += offset;
//                Top += offset;
//            }
//        }
//        /// <summary>
//        /// The width of the box.
//        /// </summary>
//        public int Width
//        {
//            readonly get => Right - Left;
//            set
//            {
//                floatv offset = (value - Width) * 0.5f;

//                Left -= offset;
//                Right += offset;
//            }
//        }
//        /// <summary>
//        /// The height of the box.
//        /// </summary>
//        public int Height
//        {
//            readonly get => Top - Bottom;
//            set
//            {
//                floatv offset = (value - Height) * 0.5f;

//                Bottom -= offset;
//                Top += offset;
//            }
//        }

//        /// <summary>
//        /// The top-left point.
//        /// </summary>
//        public readonly Vector2I TopLeft => new Vector2I(Left, Top);
//        /// <summary>
//        /// The top-right point.
//        /// </summary>
//        public readonly Vector2I TopRight => new Vector2I(Right, Top);
//        /// <summary>
//        /// The bottom-left point.
//        /// </summary>
//        public readonly Vector2I BottomLeft => new Vector2I(Left, Bottom);
//        /// <summary>
//        /// The bottom-right point.
//        /// </summary>
//        public readonly Vector2I BottomRight => new Vector2I(Right, Bottom);

//        /// <summary>
//        /// Sets the top left corner of the box whilst keeping the size the same.
//        /// </summary>
//        /// <param name="value"></param>
//        public void SetTopLeft(Vector2I value)
//        {
//            Vector2I size = Size;

//            Top = value.Y;
//            Left = value.X;
//            Bottom = value.Y - size.Y;
//            Right = value.X + size.X;
//        }

//        /// <summary>
//        /// Shifts this box by <paramref name="offset"/>.
//        /// </summary>
//        /// <param name="offset">The distance to offset.</param>
//        /// <returns></returns>
//        public void Shift(Vector2I offset)
//        {
//            Left += offset.X;
//            Right += offset.X;
//            Top += offset.Y;
//            Bottom += offset.Y;
//        }
//        /// <summary>
//        /// Shifts this box by <paramref name="offset"/>.
//        /// </summary>
//        /// <param name="offset">The distance to offset.</param>
//        /// <returns></returns>
//        public readonly BoundsI Shifted(Vector2I offset)
//        {
//            return new BoundsI(
//                Left + offset.X,
//                Right + offset.X,
//                Top + offset.Y,
//                Bottom + offset.Y);
//        }

//        /// <summary>
//        /// Determines whether this box overlaps <paramref name="box"/>.
//        /// </summary>
//        /// <param name="box">The box to compare to.</param>
//        public readonly bool Overlaps(BoundsI box)
//        {
//            return (Left < box.Right) &&
//                (Right > box.Left) &&
//                (Bottom < box.Top) &&
//                (Top > box.Bottom);
//        }
//        /// <summary>
//        /// Determines whether <paramref name="box"/> is inside this.
//        /// </summary>
//        /// <param name="box">The box to compare to.</param>
//        public readonly bool Contains(BoundsI box)
//        {
//            return (Left >= box.Right) &&
//                (Right <= box.Left) &&
//                (Bottom >= box.Top) &&
//                (Top <= box.Bottom);
//        }
//        /// <summary>
//        /// Determines whether <paramref name="point"/> is inside this.
//        /// </summary>
//        /// <param name="point">The <see cref="Vector2I"/> to compare to.</param>
//        public readonly bool Contains(Vector2I point)
//        {
//            return (point.X >= Left) &&
//                (point.X <= Right) &&
//                (point.Y >= Bottom) &&
//                (point.Y <= Top);
//        }
//        /// <summary>
//        /// Determines whether <paramref name="point"/> is inside this.
//        /// </summary>
//        /// <param name="point">The <see cref="Vector2"/> to compare to.</param>
//        public readonly bool Contains(Vector2 point) => Contains(point);

//        /// <summary>
//        /// Returns the smallest box possable to contain <paramref name="b"/> and this.
//        /// </summary>
//        /// <param name="b">The second box.</param>
//        /// <returns></returns>
//        public readonly BoundsI Add(BoundsI b)
//        {
//            return new BoundsI(
//                Math.Min(Left, b.Left),
//                Math.Max(Right, b.Right),
//                Math.Min(Bottom, b.Bottom),
//                Math.Max(Top, b.Top));
//        }

//        /// <summary>
//        /// Returns the combined volume of <paramref name="b"/> and this box.
//        /// </summary>
//        /// <param name="b">The second box.</param>
//        /// <returns></returns>
//        public readonly int CombinedVolume(BoundsI b)
//        {
//            Vector2I diff = TopLeft - b.TopLeft;

//            if (diff.X < 0)
//            {
//                diff.X = -diff.X;
//                diff.X += Width;
//            }
//            else { diff.X += b.Width; }
//            if (diff.Y < 0)
//            {
//                diff.Y = -diff.Y;
//                diff.Y += Height;
//            }
//            else { diff.Y += b.Height; }

//            return diff.X * diff.Y;
//        }

//        /// <summary>
//        /// Returns a box clamped to the bounds of <paramref name="bounds"/>.
//        /// </summary>
//        /// <param name="bounds">The constricting bounds.</param>
//        /// <returns></returns>
//        public readonly BoundsI Clamped(BoundsI bounds)
//        {
//            BoundsI r = this;
//            if (r.Left < bounds.Right)
//            {
//                r.Left = bounds.Right;
//            }
//            else if (r.Right > bounds.Left)
//            {
//                r.Right = bounds.Left;
//            }
//            if (r.Bottom < bounds.Top)
//            {
//                r.Bottom = bounds.Top;
//            }
//            else if (r.Top > bounds.Bottom)
//            {
//                r.Top = bounds.Bottom;
//            }

//            return r;
//        }

//        /// <summary>
//        /// Returns a rectangle with each side of the box extended by a value.
//        /// </summary>
//        /// <param name="value">The value to extend by</param>
//        public readonly BoundsI Expanded(Vector2I value)
//        {
//            return new BoundsI(
//                Left - value.X,
//                Right + value.X,
//                Bottom - value.Y,
//                Top + value.Y);
//        }
//        /// <summary>
//        /// Extend each side of the box by a value.
//        /// </summary>
//        /// <param name="value">The value to extend by</param>
//        public void Expand(Vector2I value)
//        {
//            Left -= value.X;
//            Right += value.X;
//            Bottom -= value.Y;
//            Top += value.Y;
//        }

//        /// <summary>
//        /// Determines whether this box intersects the path of <see cref="Line2"/> <paramref name="line"/>.
//        /// </summary>
//        /// <param name="line">The line to compare to</param>
//        /// <param name="tolerance">Added tolerance to act as thickening the line.</param>
//        /// <returns></returns>
//        public readonly bool Intersects(Line2 line, Vector2 tolerance)
//        {
//            Bounds tolBox = ((Bounds)this).Expanded(tolerance);

//            Vector2 dist = tolBox.Centre.Relative(line);

//            // Half of height
//            floatv hh = tolBox.Height * 0.5f;
//            // Half of width
//            floatv hw = tolBox.Width * 0.5f;

//            return ((dist.Y + hh >= 0) && (dist.Y - hh <= 0)) ||
//                ((dist.X + hw >= 0) && (dist.X - hw <= 0));
//        }

//        /// <summary>
//        /// Determines whether <paramref name="b"/> shares a bound with this box.
//        /// </summary>
//        /// <param name="b">THe second box.</param>
//        /// <returns></returns>
//        public readonly bool ShareBound(BoundsI b)
//        {
//            return X == b.X ||
//                Right == b.Right ||
//                Y == b.Y ||
//                Bottom == b.Bottom;
//        }

//#nullable enable
//        public readonly override string ToString()
//        {
//            return $"Left:{Left}, Right:{Right}, Top:{Top}, Bottom:{Bottom}";
//        }
//        public readonly string ToString(string? format)
//        {
//            return $"Left:{Left.ToString(format)}, Right:{Right.ToString(format)}, Top:{Top.ToString(format)}, Bottom:{Bottom.ToString(format)}";
//        }
//#nullable disable

//        public readonly override bool Equals(object obj)
//        {
//            return obj is BoundsI b &&
//                    Left == b.Left && Right == b.Right &&
//                    Top == b.Top && Bottom == b.Bottom;
//        }
//        public readonly override int GetHashCode()
//        {
//            return HashCode.Combine(Left, Right, Top, Bottom);
//        }

//        public static bool operator ==(BoundsI l, BoundsI r) => l.Equals(r);
//        public static bool operator !=(BoundsI l, BoundsI r) => !l.Equals(r);

//        public static BoundsI operator *(BoundsI box, int scale) => new BoundsI(box.Left * scale, box.Right * scale, box.Top * scale, box.Bottom * scale);
//        public static BoundsI operator *(BoundsI box, Vector2I scale) => new BoundsI(box.Left * scale.X, box.Right * scale.X, box.Top * scale.Y, box.Bottom * scale.Y);
//        public static BoundsI operator /(BoundsI box, int scale) => new BoundsI(box.Left / scale, box.Right / scale, box.Top / scale, box.Bottom / scale);
//        public static BoundsI operator /(BoundsI box, Vector2I scale) => new BoundsI(box.Left / scale.X, box.Right / scale.X, box.Top / scale.Y, box.Bottom / scale.Y);

//        public static BoundsI operator +(BoundsI box, Vector2I offset) => box.Shifted(offset);
//        public static BoundsI operator -(BoundsI box, Vector2I offset) => box.Shifted(-offset);
//        public static BoundsI operator +(BoundsI a, BoundsI b) => a.Add(b);
//        public static BoundsI operator -(BoundsI a, BoundsI b) => a.Clamped(b);

//        public static implicit operator BoundsI(Rectangle box) => new BoundsI(box);
//        public static implicit operator BoundsI(Box box) => new BoundsI(box);
//        public static implicit operator BoundsI(RectangleI box) => new BoundsI(box);

//        /// <summary>
//        /// A <see cref="Bounds"/> that spans from negative to positive infinity.
//        /// </summary>
//        public static Bounds Infinity { get; } = new Bounds(floatv.NegativeInfinity, floatv.PositiveInfinity, floatv.PositiveInfinity, floatv.NegativeInfinity);
//        /// <summary>
//        /// A <see cref="Bounds"/> with <see cref="Left"/>, <see cref="Right"/>, <see cref="Top"/> and <see cref="Bottom"/> all set to 0.
//        /// </summary>
//        public static Bounds Zero { get; } = new Bounds(0, 0, 0, 0);
//        /// <summary>
//        /// A <see cref="Bounds"/> with a <see cref="Width"/> and <see cref="Height"/> of 1 centred around origin.
//        /// </summary>
//        public static Bounds One { get; } = new Bounds(-0.5f, 0.5f, 0.5f, -0.5f);
//    }
//}
