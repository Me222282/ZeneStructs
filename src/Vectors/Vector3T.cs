using System;

namespace Zene.Structs
{
    /// <summary>
    /// An object that stores a 3 dimensional <typeparamref name="T"/> vector.
    /// </summary>
    public struct Vector3<T> where T : unmanaged
    {
        /// <summary>
        /// Creates a 3 dimensional vector from a single <typeparamref name="T"/>.
        /// </summary>
        /// <param name="value">The value to set to <see cref="X"/>, <see cref="Y"/> and <see cref="Z"/>.</param>
        public Vector3(T value)
        {
            X = value;
            Y = value;
            Z = value;
        }
        /// <summary>
        /// Creates a 3 dimensional vector from three <typeparamref name="T"/>.
        /// </summary>
        /// <param name="x">The value to set to <see cref="X"/>.</param>
        /// <param name="y">The value to set to <see cref="Y"/>.</param>
        /// <param name="z">The value to set to <see cref="Z"/>.</param>
        public Vector3(T x, T y, T z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        /// <summary>
        /// Creates a 3 dimensional vector from a 2 dimensional vector and a <typeparamref name="T"/> Z.
        /// </summary>
        /// <param name="xy">The vector to reference for <see cref="X"/> and <see cref="Y"/>.</param>
        /// <param name="z">The value to set to <see cref="Z"/>.</param>
        public Vector3(Vector2<T> xy, T z)
        {
            X = xy.X;
            Y = xy.Y;
            Z = z;
        }
        /// <summary>
        /// Creates a 3 dimensional vector from a 2 dimensional vector and a <typeparamref name="T"/> X.
        /// </summary>
        /// <param name="yz">The vector to reference for <see cref="Y"/> and <see cref="Z"/>.</param>
        /// <param name="x">The value to set to <see cref="X"/>.</param>
        public Vector3(T x, Vector2<T> yz)
        {
            Y = yz.X;
            Z = yz.Y;
            X = x;
        }

        /// <summary>
        /// Creates a 3 dimensional vector casted from a <see cref="double"/> based vector.
        /// </summary>
        /// <param name="xyz">The vector to reference for <see cref="X"/>, <see cref="Y"/> and <see cref="Z"/>.</param>
        public Vector3(Vector3 xyz)
        {
            X = (T)(object)xyz.X;
            Y = (T)(object)xyz.Y;
            Z = (T)(object)xyz.Z;
        }
        /// <summary>
        /// Creates a 3 dimensional vector casted from an <see cref="int"/> based vector.
        /// </summary>
        /// <param name="xyz">The vector to reference for <see cref="X"/>, <see cref="Y"/> and <see cref="Z"/>.</param>
        public Vector3(Vector3I xyz)
        {
            X = (T)(object)xyz.X;
            Y = (T)(object)xyz.Y;
            Z = (T)(object)xyz.Z;
        }

        /// <summary>
        /// The first value of the vector.
        /// </summary>
        public T X { get; set; }
        /// <summary>
        /// The second value of the vector.
        /// </summary>
        public T Y { get; set; }
        /// <summary>
        /// The third value of the vector.
        /// </summary>
        public T Z { get; set; }

        public override string ToString()
        {
            return $"X:{X}, Y:{Y}, Z:{Z}";
        }

        public override bool Equals(object obj)
        {
            return
                obj is Vector3<T> p &&
                X.Equals(p.X) && Y.Equals(p.Y) && Z.Equals(p.Z);
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public static bool operator ==(Vector3<T> a, Vector3<T> b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(Vector3<T> a, Vector3<T> b)
        {
            return !a.Equals(b);
        }

        public static explicit operator Vector3<T>(Vector3 obj)
        {
            return new Vector3<T>(obj);
        }
        public static explicit operator Vector3<T>(Vector3I obj)
        {
            return new Vector3<T>(obj);
        }
        public static explicit operator Vector3<T>(Vector2<T> obj)
        {
            return new Vector3<T>(obj, default);
        }
        public static explicit operator Vector3<T>(Vector4<T> obj)
        {
            return new Vector3<T>(obj.X, obj.Y, obj.Z);
        }

        public static explicit operator Vector3(Vector3<T> obj)
        {
            return new Vector3((double)(object)obj.X, (double)(object)obj.Y, (double)(object)obj.Z);
        }
        public static explicit operator Vector3I(Vector3<T> obj)
        {
            return new Vector3I((int)(object)obj.X, (int)(object)obj.Y, (int)(object)obj.Z);
        }

        public static implicit operator Vector3<T>((T, T, T) v) => new Vector3<T>(v.Item1, v.Item2, v.Item3);
        public static implicit operator Vector3<T>((Vector2<T>, T) v) => new Vector3<T>(v.Item1, v.Item2);
        public static implicit operator Vector3<T>((T, Vector2<T>) v) => new Vector3<T>(v.Item1, v.Item2);
    }
}
