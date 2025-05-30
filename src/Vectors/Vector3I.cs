﻿using System;

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
        /// Creates a 3 dimensional vector from a single <see cref="floatv"/>.
        /// </summary>
        /// <param name="value">The value to set to <see cref="X"/>, <see cref="Y"/> and <see cref="Z"/>.</param>
        public Vector3I(floatv value)
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
        /// Creates a 3 dimensional vector from three <see cref="floatv"/>.
        /// </summary>
        /// <param name="x">The value to set to <see cref="X"/>.</param>
        /// <param name="y">The value to set to <see cref="Y"/>.</param>
        /// <param name="z">The value to set to <see cref="Z"/>.</param>
        public Vector3I(floatv x, floatv y, floatv z)
        {
            X = (int)x;
            Y = (int)y;
            Z = (int)z;
        }
        /// <summary>
        /// Creates a 3 dimensional vector from a <see cref="floatv"/> based vector.
        /// </summary>
        /// <param name="xyz">The vector to reference for <see cref="X"/>, <see cref="Y"/> and <see cref="Z"/>.</param>
        public Vector3I(Vector3 xyz)
        {
            X = (int)xyz.X;
            Y = (int)xyz.Y;
            Z = (int)xyz.Z;
        }
        /// <summary>
        /// Creates a 3 dimensional vector from a <see cref="floatv"/> based 2 dimensional vector and a <see cref="floatv"/> Z.
        /// </summary>
        /// <param name="xy">The vector to reference for <see cref="X"/> and <see cref="Y"/>.</param>
        /// <param name="z">The value to set to <see cref="Z"/>.</param>
        public Vector3I(Vector2 xy, floatv z)
        {
            X = (int)xy.X;
            Y = (int)xy.Y;
            Z = (int)z;
        }
        /// <summary>
        /// Creates a 3 dimensional vector from a <see cref="floatv"/> based 2 dimensional vector and an <see cref="int"/> Z.
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
        /// Creates a 3 dimensional vector from a <see cref="floatv"/> based 2 dimensional vector and a <see cref="floatv"/> X.
        /// </summary>
        /// <param name="x">The value to set to <see cref="X"/>.</param>
        /// <param name="yz">The vector to reference for <see cref="Y"/> and <see cref="Z"/>.</param>
        public Vector3I(floatv x, Vector2 yz)
        {
            X = (int)x;
            Y = (int)yz.Y;
            Z = (int)yz.Y;
        }
        /// <summary>
        /// Creates a 3 dimensional vector from a <see cref="floatv"/> based 2 dimensional vector and an <see cref="int"/> X.
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
        /// Creates a 3 dimensional vector from a 2 dimensional vector and a <see cref="floatv"/> Z.
        /// </summary>
        /// <param name="xy">The vector to reference for <see cref="X"/> and <see cref="Y"/>.</param>
        /// <param name="z">The value to set to <see cref="Z"/>.</param>
        public Vector3I(Vector2I xy, floatv z)
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
        /// Creates a 3 dimensional vector from a 2 dimensional vector and a <see cref="floatv"/> X.
        /// </summary>
        /// <param name="yz">The vector to reference for <see cref="Y"/> and <see cref="Z"/>.</param>
        /// <param name="x">The value to set to <see cref="X"/>.</param>
        public Vector3I(floatv x, Vector2I yz)
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
        public floatv Length
        {
            get
            {
                return Maths.Sqrt((X * X) + (Y * Y) + (Z * Z));
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
        public floatv Distance(Vector3I b)
        {
            return Maths.Sqrt(SquaredDistance(b));
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
        public Vector3I Lerp(Vector3I b, floatv blend)
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
        public Vector3I BaryCentric(Vector3I b, Vector3I c, floatv u, floatv v)
        {
            return (Vector3I)(this + ((b - this) * u) + ((c - this) * v));
        }

        /// <summary>
        /// Returns a normalised version of this vector.
        /// </summary>
        /// <returns></returns>
        public Vector3 Normalised()
        {
            floatv sl = SquaredLength;
            if (sl == 0) { return Zero; }
            if (sl == 1) { return this; }

            floatv scale = 1 / Maths.Sqrt(sl);
            return new Vector3(X * scale, Y * scale, Z * scale);
        }

        /// <summary>
        /// Returns this vector multiplied by <paramref name="matrix"/>.
        /// </summary>
        /// <param name="matrix">The <see cref="MatrixSpan"/> to multiply by.</param>
        /// <returns></returns>
        public VariableVector MultiplyMatrix(MatrixSpan matrix)
        {
            if (matrix.Columns > 3)
            {
                throw new Exception();
            }

            floatv[] vs = new floatv[matrix.Rows];

            for (int i = 0; i < matrix.Rows; i++)
            {
                vs[i] = (matrix.Data[i] * X) + (matrix.Data[i + matrix.Columns] * Y) + (matrix.Data[i + (2 * matrix.Columns)] * Z);
            }

            return new VariableVector(vs);
        }

        /// <summary>
        /// Returns a vector with each component the absolute of this.
        /// </summary>
        /// <returns></returns>
        public Vector3I Abs() => new Vector3I(Math.Abs(X), Math.Abs(Y), Math.Abs(Z));

        public void Deconstruct(out int x, out int y, out int z)
        {
            x = X;
            y = Y;
            z = Z;
        }
        public void Deconstruct(out int x, out Vector2I yz)
        {
            x = X;
            yz = new Vector2I(Y, Z);
        }
        public void Deconstruct(out Vector2I xy, out int z)
        {
            xy = new Vector2I(X, Y);
            z = Z;
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
                    obj is Vector3I p && this == p
                ) ||
                (
                    obj is Vector3 pd && this == pd
                );
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Z);
        }

        public static bool operator ==(Vector3I a, Vector3I b)
        {
            return a.X == b.X && a.Y == b.Y && a.Z == b.Z;
        }
        public static bool operator !=(Vector3I a, Vector3I b) => !(a == b);
        public static bool operator ==(Vector3I a, Vector3 b)
        {
            return a.X == b.X && a.Y == b.Y && a.Z == b.Z;
        }
        public static bool operator !=(Vector3I a, Vector3 b) => !(a == b);

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
        public static Vector3I operator +(Vector3I a, VariableVector b)
        {
            return new Vector3I(a.X + b[0], a.Y + b[1], a.Z + b[2]);
        }
        public static Vector3I operator +(VariableVector a, Vector3I b)
        {
            return new Vector3I(a[0] + b.X, a[1] + b.Y, a[2] + b.Z);
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
        public static Vector3I operator -(Vector3I a, VariableVector b)
        {
            return new Vector3I(a.X - b[0], a.Y - b[1], a.Z - b[2]);
        }
        public static Vector3I operator -(VariableVector a, Vector3I b)
        {
            return new Vector3I(a[0] - b.X, a[1] - b.Y, a[2] - b.Z);
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
        public static Vector3I operator *(Vector3I a, VariableVector b)
        {
            return new Vector3I(a.X * b[0], a.Y * b[1], a.Z * b[2]);
        }
        public static Vector3I operator *(VariableVector a, Vector3I b)
        {
            return new Vector3I(a[0] * b.X, a[1] * b.Y, a[2] * b.Z);
        }

        public static VariableVector operator *(Vector3I a, IMatrix b)
        {
            MatrixSpan ms = new MatrixSpan(b.Rows, b.Columns, stackalloc floatv[b.Rows * b.Columns]);
            b.MatrixData(ms);
            return a.MultiplyMatrix(ms);
        }
        public static VariableVector operator *(IMatrix a, Vector3I b)
        {
            MatrixSpan ms = new MatrixSpan(a.Rows, a.Columns, stackalloc floatv[a.Rows * a.Columns]);
            a.MatrixData(ms);
            return b.MultiplyMatrix(ms);
        }
        public static unsafe Vector2 operator *(Vector3I a, Matrix3x2 b)
        {
            return new Vector2(
                (a.X * b._matrix[0]) + (a.Y * b._matrix[2]) + (a.Z * b._matrix[4]),
                (a.X * b._matrix[1]) + (a.Y * b._matrix[3]) + (a.Z * b._matrix[5])
            );
        }
        public static unsafe Vector3 operator *(Vector3I a, Matrix3 b)
        {
            return new Vector3(
                (a.X * b._matrix[0]) + (a.Y * b._matrix[3]) + (a.Z * b._matrix[6]),
                (a.X * b._matrix[1]) + (a.Y * b._matrix[4]) + (a.Z * b._matrix[7]),
                (a.X * b._matrix[2]) + (a.Y * b._matrix[5]) + (a.Z * b._matrix[8])
            );
        }
        public static unsafe Vector4 operator *(Vector3I a, Matrix3x4 b)
        {
            return new Vector4(
                (a.X * b._matrix[0]) + (a.Y * b._matrix[4]) + (a.Z * b._matrix[8]),
                (a.X * b._matrix[1]) + (a.Y * b._matrix[5]) + (a.Z * b._matrix[9]),
                (a.X * b._matrix[2]) + (a.Y * b._matrix[6]) + (a.Z * b._matrix[10]),
                (a.X * b._matrix[3]) + (a.Y * b._matrix[7]) + (a.Z * b._matrix[11])
            );
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
        public static Vector3I operator /(Vector3I a, VariableVector b)
        {
            return new Vector3I(a.X / b[0], a.Y / b[1], a.Z / b[2]);
        }
        public static Vector3I operator /(VariableVector a, Vector3I b)
        {
            return new Vector3I(a[0] / b.X, a[1] / b.Y, a[2] / b.Z);
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
        public static Vector3I Zero { get; } = new Vector3I(0);
        /// <summary>
        /// A vector with <see cref="X"/>, <see cref="Y"/> and <see cref="Z"/> set to 1.
        /// </summary>
        public static Vector3I One { get; } = new Vector3I(1);

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

        public static Vector3 operator +(Vector3I a, Vector3 b)
        {
            return new Vector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }
        public static Vector3 operator -(Vector3I a, Vector3 b)
        {
            return new Vector3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }
        public static Vector3 operator *(Vector3I a, Vector3 b)
        {
            return new Vector3(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        }
        public static Vector3 operator /(Vector3I a, Vector3 b)
        {
            return new Vector3(a.X / b.X, a.Y / b.Y, a.Z / b.Z);
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
