using System;
using System.Numerics;

namespace Zene.Structs
{
    /// <summary>
    /// An object that stores a 4 dimensional <typeparamref name="T"/> vector.
    /// </summary>
    public struct Vector4<T> where T : unmanaged//, INumber<T>
    {
        /// <summary>
        /// Creates a 4 dimensional vector from a single <typeparamref name="T"/>.
        /// </summary>
        /// <param name="value">The value to set to <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/> and <see cref="W"/>.</param>
        public Vector4(T value)
        {
            X = value;
            Y = value;
            Z = value;
            W = value;
        }
        /// <summary>
        /// Creates a 4 dimensional vector from four <typeparamref name="T"/>.
        /// </summary>
        /// <param name="x">The value to set to <see cref="X"/>.</param>
        /// <param name="y">The value to set to <see cref="Y"/>.</param>
        /// <param name="z">The value to set to <see cref="Z"/>.</param>
        /// <param name="w">The value to set to <see cref="W"/>.</param>
        public Vector4(T x, T y, T z, T w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }
        /// <summary>
        /// Creates a 3 dimensional vector from a 3 dimensional vector and a <typeparamref name="T"/> W.
        /// </summary>
        /// <param name="xyz">The vector to reference for <see cref="X"/>, <see cref="Y"/> and <see cref="Z"/>.</param>
        /// <param name="w">The value to set to <see cref="W"/>.</param>
        public Vector4(Vector3<T> xyz, T w)
        {
            X = xyz.X;
            Y = xyz.Y;
            Z = xyz.Z;
            W = w;
        }
        /// <summary>
        /// Creates a 3 dimensional vector from a 3 dimensional vector and a <typeparamref name="T"/> X.
        /// </summary>
        /// <param name="yzw">The vector to reference for <see cref="Y"/>, <see cref="Z"/> and <see cref="W"/>.</param>
        /// <param name="x">The value to set to <see cref="X"/>.</param>
        public Vector4(T x, Vector3<T> yzw)
        {
            Y = yzw.X;
            Z = yzw.Y;
            W = yzw.Z;
            X = x;
        }
        /// <summary>
        /// Creates a 3 dimensional vector from a 2 dimensional vector and a <typeparamref name="T"/> Z and W.
        /// </summary>
        /// <param name="xy">The vector to reference for <see cref="X"/> and <see cref="Y"/>.</param>
        /// <param name="z">The value to set to <see cref="Z"/>.</param>
        /// <param name="w">The value to set to <see cref="W"/>.</param>
        public Vector4(Vector2<T> xy, T z, T w)
        {
            X = xy.X;
            Y = xy.Y;
            Z = z;
            W = w;
        }
        /// <summary>
        /// Creates a 3 dimensional vector from a 2 dimensional vector and a <typeparamref name="T"/> X and W.
        /// </summary>
        /// <param name="yz">The vector to reference for <see cref="Y"/> and <see cref="Z"/>.</param>
        /// <param name="x">The value to set to <see cref="X"/>.</param>
        /// <param name="w">The value to set to <see cref="W"/>.</param>
        public Vector4(T x, Vector2<T> yz, T w)
        {
            Y = yz.X;
            Z = yz.Y;
            X = x;
            W = w;
        }
        /// <summary>
        /// Creates a 3 dimensional vector from a 2 dimensional vector and a <typeparamref name="T"/> X and Y.
        /// </summary>
        /// <param name="zw">The vector to reference for <see cref="Z"/> and <see cref="W"/>.</param>
        /// <param name="x">The value to set to <see cref="X"/>.</param>
        /// <param name="y">The value to set to <see cref="Y"/>.</param>
        public Vector4(T x, T y, Vector2<T> zw)
        {
            Z = zw.X;
            W = zw.Y;
            X = x;
            Y = y;
        }
        /// <summary>
        /// Creates a 4 dimensional vector from two <see cref="Vector2{T}"/>.
        /// </summary>
        /// <param name="xy">The vector to reference for <see cref="X"/> and <see cref="Y"/>.</param>
        /// <param name="zw">The vector to reference for <see cref="Z"/> and <see cref="W"/>.</param>
        public Vector4(Vector2<T> xy, Vector2<T> zw)
        {
            X = xy.X;
            Y = xy.Y;
            Z = zw.X;
            W = zw.Y;
        }

        /// <summary>
        /// Creates a 3 dimensional vector casted from a <see cref="double"/> based vector.
        /// </summary>
        /// <param name="xyzw">The vector to reference for <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/> and <see cref="W"/>.</param>
        public Vector4(Vector4 xyzw)
        {
            X = (T)(object)xyzw.X;
            Y = (T)(object)xyzw.Y;
            Z = (T)(object)xyzw.Z;
            W = (T)(object)xyzw.W;
        }
        /// <summary>
        /// Creates a 3 dimensional vector casted from an <see cref="int"/> based vector.
        /// </summary>
        /// <param name="xyzw">The vector to reference for <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/> and <see cref="W"/>.</param>
        public Vector4(Vector4I xyzw)
        {
            X = (T)(object)xyzw.X;
            Y = (T)(object)xyzw.Y;
            Z = (T)(object)xyzw.Z;
            W = (T)(object)xyzw.W;
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
        /// <summary>
        /// The fourth value of the vector.
        /// </summary>
        public T W { get; set; }
        
        public void Deconstruct(out T x, out T y, out T z, out T w)
        {
            x = X;
            y = Y;
            z = Z;
            w = W;
        }
        public void Deconstruct(out T x, out Vector3<T> yzw)
        {
            x = X;
            yzw = new Vector3<T>(Y, Z, W);
        }
        public void Deconstruct(out Vector3<T> xyz, out T w)
        {
            xyz = new Vector3<T>(X, Y, Z);
            w = W;
        }
        public void Deconstruct(out Vector2<T> xy, out T z, out T w)
        {
            xy = new Vector2<T>(X, Y);
            z = Z;
            w = W;
        }
        public void Deconstruct(out T x, out Vector2<T> yz, out T w)
        {
            x = X;
            yz = new Vector2<T>(Y, Z);
            w = W;
        }
        public void Deconstruct(out T x, out T y, out Vector2<T> zw)
        {
            x = X;
            y = Y;
            zw = new Vector2<T>(Z, W);
        }
        public void Deconstruct(out Vector2<T> xy, out Vector2<T> zw)
        {
            xy = new Vector2<T>(X, Y);
            zw = new Vector2<T>(Z, W);
        }
        
        public override string ToString()
        {
            return $"X:{X}, Y:{Y}, Z:{Z}, W:{W}";
        }

        public override bool Equals(object obj)
        {
            return obj is Vector4<T> p &&
                X.Equals(p.X) && Y.Equals(p.Y) && Z.Equals(p.Z) && W.Equals(p.W);
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public static bool operator ==(Vector4<T> a, Vector4<T> b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(Vector4<T> a, Vector4<T> b)
        {
            return !a.Equals(b);
        }

        public static explicit operator Vector4<T>(Vector4 obj)
        {
            return new Vector4<T>(obj);
        }
        public static explicit operator Vector4<T>(Vector4I obj)
        {
            return new Vector4<T>(obj);
        }
        public static explicit operator Vector4<T>(Vector2<T> obj)
        {
            return new Vector4<T>(obj, default, default);
        }
        public static explicit operator Vector4<T>(Vector3<T> obj)
        {
            return new Vector4<T>(obj, default);
        }

        public static explicit operator Vector4(Vector4<T> obj)
        {
            return new Vector4((double)(object)obj.X, (double)(object)obj.Y, (double)(object)obj.Z, (double)(object)obj.W);
        }
        public static explicit operator Vector4I(Vector4<T> obj)
        {
            return new Vector4I((int)(object)obj.X, (int)(object)obj.Y, (int)(object)obj.Z, (int)(object)obj.W);
        }

        public static implicit operator Vector4<T>((T, T, T, T) v) => new Vector4<T>(v.Item1, v.Item2, v.Item3, v.Item4);
        public static implicit operator Vector4<T>((Vector2<T>, T, T) v) => new Vector4<T>(v.Item1, v.Item2, v.Item3);
        public static implicit operator Vector4<T>((T, Vector2<T>, T) v) => new Vector4<T>(v.Item1, v.Item2, v.Item3);
        public static implicit operator Vector4<T>((T, T, Vector2<T>) v) => new Vector4<T>(v.Item1, v.Item2, v.Item3);
        public static implicit operator Vector4<T>((Vector2<T>, Vector2<T>) v) => new Vector4<T>(v.Item1, v.Item2);
        public static implicit operator Vector4<T>((Vector3<T>, T) v) => new Vector4<T>(v.Item1, v.Item2);
        public static implicit operator Vector4<T>((T, Vector3<T>) v) => new Vector4<T>(v.Item1, v.Item2);
    }
}
