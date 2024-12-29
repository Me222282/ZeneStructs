using System;

namespace Zene.Structs
{
    /// <summary>
    /// Defines an infinite plane as a point and normal in 3 dimensional space.
    /// </summary>
    public struct Plane
    {
        /// <summary>
        /// Create a plane from a position and normal.
        /// </summary>
        /// <param name="normal">The normal of the plane.</param>
        /// <param name="location">The reference location for the plane.</param>
        public Plane(Vector3 normal, Vector3 location)
        {
            Normal = normal;
            Location = location;
        }
        /// <summary>
        /// Create a plane from a position and 2 direction vectors.
        /// </summary>
        /// <param name="dir1">Direction vector 1.</param>
        /// <param name="dir2">Direction vector 2.</param>
        /// <param name="location">The reference location for the plane.</param>
        public Plane(Vector3 dir1, Vector3 dir2, Vector3 location)
        {
            Normal = dir1.Cross(dir2);
            Location = location;
        }

        /// <summary>
        /// The normal of the plane.
        /// </summary>
        public Vector3 Normal { get; set; }
        /// <summary>
        /// A point along the plane to define is position in space.
        /// </summary>
        public Vector3 Location { get; set; }

        /// <summary>
        /// Normalise the normal of this plane.
        /// </summary>
        public void Normalise() => Normal.Normalise();

        /// <summary>
        /// Returns the shortest distance from this line to <see cref="point"/>.
        /// </summary>
        /// <param name="point">The point to compare.</param>
        /// <returns></returns>
        public double DistanceFromPoint(Vector3 point)
        {
            double sl = Normal.SquaredLength;
            double v = (point - Location).Dot(Normal);

            if (sl == 1d)
            {
                return v;
            }

            return v / Math.Sqrt(sl);
        }
        /// <summary>
        /// Returns the squared shortest distance from this line to <see cref="point"/>.
        /// </summary>
        /// <param name="point">The point to compare.</param>
        /// <returns></returns>
        public double SquaredDistanceFromPoint(Vector3 point)
        {
            double sl = Normal.SquaredLength;
            double v = (point - Location).Dot(Normal);
            v *= v;

            if (sl == 1d)
            {
                return v;
            }

            return v / sl;
        }

        /// <summary>
        /// Returns the projection of the point <see cref="x"/> onto this plane.
        /// </summary>
        /// <param name="x">The point to project.</param>
        /// <returns></returns>
        public Vector3 Project(Vector3 x)
        {
            double v = (x - Location).Dot(Normal);
            Vector3 diff = (v * Normal) / Normal.SquaredLength;
            return x - diff;
        }
        /// <summary>
        /// Returns the reflection of the point <see cref="x"/> about this plane.
        /// </summary>
        /// <param name="x">The point to reflect.</param>
        /// <returns></returns>
        public Vector3 Reflect(Vector3 x)
        {
            double v = (x - Location).Dot(Normal);
            Vector3 diff = (v * Normal) / Normal.SquaredLength;
            return x - (2d * diff);
        }
        /// <summary>
        /// Returns the projection of the line <see cref="l"/> onto this plane.
        /// </summary>
        /// <param name="l">The line to project.</param>
        /// <returns></returns>
        public Line3 Project(Line3 l)
        {
            Vector3 loc = Project(l.Location);
            Vector3 dir = Normal.Cross(Normal.Cross(l.Direction));
            return new Line3(dir, loc);
            // Vector3 b = Project(l.Location + l.Direction);
            // return new Line3(b - loc, loc);
        }
        /// <summary>
        /// Returns the reflection of the line <see cref="l"/> about this plane.
        /// </summary>
        /// <param name="l">The line to reflect.</param>
        /// <returns></returns>
        public Line3 Reflect(Line3 l)
        {
            Vector3 a = Intersects(l);
            Vector3 b;
            if (a == l.Location)
            {
                b = Reflect(a + l.Direction);
            }
            else
            {
                b = Reflect(l.Location);
            }

            return new Line3(b - a, a);
        }

        /// <summary>
        /// Returns the line with which two planes intersect. If they are parallel, returns <see cref="Vector2.PositiveInfinity"/> for both components.
        /// </summary>
        /// <param name="p">The plane to intersect.</param>
        /// <returns></returns>
        public Line3 Intersects(Plane p)
        {
            Vector3 n1 = Normal;
            Vector3 n2 = p.Normal;

            Vector3 dir = n1.Cross(n2);

            // equals 0 means planes are parallel
            if (dir == Vector3.Zero)
            {
                return new Line3(Vector3.PositiveInfinity, Vector3.PositiveInfinity);
            }

            double d1 = n1.Dot(Location);
            double d2 = n2.Dot(p.Location);
            double z;

            // annoying
            if ((n1.Y == 0d && n1.Z == 0d) ||
                (n2.Y == 0d && n2.Z == 0d))
            {
                z = ((d1 * n2.X) - (d2 * n1.X)) / ((n1.Z * n2.X) - (n2.Z * n1.X));
                double x = (d1 - (z * n1.Z)) / n1.X;

                return new Line3(dir, (x, 0d, z));
            }

            z = ((d1 * n2.Y) - (d2 * n1.Y)) / ((n1.Z * n2.Y) - (n2.Z * n1.Y));
            double y = (d1 - (z * n1.Z)) / n1.Y;

            return new Line3(dir, (0d, y, z));
        }
        /// <summary>
        /// Returns the point at which this plane and the line <see cref="l"/> intersect. If they are parallel, returns <see cref="Vector2.PositiveInfinity"/>.
        /// </summary>
        /// <param name="l">The line to intersect.</param>
        /// <returns></returns>
        public Vector3 Intersects(Line3 l)
        {
            Vector3 loc = l.Location;
            Vector3 dir = l.Direction;
            double t = (Location - loc).Dot(Normal) / dir.Dot(Normal);

            if (double.IsInfinity(t))
            {
                return Vector2.PositiveInfinity;
            }

            return loc + (t * dir);
        }

#nullable enable
        public override string ToString()
        {
            return $"{Normal.X}x + {Normal.Y}y + {Normal.Z}z = {Normal.Dot(Location)}";
        }
        public string ToString(string? format)
        {
            return $"{Normal.X.ToString(format)}x + {Normal.Y.ToString(format)}y + {Normal.Z.ToString(format)}z = {Normal.Dot(Location).ToString(format)}";
        }
#nullable disable

        public override bool Equals(object obj)
        {
            return obj is Plane plane &&
                Normal == plane.Normal &&
                Location == plane.Location;
        }
        public override int GetHashCode() => HashCode.Combine(Normal, Location);

        public static bool operator ==(Plane l, Plane r) => l.Equals(r);
        public static bool operator !=(Plane l, Plane r) => !l.Equals(r);
    }
}
