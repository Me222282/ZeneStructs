using System;

namespace Zene.Structs
{
    /// <summary>
    /// Defines an infinite line as a point and direction in 2 dimensional space.
    /// </summary>
    public struct Line2
    {
        /// <summary>
        /// Create a line from a position and direction.
        /// </summary>
        /// <param name="dir">The direction of the line.</param>
        /// <param name="loc">The reference location for the line.</param>
        public Line2(Vector2 dir, Vector2 loc)
        {
            _direction = dir;
            Location = loc;
        }
        /// <summary>
        /// Create a line from a position and direction.
        /// </summary>
        /// <param name="dirX">The x value for the direction of the line.</param>
        /// <param name="dirY">The y value for the direction of the line.</param>
        /// <param name="locX">The x value for the reference location for the line.</param>
        /// <param name="locY">The y value for the reference location for the line.</param>
        public Line2(double dirX, double dirY, double locX, double locY)
        {
            _direction = new Vector2(dirX, dirY);
            Location = new Vector2(locX, locY);
        }
        /// <summary>
        /// Creates a line based off a segment.
        /// </summary>
        /// <param name="seg">The segment to reference from.</param>
        public Line2(Segment2 seg)
        {
            Location = seg.A;
            _direction = seg.Change;
        }
        /// <summary>
        /// Creates a line based off a segment defined as a tuple.
        /// </summary>
        /// <param name="seg">The segment tuple to reference from.</param>
        public Line2(Tuple<Vector2, Vector2> seg)
        {
            Location = seg.Item1;
            _direction = (seg.Item2 - seg.Item1);
        }

        private Vector2 _direction;
        /// <summary>
        /// The direction of the line.
        /// </summary>
        public Vector2 Direction
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
        public Vector2 Location { get; set; }

        /// <summary>
        /// The normal to the direction of the line.
        /// </summary>
        public Vector2 Normal => _direction.Rotated90();

        /// <summary>
        /// Normalise the direction component of this line.
        /// </summary>
        public void Normalise() => _direction.Normalise();

        /// <summary>
        /// Returns the point at which two lines intersect. If they are parallel, returns <see cref="Vector2.PositiveInfinity"/>.
        /// </summary>
        /// <param name="line">The line to intersect.</param>
        /// <returns></returns>
        public Vector2 Intersects(Line2 line)
        {
            Vector2 b = Direction;
            Vector2 d = line.Direction;

            double pDot = b.PerpDot(d);

            // If b dot d == 0, it means the lines are parallel and have an intersection of infinity
            if (pDot == 0) { return Vector2.PositiveInfinity; }

            Vector2 c = line.Location - Location;
            double t = c.PerpDot(d) / pDot;

            return Location + (t * b);
        }

        /// <summary>
        /// Returns the reflection of the point <paramref name="x"/> about this line.
        /// </summary>
        /// <param name="x">The point to reflect.</param>
        /// <returns></returns>
        public Vector2 Reflect(Vector2 x)
        {
            Vector2 dir = Direction;
            
            double t = (x - Location).Dot(dir) / dir.SquaredLength;
            return (2d * (Location + t * dir)) - x;
        }
        /// <summary>
        /// Returns the projection of the point <paramref name="x"/> onto this line.
        /// </summary>
        /// <param name="x">The point to project.</param>
        /// <returns></returns>
        public Vector2 Project(Vector2 x)
        {
            Vector2 dir = Direction;
            double t = (x - Location).Dot(dir) / dir.SquaredLength;
            return Location + t * dir;
        }
        /// <summary>
        /// Returns the reflection of the line <paramref name="l2"/> about this line.
        /// </summary>
        /// <remarks>
        /// The direction of the reflected line points away from the point of intersection.
        /// </remarks>
        /// <param name="l2">The line to reflect.</param>
        /// <returns></returns>
        public Line2 Reflect(Line2 l2)
        {
            Vector2 np = Reflect(l2.Location);
            Vector2 i = Intersects(l2);
            return new Line2(np - i, i);
        }
        
        /// <summary>
        /// Gets the x component of the point along the line with the y component of <paramref name="y"/>.
        /// </summary>
        public double GetX(double y)
        {
            // Line is straight
            if (_direction.X == 0)
            {
                return Location.X;
            }

            return Location.X + ((_direction.X / _direction.Y) * (y - Location.Y));
        }
        /// <summary>
        /// Gets the y component of the point along the line with the x component of <paramref name="x"/>.
        /// </summary>
        public double GetY(double x)
        {
            // Line is straight
            if (_direction.Y == 0)
            {
                return Location.Y;
            }

            return Location.Y + ((_direction.Y / _direction.X) * (x - Location.X));
        }

        public Line2 GetPerp() => new Line2((-_direction.Y, _direction.X), Location);

        /// <summary>
        /// Returns the shortest distance from this line to <paramref name="point"/>.
        /// </summary>
        /// <param name="point">The point to compare.</param>
        /// <returns></returns>
        public double DistanceFromPoint(Vector3 point)
        {
            Vector2 n = Direction.Rotated90();

            double sl = n.SquaredLength;
            double v = (point - Location).Dot(n);

            if (sl == 1d)
            {
                return v;
            }

            return v / Math.Sqrt(sl);
        }
        /// <summary>
        /// Returns the squared shortest distance from this line to <paramref name="point"/>.
        /// </summary>
        /// <param name="point">The point to compare.</param>
        /// <returns></returns>
        public double SquaredDistanceFromPoint(Vector3 point)
        {
            Vector2 n = Direction.Rotated90();

            double sl = n.SquaredLength;
            double v = (point - Location).Dot(n);
            v *= v;

            if (sl == 1d)
            {
                return v;
            }

            return v / sl;
        }

        /// <summary>
        /// Determines whether <paramref name="point"/> exists within this line.
        /// </summary>
        /// <param name="point">The point to query.</param>
        /// <returns></returns>
        public bool Contains(Vector2 point) => (point - Location).PerpDot(Direction) == 0d;
        
        /// <summary>
        /// Determines whether <paramref name="l"/> represents an equivalent line to this.
        /// </summary>
        /// <param name="l">The line to query.</param>
        /// <returns></returns>
        public bool GeometricallyEquals(Line2 l)
        {
            Vector2 c = Direction.PerpDot(l.Direction);
            
            if (c != 0d) { return false; }
            
            Vector2 diff = Location - l.Location;
            
            return diff == 0d || diff.PerpDot(Direction) == 0d;
        }
        
#nullable enable
        public override string ToString()
        {
            double m = _direction.Y / _direction.X;

            if (double.IsInfinity(m))
            {
                return $"x = {Location.X}";
            }
            if (m == 0)
            {
                return $"y = {Location.Y}";
            }
            double c = Location.Y - (m * Location.X);

            if (c < 0)
            {
                return $"y = {m}x - {-c}";
            }
            if (c == 0)
            {
                return $"y = {m}x";
            }
            if (m < 0)
            {
                return $"y = {c} - {-m}x";
            }

            return $"y = {m}x + {c}";
        }
        public string ToString(string? format)
        {
            double m = _direction.Y / _direction.X;

            if (double.IsInfinity(m))
            {
                return $"x = {Location.X.ToString(format)}";
            }
            if (m == 0)
            {
                return $"y = {Location.Y.ToString(format)}";
            }
            double c = Location.Y - (m * Location.X);

            if (c < 0)
            {
                return $"y = {m.ToString(format)}x - {(-c).ToString(format)}";
            }
            if (c == 0)
            {
                return $"y = {(-m).ToString(format)}x";
            }
            if (m < 0)
            {
                return $"y = {c.ToString(format)} - {m.ToString(format)}x";
            }

            return $"y = {m.ToString(format)}x + {c.ToString(format)}";
        }
#nullable disable

        public override bool Equals(object obj)
        {
            return obj is Line2 line &&
                _direction == line.Direction &&
                Location == line.Location;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(_direction, Location);
        }

        public static bool operator ==(Line2 l, Line2 r) => l.Equals(r);
        public static bool operator !=(Line2 l, Line2 r) => !l.Equals(r);
    }
}
