using System;

namespace Zene.Structs
{
    /// <summary>
    /// A box stored by the <see cref="X"/>, <see cref="Y"/>, <see cref="Width"/> and <see cref="Height"/> values as integers.
    /// </summary>
    public struct RectangleI : IBox
    {
        /// <summary>
        /// Creates a rectangle box from a location and size.
        /// </summary>
        /// <param name="x">The x value of the location.</param>
        /// <param name="y">The y value of the location.</param>
        /// <param name="w">The width value of the size.</param>
        /// <param name="h">The height value of the size.</param>
        public RectangleI(int x, int y, int w, int h)
        {
            X = x;
            Y = y;
            Width = w;
            Height = h;
        }
        /// <summary>
        /// Creates a rectangle box from a location and size.
        /// </summary>
        /// <param name="location">The location of the rectangle.</param>
        /// <param name="size">The size of the rectangle.</param>
        public RectangleI(Vector2I location, Vector2I size)
        {
            X = location.X;
            Y = location.Y;
            Width = size.X;
            Height = size.Y;
        }
        /// <summary>
        /// Creates a rectangle box from a <see cref="double"/> based location and size.
        /// </summary>
        /// <param name="x">The x value of the location.</param>
        /// <param name="y">The y value of the location.</param>
        /// <param name="w">The width value of the size.</param>
        /// <param name="h">The height value of the size.</param>
        public RectangleI(double x, double y, double w, double h)
        {
            X = (int)x;
            Y = (int)y;
            Width = (int)w;
            Height = (int)h;
        }
        /// <summary>
        /// Creates a rectangle box from a <see cref="double"/> based location and size.
        /// </summary>
        /// <param name="location">The location of the rectangle.</param>
        /// <param name="size">The size of the rectangle.</param>
        public RectangleI(Vector2 location, Vector2 size)
        {
            X = (int)location.X;
            Y = (int)location.Y;
            Width = (int)size.X;
            Height = (int)size.Y;
        }
        /// <summary>
        /// Creates a rectangle box from an unknown box.
        /// </summary>
        /// <param name="box">The unknown box to reference from.</param>
        public RectangleI(IBox box)
        {
            X = (int)box.Left;
            Y = (int)box.Top;
            Width = (int)box.Width;
            Height = (int)box.Height;
        }

        /// <summary>
        /// The left x location of the box.
        /// </summary>
        public int X { get; set; }
        double IBox.X => X;
        /// <summary>
        /// The top y location of the box.
        /// </summary>
        public int Y { get; set; }
        double IBox.Y => Y;
        /// <summary>
        /// The width of the box.
        /// </summary>
        public int Width { get; set; }
        double IBox.Width => Width;
        /// <summary>
        /// The height of the box.
        /// </summary>
        public int Height { get; set; }
        double IBox.Height => Height;

        /// <summary>
        /// The center location of the box.
        /// </summary>
        public Vector2I Centre
        {
            get => new Vector2I(X + (Width * 0.5), Y - (Height * 0.5));
            set
            {
                X = value.X - (Width / 2);
                Y = value.Y - (Height / 2);
            }
        }
        Vector2 IBox.Centre => new Vector2(X + (Width * 0.5), Y - (Height * 0.5));
        Vector2 IBox.Size => Size;

        /// <summary>
        /// The top-left location of the box.
        /// </summary>
        public Vector2I Location
        {
            get => new Vector2I(X, Y);
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }
        /// <summary>
        /// The width and height of the box.
        /// </summary>
        public Vector2I Size
        {
            get => new Vector2I(Width, Height);
            set
            {
                Width = value.X;
                Height = value.Y;
            }
        }

        /// <summary>
        /// The left side of the box.
        /// </summary>
        public int Left
        {
            get => X;
            set
            {
                Width += X - value;
                if (Width < 0)
                {
                    Width = 0;
                }
                X = value;
            }
        }
        double IBox.Left
        {
            get => X;
            set => Left = (int)value;
        }
        /// <summary>
        /// The right side of the box.
        /// </summary>
        public int Right
        {
            get => X + Width;
            set
            {
                Width = value - X;
                if (Width < 0)
                {
                    Width = 0;
                }
            }
        }
        double IBox.Right
        {
            get => X + Width;
            set => Right = (int)value;
        }
        /// <summary>
        /// The bottom side of the box.
        /// </summary>
        public int Bottom
        {
            get => Y - Height;
            set
            {
                Height = Y - value;
                if (Height < 0)
                {
                    Height = 0;
                }
                Y = value;
            }
        }
        double IBox.Bottom
        {
            get => Y - Height;
            set => Bottom = (int)value;
        }
        /// <summary>
        /// The top side of the box.
        /// </summary>
        public int Top
        {
            get => Y;
            set
            {
                Height += value - Y;
                if (Height < 0)
                {
                    Height = 0;
                }
            }
        }
        double IBox.Top
        {
            get => Y;
            set => Top = (int)value;
        }

#nullable enable
        public override string ToString()
        {
            return $"X:{X}, Y:{X}, Width:{Width}, Height:{Height}";
        }
        public string ToString(string? format)
        {
            return $"X:{X.ToString(format)}, Y:{Y.ToString(format)}, Width:{Width.ToString(format)}, Height:{Height.ToString(format)}";
        }
#nullable disable

        public override bool Equals(object obj)
        {
            return obj is IBox b &&
                    X == b.Left && Width == b.Width &&
                    Y == b.Top && Height == b.Height;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Width, Height);
        }

        public static bool operator ==(RectangleI l, RectangleI r)
        {
            return l.Equals(r);
        }
        public static bool operator !=(RectangleI l, RectangleI r)
        {
            return !l.Equals(r);
        }

        public static explicit operator RectangleI(Box box)
        {
            return new RectangleI(box);
        }
        public static explicit operator RectangleI(Rectangle rect)
        {
            return new RectangleI(rect);
        }

        /// <summary>
        /// A rectangle with <see cref="X"/>, <see cref="Y"/>, <see cref="Width"/> and <see cref="Height"/> set to 0.
        /// </summary>
        public static RectangleI Zero { get; } = new RectangleI(0, 0, 0, 0);
        /// <summary>
        /// A rectangle with a <see cref="Width"/> and <see cref="Height"/> of 1 with the top-left at origin.
        /// </summary>
        public static RectangleI One { get; } = new RectangleI(0, 0, 1, 1);
    }
}
