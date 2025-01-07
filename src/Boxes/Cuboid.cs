//using System;

//namespace Zene.Structs
//{
//    /// <summary>
//    /// A box stored by the <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Width"/>, <see cref="Height"/> and <see cref="Depth"/> values.
//    /// </summary>
//    public struct Cuboid : IBox3
//    {
//        /// <summary>
//        /// Creates a cuboid box from a location and size.
//        /// </summary>
//        /// <param name="x">The x value of the location.</param>
//        /// <param name="y">The y value of the location.</param>
//        /// <param name="z">The y value of the location.</param>
//        /// <param name="w">The width value of the size.</param>
//        /// <param name="h">The height value of the size.</param>
//        /// <param name="d">The depth value of the size.</param>
//        public Cuboid(floatv x, floatv y, floatv z, floatv w, floatv h, floatv d)
//        {
//            X = x;
//            Y = y;
//            Z = z;
//            Width = w;
//            Height = h;
//            Depth = d;
//        }
//        /// <summary>
//        /// Creates a cuboid box from a location and size.
//        /// </summary>
//        /// <param name="location">The location of the rectangle.</param>
//        /// <param name="size">The size of the rectangle.</param>
//        public Cuboid(Vector3 location, Vector3 size)
//        {
//            X = location.X;
//            Y = location.Y;
//            Z = location.Z;
//            Width = size.X;
//            Height = size.Y;
//            Depth = size.Z;
//        }
//        /// <summary>
//        /// Creates a cuboid box from an unknown box.
//        /// </summary>
//        /// <param name="box">The unknown box to reference from.</param>
//        public Cuboid(IBox3 box)
//        {
//            X = box.Left;
//            Y = box.Top;
//            Z = box.Front;
//            Width = box.Width;
//            Height = box.Height;
//            Depth = box.Depth;
//        }

//        /// <summary>
//        /// The left x location of the box.
//        /// </summary>
//        public floatv X { get; set; }
//        /// <summary>
//        /// The top y location of the box.
//        /// </summary>
//        public floatv Y { get; set; }
//        /// <summary>
//        /// The front z location of the box.
//        /// </summary>
//        public floatv Z { get; set; }
//        public floatv Width { get; set; }
//        public floatv Height { get; set; }
//        public floatv Depth { get; set; }

//        public Vector3 Centre => new Vector3(X + (Width * 0.5f), Y - (Height * 0.5f), Z + (Depth * 0.5f));

//        /// <summary>
//        /// The top-left-front location of the box.
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

//        public floatv Left
//        {
//            get => X;
//            set
//            {
//                Width += X - value;
//                X = value;
//            }
//        }
//        public floatv Right
//        {
//            get => X + Width;
//            set => Width = value - X;
//        }
//        public floatv Bottom
//        {
//            get => Y - Height;
//            set
//            {
//                Height = Y - value;
//                Y = value;
//            }
//        }
//        public floatv Top
//        {
//            get => Y;
//            set => Height += value - Y;
//        }
//        public floatv Front
//        {
//            get => Z;
//            set
//            {
//                Depth += value - Y;
//                Z = value;
//            }
//        }
//        public floatv Back
//        {
//            get => Z + Depth;
//            set => Depth = value - Z;
//        }

//#nullable enable
//        public override string ToString()
//        {
//            return $"X:{X}, Y:{X}, Z:{Z}, Width:{Width}, Height:{Height}, Depth:{Depth}";
//        }
//        public string ToString(string? format)
//        {
//            return @$"X:{X.ToString(format)}, Y:{Y.ToString(format)}, Z:{Z.ToString(format)}, Width:{Width.ToString(format)}, Height:{
//                Height.ToString(format)}, Depth:{Depth.ToString(format)}";
//        }
//#nullable disable

//        public override bool Equals(object obj)
//        {
//            return obj is IBox3 b &&
//                    X == b.Left && Width == b.Width &&
//                    Y == b.Top && Height == b.Height &&
//                    Z == b.Front && Depth == b.Depth;
//        }
//        public override int GetHashCode() => HashCode.Combine(X, Y, Z, Width, Height, Depth);

//        public static bool operator ==(Cuboid l, Cuboid r) => l.Equals(r);
//        public static bool operator !=(Cuboid l, Cuboid r) => !l.Equals(r);

//        public static Cuboid operator *(Cuboid box, floatv scale)
//            => new Cuboid(box.X * scale, box.Y * scale, box.Z * scale, box.Width * scale, box.Height * scale, box.Depth * scale);
//        public static Cuboid operator /(Cuboid box, floatv scale)
//            => new Cuboid(box.X / scale, box.Y / scale, box.Z / scale, box.Width / scale, box.Height / scale, box.Depth / scale);

//        public static explicit operator Cuboid(Box3 box) => new Cuboid(box);

//        /// <summary>
//        /// A <see cref="Cuboid"/> with <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Width"/>, <see cref="Height"/> and <see cref="Depth"/> all set to 0.
//        /// </summary>
//        public static Cuboid Zero { get; } = new Cuboid(0, 0, 0, 0, 0, 0);
//        /// <summary>
//        /// A <see cref="Cuboid"/> with a <see cref="Width"/>, <see cref="Height"/> and <see cref="Depth"/> of 1 centred around origin.
//        /// </summary>
//        public static Cuboid One { get; } = new Cuboid(-0.5f, 0.5f, -0.5f, 1, 1, 1);
//    }
//}
