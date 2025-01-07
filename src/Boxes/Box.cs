using System;

namespace Zene.Structs
{
    /// <summary>
    /// A box stored by its <see cref="Location"/> and <see cref="Size"/> values.
    /// </summary>
    public struct Box
    {
        /// <summary>
        /// Creates a box from a location and size.
        /// </summary>
        /// <param name="x">The x value of the location.</param>
        /// <param name="y">The y value of the location.</param>
        /// <param name="w">The width value of the size.</param>
        /// <param name="h">The height value of the size.</param>
        public Box(floatv x, floatv y, floatv w, floatv h)
        {
            X = x;
            Y = y;
            Width = w;
            Height = h;
        }
        /// <summary>
        /// Creates a box from a location and size.
        /// </summary>
        /// <param name="location">The location of the centre of the box.</param>
        /// <param name="size">The size of the box.</param>
        public Box(Vector2 location, Vector2 size)
        {
            X = location.X;
            Y = location.Y;
            Width = size.X;
            Height = size.Y;
        }
        /// <summary>
        /// Creates a box from a <see cref="Rectangle"/>.
        /// </summary>
        /// <param name="rect">The rectanle.</param>
        public Box(Rectangle rect)
        {
            Location = rect.Centre;
            Width = rect.Width;
            Height = rect.Height;
        }
        /// <summary>
        /// Creates a box from a <see cref="Bounds"/> box.
        /// </summary>
        /// <param name="box">The bounding box.</param>
        public Box(Bounds box)
        {
            X = box.X;
            Y = box.Y;
            Width = box.Width;
            Height = box.Height;
        }
        /// <summary>
        /// Creates a box from a <see cref="RectangleI"/> box.
        /// </summary>
        /// <param name="rect">The rectangle..</param>
        public Box(RectangleI rect)
        {
            Location = rect.Centre;
            Width = rect.Width;
            Height = rect.Height;
        }

        /// <summary>
        /// The centre x coordinate of the box.
        /// </summary>
        public floatv X { readonly get; set; }
        /// <summary>
        /// The centre y coordinate of the box.
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
        /// The x coordinate of the left side of the box.
        /// </summary>
        public floatv Left
        {
            readonly get => X - (Width * 0.5f);
            set
            {
                floatv diff = value - Left;
                Width -= diff;
                X -= diff * 0.5f;
            }
        }
        /// <summary>
        /// The x coordinate of the right side of the box.
        /// </summary>
        public floatv Right
        {
            readonly get => X + (Width * 0.5f);
            set
            {
                floatv diff = value - Right;
                Width += diff;
                X += diff * 0.5f;
            }
        }
        /// <summary>
        /// The y coordinate of the bottom of the box.
        /// </summary>
        public floatv Bottom
        {
            readonly get => Y - (Height * 0.5f);
            set
            {
                floatv diff = value - Bottom;
                Height -= diff;
                Y -= diff * 0.5f;
            }
        }
        /// <summary>
        /// The y coordinate of the top of the box.
        /// </summary>
        public floatv Top
        {
            readonly get => Y + (Height * 0.5f);
            set
            {
                floatv diff = value - Top;
                Height += diff;
                Y += diff * 0.5f;
            }
        }

        /// <summary>
        /// The top-left point.
        /// </summary>
        public Vector2 TopLeft
        {
            readonly get => new Vector2(Left, Top);
            set => (Left, Top) = value;
        }
        /// <summary>
        /// The top-right point.
        /// </summary>
        public Vector2 TopRight
        {
            readonly get => new Vector2(Right, Top);
            set => (Right, Top) = value;
        }
        /// <summary>
        /// The bottom-left point.
        /// </summary>
        public Vector2 BottomLeft
        {
            readonly get => new Vector2(Left, Bottom);
            set => (Left, Bottom) = value;
        }
        /// <summary>
        /// The bottom-right point.
        /// </summary>
        public Vector2 BottomRight
        {
            readonly get => new Vector2(Right, Bottom);
            set => (Right, Bottom) = value;
        }

        /// <summary>
        /// Determines whether this box overlaps <paramref name="box"/>.
        /// </summary>
        /// <param name="box">The box to compare to.</param>
        public readonly bool Overlaps(Box box)
        {
            Vector2 diff = (Location - box.Location).Abs();
            return diff.X <= 0.5f * (Width + box.Width) &&
                diff.Y <= 0.5f * (Height + box.Height);
        }
        /// <summary>
        /// Determines whether <paramref name="box"/> is inside this.
        /// </summary>
        /// <param name="box">The box to compare to.</param>
        public readonly bool Contains(Box box)
        {
            Vector2 diff = (Location - box.Location).Abs();
            return diff.X < 0.5f * (Width - box.Width) &&
                diff.Y < 0.5f * (Height - box.Height);
        }
        /// <summary>
        /// Determines whether <paramref name="point"/> is inside this.
        /// </summary>
        /// <param name="point">The <see cref="Vector2"/> to compare to.</param>
        public readonly bool Contains(Vector2 point)
        {
            Vector2 diff = (Location - point).Abs();

            return diff.X <= Size.X && diff.Y <= Size.Y;
        }
        /// <summary>
        /// Determines whether <paramref name="point"/> is inside this.
        /// </summary>
        /// <param name="point">The <see cref="Vector2I"/> to compare to.</param>
        public readonly bool Contains(Vector2I point) => Contains(point);

        /// <summary>
        /// Returns the smallest box possable to contain <paramref name="b"/> and this.
        /// </summary>
        /// <param name="b">The second box.</param>
        /// <returns></returns>
        public readonly Box Add(Box b) => ((Bounds)this).Add(b);

        /// <summary>
        /// Returns the combined volume of <paramref name="b"/> and this box.
        /// </summary>
        /// <param name="b">The second box.</param>
        /// <returns></returns>
        public readonly floatv CombinedVolume(Box b)
        {
            Vector2 diff = (Location - b.Location).Abs();
            Vector2 sd = (Size - b.Size).Abs();

            if (diff.X < sd.X)
            {
                diff.X = b.Size.X;
            }
            else { diff.X += 0.5f * (Size.X + b.Size.X); }
            if (diff.Y < sd.Y)
            {
                diff.Y = b.Size.Y;
            }
            else { diff.Y += 0.5f * (Size.Y + b.Size.Y); }

            return diff.X * diff.Y;
        }

        /// <summary>
        /// Returns a box clamped to the bounds of <paramref name="bounds"/>.
        /// </summary>
        /// <param name="bounds">The constricting bounds.</param>
        /// <returns></returns>
        public readonly Box Clamped(Box bounds) => ((Bounds)this).Clamped(bounds);

        /// <summary>
        /// Returns a rectangle with each side of the box extended by a value.
        /// </summary>
        /// <param name="value">The value to extend by</param>
        public readonly Box Expanded(Vector2 value)
        {
            return new Bounds(
                X - value.X,
                Y + value.Y,
                Width + value.X * 2,
                Height + value.Y * 2);
        }
        /// <summary>
        /// Extend each side of the box by a value.
        /// </summary>
        /// <param name="value">The value to extend by</param>
        public void Expand(Vector2 value)
        {
            X -= value.X;
            Right += value.X;
            Bottom -= value.Y;
            Y += value.Y;
        }

        /// <summary>
        /// Determines whether this box intersects the path of <see cref="Line2"/> <paramref name="line"/>.
        /// </summary>
        /// <param name="line">The line to compare to</param>
        /// <param name="tolerance">Added tolerance to act as thickening the line.</param>
        /// <returns></returns>
        public readonly bool Intersects(Line2 line, Vector2 tolerance)
        {
            Box tolBox = Expanded(tolerance);

            Vector2 dist = tolBox.Location.Relative(line);

            // Half of height
            floatv hh = tolBox.Height * 0.5f;
            // Half of width
            floatv hw = tolBox.Width * 0.5f;

            return ((dist.Y + hh >= 0) && (dist.Y - hh <= 0)) ||
                ((dist.X + hw >= 0) && (dist.X - hw <= 0));
        }

        /// <summary>
        /// Determines whether <paramref name="b"/> shares a bound with this box.
        /// </summary>
        /// <param name="b">THe second box.</param>
        /// <returns></returns>
        public readonly bool ShareBound(Box b)
        {
            Vector2 diff = (Location - b.Location).Abs();
            return diff.X == 0.5f * (Width + b.Width) ||
                diff.Y == 0.5f * (Height + b.Height);
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
            return obj is Box b &&
                    X == b.X && Y == b.Y &&
                    Width == b.Width && Height == b.Height;
        }
        public readonly override int GetHashCode() => HashCode.Combine(X, Y, Width, Height);

        public static bool operator ==(Box l, Box r) => l.Equals(r);
        public static bool operator !=(Box l, Box r) => !l.Equals(r);

        public static Box operator *(Box box, floatv scale) => new Box(box.X * scale, box.Y * scale, box.Width * scale, box.Height * scale);
        public static Box operator *(Box box, Vector2 scale) => new Box(box.X * scale.X, box.Y * scale.Y, box.Width * scale.X, box.Height * scale.Y);
        public static Box operator /(Box box, floatv scale)
        {
            floatv div = 1 / scale;
            return new Box(box.X * div, box.Y * div, box.Width * div, box.Height * div);
        }
        public static Box operator /(Box box, Vector2 scale)
        {
            Vector2 div = 1 / scale;
            return new Box(box.X * div.X, box.Y * div.Y, box.Width * div.X, box.Height * div.Y);
        }

        public static Box operator +(Box box, Vector2 offset) => new Box(box.X + offset.X, box.Y + offset.Y, box.Width, box.Height);
        public static Box operator -(Box box, Vector2 offset) => new Box(box.X - offset.X, box.Y - offset.Y, box.Width, box.Height);
        public static Box operator +(Box a, Box b) => a.Add(b);
        public static Box operator -(Box a, Box b) => a.Clamped(b);

        public static implicit operator Box(Rectangle box) => new Box(box);
        public static implicit operator Box(Bounds box) => new Box(box);
        public static implicit operator Box(RectangleI box) => new Box(box);

        /// <summary>
        /// A <see cref="Box"/> that spans from negative to positive infinity.
        /// </summary>
        public static Box Infinity { get; } = new Box(0, 0, floatv.PositiveInfinity, floatv.PositiveInfinity);
        /// <summary>
        /// A <see cref="Box"/> with <see cref="X"/>, <see cref="Y"/>, <see cref="Width"/> and <see cref="Height"/> all set to 0.
        /// </summary>
        public static Box Zero { get; } = new Box(0, 0, 0, 0);
        /// <summary>
        /// A <see cref="Box"/> with a <see cref="Width"/> and <see cref="Height"/> of 1 centred around origin.
        /// </summary>
        public static Box One { get; } = new Box(0, 0, 1, 1);
    }
}
