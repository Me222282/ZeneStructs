using System;

namespace Zene.Structs
{
    /// <summary>
    /// An object that represents a degree angle value.
    /// </summary>
    public struct Degrees
    {
        private const double _overPI = 1 / Math.PI;

        /// <summary>
        /// Creates a degrees value from a <see cref="double"/>.
        /// </summary>
        /// <param name="degrees">The degrees angle.</param>
        public Degrees(double degrees)
        {
            _degrees = degrees;
        }
        /// <summary>
        /// Creates a degrees value from a <see cref="float"/>.
        /// </summary>
        /// <param name="degrees">The degrees angle.</param>
        public Degrees(float degrees)
        {
            _degrees = degrees;
        }

        private readonly double _degrees;

#nullable enable
        public override string ToString()
        {
            return _degrees.ToString();
        }
        public string ToString(string? format)
        {
            return _degrees.ToString(format);
        }
#nullable disable

        /// <summary>
        /// Creates a degrees value from a radian angle value.
        /// </summary>
        /// <param name="radian">The radians angle.</param>
        /// <returns></returns>
        public static Degrees Radian(double radian)
        {
            return new Degrees(radian * 180 * _overPI);
        }

        /// <summary>
        /// Creates a degrees value from a percentage angle value.
        /// </summary>
        /// <param name="percent">The percentage around the angle.</param>
        /// <returns></returns>
        public static Degrees Percent(double percent)
        {
            return new Degrees(percent * 360);
        }

        public override bool Equals(object obj)
        {
            return (obj is double d && _degrees == d)
                ||
                (obj is float f && _degrees == f)
                ||
                (obj is Degrees deg && _degrees == deg._degrees)
                ||
                (obj is Radian r && _degrees == (r * 180 * _overPI));
        }
        public override int GetHashCode() => HashCode.Combine(_degrees);

        public static bool operator ==(Degrees l, Degrees r) => l.Equals(r);
        public static bool operator !=(Degrees l, Degrees r) => !l.Equals(r);

        public static bool operator ==(Degrees l, Radian r) => l.Equals(r);
        public static bool operator !=(Degrees l, Radian r) => !l.Equals(r);

        public static implicit operator double(Degrees d) => d._degrees;
        public static implicit operator Degrees(double d) => new Degrees(d);

        public static explicit operator float(Degrees r) => (float)r._degrees;
        public static implicit operator Degrees(float f) => new Degrees(f);

        public static implicit operator Degrees(Radian r) => new Degrees(r * 180 * _overPI);

        public static Degrees operator -(Degrees d) => new Degrees(-d._degrees);
    }
}
