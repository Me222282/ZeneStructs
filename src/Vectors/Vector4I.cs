using System;

namespace Zene.Structs
{
    /// <summary>
    /// An object that stores a 4 dimensional <see cref="int"/> vector.
    /// </summary>
    public struct Vector4I
    {
        /// <summary>
        /// Creates a 4 dimensional vector from a single <see cref="int"/>.
        /// </summary>
        /// <param name="value">The value to set to <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/> and <see cref="W"/>.</param>
        public Vector4I(int value)
        {
            X = value;
            Y = value;
            Z = value;
            W = value;
        }
        /// <summary>
        /// Creates a 4 dimensional vector from a single <see cref="double"/>.
        /// </summary>
        /// <param name="value">The value to set to <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/> and <see cref="W"/>.</param>
        public Vector4I(double value)
        {
            X = (int)value;
            Y = X;
            Z = X;
            W = X;
        }
        /// <summary>
        /// Creates a 4 dimensional vector from four <see cref="int"/>.
        /// </summary>
        /// <param name="x">The value to set to <see cref="X"/>.</param>
        /// <param name="y">The value to set to <see cref="Y"/>.</param>
        /// <param name="z">The value to set to <see cref="Z"/>.</param>
        /// <param name="w">The value to set to <see cref="W"/>.</param>
        public Vector4I(int x, int y, int z, int w)
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
        public Vector4I(double x, double y, double z, double w)
        {
            X = (int)x;
            Y = (int)y;
            Z = (int)z;
            W = (int)w;
        }
        /// <summary>
        /// Creates a 4 dimensional vector from two <see cref="Vector2"/>.
        /// </summary>
        /// <param name="xy">The vector to reference for <see cref="X"/> and <see cref="Y"/>.</param>
        /// <param name="zw">The vector to reference for <see cref="Z"/> and <see cref="W"/>.</param>
        public Vector4I(Vector2 xy, Vector2 zw)
        {
            X = (int)xy.X;
            Y = (int)xy.Y;
            Z = (int)zw.X;
            W = (int)zw.Y;
        }
        /// <summary>
        /// Creates a 4 dimensional vector from two <see cref="Vector2I"/>.
        /// </summary>
        /// <param name="xy">The vector to reference for <see cref="X"/> and <see cref="Y"/>.</param>
        /// <param name="zw">The vector to reference for <see cref="Z"/> and <see cref="W"/>.</param>
        public Vector4I(Vector2I xy, Vector2I zw)
        {
            X = xy.X;
            Y = xy.Y;
            Z = zw.X;
            W = zw.Y;
        }
        /// <summary>
        /// Creates a 4 dimensional vector from a <see cref="double"/> based 2 dimensional vector and an <see cref="int"/> Z and W.
        /// </summary>
        /// <param name="xy">The vector to reference for <see cref="X"/> and <see cref="Y"/>.</param>
        /// <param name="z">The value to set to <see cref="Z"/>.</param>
        /// <param name="w">The value to set to <see cref="W"/>.</param>
        public Vector4I(Vector2 xy, int z, int w)
        {
            X = (int)xy.X;
            Y = (int)xy.Y;
            Z = z;
            W = w;
        }
        /// <summary>
        /// Creates a 4 dimensional vector from a <see cref="double"/> based 2 dimensional vector and a <see cref="double"/> Z and W.
        /// </summary>
        /// <param name="xy">The vector to reference for <see cref="X"/> and <see cref="Y"/>.</param>
        /// <param name="z">The value to set to <see cref="Z"/>.</param>
        /// <param name="w">The value to set to <see cref="W"/>.</param>
        public Vector4I(Vector2 xy, double z, double w)
        {
            X = (int)xy.X;
            Y = (int)xy.Y;
            Z = (int)z;
            W = (int)w;
        }
        /// <summary>
        /// Creates a 4 dimensional vector from a 2 dimensional vector and an <see cref="int"/> Z and W.
        /// </summary>
        /// <param name="xy">The vector to reference for <see cref="X"/> and <see cref="Y"/>.</param>
        /// <param name="z">The value to set to <see cref="Z"/>.</param>
        /// <param name="w">The value to set to <see cref="W"/>.</param>
        public Vector4I(Vector2I xy, int z, int w)
        {
            X = xy.X;
            Y = xy.Y;
            Z = z;
            W = w;
        }
        /// <summary>
        /// Creates a 4 dimensional vector from a 2 dimensional vector and a <see cref="double"/> Z and W.
        /// </summary>
        /// <param name="xy">The vector to reference for <see cref="X"/> and <see cref="Y"/>.</param>
        /// <param name="z">The value to set to <see cref="Z"/>.</param>
        /// <param name="w">The value to set to <see cref="W"/>.</param>
        public Vector4I(Vector2I xy, double z, double w)
        {
            X = xy.X;
            Y = xy.Y;
            Z = (int)z;
            W = (int)w;
        }
        /// <summary>
        /// Creates a 4 dimensional vector from a <see cref="double"/> based 3 dimensional vector and an <see cref="int"/> W.
        /// </summary>
        /// <param name="xyz">The vector to reference for <see cref="X"/>, <see cref="Y"/> and <see cref="Z"/>.</param>
        /// <param name="w">The value to set to <see cref="W"/>.</param>
        public Vector4I(Vector3 xyz, int w)
        {
            X = (int)xyz.X;
            Y = (int)xyz.Y;
            Z = (int)xyz.Z;
            W = w;
        }
        /// <summary>
        /// Creates a 4 dimensional vector from a <see cref="double"/> based 3 dimensional vector and a <see cref="double"/> W.
        /// </summary>
        /// <param name="xyz">The vector to reference for <see cref="X"/>, <see cref="Y"/> and <see cref="Z"/>.</param>
        /// <param name="w">The value to set to <see cref="W"/>.</param>
        public Vector4I(Vector3 xyz, double w)
        {
            X = (int)xyz.X;
            Y = (int)xyz.Y;
            Z = (int)xyz.Z;
            W = (int)w;
        }
        /// <summary>
        /// Creates a 4 dimensional vector from a 3 dimensional vector and an <see cref="int"/> W.
        /// </summary>
        /// <param name="xyz">The vector to reference for <see cref="X"/>, <see cref="Y"/> and <see cref="Z"/>.</param>
        /// <param name="w">The value to set to <see cref="W"/>.</param>
        public Vector4I(Vector3I xyz, int w)
        {
            X = xyz.X;
            Y = xyz.Y;
            Z = xyz.Z;
            W = w;
        }
        /// <summary>
        /// Creates a 4 dimensional vector from a 3 dimensional vector and a <see cref="double"/> W.
        /// </summary>
        /// <param name="xyz">The vector to reference for <see cref="X"/>, <see cref="Y"/> and <see cref="Z"/>.</param>
        /// <param name="w">The value to set to <see cref="W"/>.</param>
        public Vector4I(Vector3I xyz, double w)
        {
            X = xyz.X;
            Y = xyz.Y;
            Z = xyz.Z;
            W = (int)w;
        }
        /// <summary>
        /// Creates a 4 dimensional vector from a <see cref="double"/> based vector.
        /// </summary>
        /// <param name="xyzw">The vector to reference for <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/> and <see cref="W"/>.</param>
        public Vector4I(Vector4 xyzw)
        {
            X = (int)xyzw.X;
            Y = (int)xyzw.Y;
            Z = (int)xyzw.Z;
            W = (int)xyzw.W;
        }

        /// <summary>
        /// The first value of the vector.
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// The second value of the vector.
        /// </summary>
        public int Y { get; set; }
        /// <summary>
        /// The third value of the vector.
        /// </summary>
        public int Z { get; set; }
        /// <summary>
        /// The fourth value of the vector.
        /// </summary>
        public int W { get; set; }

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
        public int SquaredLength
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
        public double Distance(Vector4I b)
        {
            return Math.Sqrt(SquaredDistance(b));
        }
        /// <summary>
        /// The squared distance from this vector to <paramref name="b"/>.
        /// </summary>
        /// <param name="b">The vector to reference.</param>
        /// <returns></returns>
        public int SquaredDistance(Vector4I b)
        {
            return ((b.X - X) * (b.X - X)) + ((b.Y - Y) * (b.Y - Y)) + ((b.Z - Z) * (b.Z - Z)) + ((b.W - W) * (b.W - W));
        }

        /// <summary>
        /// The dot product of this vector to <paramref name="b"/>.
        /// </summary>
        /// <param name="b">The vector to reference.</param>
        /// <returns></returns>
        public int Dot(Vector4I b)
        {
            return (X * b.X) + (Y * b.Y) + (Z * b.Z) + (W * b.W);
        }

        /// <summary>
        /// A linear interpolation between this vector and <paramref name="b"/>.
        /// </summary>
        /// <param name="b">The vector to interpolate to.</param>
        /// <param name="blend">The percentage interpolation.</param>
        /// <returns></returns>
        public Vector4I Lerp(Vector4I b, double blend)
        {
            return new Vector4I(
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
        public Vector4I BaryCentric(Vector4I b, Vector4I c, int u, int v)
        {
            return (this + ((b - this) * u)) + ((c - this) * v);
        }

        /// <summary>
        /// Returns a normalised version of this vector.
        /// </summary>
        /// <returns></returns>
        public Vector4I Normalised()
        {
            if (Length == 0) { return Zero; }

            double scale = 1.0 / Length;
            return new Vector4I(X * scale, Y * scale, Z * scale, W * scale);
        }
        /// <summary>
        /// Normalises this vector.
        /// </summary>
        /// <returns></returns>
        public void Normalise()
        {
            double scale = 1.0 / Length;

            X = (int)(X * scale);
            Y = (int)(Y * scale);
            Z = (int)(Z * scale);
            W = (int)(W * scale);
        }

        /// <summary>
        /// Returns this vector multiplied by <paramref name="matrix"/>.
        /// </summary>
        /// <param name="matrix">The <see cref="Matrix2x4"/> to multiply by.</param>
        /// <returns></returns>
        public Vector2I MultiplyMatrix(Matrix2x4 matrix)
        {
            return new Vector2I(
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
        public Vector3I MultiplyMatrix(Matrix3x4 matrix)
        {
            return new Vector3I(
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
        public Vector4I MultiplyMatrix(Matrix4 matrix)
        {
            return new Vector4I(
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
                    obj is Vector4I p &&
                    X == p.X && Y == p.Y && Z == p.Z && W == p.W
                ) ||
                (
                    obj is Vector4 pd &&
                    X == pd.X && Y == pd.Y && Z == pd.Z && W == pd.W
                );
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Z, W);
        }

        public static bool operator ==(Vector4I a, Vector4I b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(Vector4I a, Vector4I b)
        {
            return !a.Equals(b);
        }
        public static bool operator ==(Vector4I a, Vector4 b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(Vector4I a, Vector4 b)
        {
            return !a.Equals(b);
        }

        public static Vector4I operator +(Vector4I a, int b)
        {
            return new Vector4I(a.X + b, a.Y + b, a.Z + b, a.W + b);
        }
        public static Vector4I operator +(int a, Vector4I b)
        {
            return b + a;
        }
        public static Vector4I operator +(Vector4I a, Vector4I b)
        {
            return new Vector4I(a.X + b.X, a.Y + b.Y, a.Z + b.Z, a.W + b.W);
        }

        public static Vector4I operator -(Vector4I a, int b)
        {
            return new Vector4I(a.X - b, a.Y - b, a.Z - b, a.W - b);
        }
        public static Vector4I operator -(Vector4I a, Vector4I b)
        {
            return new Vector4I(a.X - b.X, a.Y - b.Y, a.Z - b.Z, a.W - b.W);
        }
        public static Vector4I operator -(Vector4I v)
        {
            return new Vector4I(-v.X, -v.Y, -v.Z, -v.W);
        }

        public static Vector4I operator *(Vector4I a, int b)
        {
            return new Vector4I(a.X * b, a.Y * b, a.Z * b, a.W * b);
        }
        public static Vector4I operator *(int a, Vector4I b)
        {
            return b * a;
        }
        public static Vector4I operator *(Vector4I a, Vector4I b)
        {
            return new Vector4I(a.X * b.X, a.Y * b.Y, a.Z * b.Z, a.W * b.W);
        }

        public static Vector2I operator *(Vector4I a, Matrix2x4 b)
        {
            return a.MultiplyMatrix(b);
        }
        public static Vector2I operator *(Matrix2x4 a, Vector4I b)
        {
            return b.MultiplyMatrix(a);
        }

        public static Vector3I operator *(Vector4I a, Matrix3x4 b)
        {
            return a.MultiplyMatrix(b);
        }
        public static Vector3I operator *(Matrix3x4 a, Vector4I b)
        {
            return b.MultiplyMatrix(a);
        }

        public static Vector4I operator *(Vector4I a, Matrix4 b)
        {
            return a.MultiplyMatrix(b);
        }
        public static Vector4I operator *(Matrix4 a, Vector4I b)
        {
            return b.MultiplyMatrix(a);
        }

        public static Vector4I operator /(Vector4I a, int b)
        {
            return new Vector4I(a.X / b, a.Y / b, a.Z / b, a.W / b);
        }
        public static Vector4I operator /(Vector4I a, Vector4I b)
        {
            return new Vector4I(a.X / b.X, a.Y / b.Y, a.Z / b.Z, a.W / b.W);
        }

        public static implicit operator Vector4I(Vector2 p)
        {
            return new Vector4I(p, 1, 1);
        }
        public static implicit operator Vector4I(Vector3 p)
        {
            return new Vector4I(p, 1);
        }
        public static implicit operator Vector4I(Vector2I p)
        {
            return new Vector4I(p, 1, 1);
        }
        public static implicit operator Vector4I(Vector3I p)
        {
            return new Vector4I(p, 1);
        }
        public static implicit operator Vector4I(Vector4 p)
        {
            return new Vector4I(p);
        }

        /// <summary>
        /// A vector with <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/> and <see cref="W"/> set to 0.
        /// </summary>
        public static Vector4I Zero { get; } = new Vector4I(0, 0, 0, 0);
        /// <summary>
        /// A vector with <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/> and <see cref="W"/> set to 1.
        /// </summary>
        public static Vector4I One { get; } = new Vector4I(1, 1, 1, 1);

        public static Vector4I operator +(Vector4I a, Vector2 b)
        {
            return new Vector4I(a.X + b.X, a.Y + b.Y, a.Z, a.W);
        }
        public static Vector4I operator +(Vector4I a, Vector3 b)
        {
            return new Vector4I(a.X + b.X, a.Y + b.Y, a.Z + b.Z, a.W);
        }
        public static Vector4I operator -(Vector4I a, Vector2 b)
        {
            return new Vector4I(a.X - b.X, a.Y - b.Y, a.Z, a.W);
        }
        public static Vector4I operator -(Vector4I a, Vector3 b)
        {
            return new Vector4I(a.X - b.X, a.Y - b.Y, a.Z - b.Z, a.W);
        }
        public static Vector4I operator +(Vector4I a, Vector2I b)
        {
            return new Vector4I(a.X + b.X, a.Y + b.Y, a.Z, a.W);
        }
        public static Vector4I operator +(Vector4I a, Vector3I b)
        {
            return new Vector4I(a.X + b.X, a.Y + b.Y, a.Z + b.Z, a.W);
        }
        public static Vector4I operator -(Vector4I a, Vector2I b)
        {
            return new Vector4I(a.X - b.X, a.Y - b.Y, a.Z, a.W);
        }
        public static Vector4I operator -(Vector4I a, Vector3I b)
        {
            return new Vector4I(a.X - b.X, a.Y - b.Y, a.Z - b.Z, a.W);
        }

        public static Vector4 operator +(Vector4I a, Vector4 b)
        {
            return new Vector4(a.X + b.X, a.Y + b.Y, a.Z + b.Z, a.W + b.W);
        }
        public static Vector4 operator -(Vector4I a, Vector4 b)
        {
            return new Vector4(a.X - b.X, a.Y - b.Y, a.Z - b.Z, a.W - b.W);
        }
        public static Vector4 operator *(Vector4I a, Vector4 b)
        {
            return new Vector4(a.X * b.X, a.Y * b.Y, a.Z * b.Z, a.W * b.W);
        }
        public static Vector4 operator /(Vector4I a, Vector4 b)
        {
            return new Vector4(a.X / b.X, a.Y / b.Y, a.Z / b.Z, a.W / b.W);
        }

        public static implicit operator Vector4I((int, int, int, int) v)
        {
            return new Vector4I(v.Item1, v.Item2, v.Item3, v.Item4);
        }
    }
}
