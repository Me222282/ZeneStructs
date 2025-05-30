﻿using System;

namespace Zene.Structs
{
    /// <summary>
    /// Defines a line segment as two points in 3 dimensional space.
    /// </summary>
    public struct Segment3
    {
        /// <summary>
        /// Creates a segment from two points.
        /// </summary>
        /// <param name="a">The first point to reference.</param>
        /// <param name="b">The second point to reference.</param>
        public Segment3(Vector3 a, Vector3 b)
        {
            A = a;
            B = b;
        }
        /// <summary>
        /// Creates a segment from two points.
        /// </summary>
        /// <param name="aX">The x value of the first point to reference.</param>
        /// <param name="aY">The y value of the first point to reference.</param>
        /// <param name="aZ">The z value of the first point to reference.</param>
        /// <param name="bX">The x value of the second point to reference.</param>
        /// <param name="bY">The y value of the second point to reference.</param>
        /// <param name="bZ">The z value of the second point to reference.</param>
        public Segment3(floatv aX, floatv aY, floatv aZ, floatv bX, floatv bY, floatv bZ)
        {
            A = new Vector3(aX, aY, aZ);
            B = new Vector3(bX, bY, bZ);
        }
        /// <summary>
        /// Creates a segment from a line and distance.
        /// </summary>
        /// <remarks>
        /// Uses the reference point of the line as the value of <see cref="A"/>.
        /// </remarks>
        /// <param name="l">The line to use as a reference.</param>
        /// <param name="distance">THe distance along the line to bee used as the segment.</param>
        public Segment3(Line3 l, floatv distance)
        {
            A = l.Location;
            B = l.Location + (l.Direction * distance);
        }

        /// <summary>
        /// The first point in space.
        /// </summary>
        public Vector3 A { get; set; }
        /// <summary>
        /// The second point in space.
        /// </summary>
        public Vector3 B { get; set; }

        /// <summary>
        /// The x distance and y distance between points <see cref="A"/> and <see cref="B"/>.
        /// </summary>
        public Vector3 Change => B - A;
        ///// <summary>
        ///// The smallest box that can fit around this segment.
        ///// </summary>
        //public Box3 Bounds
        //{
        //    get => new Box3(
        //            A.X < B.X ? A.X : B.X,
        //            A.X > B.X ? A.X : B.X,
        //            A.Y > B.Y ? A.Y : B.Y,
        //            A.Y < B.Y ? A.Y : B.Y,
        //            A.Z < B.Z ? A.Z : B.Z,
        //            A.Z > B.Z ? A.Z : B.Z);
        //}

#nullable enable
        public override string ToString()
        {
            return $"A:{A}, B:{B}";
        }
        public string ToString(string? format)
        {
            return $"A:{A.ToString(format)}, B:{B.ToString(format)}";
        }
#nullable disable

        public override bool Equals(object obj)
        {
            return obj is Segment3 seg && this == seg;
        }
        public override int GetHashCode() => HashCode.Combine(A, B);

        public static bool operator ==(Segment3 l, Segment3 r)
        {
            return l.A == r.A &&
                l.B == r.B;
        }
        public static bool operator !=(Segment3 l, Segment3 r) => !(l == r);
    }
}
