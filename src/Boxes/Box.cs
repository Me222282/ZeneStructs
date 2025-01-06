using System;

namespace Zene.Structs
{
    /// <summary>
    /// A box stored by the <see cref="Left"/>, <see cref="Right"/>, <see cref="Top"/> and <see cref="Bottom"/> values.
    /// </summary>
    public struct Box : IBox
    {
        /// <summary>
        /// Creates a box from its left, right, top and bottom locations.
        /// </summary>
        /// <param name="l">The x loction of the left side.</param>
        /// <param name="r">The x loction of the right side.</param>
        /// <param name="t">The y loction of the top side.</param>
        /// <param name="b">The y loction of the bottom side.</param>
        public Box(floatv l, floatv r, floatv t, floatv b)
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
        public Box(Vector2 location, Vector2 size)
        {
            Left = 0;
            Right = 0;
            Bottom = 0;
            Top = 0;

            Location = location;
            Size = size;
        }
        /// <summary>
        /// Creates a box from an unknown box.
        /// </summary>
        /// <param name="box">THe unknown box to reference from.</param>
        public Box(IBox box)
        {
            Left = box.Left;
            Right = box.Right;
            Bottom = box.Bottom;
            Top = box.Top;
        }

        public floatv Left { get; set; }
        public floatv Right { get; set; }
        public floatv Bottom { get; set; }
        public floatv Top { get; set; }

        /// <summary>
        /// The center location of the box.
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

        public Vector2 Centre
        {
            get => Location;
            set => Location = value;
        }

        public floatv X
        {
            get => Left + (Width * 0.5f);
            set
            {
                floatv offset = value - X;

                Left += offset;
                Right += offset;
            }
        }
        public floatv Y
        {
            get => Bottom + (Height * 0.5f);
            set
            {
                floatv offset = value - Y;

                Bottom += offset;
                Top += offset;
            }
        }
        public floatv Width
        {
            get => Right - Left;
            set
            {
                floatv offset = (value - Width) * 0.5f;

                Left -= offset;
                Right += offset;
            }
        }
        public floatv Height
        {
            get => Top - Bottom;
            set
            {
                floatv offset = (value - Height) * 0.5f;

                Bottom -= offset;
                Top += offset;
            }
        }

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

#nullable enable
        public override string ToString()
        {
            return $"Left:{Left}, Right:{Right}, Top:{Top}, Bottom:{Bottom}";
        }
        public string ToString(string? format)
        {
            return $"Left:{Left.ToString(format)}, Right:{Right.ToString(format)}, Top:{Top.ToString(format)}, Bottom:{Bottom.ToString(format)}";
        }
#nullable disable

        public override bool Equals(object obj)
        {
            return obj is IBox b &&
                    Left == b.Left && Right == b.Right &&
                    Top == b.Top && Bottom == b.Bottom;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Left, Right, Top, Bottom);
        }

        public static bool operator ==(Box l, Box r) => l.Equals(r);
        public static bool operator !=(Box l, Box r) => !l.Equals(r);

        public static Box operator *(Box box, floatv scale) => new Box(box.Left * scale, box.Right * scale, box.Top * scale, box.Bottom * scale);
        public static Box operator /(Box box, floatv scale) => new Box(box.Left / scale, box.Right / scale, box.Top / scale, box.Bottom / scale);

        public static explicit operator Box(Rectangle box) => new Box(box);

        /// <summary>
        /// A <see cref="Box"/> that spans from negative to positive infinity.
        /// </summary>
        public static Box Infinity { get; } = new Box(floatv.NegativeInfinity, floatv.PositiveInfinity, floatv.PositiveInfinity, floatv.NegativeInfinity);
        /// <summary>
        /// A <see cref="Box"/> with <see cref="Left"/>, <see cref="Right"/>, <see cref="Top"/> and <see cref="Bottom"/> all set to 0.
        /// </summary>
        public static Box Zero { get; } = new Box(0, 0, 0, 0);
        /// <summary>
        /// A <see cref="Box"/> with a <see cref="Width"/> and <see cref="Height"/> of 1 centred around origin.
        /// </summary>
        public static Box One { get; } = new Box(-0.5f, 0.5f, 0.5f, -0.5f);
    }
}
