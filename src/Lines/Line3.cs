using System;

namespace Zene.Structs
{
    /// <summary>
    /// Defines an infinite line as a point and direction in 3 dimensional space.
    /// </summary>
    public struct Line3
    {
        /// <summary>
        /// Create a line from a position and direction.
        /// </summary>
        /// <param name="dir">The direction of the line.</param>
        /// <param name="loc">The reference location for the line.</param>
        public Line3(Vector3 dir, Vector3 loc)
        {
            _direction = dir;
            Location = loc;
        }
        /// <summary>
        /// Create a line from a position and direction.
        /// </summary>
        /// <param name="dirX">The x value for the direction of the line.</param>
        /// <param name="dirY">The y value for the direction of the line.</param>
        /// <param name="dirZ">The z value for the direction of the line.</param>
        /// <param name="locX">The x value for the reference location for the line.</param>
        /// <param name="locY">The y value for the reference location for the line.</param>
        /// <param name="locZ">The z value for the reference location for the line.</param>
        public Line3(double dirX, double dirY, double dirZ, double locX, double locY, double locZ)
        {
            _direction = new Vector3(dirX, dirY, dirZ);
            Location = new Vector3(locX, locY, locZ);
        }
        /// <summary>
        /// Creates a line based off a segment.
        /// </summary>
        /// <param name="seg">The segment to reference from.</param>
        public Line3(Segment3 seg)
        {
            Location = seg.A;
            _direction = seg.Change;
        }
        /// <summary>
        /// Creates a line based off a segment defined as a tuple.
        /// </summary>
        /// <param name="seg">The segment tuple to reference from.</param>
        public Line3(Tuple<Vector3, Vector3> seg)
        {
            Location = seg.Item1;
            _direction = (seg.Item2 - seg.Item1);
        }

        private Vector3 _direction;
        /// <summary>
        /// The direction of the line.
        /// </summary>
        public Vector3 Direction
        {
            get => _direction;
            set
            {
                _direction = value;
            }
        }
        /// <summary>
        /// A point along the line to define is position in space.
        /// </summary>
        public Vector3 Location { get; set; }

        /// <summary>
        /// Normalise the direction component of this line.
        /// </summary>
        public void Normalise() => _direction.Normalise();

        /// <summary>
        /// Returns the reflection of the point <paramref name="x"/> about this line.
        /// </summary>
        /// <param name="x">The point to reflect.</param>
        /// <returns></returns>
        public Vector3 Reflect(Vector3 x)
        {
            Vector3 dir = Direction;
            
            double t = (x - Location).Dot(dir) / dir.SquaredLength;
            return (2d * (Location + t * dir)) - x;
        }
        /// <summary>
        /// Returns the projection of the point <paramref name="x"/> onto this line.
        /// </summary>
        /// <param name="x">The point to project.</param>
        /// <returns></returns>
        public Vector3 Project(Vector3 x)
        {
            Vector3 dir = Direction;
            double t = (x - Location).Dot(dir) / dir.SquaredLength;
            return Location + t * dir;
        }

        /// <summary>
        /// Returns the shortest distance from this line to <paramref name="line"/>.
        /// </summary>
        /// <param name="line">The line to compare.</param>
        /// <returns></returns>
        public double DistanceFromLine(Line3 line)
        {
            Vector3 x = Direction.Cross(line.Direction);

            return Math.Abs(x.Dot(Location.Cross(line.Location)) / x.Length);
        }
        /// <summary>
        /// Returns the squared shortest distance from this line to <paramref name="line"/>.
        /// </summary>
        /// <param name="line">The line to compare.</param>
        /// <returns></returns>
        public double SquaredDistanceFromLine(Line3 line)
        {
            Vector3 x = Direction.Cross(line.Direction);
            double y = x.Dot(Location.Cross(line.Location));

            return (y * y) / x.SquaredLength;
        }

        /// <summary>
        /// Determines whether <paramref name="point"/> exists within this line.
        /// </summary>
        /// <param name="point">The point to query</param>
        /// <returns></returns>
        public bool Contains(Vector3 point) => (point - Location).Cross(Direction) == 0d;

        /// <summary>
        /// Gets the x component of the point along the line with the y component of <paramref name="y"/>.
        /// </summary>
        public double GetXFromY(double y)
        {
            // Line is straight
            if (_direction.X == 0)
            {
                return Location.X;
            }

            return Location.X + ((_direction.X / _direction.Y) * (y - Location.Y));
        }
        /// <summary>
        /// Gets the x component of the point along the line with the z component of <paramref name="z"/>.
        /// </summary>
        public double GetXFromZ(double z)
        {
            // Line is straight
            if (_direction.X == 0)
            {
                return Location.X;
            }

            return Location.X + ((_direction.X / _direction.Z) * (z - Location.Z));
        }

        /// <summary>
        /// Gets the y component of the point along the line with the x component of <paramref name="x"/>.
        /// </summary>
        public double GetYFromX(double x)
        {
            // Line is straight
            if (_direction.Y == 0)
            {
                return Location.Y;
            }

            return Location.Y + ((_direction.Y / _direction.X) * (x - Location.X));
        }
        /// <summary>
        /// Gets the y component of the point along the line with the z component of <paramref name="z"/>.
        /// </summary>
        public double GetYFromZ(double z)
        {
            // Line is straight
            if (_direction.Y == 0)
            {
                return Location.Y;
            }

            return Location.Y + ((_direction.Y / _direction.Z) * (z - Location.Z));
        }

        /// <summary>
        /// Gets the z component of the point along the line with the x component of <paramref name="x"/>.
        /// </summary>
        public double GetZFromX(double x)
        {
            // Line is straight
            if (_direction.Z == 0)
            {
                return Location.Z;
            }

            return Location.Z + ((_direction.Z / _direction.X) * (x - Location.X));
        }
        /// <summary>
        /// Gets the z component of the point along the line with the y component of <paramref name="y"/>.
        /// </summary>
        public double GetZFromY(double y)
        {
            // Line is straight
            if (_direction.Z == 0)
            {
                return Location.Z;
            }

            return Location.Z + ((_direction.Z / _direction.Y) * (y - Location.Y));
        }

#nullable enable
        public override string ToString() => $"Location:{Location}, Direction:{_direction}";
        public string ToString(string? format) => $"Location:{Location.ToString(format)}, Direction:{_direction.ToString(format)}";
#nullable disable

        public override bool Equals(object obj)
        {
            return obj is Line3 line &&
                _direction == line.Direction &&
                Location == line.Location;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(_direction, Location);
        }

        public static bool operator ==(Line3 l, Line3 r) => l.Equals(r);
        public static bool operator !=(Line3 l, Line3 r) => !l.Equals(r);
    }
}
