using System;

namespace Zene.Structs
{
    /// <summary>
    /// An object that stores a 3 dimensional <see cref="int"/> vector.
    /// </summary>
    public struct Vector3I
    {
        /// <summary>
        /// Creates a 3 dimensional vector from a single <see cref="int"/>.
        /// </summary>
        /// <param name="value">The value to set to <see cref="X"/>, <see cref="Y"/> and <see cref="Z"/>.</param>
        public Vector3I(int value)
        {
            X = value;
            Y = value;
            Z = value;
        }
        /// <summary>
        /// Creates a 3 dimensional vector from a single <see cref="double"/>.
        /// </summary>
        /// <param name="value">The value to set to <see cref="X"/>, <see cref="Y"/> and <see cref="Z"/>.</param>
        public Vector3I(double value)
        {
            X = (int)value;
            Y = X;
            Z = X;
        }
        /// <summary>
        /// Creates a 3 dimensional vector from three <see cref="int"/>.
        /// </summary>
        /// <param name="x">The value to set to <see cref="X"/>.</param>
        /// <param name="y">The value to set to <see cref="Y"/>.</param>
        /// <param name="z">The value to set to <see cref="Z"/>.</param>
        public Vector3I(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        /// <summary>
        /// Creates a 3 dimensional vector from three <see cref="double"/>.
        /// </summary>
        /// <param name="x">The value to set to <see cref="X"/>.</param>
        /// <param name="y">The value to set to <see cref="Y"/>.</param>
        /// <param name="z">The value to set to <see cref="Z"/>.</param>
        public Vector3I(double x, double y, double z)
        {
            X = (int)x;
            Y = (int)y;
            Z = (int)z;
        }
        /// <summary>
        /// Creates a 3 dimensional vector from a <see cref="double"/> based vector.
        /// </summary>
        /// <param name="xyz">The vector to reference for <see cref="X"/>, <see cref="Y"/> and <see cref="Z"/>.</param>
        public Vector3I(Vector3 xyz)
        {
            X = (int)xyz.X;
            Y = (int)xyz.Y;
            Z = (int)xyz.Z;
        }
        /// <summary>
        /// Creates a 3 dimensional vector from a <see cref="double"/> based 2 dimensional vector and a <see cref="double"/> Z.
        /// </summary>
        /// <param name="xy">The vector to reference for <see cref="X"/> and <see cref="Y"/>.</param>
        /// <param name="z">The value to set to <see cref="Z"/>.</param>
        public Vector3I(Vector2 xy, double z)
        {
            X = (int)xy.X;
            Y = (int)xy.Y;
            Z = (int)z;
        }
        /// <summary>
        /// Creates a 3 dimensional vector from a <see cref="double"/> based 2 dimensional vector and an <see cref="int"/> Z.
        /// </summary>
        /// <param name="xy">The vector to reference for <see cref="X"/> and <see cref="Y"/>.</param>
        /// <param name="z">The value to set to <see cref="Z"/>.</param>
        public Vector3I(Vector2 xy, int z)
        {
            X = (int)xy.X;
            Y = (int)xy.Y;
            Z = z;
        }
        /// <summary>
        /// Creates a 3 dimensional vector from a <see cref="double"/> based 2 dimensional vector and a <see cref="double"/> X.
        /// </summary>
        /// <param name="x">The value to set to <see cref="X"/>.</param>
        /// <param name="yz">The vector to reference for <see cref="Y"/> and <see cref="Z"/>.</param>
        public Vector3I(double x, Vector2 yz)
        {
            X = (int)x;
            Y = (int)yz.Y;
            Z = (int)yz.Y;
        }
        /// <summary>
        /// Creates a 3 dimensional vector from a <see cref="double"/> based 2 dimensional vector and an <see cref="int"/> X.
        /// </summary>
        /// <param name="xy">The value to set to <see cref="X"/>.</param>
        /// <param name="z">The vector to reference for <see cref="Y"/> and <see cref="Z"/>.</param>
        public Vector3I(int x, Vector2 yz)
        {
            X = x;
            Y = (int)yz.X;
            Z = (int)yz.Y;
        }
        /// <summary>
        /// Creates a 3 dimensional vector from a 2 dimensional vector and a <see cref="double"/> Z.
        /// </summary>
        /// <param name="xy">The vector to reference for <see cref="X"/> and <see cref="Y"/>.</param>
        /// <param name="z">The value to set to <see cref="Z"/>.</param>
        public Vector3I(Vector2I xy, double z)
        {
            X = xy.X;
            Y = xy.Y;
            Z = (int)z;
        }
        /// <summary>
        /// Creates a 3 dimensional vector from a based 2 dimensional vector and an <see cref="int"/> Z.
        /// </summary>
        /// <param name="xy">The vector to reference for <see cref="X"/> and <see cref="Y"/>.</param>
        /// <param name="z">The value to set to <see cref="Z"/>.</param>
        public Vector3I(Vector2I xy, int z)
        {
            X = xy.X;
            Y = xy.Y;
            Z = z;
        }
        /// <summary>
        /// Creates a 3 dimensional vector from a 2 dimensional vector and a <see cref="double"/> X.
        /// </summary>
        /// <param name="yz">The vector to reference for <see cref="Y"/> and <see cref="Z"/>.</param>
        /// <param name="x">The value to set to <see cref="X"/>.</param>
        public Vector3I(double x, Vector2I yz)
        {
            Y = yz.X;
            Z = yz.Y;
            X = (int)x;
        }
        /// <summary>
        /// Creates a 3 dimensional vector from a based 2 dimensional vector and an <see cref="int"/> X.
        /// </summary>
        /// <param name="yz">The vector to reference for <see cref="Y"/> and <see cref="Z"/>.</param>
        /// <param name="x">The value to set to <see cref="X"/>.</param>
        public Vector3I(int x, Vector2I yz)
        {
            Y = yz.X;
            Z = yz.Y;
            X = x;
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
        /// The length of the vector (distance from origin).
        /// </summary>
        public double Length
        {
            get
            {
                return Math.Sqrt((X * X) + (Y * Y) + (Z * Z));
            }
        }
        /// <summary>
        /// The squared length of the vector (distance from origin squared).
        /// </summary>
        public int SquaredLength
        {
            get
            {
                return (X * X) + (Y * Y) + (Z * Z);
            }
        }

        /// <summary>
        /// The distance from this vector to <paramref name="b"/>.
        /// </summary>
        /// <param name="b">The vector to reference.</param>
        /// <returns></returns>
        public double Distance(Vector3I b)
        {
            return Math.Sqrt(SquaredDistance(b));
        }
        /// <summary>
        /// The squared distance from this vector to <paramref name="b"/>.
        /// </summary>
        /// <param name="b">The vector to reference.</param>
        /// <returns></returns>
        public int SquaredDistance(Vector3I b)
        {
            return ((b.X - X) * (b.X - X)) + ((b.Y - Y) * (b.Y - Y)) + ((b.Z - Z) * (b.Z - Z));
        }

        /// <summary>
        /// The dot product of this vector to <paramref name="b"/>.
        /// </summary>
        /// <param name="b">The vector to reference.</param>
        /// <returns></returns>
        public int Dot(Vector3I b)
        {
            return (X * b.X) + (Y * b.Y) + (Z * b.Z);
        }

        /// <summary>
        /// The dot cross product of this vector to <paramref name="b"/>.
        /// </summary>
        /// <param name="b">The vector to reference.</param>
        /// <returns></returns>
        public Vector3I Cross(Vector3I b)
        {
            return new Vector3I(
                (Y * b.Z) - (Z * b.Y),
                (Z * b.X) - (X * b.Z),
                (X * b.Y) - (Y * b.X));
        }

        /// <summary>
        /// A linear interpolation between this vector and <paramref name="b"/>.
        /// </summary>
        /// <param name="b">The vector to interpolate to.</param>
        /// <param name="blend">The percentage interpolation.</param>
        /// <returns></returns>
        public Vector3I Lerp(Vector3I b, double blend)
        {
            return new Vector3I(
                (blend * (b.X - X)) + X,
                (blend * (b.Y - Y)) + Y,
                (blend * (b.Z - Z)) + Z);
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
        public Vector3I BaryCentric(Vector3I b, Vector3I c, int u, int v)
        {
            return (this + ((b - this) * u)) + ((c - this) * v);
        }

        /// <summary>
        /// Returns a normalised version of this vector.
        /// </summary>
        /// <returns></returns>
        public Vector3I Normalised()
        {
            if (Length == 0) { return Zero; }

            double scale = 1.0 / Length;
            return new Vector3I(X * scale, Y * scale, Z * scale);
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
        }

        /// <summary>
        /// Returns this vector multiplied by <paramref name="matrix"/>.
        /// </summary>
        /// <param name="matrix">The <see cref="Matrix2x3"/> to multiply by.</param>
        /// <returns></returns>
        public Vector2I MultiplyMatrix(Matrix2x3 matrix)
        {
            return new Vector2I(
                //(matrix[0, 0] * X) + (matrix[0, 1] * Y) + (matrix[0, 2] * Z),
                //(matrix[1, 0] * X) + (matrix[1, 1] * Y) + (matrix[1, 2] * Z));
                (matrix[0, 0] * X) + (matrix[1, 0] * Y) + (matrix[2, 0] * Z),
                (matrix[0, 1] * X) + (matrix[1, 1] * Y) + (matrix[2, 1] * Z));
        }
        /// <summary>
        /// Returns this vector multiplied by <paramref name="matrix"/>.
        /// </summary>
        /// <param name="matrix">The <see cref="Matrix3"/> to multiply by.</param>
        /// <returns></returns>
        public Vector3I MultiplyMatrix(Matrix3 matrix)
        {
            return new Vector3I(
                //(matrix[0, 0] * X) + (matrix[0, 1] * Y) + (matrix[0, 2] * Z),
                //(matrix[1, 0] * X) + (matrix[1, 1] * Y) + (matrix[1, 2] * Z),
                //(matrix[2, 0] * X) + (matrix[2, 1] * Y) + (matrix[2, 2] * Z));
                (matrix[0, 0] * X) + (matrix[1, 0] * Y) + (matrix[2, 0] * Z),
                (matrix[0, 1] * X) + (matrix[1, 1] * Y) + (matrix[2, 1] * Z),
                (matrix[0, 2] * X) + (matrix[1, 2] * Y) + (matrix[2, 2] * Z));
        }
        /// <summary>
        /// Returns this vector multiplied by <paramref name="matrix"/>.
        /// </summary>
        /// <param name="matrix">The <see cref="Matrix4x3"/> to multiply by.</param>
        /// <returns></returns>
        public Vector4I MultiplyMatrix(Matrix4x3 matrix)
        {
            return new Vector4I(
                //(matrix[0, 0] * X) + (matrix[0, 1] * Y) + (matrix[0, 2] * Z),
                //(matrix[1, 0] * X) + (matrix[1, 1] * Y) + (matrix[1, 2] * Z),
                //(matrix[2, 0] * X) + (matrix[2, 1] * Y) + (matrix[2, 2] * Z),
                //(matrix[3, 0] * X) + (matrix[3, 1] * Y) + (matrix[3, 2] * Z));
                (matrix[0, 0] * X) + (matrix[1, 0] * Y) + (matrix[2, 0] * Z),
                (matrix[0, 1] * X) + (matrix[1, 1] * Y) + (matrix[2, 1] * Z),
                (matrix[0, 2] * X) + (matrix[1, 2] * Y) + (matrix[2, 2] * Z),
                (matrix[0, 3] * X) + (matrix[1, 3] * Y) + (matrix[2, 3] * Z));
        }

        public override string ToString()
        {
            return $"X:{X}, Y:{Y}, Z:{Z}";
        }
        public string ToString(string format)
        {
            return $"X:{X.ToString(format)}, Y:{Y.ToString(format)}, Z:{Z.ToString(format)}";
        }

        public override bool Equals(object obj)
        {
            return
                (
                    obj is Vector3I p &&
                    X == p.X && Y == p.Y && Z == p.Z
                ) ||
                (
                    obj is Vector3 pd &&
                    X == pd.X && Y == pd.Y && Z == pd.Z
                );
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Z);
        }

        public static bool operator ==(Vector3I a, Vector3I b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(Vector3I a, Vector3I b)
        {
            return !a.Equals(b);
        }
        public static bool operator ==(Vector3I a, Vector3 b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(Vector3I a, Vector3 b)
        {
            return !a.Equals(b);
        }

        /*
        public static Vector3I operator +(Vector3I a, int b)
        {
            return new Vector3I(a.X + b, a.Y + b, a.Z + b);
        }
        public static Vector3I operator +(int a, Vector3I b)
        {
            return b + a;
        }*/
        public static Vector3I operator +(Vector3I a, Vector3I b)
        {
            return new Vector3I(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        /*
        public static Vector3I operator -(Vector3I a, int b)
        {
            return new Vector3I(a.X - b, a.Y - b, a.Z - b);
        }*/
        public static Vector3I operator -(Vector3I a, Vector3I b)
        {
            return new Vector3I(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }
        public static Vector3I operator -(Vector3I v)
        {
            return new Vector3I(-v.X, -v.Y, -v.Z);
        }

        /*
        public static Vector3I operator *(Vector3I a, int b)
        {
            return new Vector3I(a.X * b, a.Y * b, a.Z * b);
        }
        public static Vector3I operator *(int a, Vector3I b)
        {
            return b * a;
        }*/
        public static Vector3I operator *(Vector3I a, Vector3I b)
        {
            return new Vector3I(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        }

        public static Vector2I operator *(Vector3I a, Matrix2x3 b)
        {
            return a.MultiplyMatrix(b);
        }
        public static Vector2I operator *(Matrix2x3 a, Vector3I b)
        {
            return b.MultiplyMatrix(a);
        }

        public static Vector3I operator *(Vector3I a, Matrix3 b)
        {
            return a.MultiplyMatrix(b);
        }
        public static Vector3I operator *(Matrix3 a, Vector3I b)
        {
            return b.MultiplyMatrix(a);
        }

        public static Vector4I operator *(Vector3I a, Matrix4x3 b)
        {
            return a.MultiplyMatrix(b);
        }
        public static Vector4I operator *(Matrix4x3 a, Vector3I b)
        {
            return b.MultiplyMatrix(a);
        }

        /*
        public static Vector3I operator /(Vector3I a, int b)
        {
            return new Vector3I(a.X / b, a.Y / b, a.Z / b);
        }*/
        public static Vector3I operator /(Vector3I a, Vector3I b)
        {
            return new Vector3I(a.X / b.X, a.Y / b.Y, a.Z / b.Z);
        }

        public static explicit operator Vector3I(Vector2 p)
        {
            return new Vector3I(p, 0);
        }
        public static explicit operator Vector3I(Vector4 p)
        {
            return new Vector3I(p.X, p.Y, p.Z);
        }
        public static implicit operator Vector3I(Vector2I p)
        {
            return new Vector3I(p, 0);
        }
        public static explicit operator Vector3I(Vector4I p)
        {
            return new Vector3I(p.X, p.Y, p.Z);
        }
        public static explicit operator Vector3I(Vector3 p)
        {
            return new Vector3I(p);
        }

        /// <summary>
        /// A vector with <see cref="X"/>, <see cref="Y"/> and <see cref="Z"/> set to 0.
        /// </summary>
        public static Vector3I Zero { get; } = new Vector3I(0, 0, 0);
        /// <summary>
        /// A vector with <see cref="X"/>, <see cref="Y"/> and <see cref="Z"/> set to 1.
        /// </summary>
        public static Vector3I One { get; } = new Vector3I(1, 1, 1);

        public static Vector3I operator +(Vector3I a, Vector2 b)
        {
            return new Vector3I(a.X + b.X, a.Y + b.Y, a.Z);
        }
        public static Vector3I operator +(Vector3I a, Vector2I b)
        {
            return new Vector3I(a.X + b.X, a.Y + b.Y, a.Z);
        }
        public static Vector3I operator -(Vector3I a, Vector2 b)
        {
            return new Vector3I(a.X - b.X, a.Y - b.Y, a.Z);
        }
        public static Vector3I operator -(Vector3I a, Vector2I b)
        {
            return new Vector3I(a.X - b.X, a.Y - b.Y, a.Z);
        }

        public static Vector3 operator +(Vector3 a, Vector3I b)
        {
            return new Vector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }
        public static Vector3 operator -(Vector3 a, Vector3I b)
        {
            return new Vector3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }
        public static Vector3 operator *(Vector3 a, Vector3I b)
        {
            return new Vector3(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        }
        public static Vector3 operator /(Vector3 a, Vector3I b)
        {
            return new Vector3(a.X / b.X, a.Y / b.Y, a.Z / b.Z);
        }

        public static implicit operator Vector3I((int, int, int) v) => new Vector3I(v.Item1, v.Item2, v.Item3);
        public static implicit operator Vector3I((Vector2I, int) v) => new Vector3I(v.Item1, v.Item2);
        public static implicit operator Vector3I((int, Vector2I) v) => new Vector3I(v.Item1, v.Item2);
        public static implicit operator Vector3I(int v) => new Vector3I(v);
    }
}
