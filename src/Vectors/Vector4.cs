using System;

namespace Zene.Structs
{
    /// <summary>
    /// An object that stores a 4 dimensional <see cref="double"/> vector.
    /// </summary>
    public struct Vector4
    {
        /// <summary>
        /// Creates a 4 dimensional vector from a single <see cref="int"/>.
        /// </summary>
        /// <param name="value">The value to set to <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/> and <see cref="W"/>.</param>
        public Vector4(int value)
        {
            X = value;
            Y = X;
            Z = X;
            W = X;
        }
        /// <summary>
        /// Creates a 4 dimensional vector from a single <see cref="double"/>.
        /// </summary>
        /// <param name="value">The value to set to <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/> and <see cref="W"/>.</param>
        public Vector4(double value)
        {
            X = value;
            Y = value;
            Z = value;
            W = value;
        }
        /// <summary>
        /// Creates a 4 dimensional vector from four <see cref="int"/>.
        /// </summary>
        /// <param name="x">The value to set to <see cref="X"/>.</param>
        /// <param name="y">The value to set to <see cref="Y"/>.</param>
        /// <param name="z">The value to set to <see cref="Z"/>.</param>
        /// <param name="w">The value to set to <see cref="W"/>.</param>
        public Vector4(int x, int y, int z, int w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }
        /// <summary>
        /// Creates a 4 dimensional vector from four <see cref="double"/>.
        /// </summary>
        /// <param name="x">The value to set to <see cref="X"/>.</param>
        /// <param name="y">The value to set to <see cref="Y"/>.</param>
        /// <param name="z">The value to set to <see cref="Z"/>.</param>
        /// <param name="w">The value to set to <see cref="W"/>.</param>
        public Vector4(double x, double y, double z, double w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }
        /// <summary>
        /// Creates a 4 dimensional vector from two <see cref="Vector2"/>.
        /// </summary>
        /// <param name="xy">The vector to reference for <see cref="X"/> and <see cref="Y"/>.</param>
        /// <param name="zw">The vector to reference for <see cref="Z"/> and <see cref="W"/>.</param>
        public Vector4(Vector2 xy, Vector2 zw)
        {
            X = xy.X;
            Y = xy.Y;
            Z = zw.X;
            W = zw.Y;
        }
        /// <summary>
        /// Creates a 4 dimensional vector from two <see cref="Vector2I"/>.
        /// </summary>
        /// <param name="xy">The vector to reference for <see cref="X"/> and <see cref="Y"/>.</param>
        /// <param name="zw">The vector to reference for <see cref="Z"/> and <see cref="W"/>.</param>
        public Vector4(Vector2I xy, Vector2I zw)
        {
            X = xy.X;
            Y = xy.Y;
            Z = zw.X;
            W = zw.Y;
        }
        /// <summary>
        /// Creates a 4 dimensional vector from a 2 dimensional vector and a <see cref="double"/> Z and W.
        /// </summary>
        /// <param name="xy">The vector to reference for <see cref="X"/> and <see cref="Y"/>.</param>
        /// <param name="z">The value to set to <see cref="Z"/>.</param>
        /// <param name="w">The value to set to <see cref="W"/>.</param>
        public Vector4(Vector2 xy, double z, double w)
        {
            X = xy.X;
            Y = xy.Y;
            Z = z;
            W = w;
        }
        /// <summary>
        /// Creates a 4 dimensional vector from a 2 dimensional vector and a <see cref="double"/> X and W.
        /// </summary>
        /// <param name="yz">The vector to reference for <see cref="Y"/> and <see cref="Z"/>.</param>
        /// <param name="x">The value to set to <see cref="X"/>.</param>
        /// <param name="w">The value to set to <see cref="W"/>.</param>
        public Vector4(double x, Vector2 yz, double w)
        {
            Y = yz.X;
            Z = yz.Y;
            X = x;
            W = w;
        }
        /// <summary>
        /// Creates a 4 dimensional vector from a 2 dimensional vector and a <see cref="double"/> X and Y.
        /// </summary>
        /// <param name="zw">The vector to reference for <see cref="Z"/> and <see cref="W"/>.</param>
        /// <param name="x">The value to set to <see cref="X"/>.</param>
        /// <param name="y">The value to set to <see cref="Y"/>.</param>
        public Vector4(double x, double y, Vector2 zw)
        {
            Z = zw.X;
            W = zw.Y;
            X = x;
            Y = y;
        }
        /// <summary>
        /// Creates a 4 dimensional vector from an <see cref="int"/> based 2 dimensional vector and a <see cref="double"/> Z and W.
        /// </summary>
        /// <param name="xy">The vector to reference for <see cref="X"/> and <see cref="Y"/>.</param>
        /// <param name="z">The value to set to <see cref="Z"/>.</param>
        /// <param name="w">The value to set to <see cref="W"/>.</param>
        public Vector4(Vector2I xy, double z, double w)
        {
            X = xy.X;
            Y = xy.Y;
            Z = z;
            W = w;
        }
        /// <summary>
        /// Creates a 4 dimensional vector from an <see cref="int"/> based 2 dimensional vector and a <see cref="double"/> X and W.
        /// </summary>
        /// <param name="yz">The vector to reference for <see cref="Y"/> and <see cref="Z"/>.</param>
        /// <param name="x">The value to set to <see cref="X"/>.</param>
        /// <param name="w">The value to set to <see cref="W"/>.</param>
        public Vector4(double x, Vector2I yz, double w)
        {
            Y = yz.X;
            Z = yz.Y;
            X = x;
            W = w;
        }
        /// <summary>
        /// Creates a 4 dimensional vector from an <see cref="int"/> based 2 dimensional vector and a <see cref="double"/> X and Y.
        /// </summary>
        /// <param name="zw">The vector to reference for <see cref="X"/> and <see cref="W"/>.</param>
        /// <param name="x">The value to set to <see cref="X"/>.</param>
        /// <param name="y">The value to set to <see cref="Y"/>.</param>
        public Vector4(double x, double y, Vector2I zw)
        {
            Z = zw.X;
            W = zw.Y;
            X = x;
            Y = y;
        }
        /// <summary>
        /// Creates a 4 dimensional vector from a 3 dimensional vector and a <see cref="double"/> W.
        /// </summary>
        /// <param name="xyz">The vector to reference for <see cref="X"/>, <see cref="Y"/> and <see cref="Z"/>.</param>
        /// <param name="w">The value to set to <see cref="W"/>.</param>
        public Vector4(Vector3 xyz, double w)
        {
            X = xyz.X;
            Y = xyz.Y;
            Z = xyz.Z;
            W = w;
        }
        /// <summary>
        /// Creates a 4 dimensional vector from a 3 dimensional vector and a <see cref="double"/> X.
        /// </summary>
        /// <param name="yzw">The vector to reference for <see cref="Y"/>, <see cref="Z"/> and <see cref="W"/>.</param>
        /// <param name="x">The value to set to <see cref="X"/>.</param>
        public Vector4(double x, Vector3 yzw)
        {
            Y = yzw.X;
            Z = yzw.Y;
            W = yzw.Z;
            X = x;
        }
        /// <summary>
        /// Creates a 4 dimensional vector from an <see cref="int"/> based 3 dimensional vector and a <see cref="double"/> W.
        /// </summary>
        /// <param name="xyz">The vector to reference for <see cref="X"/>, <see cref="Y"/> and <see cref="Z"/>.</param>
        /// <param name="w">The value to set to <see cref="W"/>.</param>
        public Vector4(Vector3I xyz, double w)
        {
            X = xyz.X;
            Y = xyz.Y;
            Z = xyz.Z;
            W = w;
        }
        /// <summary>
        /// Creates a 4 dimensional vector from an <see cref="int"/> based 3 dimensional vector and a <see cref="double"/> X.
        /// </summary>
        /// <param name="yzw">The vector to reference for <see cref="Y"/>, <see cref="Z"/> and <see cref="W"/>.</param>
        /// <param name="x">The value to set to <see cref="X"/>.</param>
        public Vector4(double x, Vector3I yzw)
        {
            Y = yzw.X;
            Z = yzw.Y;
            W = yzw.Z;
            X = x;
        }
        /// <summary>
        /// Creates a 4 dimensional vector from an <see cref="int"/> based vector.
        /// </summary>
        /// <param name="xyzw">The vector to reference for <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/> and <see cref="W"/>.</param>
        public Vector4(Vector4I xyzw)
        {
            X = xyzw.X;
            Y = xyzw.Y;
            Z = xyzw.Z;
            W = xyzw.W;
        }

        /// <summary>
        /// The first value of the vector.
        /// </summary>
        public double X { get; set; }
        /// <summary>
        /// The second value of the vector.
        /// </summary>
        public double Y { get; set; }
        /// <summary>
        /// The third value of the vector.
        /// </summary>
        public double Z { get; set; }
        /// <summary>
        /// The fourth value of the vector.
        /// </summary>
        public double W { get; set; }

        /// <summary>
        /// The length of the vector (distance from origin).
        /// </summary>
        public double Length
        {
            get
            {
                return Math.Sqrt((X * X) + (Y * Y) + (Z * Z) + (W * W));
            }
        }
        /// <summary>
        /// The squared length of the vector (distance from origin squared).
        /// </summary>
        public double SquaredLength
        {
            get
            {
                return (X * X) + (Y * Y) + (Z * Z) + (W * W);
            }
        }

        /// <summary>
        /// The distance from this vector to <paramref name="b"/>.
        /// </summary>
        /// <param name="b">The vector to reference.</param>
        /// <returns></returns>
        public double Distance(Vector4 b)
        {
            return Math.Sqrt(SquaredDistance(b));
        }
        /// <summary>
        /// The squared distance from this vector to <paramref name="b"/>.
        /// </summary>
        /// <param name="b">The vector to reference.</param>
        /// <returns></returns>
        public double SquaredDistance(Vector4 b)
        {
            return ((b.X - X) * (b.X - X)) + ((b.Y - Y) * (b.Y - Y)) + ((b.Z - Z) * (b.Z - Z)) + ((b.W - W) * (b.W - W));
        }

        /// <summary>
        /// The dot product of this vector to <paramref name="b"/>.
        /// </summary>
        /// <param name="b">The vector to reference.</param>
        /// <returns></returns>
        public double Dot(Vector4 b)
        {
            return (X * b.X) + (Y * b.Y) + (Z * b.Z) + (W * b.W);
        }

        /// <summary>
        /// A linear interpolation between this vector and <paramref name="b"/>.
        /// </summary>
        /// <param name="b">The vector to interpolate to.</param>
        /// <param name="blend">The percentage interpolation.</param>
        /// <returns></returns>
        public Vector4 Lerp(Vector4 b, double blend)
        {
            return new Vector4(
                (blend * (b.X - X)) + X,
                (blend * (b.Y - Y)) + Y,
                (blend * (b.Z - Z)) + Z,
                (blend * (b.W - W)) + W);
        }

        /// <summary>
        /// Returns a triangular interpolation of this vector, <paramref name="b"/> and <paramref name="c"/>.
        /// </summary>
        /// <remarks>
        /// Increasing <paramref name="u"/> points to <paramref name="b"/>.<br />
        /// Increasing <paramref name="v"/> points to <paramref name="c"/>.
        /// </remarks>
        /// <param name="b">The second vector in the interpolation triangle.</param>
        /// <param name="c">The thrid vector in the interpolation triangle.</param>
        /// <param name="u">The percentage <paramref name="b"/> is included.</param>
        /// <param name="v">The percentage <paramref name="c"/> is included.</param>
        /// <returns></returns>
        public Vector4 BaryCentric(Vector4 b, Vector4 c, double u, double v)
        {
            return (this + ((b - this) * u)) + ((c - this) * v);
        }

        /// <summary>
        /// Returns a normalised version of this vector.
        /// </summary>
        /// <returns></returns>
        public Vector4 Normalised()
        {
            if (Length == 0) { return Zero; }

            double scale = 1.0 / Length;
            return new Vector4(X * scale, Y * scale, Z * scale, W * scale);
        }
        /// <summary>
        /// Normalises this vector.
        /// </summary>
        /// <returns></returns>
        public void Normalise()
        {
            double scale = 1.0 / Length;

            X *= scale;
            Y *= scale;
            Z *= scale;
            W *= scale;
        }

        /// <summary>
        /// Returns this vector multiplied by <paramref name="matrix"/>.
        /// </summary>
        /// <param name="matrix">The <see cref="Matrix2x4"/> to multiply by.</param>
        /// <returns></returns>
        public Vector2 MultiplyMatrix(Matrix2x4 matrix)
        {
            return new Vector2(
                //(matrix[0, 0] * X) + (matrix[0, 1] * Y) + (matrix[0, 2] * Z) + (matrix[0, 3] * W),
                //(matrix[1, 0] * X) + (matrix[1, 1] * Y) + (matrix[1, 2] * Z) + (matrix[1, 3] * W));
                (matrix[0, 0] * X) + (matrix[1, 0] * Y) + (matrix[2, 0] * Z) + (matrix[3, 0] * W),
                (matrix[0, 1] * X) + (matrix[1, 1] * Y) + (matrix[2, 1] * Z) + (matrix[3, 1] * W));
        }
        /// <summary>
        /// Returns this vector multiplied by <paramref name="matrix"/>.
        /// </summary>
        /// <param name="matrix">The <see cref="Matrix3x4"/> to multiply by.</param>
        /// <returns></returns>
        public Vector3 MultiplyMatrix(Matrix3x4 matrix)
        {
            return new Vector3(
                //(matrix[0, 0] * X) + (matrix[0, 1] * Y) + (matrix[0, 2] * Z) + (matrix[0, 3] * W),
                //(matrix[1, 0] * X) + (matrix[1, 1] * Y) + (matrix[1, 2] * Z) + (matrix[1, 3] * W),
                //(matrix[2, 0] * X) + (matrix[2, 1] * Y) + (matrix[2, 2] * Z) + (matrix[2, 3] * W));
                (matrix[0, 0] * X) + (matrix[1, 0] * Y) + (matrix[2, 0] * Z) + (matrix[3, 0] * W),
                (matrix[0, 1] * X) + (matrix[1, 1] * Y) + (matrix[2, 1] * Z) + (matrix[3, 1] * W),
                (matrix[0, 2] * X) + (matrix[1, 2] * Y) + (matrix[2, 2] * Z) + (matrix[3, 2] * W));
        }
        /// <summary>
        /// Returns this vector multiplied by <paramref name="matrix"/>.
        /// </summary>
        /// <param name="matrix">The <see cref="Matrix4"/> to multiply by.</param>
        /// <returns></returns>
        public Vector4 MultiplyMatrix(Matrix4 matrix)
        {
            return new Vector4(
                //(matrix[0, 0] * X) + (matrix[0, 1] * Y) + (matrix[0, 2] * Z) + (matrix[0, 3] * W),
                //(matrix[1, 0] * X) + (matrix[1, 1] * Y) + (matrix[1, 2] * Z) + (matrix[1, 3] * W),
                //(matrix[2, 0] * X) + (matrix[2, 1] * Y) + (matrix[2, 2] * Z) + (matrix[2, 3] * W),
                //(matrix[3, 0] * X) + (matrix[3, 1] * Y) + (matrix[3, 2] * Z) + (matrix[3, 3] * W));
                (matrix[0, 0] * X) + (matrix[1, 0] * Y) + (matrix[2, 0] * Z) + (matrix[3, 0] * W),
                (matrix[0, 1] * X) + (matrix[1, 1] * Y) + (matrix[2, 1] * Z) + (matrix[3, 1] * W),
                (matrix[0, 2] * X) + (matrix[1, 2] * Y) + (matrix[2, 2] * Z) + (matrix[3, 2] * W),
                (matrix[0, 3] * X) + (matrix[1, 3] * Y) + (matrix[2, 3] * Z) + (matrix[3, 3] * W));
        }

        public override string ToString()
        {
            return $"X:{X}, Y:{Y}, Z:{Z}, W:{W}";
        }
        public string ToString(string format)
        {
            return $"X:{X.ToString(format)}, Y:{Y.ToString(format)}, Z:{Z.ToString(format)}, W:{W.ToString(format)}";
        }

        public override bool Equals(object obj)
        {
            return
                (
                    obj is Vector4 p &&
                    X == p.X && Y == p.Y && Z == p.Z && W == p.W
                ) ||
                (
                    obj is Vector4I pi &&
                    X == pi.X && Y == pi.Y && Z == pi.Z && W == pi.W
                );
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Z, W);
        }

        public static bool operator ==(Vector4 a, Vector4 b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(Vector4 a, Vector4 b)
        {
            return !a.Equals(b);
        }
        public static bool operator ==(Vector4 a, Vector4I b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(Vector4 a, Vector4I b)
        {
            return !a.Equals(b);
        }

        public static Vector4 operator +(Vector4 a, double b)
        {
            return new Vector4(a.X + b, a.Y + b, a.Z + b, a.W + b);
        }
        public static Vector4 operator +(double a, Vector4 b)
        {
            return b + a;
        }
        public static Vector4 operator +(Vector4 a, Vector4 b)
        {
            return new Vector4(a.X + b.X, a.Y + b.Y, a.Z + b.Z, a.W + b.W);
        }

        public static Vector4 operator -(Vector4 a, double b)
        {
            return new Vector4(a.X - b, a.Y - b, a.Z - b, a.W - b);
        }
        public static Vector4 operator -(Vector4 a, Vector4 b)
        {
            return new Vector4(a.X - b.X, a.Y - b.Y, a.Z - b.Z, a.W - b.W);
        }
        public static Vector4 operator -(Vector4 v)
        {
            return new Vector4(-v.X, -v.Y, -v.Z, -v.W);
        }

        public static Vector4 operator *(Vector4 a, double b)
        {
            return new Vector4(a.X * b, a.Y * b, a.Z * b, a.W * b);
        }
        public static Vector4 operator *(double a, Vector4 b)
        {
            return b * a;
        }
        public static Vector4 operator *(Vector4 a, Vector4 b)
        {
            return new Vector4(a.X * b.X, a.Y * b.Y, a.Z * b.Z, a.W * b.W);
        }

        public static Vector2 operator *(Vector4 a, Matrix2x4 b)
        {
            return a.MultiplyMatrix(b);
        }
        public static Vector2 operator *(Matrix2x4 a, Vector4 b)
        {
            return b.MultiplyMatrix(a);
        }

        public static Vector3 operator *(Vector4 a, Matrix3x4 b)
        {
            return a.MultiplyMatrix(b);
        }
        public static Vector3 operator *(Matrix3x4 a, Vector4 b)
        {
            return b.MultiplyMatrix(a);
        }

        public static Vector4 operator *(Vector4 a, Matrix4 b)
        {
            return a.MultiplyMatrix(b);
        }
        public static Vector4 operator *(Matrix4 a, Vector4 b)
        {
            return b.MultiplyMatrix(a);
        }

        public static Vector4 operator /(Vector4 a, double b)
        {
            return new Vector4(a.X / b, a.Y / b, a.Z / b, a.W / b);
        }
        public static Vector4 operator /(Vector4 a, Vector4 b)
        {
            return new Vector4(a.X / b.X, a.Y / b.Y, a.Z / b.Z, a.W / b.W);
        }

        public static explicit operator Vector4(Vector2 p)
        {
            return new Vector4(p, 0, 1);
        }
        public static explicit operator Vector4(Vector3 p)
        {
            return new Vector4(p, 1);
        }
        public static explicit operator Vector4(Vector2I p)
        {
            return new Vector4(p, 0, 1);
        }
        public static explicit operator Vector4(Vector3I p)
        {
            return new Vector4(p, 1);
        }
        public static implicit operator Vector4(Vector4I p)
        {
            return new Vector4(p);
        }

        /// <summary>
        /// A vector with <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/> and <see cref="W"/> set to 0.
        /// </summary>
        public static Vector4 Zero { get; } = new Vector4(0, 0, 0, 0);
        /// <summary>
        /// A vector with <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/> and <see cref="W"/> set to 1.
        /// </summary>
        public static Vector4 One { get; } = new Vector4(1, 1, 1, 1);
        /// <summary>
        /// A vector with <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/> and <see cref="W"/> set to <see cref="double.PositiveInfinity"/>.
        /// </summary>
        public static Vector4 PositiveInfinity { get; } = new Vector4(double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity);
        /// <summary>
        /// A vector with <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/> and <see cref="W"/> set to <see cref="double.NegativeInfinity"/>.
        /// </summary>
        public static Vector4 NegativeInfinity { get; } = new Vector4(double.NegativeInfinity, double.NegativeInfinity, double.NegativeInfinity, double.NegativeInfinity);

        public static Vector4 operator +(Vector4 a, Vector2 b)
        {
            return new Vector4(a.X + b.X, a.Y + b.Y, a.Z, a.W);
        }
        public static Vector4 operator +(Vector4 a, Vector3 b)
        {
            return new Vector4(a.X + b.X, a.Y + b.Y, a.Z + b.Z, a.W);
        }
        public static Vector4 operator -(Vector4 a, Vector2 b)
        {
            return new Vector4(a.X - b.X, a.Y - b.Y, a.Z, a.W);
        }
        public static Vector4 operator -(Vector4 a, Vector3 b)
        {
            return new Vector4(a.X - b.X, a.Y - b.Y, a.Z - b.Z, a.W);
        }
        public static Vector4 operator +(Vector4 a, Vector2I b)
        {
            return new Vector4(a.X + b.X, a.Y + b.Y, a.Z, a.W);
        }
        public static Vector4 operator +(Vector4 a, Vector3I b)
        {
            return new Vector4(a.X + b.X, a.Y + b.Y, a.Z + b.Z, a.W);
        }
        public static Vector4 operator -(Vector4 a, Vector2I b)
        {
            return new Vector4(a.X - b.X, a.Y - b.Y, a.Z, a.W);
        }
        public static Vector4 operator -(Vector4 a, Vector3I b)
        {
            return new Vector4(a.X - b.X, a.Y - b.Y, a.Z - b.Z, a.W);
        }

        public static Vector4 operator +(Vector4 a, Vector4I b)
        {
            return new Vector4(a.X + b.X, a.Y + b.Y, a.Z + b.Z, a.W + b.W);
        }
        public static Vector4 operator -(Vector4 a, Vector4I b)
        {
            return new Vector4(a.X - b.X, a.Y - b.Y, a.Z - b.Z, a.W - b.W);
        }
        public static Vector4 operator *(Vector4 a, Vector4I b)
        {
            return new Vector4(a.X * b.X, a.Y * b.Y, a.Z * b.Z, a.W * b.W);
        }
        public static Vector4 operator /(Vector4 a, Vector4I b)
        {
            return new Vector4(a.X / b.X, a.Y / b.Y, a.Z / b.Z, a.W / b.W);
        }

        public static implicit operator Vector4((double, double, double, double) v) => new Vector4(v.Item1, v.Item2, v.Item3, v.Item4);
        public static implicit operator Vector4((Vector2, double, double) v) => new Vector4(v.Item1, v.Item2, v.Item3);
        public static implicit operator Vector4((double, Vector2, double) v) => new Vector4(v.Item1, v.Item2, v.Item3);
        public static implicit operator Vector4((double, double, Vector2) v) => new Vector4(v.Item1, v.Item2, v.Item3);
        public static implicit operator Vector4((Vector2, Vector2) v) => new Vector4(v.Item1, v.Item2);
        public static implicit operator Vector4((Vector3, double) v) => new Vector4(v.Item1, v.Item2);
        public static implicit operator Vector4((double, Vector3) v) => new Vector4(v.Item1, v.Item2);
    }
}
