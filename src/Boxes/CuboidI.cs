﻿//using System;

//namespace Zene.Structs
//{
//    /// <summary>
//    /// A box stored by the <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Width"/>, <see cref="Height"/> and <see cref="Depth"/> values as integers.
//    /// </summary>
//    public struct CuboidI : IBox3
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
//        public CuboidI(int x, int y, int z, int w, int h, int d)
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
//        public CuboidI(Vector3I location, Vector3I size)
//        {
//            X = location.X;
//            Y = location.Y;
//            Z = location.Z;
//            Width = size.X;
//            Height = size.Y;
//            Depth = size.Z;
//        }
//        /// <summary>
//        /// Creates a cuboid box from a <see cref="floatv"/> based location and size.
//        /// </summary>
//        /// <param name="x">The x value of the location.</param>
//        /// <param name="y">The y value of the location.</param>
//        /// <param name="z">The y value of the location.</param>
//        /// <param name="w">The width value of the size.</param>
//        /// <param name="h">The height value of the size.</param>
//        /// <param name="d">The depth value of the size.</param>
//        public CuboidI(floatv x, floatv y, floatv z, floatv w, floatv h, floatv d)
//        {
//            X = (int)x;
//            Y = (int)y;
//            Z = (int)z;
//            Width = (int)w;
//            Height = (int)h;
//            Depth = (int)d;
//        }
//        /// <summary>
//        /// Creates a cuboid box from a <see cref="floatv"/> based location and size.
//        /// </summary>
//        /// <param name="location">The location of the rectangle.</param>
//        /// <param name="size">The size of the rectangle.</param>
//        public CuboidI(Vector3 location, Vector3 size)
//        {
//            X = (int)location.X;
//            Y = (int)location.Y;
//            Z = (int)location.Z;
//            Width = (int)size.X;
//            Height = (int)size.Y;
//            Depth = (int)size.Z;
//        }
//        /// <summary>
//        /// Creates a cuboid box from an unknown box.
//        /// </summary>
//        /// <param name="box">The unknown box to reference from.</param>
//        public CuboidI(IBox3 box)
//        {
//            X = (int)box.Left;
//            Y = (int)box.Top;
//            Z = (int)box.Front;
//            Width = (int)box.Width;
//            Height = (int)box.Height;
//            Depth = (int)box.Depth;
//        }

//        /// <summary>
//        /// The left x location of the box.
//        /// </summary>
//        public int X { get; set; }
//        floatv IBox.X => X;
//        /// <summary>
//        /// The top y location of the box.
//        /// </summary>
//        public int Y { get; set; }
//        floatv IBox.Y => Y;
//        /// <summary>
//        /// The front z location of the box.
//        /// </summary>
//        public int Z { get; set; }
//        floatv IBox3.Z => Z;
//        public int Width { get; set; }
//        floatv IBox.Width => Width;
//        public int Height { get; set; }
//        floatv IBox.Height => Height;
//        public int Depth { get; set; }
//        floatv IBox3.Depth => Depth;

//        public Vector3I Centre => new Vector3I(X + (Width * 0.5f), Y - (Height * 0.5f), Z + (Depth * 0.5f));
//        Vector3 IBox3.Centre => new Vector3(X + (Width * 0.5), Y - (Height * 0.5), Z + (Depth * 0.5));
//        Vector3 IBox3.Size => new Vector3(Width, Height, Depth);

//        /// <summary>
//        /// The top-left-front location of the box.
//        /// </summary>
//        public Vector3I Location
//        {
//            get => new Vector3I(X, Y, Z);
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
//        public Vector3I Size
//        {
//            get => new Vector3I(Width, Height, Depth);
//            set
//            {
//                Width = value.X;
//                Height = value.Y;
//                Depth = value.Z;
//            }
//        }

//        public int Left
//        {
//            get => X;
//            set
//            {
//                Width += X - value;
//                X = value;
//            }
//        }
//        floatv IBox.Left
//        {
//            get => X;
//            set => Left = (int)value;
//        }
//        public int Right
//        {
//            get => X + Width;
//            set => Width = value - X;
//        }
//        floatv IBox.Right
//        {
//            get => X + Width;
//            set => Right = (int)value;
//        }
//        public int Bottom
//        {
//            get => Y - Height;
//            set
//            {
//                Height = Y - value;
//                Y = value;
//            }
//        }
//        floatv IBox.Bottom
//        {
//            get => Y - Height;
//            set => Bottom = (int)value;
//        }
//        public int Top
//        {
//            get => Y;
//            set => Height += value - Y;
//        }
//        floatv IBox.Top
//        {
//            get => Y;
//            set => Top = (int)value;
//        }
//        public int Front
//        {
//            get => Z;
//            set
//            {
//                Depth += value - Y;
//                Z = value;
//            }
//        }
//        floatv IBox3.Front
//        {
//            get => Z;
//            set => Front = (int)value;
//        }
//        public int Back
//        {
//            get => Z + Depth;
//            set => Depth = value - Z;
//        }
//        floatv IBox3.Back
//        {
//            get => Z + Depth;
//            set => Back = (int)value;
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
//            return obj is IBox3 b && this == b;
//        }
//        public override int GetHashCode() => HashCode.Combine(X, Y, Z, Width, Height, Depth);

//        public static bool operator ==(CuboidI l, CuboidI r)
//        {
//            return l.X == r.Left && l.Width == r.Width &&
//                l.Y == r.Top && l.Height == r.Height &&
//                l.Z == r.Front && l.Depth == r.Depth;
//        }
//        public static bool operator !=(CuboidI l, CuboidI r) => !(l == r);

//        public static CuboidI operator *(CuboidI box, int scale)
//            => new CuboidI(box.X * scale, box.Y * scale, box.Z * scale, box.Width * scale, box.Height * scale, box.Depth * scale);
//        public static CuboidI operator /(CuboidI box, int scale)
//            => new CuboidI(box.X / scale, box.Y / scale, box.Z / scale, box.Width / scale, box.Height / scale, box.Depth / scale);

//        public static explicit operator CuboidI(Box3 box) => new CuboidI(box);
//        public static explicit operator CuboidI(Cuboid rect) => new CuboidI(rect);

//        /// <summary>
//        /// A <see cref="CuboidI"/> with <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Width"/>, <see cref="Height"/> and <see cref="Depth"/> all set to 0.
//        /// </summary>
//        public static CuboidI Zero { get; } = new CuboidI(0, 0, 0, 0, 0, 0);
//        /// <summary>
//        /// A <see cref="CuboidI"/> with a <see cref="Width"/>, <see cref="Height"/> and <see cref="Depth"/> of 1 with the top-front-left at origin.
//        /// </summary>
//        public static CuboidI One { get; } = new CuboidI(0, 0, 0, 1, 1, 1);
//    }
//}
