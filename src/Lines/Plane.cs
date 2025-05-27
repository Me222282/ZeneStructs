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
        /// Create a plane from a triangle.
        /// </summary>
        /// <param name="triangle">The 3 refernece points from which to create the plane.</param>
        public Plane(Triangle3 triangle)
        {
            Normal = (triangle.A - triangle.B).Cross(triangle.C - triangle.B);
            Location = triangle.A;
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
        /// Returns the shortest distance from this line to <paramref name="point"/>.
        /// </summary>
        /// <param name="point">The point to compare.</param>
        /// <returns></returns>
        public floatv DistanceFromPoint(Vector3 point)
        {
            floatv sl = Normal.SquaredLength;
            floatv v = (point - Location).Dot(Normal);

            if (sl == 1d)
            {
                return v;
            }

            return v / Maths.Sqrt(sl);
        }
        /// <summary>
        /// Returns the squared shortest distance from this line to <paramref name="point"/>.
        /// </summary>
        /// <param name="point">The point to compare.</param>
        /// <returns></returns>
        public floatv SquaredDistanceFromPoint(Vector3 point)
        {
            floatv sl = Normal.SquaredLength;
            floatv v = (point - Location).Dot(Normal);
            v *= v;

            if (sl == 1d)
            {
                return v;
            }

            return v / sl;
        }

        /// <summary>
        /// Returns the projection of the point <paramref name="x"/> onto this plane.
        /// </summary>
        /// <param name="x">The point to project.</param>
        /// <returns></returns>
        public Vector3 Project(Vector3 x)
        {
            floatv v = (x - Location).Dot(Normal);
            Vector3 diff = Normal * (v / Normal.SquaredLength);
            return x - diff;
        }
        /// <summary>
        /// Returns the reflection of the point <paramref name="x"/> about this plane.
        /// </summary>
        /// <param name="x">The point to reflect.</param>
        /// <returns></returns>
        public Vector3 Reflect(Vector3 x)
        {
            floatv v = (x - Location).Dot(Normal);
            Vector3 diff = Normal * (v / Normal.SquaredLength);
            return x - (2d * diff);
        }
        /// <summary>
        /// Returns the projection of the line <paramref name="l"/> onto this plane.
        /// </summary>
        /// <param name="l">The line to project.</param>
        /// <returns></returns>
        public Line3 Project(Line3 l)
        {
            Vector3 loc = Project(l.Location);
            Vector3 dir = Normal.Cross(Normal.Cross(l.Direction));
            return new Line3(dir, loc);
        }
        /// <summary>
        /// Returns the reflection of the line <paramref name="l"/> about this plane.
        /// </summary>
        /// <param name="l">The line to reflect.</param>
        /// <returns></returns>
        public Line3 Reflect(Line3 l)
        {
            Vector3 a = Intersects(l);
            Vector3 b = Reflect(a + l.Direction);
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

            floatv d1 = n1.Dot(Location);
            floatv d2 = n2.Dot(p.Location);
            floatv z;

            // annoying
            if ((n1.Y == 0d && n1.Z == 0d) ||
                (n2.Y == 0d && n2.Z == 0d))
            {
                z = ((d1 * n2.X) - (d2 * n1.X)) / ((n1.Z * n2.X) - (n2.Z * n1.X));
                floatv x = (d1 - (z * n1.Z)) / n1.X;

                return new Line3(dir, (x, 0d, z));
            }

            z = ((d1 * n2.Y) - (d2 * n1.Y)) / ((n1.Z * n2.Y) - (n2.Z * n1.Y));
            floatv y = (d1 - (z * n1.Z)) / n1.Y;

            return new Line3(dir, (0d, y, z));
        }
        /// <summary>
        /// Returns the point at which this plane and the line <paramref name="l"/> intersect. If they are parallel, returns <see cref="Vector2.PositiveInfinity"/>.
        /// </summary>
        /// <param name="l">The line to intersect.</param>
        /// <returns></returns>
        public Vector3 Intersects(Line3 l)
        {
            Vector3 loc = l.Location;
            Vector3 dir = l.Direction;
            
            floatv dn = dir.Dot(Normal);
            if (dn == 0d)
            {
                return Vector2.PositiveInfinity;
            }
            floatv t = (Location - loc).Dot(Normal) / dn;
            return loc + (t * dir);
        }

        /// <summary>
        /// Determines whether <paramref name="point"/> exists within this plane.
        /// </summary>
        /// <param name="point">The point to query.</param>
        /// <returns></returns>
        public bool Contains(Vector3 point) => (point - Location).Dot(Normal) == 0d;
        /// <summary>
        /// Determines whether <paramref name="line"/> exists within this plane.
        /// </summary>
        /// <param name="line">The line to query.</param>
        /// <returns></returns>
        public bool Contains(Line3 line) => Contains(line.Location) && line.Direction.Dot(Normal) == 0d;
        
        /// <summary>
        /// Determines whether <paramref name="p"/> represents an equivalent plane to this.
        /// </summary>
        /// <param name="p">The plane to query.</param>
        /// <returns></returns>
        public bool GeometricallyEquals(Plane p)
        {
            if (Normal.Cross(p.Normal) != 0d) { return false; }
            
            floatv d1 = Location.Dot(Normal);
            floatv d2 = p.Location.Dot(Normal);
            
            return d1 == d2;
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
            return obj is Plane plane && this == plane;
        }
        public override int GetHashCode() => HashCode.Combine(Normal, Location);

        public static bool operator ==(Plane l, Plane r)
        {
            return l.Normal == r.Normal &&
                l.Location == r.Location;
        }
        public static bool operator !=(Plane l, Plane r) => !(l == r);
    }
}
