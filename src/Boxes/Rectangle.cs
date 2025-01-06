using System;

namespace Zene.Structs
{
    /// <summary>
    /// A box stored by the <see cref="X"/>, <see cref="Y"/>, <see cref="Width"/> and <see cref="Height"/> values.
    /// </summary>
    public struct Rectangle : IBox
    {
        /// <summary>
        /// Creates a rectangle box from a location and size.
        /// </summary>
        /// <param name="x">The x value of the location.</param>
        /// <param name="y">The y value of the location.</param>
        /// <param name="w">The width value of the size.</param>
        /// <param name="h">The height value of the size.</param>
        public Rectangle(floatv x, floatv y, floatv w, floatv h)
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
        public Rectangle(Vector2 location, Vector2 size)
        {
            X = location.X;
            Y = location.Y;
            Width = size.X;
            Height = size.Y;
        }
        /// <summary>
        /// Creates a rectangle box from an unknown box.
        /// </summary>
        /// <param name="box">The unknown box to reference from.</param>
        public Rectangle(IBox box)
        {
            X = box.Left;
            Y = box.Top;
            Width = box.Width;
            Height = box.Height;
        }

        /// <summary>
        /// The left x location of the box.
        /// </summary>
        public floatv X { get; set; }
        /// <summary>
        /// The top y location of the box.
        /// </summary>
        public floatv Y { get; set; }
        public floatv Width { get; set; }
        public floatv Height { get; set; }

        public Vector2 Centre
        {
            get => new Vector2(X + (Width * 0.5f), Y - (Height * 0.5f));
            set
            {
                X = value.X - (Width * 0.5f);
                Y = value.Y - (Height * 0.5f);
            }
        }

        /// <summary>
        /// The top-left location of the box.
        /// </summary>
        public Vector2 Location
        {
            get => new Vector2(X, Y);
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
            get => new Vector2(Width, Height);
            set
            {
                Width = value.X;
                Height = value.Y;
            }
        }

        public floatv Left
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
        public floatv Right
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
        public floatv Bottom
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
        public floatv Top
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

        public static bool operator ==(Rectangle l, Rectangle r) => l.Equals(r);
        public static bool operator !=(Rectangle l, Rectangle r) => !l.Equals(r);

        public static Rectangle operator *(Rectangle box, floatv scale) => new Rectangle(box.X * scale, box.Y * scale, box.Width * scale, box.Height * scale);
        public static Rectangle operator /(Rectangle box, floatv scale) => new Rectangle(box.X / scale, box.Y / scale, box.Width / scale, box.Height / scale);

        public static explicit operator Rectangle(Box box) => new Rectangle(box);

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
