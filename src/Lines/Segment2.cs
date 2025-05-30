﻿using System;
using System.Reflection.Metadata.Ecma335;

namespace Zene.Structs
{
    /// <summary>
    /// Defines a line segment as two points in 2 dimensional space.
    /// </summary>
    public struct Segment2
    {
        /// <summary>
        /// Creates a segment from two points.
        /// </summary>
        /// <param name="a">The first point to reference.</param>
        /// <param name="b">The second point to reference.</param>
        public Segment2(Vector2 a, Vector2 b)
        {
            A = a;
            B = b;
        }
        /// <summary>
        /// Creates a segment from two points.
        /// </summary>
        /// <param name="aX">The x value of the first point to reference.</param>
        /// <param name="aY">The y value of the first point to reference.</param>
        /// <param name="bX">The x value of the second point to reference.</param>
        /// <param name="bY">The y value of the second point to reference.</param>
        public Segment2(floatv aX, floatv aY, floatv bX, floatv bY)
        {
            A = new Vector2(aX, aY);
            B = new Vector2(bX, bY);
        }
        /// <summary>
        /// Creates a segment from a line and distance.
        /// </summary>
        /// <remarks>
        /// Uses the reference point of the line as the value of <see cref="A"/>.
        /// </remarks>
        /// <param name="l">The line to use as a reference.</param>
        /// <param name="distance">THe distance along the line to bee used as the segment.</param>
        public Segment2(Line2 l, floatv distance)
        {
            A = l.Location;
            B = l.Location + (l.Direction * distance);
        }

        /// <summary>
        /// The first point in space.
        /// </summary>
        public Vector2 A { get; set; }
        /// <summary>
        /// The second point in space.
        /// </summary>
        public Vector2 B { get; set; }

        /// <summary>
        /// The x distance and y distance between points <see cref="A"/> and <see cref="B"/>.
        /// </summary>
        public Vector2 Change => B - A;

        /// <summary>
        /// Determines whether two line segments would intersect and outputs the intersection point.
        /// </summary>
        /// <param name="seg">The line segment to compare to.</param>
        /// <param name="intersection">The point at which the two lines will intersect. If false, this is <see cref="Vector2.Zero"/>.</param>
        public bool Intersects(Segment2 seg, out Vector2 intersection)
        {
            intersection = Vector2.Zero;

            Vector2 b = Change;
            Vector2 d = seg.Change;

            floatv pDot = b.PerpDot(d);

            // If b dot d == 0, it means the lines are parallel
            if (pDot == 0) { return false; }
            
            floatv div = 1 / pDot;
            
            Vector2 c = seg.A - A;
            floatv t = c.PerpDot(d) * div;
            if (t < 0 || t > 1) { return false; }

            floatv u = c.PerpDot(b) * div;
            if (u < 0 || u > 1) { return false; }

            intersection = A + (t * b);

            return true;
        }

        /// <summary>
        /// The smallest box that can fit around this segment.
        /// </summary>
        public Bounds Bounds
        {
            get => new Bounds(
                    A.X < B.X ? A.X : B.X,
                    A.X > B.X ? A.X : B.X,
                    A.Y > B.Y ? A.Y : B.Y,
                    A.Y < B.Y ? A.Y : B.Y);
        }

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
            return obj is Segment2 seg && this == seg;
        }
        public override int GetHashCode() => HashCode.Combine(A, B);

        public static bool operator ==(Segment2 l,Segment2 r)
        {
            return l.A == r.A &&
                l.B == r.B;
        }
        public static bool operator !=(Segment2 l, Segment2 r) => !(l == r);
    }
}
