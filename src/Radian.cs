using System;

namespace Zene.Structs
{
    /// <summary>
    /// An object that represents a radian angle value.
    /// </summary>
    public struct Radian
    {
        private const floatv _over180 = (floatv)1 / 180;

        /// <summary>
        /// Creates a radians value from a <see cref="double"/>.
        /// </summary>
        /// <param name="radians">The radians angle.</param>
        public Radian(double radians)
        {
            _radian = (floatv)radians;
        }
        /// <summary>
        /// Creates a radians value from a <see cref="float"/>.
        /// </summary>
        /// <param name="radians">The radians angle.</param>
        public Radian(float radians)
        {
            _radian = (floatv)radians;
        }

        private readonly floatv _radian;

#nullable enable
        public override string ToString() => _radian.ToString();
        public string ToString(string? format) => _radian.ToString(format);
#nullable disable

        /// <summary>
        /// Creates a radians value from a degree angle value.
        /// </summary>
        /// <param name="degrees">The degrees angle.</param>
        /// <returns></returns>
        public static Radian Degrees(floatv degrees)
        {
            return new Radian(degrees * _over180 * Maths.PI);
        }

        /// <summary>
        /// Creates a radians value from a percentage angle value.
        /// </summary>
        /// <param name="percent">The percentage around the angle.</param>
        /// <returns></returns>
        public static Radian Percent(floatv percent)
        {
            return new Radian(percent * 2 * Maths.PI);
        }

        public Radian Lerp(Radian b, floatv blend) => _radian.Lerp(b._radian, blend);

        public override bool Equals(object obj)
        {
            return (obj is double d && _radian == d)
                ||
                (obj is float f && _radian == f)
                ||
                (obj is Radian r && _radian == r._radian)
                ||
                (obj is Degrees deg && _radian == (deg * _over180 * Math.PI));
        }
        public override int GetHashCode() => HashCode.Combine(_radian);

        public static bool operator ==(Radian l, Radian r) => l._radian == r._radian;
        public static bool operator !=(Radian l, Radian r) => l._radian != r._radian;

        public static bool operator ==(Radian l, Degrees r) => l._radian == (r * _over180 * Math.PI);
        public static bool operator !=(Radian l, Degrees r) => l._radian != (r * _over180 * Math.PI);

        public static implicit operator double(Radian r) => r._radian;
        public static implicit operator Radian(double d) => new Radian(d);

        public static implicit operator float(Radian r) => (float)r._radian;
        public static implicit operator Radian(float f) => new Radian(f);

        public static implicit operator Radian(Degrees deg) => new Radian(deg * _over180 * Math.PI);

        public static Radian operator -(Radian r) => new Radian(-r._radian);

        public static Radian Zero { get; } = new Radian();
        public static Radian Quarter { get; } = new Radian(Maths.PI * 0.5);
        public static Radian Half { get; } = new Radian(Maths.PI);
        public static Radian Full { get; } = new Radian(Maths.PI * 2);
    }
}
