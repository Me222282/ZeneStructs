using System;
using System.Numerics;

namespace Zene.Structs
{
    /// <summary>
    /// An object that stores a 2 dimensional <typeparamref name="T"/> vector.
    /// </summary>
    public struct Vector2<T> where T : unmanaged//, INumber<T>
    {
        /// <summary>
        /// Creates a 2 dimensional vector from a single <typeparamref name="T"/>.
        /// </summary>
        /// <param name="value">The value to set to both <see cref="X"/> and <see cref="Y"/>.</param>
        public Vector2(T value)
        {
            X = value;
            Y = value;
        }
        /// <summary>
        /// Creates a 2 dimensional vector from two <typeparamref name="T"/>.
        /// </summary>
        /// <param name="x">The value to set to <see cref="X"/>.</param>
        /// <param name="y">The value to set to <see cref="Y"/>.</param>
        public Vector2(T x, T y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Creates a 2 dimensional vector casted from a <see cref="double"/> based vector.
        /// </summary>
        /// <param name="xy">The vector to reference for <see cref="X"/> and <see cref="Y"/>.</param>
        public Vector2(Vector2 xy)
        {
            X = (T)(object)xy.X;
            Y = (T)(object)xy.Y;
        }
        /// <summary>
        /// Creates a 2 dimensional vector casted from an <see cref="int"/> based vector.
        /// </summary>
        /// <param name="xy">The vector to reference for <see cref="X"/> and <see cref="Y"/>.</param>
        public Vector2(Vector2I xy)
        {
            X = (T)(object)xy.X;
            Y = (T)(object)xy.Y;
        }

        /// <summary>
        /// The first value of the vector.
        /// </summary>
        public T X { get; set; }
        /// <summary>
        /// The second value of the vector.
        /// </summary>
        public T Y { get; set; }

        ///// <summary>
        ///// The length of the vector (distance from origin).
        ///// </summary>
        //public floatv Length
        //{
        //    get
        //    {
        //        return Maths.Sqrt((X * X) + (Y * Y));
        //    }
        //}
        ///// <summary>
        ///// The squared length of the vector (distance from origin squared).
        ///// </summary>
        //public floatv SquaredLength
        //{
        //    get
        //    {
        //        return (X * X) + (Y * Y);
        //    }
        //}

        public void Deconstruct(out T x, out T y)
        {
            x = X;
            y = Y;
        }
        
        public override string ToString()
        {
            return $"X:{X}, Y:{Y}";
        }

        public override bool Equals(object obj)
        {
            return
                obj is Vector2<T> p &&
                X.Equals(p.X) && Y.Equals(p.Y);
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public static bool operator ==(Vector2<T> a, Vector2<T> b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(Vector2<T> a, Vector2<T> b)
        {
            return !a.Equals(b);
        }

        public static explicit operator Vector2<T>(Vector2 obj)
        {
            return new Vector2<T>(obj);
        }
        public static explicit operator Vector2<T>(Vector2I obj)
        {
            return new Vector2<T>(obj);
        }
        public static explicit operator Vector2<T>(Vector3<T> obj)
        {
            return new Vector2<T>(obj.X, obj.Y);
        }
        public static explicit operator Vector2<T>(Vector4<T> obj)
        {
            return new Vector2<T>(obj.X, obj.Y);
        }

        public static explicit operator Vector2(Vector2<T> obj)
        {
            return new Vector2((double)(object)obj.X, (double)(object)obj.Y);
        }
        public static explicit operator Vector2I(Vector2<T> obj)
        {
            return new Vector2I((int)(object)obj.X, (int)(object)obj.Y);
        }

        public static implicit operator Vector2<T>((T, T) v)
        {
            return new Vector2<T>(v.Item1, v.Item2);
        }
    }
}
