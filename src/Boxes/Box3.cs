//using System;

//namespace Zene.Structs
//{
//    /// <summary>
//    /// A box stored by the <see cref="Left"/>, <see cref="Right"/>, <see cref="Top"/>, <see cref="Bottom"/>, <see cref="Front"/> and <see cref="Back"/> values.
//    /// </summary>
//    public struct Box3 : IBox3
//    {
//        /// <summary>
//        /// Creates a box from its left, right, top, bottom, front and back locations.
//        /// </summary>
//        /// <param name="left">The x loction of the left side.</param>
//        /// <param name="right">The x loction of the right side.</param>
//        /// <param name="top">The y loction of the top side.</param>
//        /// <param name="bottom">The y loction of the bottom side.</param>
//        /// <param name="front">The z loction of the front side.</param>
//        /// <param name="back">The z loction of the back side.</param>
//        public Box3(floatv left, floatv right, floatv top, floatv bottom, floatv front, floatv back)
//        {
//            Left = left;
//            Right = right;
//            Top = top;
//            Bottom = bottom;
//            Front = front;
//            Back = back;
//        }
//        /// <summary>
//        /// Creates a box from a location and size.
//        /// </summary>
//        /// <param name="location">The location of the centre of the box.</param>
//        /// <param name="size">The size of the box.</param>
//        public Box3(Vector3 location, Vector3 size)
//        {
//            Left = 0;
//            Right = 0;
//            Bottom = 0;
//            Top = 0;
//            Front = 0;
//            Back = 0;

//            Location = location;
//            Size = size;
//        }
//        /// <summary>
//        /// Creates a box from an unknown box.
//        /// </summary>
//        /// <param name="box">THe unknown box to reference from.</param>
//        public Box3(IBox3 box)
//        {
//            Left = box.Left;
//            Right = box.Right;
//            Bottom = box.Bottom;
//            Top = box.Top;
//            Front = box.Front;
//            Back = box.Back;
//        }

//        public floatv Left { get; set; }
//        public floatv Right { get; set; }
//        public floatv Bottom { get; set; }
//        public floatv Top { get; set; }
//        public floatv Front { get; set; }
//        public floatv Back { get; set; }

//        /// <summary>
//        /// The center location of the box.
//        /// </summary>
//        public Vector3 Location
//        {
//            get => new Vector3(X, Y, Z);
//            set
//            {
//                X = value.X;
//                Y = value.Y;
//                Z = value.Z;
//            }
//        }
//        /// <summary>
//        /// The width and height of the box.
//        /// </summary>
//        public Vector3 Size
//        {
//            get => new Vector3(Width, Height, Depth);
//            set
//            {
//                Width = value.X;
//                Height = value.Y;
//                Depth = value.Z;
//            }
//        }

//        public Vector3 Centre
//        {
//            get => Location;
//            set => Location = value;
//        }

//        public floatv X
//        {
//            get => Left + (Width * 0.5f);
//            set
//            {
//                floatv offset = value - X;

//                Left += offset;
//                Right += offset;
//            }
//        }
//        public floatv Y
//        {
//            get => Bottom + (Height * 0.5f);
//            set
//            {
//                floatv offset = value - Y;

//                Bottom += offset;
//                Top += offset;
//            }
//        }
//        public floatv Z
//        {
//            get => Front + (Depth * 0.5f);
//            set
//            {
//                floatv offset = value - Z;

//                Front += offset;
//                Depth += offset;
//            }
//        }
//        public floatv Width
//        {
//            get => Right - Left;
//            set
//            {
//                floatv offset = (value - Width) * 0.5f;

//                Left -= offset;
//                Right += offset;
//            }
//        }
//        public floatv Height
//        {
//            get => Top - Bottom;
//            set
//            {
//                floatv offset = (value - Height) * 0.5f;

//                Bottom -= offset;
//                Top += offset;
//            }
//        }
//        public floatv Depth
//        {
//            get => Back - Front;
//            set
//            {
//                floatv offset = (value - Depth) * 0.5f;

//                Front -= offset;
//                Back += offset;
//            }
//        }

//#nullable enable
//        public override string ToString()
//        {
//            return $"Left:{Left}, Right:{Right}, Top:{Top}, Bottom:{Bottom}, Front:{Front}, Back:{Back}";
//        }
//        public string ToString(string? format)
//        {
//            return @$"Left:{Left.ToString(format)}, Right:{Right.ToString(format)}, Top:{Top.ToString(format)}, Bottom:{Bottom.ToString(format)}, Front:{
//                Front.ToString(format)}, Back:{Back.ToString(format)}";
//        }
//#nullable disable

//        public override bool Equals(object obj)
//        {
//            return obj is IBox3 b this == b;
//        }
//        public override int GetHashCode()
//        {
//            return HashCode.Combine(Left, Right, Top, Bottom, Front, Back);
//        }

//        public static bool operator ==(Box3 l, Box3 r)
//        {
//            return l.Left == r.Left && l.Right == r.Right &&
//                l.Top == r.Top && l.Bottom == r.Bottom &&
//                l.Back == r.Back && l.Front == r.Front;
//        }
//        public static bool operator !=(Box3 l, Box3 r) => !(l == r);

//        public static explicit operator Box3(Cuboid box) => new Box3(box);

//        public static Box3 operator *(Box3 box, floatv scale)
//            => new Box3(box.Left * scale, box.Right * scale, box.Top * scale, box.Bottom * scale, box.Front * scale, box.Back * scale);
//        public static Box3 operator /(Box3 box, floatv scale)
//            => new Box3(box.Left / scale, box.Right / scale, box.Top / scale, box.Bottom / scale, box.Front / scale, box.Back / scale);

//        /// <summary>
//        /// A <see cref="Box3"/> that spans from negative to positive infinity.
//        /// </summary>
//        public static Box3 Infinity { get; } = new Box3(floatv.NegativeInfinity, floatv.PositiveInfinity, floatv.PositiveInfinity,
//                                                        floatv.NegativeInfinity, floatv.NegativeInfinity, floatv.PositiveInfinity);
//        /// <summary>
//        /// A <see cref="Box3"/> with <see cref="Left"/>, <see cref="Right"/>, <see cref="Top">, <see cref="Bottom"/>, <see cref="Front"/> and <see cref="Back"/> all set to 0.
//        /// </summary>
//        public static Box3 Zero { get; } = new Box3(0, 0, 0, 0, 0, 0);
//        /// <summary>
//        /// A <see cref="Box3"/> with a <see cref="Width"/>, <see cref="Height"/> and <see cref="Depth"/> of 1 centred around origin.
//        /// </summary>
//        public static Box3 One { get; } = new Box3(-0.5f, 0.5f, 0.5f, -0.5f, -0.5f, 0.5f);
//    }
//}
