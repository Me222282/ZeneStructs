using System;

namespace Zene.Structs
{
    /// <summary>
    /// An object that stores a 3 dimensional <see cref="floatv"/> vector.
    /// </summary>
    public struct Vector3
    {
        /// <summary>
        /// Creates a 3 dimensional vector from a single <see cref="double"/>.
        /// </summary>
        /// <param name="value">The value to set to <see cref="X"/>, <see cref="Y"/> and <see cref="Z"/>.</param>
        public Vector3(double value)
        {
            X = (floatv)value;
            Y = X;
            Z = X;
        }
        /// <summary>
        /// Creates a 3 dimensional vector from a single <see cref="float"/>.
        /// </summary>
        /// <param name="value">The value to set to <see cref="X"/>, <see cref="Y"/> and <see cref="Z"/>.</param>
        public Vector3(float value)
        {
            X = value;
            Y = X;
            Z = X;
        }
        /// <summary>
        /// Creates a 3 dimensional vector from a single <see cref="int"/>.
        /// </summary>
        /// <param name="value">The value to set to <see cref="X"/>, <see cref="Y"/> and <see cref="Z"/>.</param>
        public Vector3(int value)
        {
            X = value;
            Y = X;
            Z = X;
        }
        /// <summary>
        /// Creates a 3 dimensional vector from three <see cref="double"/>.
        /// </summary>
        /// <param name="x">The value to set to <see cref="X"/>.</param>
        /// <param name="y">The value to set to <see cref="Y"/>.</param>
        /// <param name="z">The value to set to <see cref="Z"/>.</param>
        public Vector3(double x, double y, double z)
        {
            X = (floatv)x;
            Y = (floatv)y;
            Z = (floatv)z;
        }
        /// <summary>
        /// Creates a 3 dimensional vector from three <see cref="float"/>.
        /// </summary>
        /// <param name="x">The value to set to <see cref="X"/>.</param>
        /// <param name="y">The value to set to <see cref="Y"/>.</param>
        /// <param name="z">The value to set to <see cref="Z"/>.</param>
        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        /// <summary>
        /// Creates a 3 dimensional vector from three <see cref="int"/>.
        /// </summary>
        /// <param name="x">The value to set to <see cref="X"/>.</param>
        /// <param name="y">The value to set to <see cref="Y"/>.</param>
        /// <param name="z">The value to set to <see cref="Z"/>.</param>
        public Vector3(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        /// <summary>
        /// Creates a 3 dimensional vector from an <see cref="int"/> based vector.
        /// </summary>
        /// <param name="xyz">The vector to reference for <see cref="X"/>, <see cref="Y"/> and <see cref="Z"/>.</param>
        public Vector3(Vector3I xyz)
        {
            X = xyz.X;
            Y = xyz.Y;
            Z = xyz.Z;
        }
        /// <summary>
        /// Creates a 3 dimensional vector from a 2 dimensional vector and a <see cref="floatv"/> Z.
        /// </summary>
        /// <param name="xy">The vector to reference for <see cref="X"/> and <see cref="Y"/>.</param>
        /// <param name="z">The value to set to <see cref="Z"/>.</param>
        public Vector3(Vector2 xy, floatv z)
        {
            X = xy.X;
            Y = xy.Y;
            Z = z;
        }
        /// <summary>
        /// Creates a 3 dimensional vector from a 2 dimensional vector and a <see cref="floatv"/> X.
        /// </summary>
        /// <param name="x">The value to set to <see cref="X"/>.</param>
        /// <param name="yz">The vector to reference for <see cref="Y"/> and <see cref="Z"/>.</param>
        public Vector3(floatv x, Vector2 yz)
        {
            X = x;
            Y = yz.X;
            Z = yz.Y;
        }
        /// <summary>
        /// Creates a 3 dimensional vector from an <see cref="int"/> based 2 dimensional vector and a <see cref="floatv"/> Z.
        /// </summary>
        /// <param name="xy">The vector to reference for <see cref="X"/> and <see cref="Y"/>.</param>
        /// <param name="z">The value to set to <see cref="Z"/>.</param>
        public Vector3(Vector2I xy, floatv z)
        {
            X = xy.X;
            Y = xy.Y;
            Z = z;
        }
        /// <summary>
        /// Creates a 3 dimensional vector from an <see cref="int"/> based 2 dimensional vector and a <see cref="floatv"/> X.
        /// </summary>
        /// <param name="x">The value to set to <see cref="X"/>.</param>
        /// <param name="yz">The vector to reference for <see cref="Y"/> and <see cref="Z"/>.</param>
        public Vector3(floatv x, Vector2I yz)
        {
            X = x;
            Y = yz.X;
            Z = yz.Y;
        }

        /// <summary>
        /// The first value of the vector.
        /// </summary>
        public floatv X { get; set; }
        /// <summary>
        /// The second value of the vector.
        /// </summary>
        public floatv Y { get; set; }
        /// <summary>
        /// The third value of the vector.
        /// </summary>
        public floatv Z { get; set; }

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
        public floatv SquaredLength
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
        public floatv Distance(Vector3 b)
        {
            return Maths.Sqrt(SquaredDistance(b));
        }
        /// <summary>
        /// The squared distance from this vector to <paramref name="b"/>.
        /// </summary>
        /// <param name="b">The vector to reference.</param>
        /// <returns></returns>
        public floatv SquaredDistance(Vector3 b)
        {
            return ((b.X - X) * (b.X - X)) + ((b.Y - Y) * (b.Y - Y)) + ((b.Z - Z) * (b.Z - Z));
        }

        /// <summary>
        /// The dot product of this vector to <paramref name="b"/>.
        /// </summary>
        /// <param name="b">The vector to reference.</param>
        /// <returns></returns>
        public floatv Dot(Vector3 b)
        {
            return (X * b.X) + (Y * b.Y) + (Z * b.Z);
        }

        /// <summary>
        /// The cross product of this vector to <paramref name="b"/>.
        /// </summary>
        /// <param name="b">The vector to reference.</param>
        /// <returns></returns>
        public Vector3 Cross(Vector3 b)
        {
            return new Vector3(
                (Y * b.Z) - (Z * b.Y),
                (Z * b.X) - (X * b.Z),
                (X * b.Y) - (Y * b.X));
        }

        /// <summary>
        /// Determines whether this vector is parallel to <paramref name="b"/>.
        /// </summary>
        /// <param name="b">The vector to reference.</param>
        /// <returns></returns>
        public bool IsParallel(Vector3 b)
            => (this * this * b.SquaredLength) == (b * b * SquaredLength);

        /// <summary>
        /// A linear interpolation between this vector and <paramref name="b"/>.
        /// </summary>
        /// <param name="b">The vector to interpolate to.</param>
        /// <param name="blend">The percentage interpolation.</param>
        /// <returns></returns>
        public Vector3 Lerp(Vector3 b, floatv blend)
        {
            return new Vector3(
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
        public Vector3 BaryCentric(Vector3 b, Vector3 c, floatv u, floatv v)
        {
            return this + ((b - this) * u) + ((c - this) * v);
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
        /// Normalises this vector.
        /// </summary>
        /// <returns></returns>
        public void Normalise()
        {
            floatv sl = SquaredLength;
            if (sl == 1 || sl == 0) { return; }

            floatv scale = 1 / Maths.Sqrt(sl);
            X *= scale;
            Y *= scale;
            Z *= scale;
        }

        /// <summary>
        /// Returns a vector with each component the absolute of this.
        /// </summary>
        /// <returns></returns>
        public Vector3 Abs() => new Vector3(Math.Abs(X), Math.Abs(Y), Math.Abs(Z));

        /// <summary>
        /// Returns this vector as a <see cref="Vector3I"/> rounded to the nearest value.
        /// </summary>
        /// <returns></returns>
        public Vector3I RoundedInt() => new Vector3I(Maths.Round(X), Maths.Round(Y), Maths.Round(Z));

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
        
        public void Deconstruct(out floatv x, out floatv y, out floatv z)
        {
            x = X;
            y = Y;
            z = Z;
        }
        public void Deconstruct(out floatv x, out Vector2 yz)
        {
            x = X;
            yz = new Vector2(Y, Z);
        }
        public void Deconstruct(out Vector2 xy, out floatv z)
        {
            xy = new Vector2(X, Y);
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
                    obj is Vector3 p && this == p
                ) ||
                (
                    obj is Vector3I pi && this == pi
                );
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Z);
        }

        public static bool operator ==(Vector3 a, Vector3 b)
        {
            return a.X == b.X && a.Y == b.Y && a.Z == b.Z;
        }
        public static bool operator !=(Vector3 a, Vector3 b) => !(a == b);
        public static bool operator ==(Vector3 a, Vector3I b)
        {
            return a.X == b.X && a.Y == b.Y && a.Z == b.Z;
        }
        public static bool operator !=(Vector3 a, Vector3I b) => !(a == b);

        /*
        public static Vector3 operator +(Vector3 a, double b)
        {
            return new Vector3(a.X + b, a.Y + b, a.Z + b);
        }
        public static Vector3 operator +(double a, Vector3 b)
        {
            return b + a;
        }*/
        public static Vector3 operator +(Vector3 a, Vector3 b)
        {
            return new Vector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }
        public static Vector3 operator +(Vector3 a, VariableVector b)
        {
            return new Vector3(a.X + b[0], a.Y + b[1], a.Z + b[2]);
        }
        public static Vector3 operator +(VariableVector a, Vector3 b)
        {
            return new Vector3(a[0] + b.X, a[1] + b.Y, a[2] + b.Z);
        }

        /*
        public static Vector3 operator -(Vector3 a, double b)
        {
            return new Vector3(a.X - b, a.Y - b, a.Z - b);
        }*/
        public static Vector3 operator -(Vector3 v)
        {
            return new Vector3(-v.X, -v.Y, -v.Z);
        }
        public static Vector3 operator -(Vector3 a, Vector3 b)
        {
            return new Vector3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }
        public static Vector3 operator -(Vector3 a, VariableVector b)
        {
            return new Vector3(a.X - b[0], a.Y - b[1], a.Z - b[2]);
        }
        public static Vector3 operator -(VariableVector a, Vector3 b)
        {
            return new Vector3(a[0] - b.X, a[1] - b.Y, a[2] - b.Z);
        }

        /*
        public static Vector3 operator *(Vector3 a, double b)
        {
            return new Vector3(a.X * b, a.Y * b, a.Z * b);
        }
        public static Vector3 operator *(double a, Vector3 b)
        {
            return b * a;
        }*/
        public static Vector3 operator *(Vector3 a, Vector3 b)
        {
            return new Vector3(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        }
        public static Vector3 operator *(Vector3 a, VariableVector b)
        {
            return new Vector3(a.X * b[0], a.Y * b[1], a.Z * b[2]);
        }
        public static Vector3 operator *(VariableVector a, Vector3 b)
        {
            return new Vector3(a[0] * b.X, a[1] * b.Y, a[2] * b.Z);
        }

        public static VariableVector operator *(Vector3 a, IMatrix b)
        {
            MatrixSpan ms = new MatrixSpan(b.Rows, b.Columns, stackalloc floatv[b.Rows * b.Columns]);
            b.MatrixData(ms);
            return a.MultiplyMatrix(ms);
        }
        public static VariableVector operator *(IMatrix a, Vector3 b)
        {
            MatrixSpan ms = new MatrixSpan(a.Rows, a.Columns, stackalloc floatv[a.Rows * a.Columns]);
            a.MatrixData(ms);
            return b.MultiplyMatrix(ms);
        }
        public static unsafe Vector2 operator *(Vector3 a, Matrix3x2 b)
        {
            return new Vector2(
                (a.X * b._matrix[0]) + (a.Y * b._matrix[2]) + (a.Z * b._matrix[4]),
                (a.X * b._matrix[1]) + (a.Y * b._matrix[3]) + (a.Z * b._matrix[5])
            );
        }
        public static unsafe Vector3 operator *(Vector3 a, Matrix3 b)
        {
            return new Vector3(
                (a.X * b._matrix[0]) + (a.Y * b._matrix[3]) + (a.Z * b._matrix[6]),
                (a.X * b._matrix[1]) + (a.Y * b._matrix[4]) + (a.Z * b._matrix[7]),
                (a.X * b._matrix[2]) + (a.Y * b._matrix[5]) + (a.Z * b._matrix[8])
            );
        }
        public static unsafe Vector4 operator *(Vector3 a, Matrix3x4 b)
        {
            return new Vector4(
                (a.X * b._matrix[0]) + (a.Y * b._matrix[4]) + (a.Z * b._matrix[8]),
                (a.X * b._matrix[1]) + (a.Y * b._matrix[5]) + (a.Z * b._matrix[9]),
                (a.X * b._matrix[2]) + (a.Y * b._matrix[6]) + (a.Z * b._matrix[10]),
                (a.X * b._matrix[3]) + (a.Y * b._matrix[7]) + (a.Z * b._matrix[11])
            );
        }

        /*
        public static Vector3 operator /(Vector3 a, double b)
        {
            return new Vector3(a.X / b, a.Y / b, a.Z / b);
        }*/
        public static Vector3 operator /(Vector3 a, Vector3 b)
        {
            return new Vector3(a.X / b.X, a.Y / b.Y, a.Z / b.Z);
        }
        public static Vector3 operator /(Vector3 a, VariableVector b)
        {
            return new Vector3(a.X / b[0], a.Y / b[1], a.Z / b[2]);
        }
        public static Vector3 operator /(VariableVector a, Vector3 b)
        {
            return new Vector3(a[0] / b.X, a[1] / b.Y, a[2] / b.Z);
        }

        public static implicit operator Vector3(Vector2 p)
        {
            return new Vector3(p, 0);
        }
        public static explicit operator Vector3(Vector4 p)
        {
            return new Vector3(p.X, p.Y, p.Z);
        }
        public static implicit operator Vector3(Vector2I p)
        {
            return new Vector3(p, 0);
        }
        public static explicit operator Vector3(Vector4I p)
        {
            return new Vector3(p.X, p.Y, p.Z);
        }
        public static implicit operator Vector3(Vector3I p)
        {
            return new Vector3(p);
        }

        /// <summary>
        /// A vector with <see cref="X"/>, <see cref="Y"/> and <see cref="Z"/> set to 0.
        /// </summary>
        public static Vector3 Zero { get; } = new Vector3(0);
        /// <summary>
        /// A vector with <see cref="X"/>, <see cref="Y"/> and <see cref="Z"/> set to 1.
        /// </summary>
        public static Vector3 One { get; } = new Vector3(1);
        /// <summary>
        /// A vector with <see cref="X"/>, <see cref="Y"/> and <see cref="Z"/> set to <see cref="floatv.PositiveInfinity"/>.
        /// </summary>
        public static Vector3 PositiveInfinity { get; } = new Vector3(floatv.PositiveInfinity);
        /// <summary>
        /// A vector with <see cref="X"/>, <see cref="Y"/> and <see cref="Z"/> set to <see cref="floatv.NegativeInfinity"/>.
        /// </summary>
        public static Vector3 NegativeInfinity { get; } = new Vector3(floatv.NegativeInfinity);

        /// <summary>
        /// Returns the direction perpendicular to the plane defined by <paramref name="a"/>, <paramref name="b"/>, <paramref name="c"/>.
        /// </summary>
        /// <param name="a">The first point on the plane.</param>
        /// <param name="b">The second point on the plane.</param>
        /// <param name="c">The third point on the plane.</param>
        /// <returns>A normalised vector representing the direction perpendicular to the plane.</returns>
        public static Vector3 PlaneNormal(Vector3 a, Vector3 b, Vector3 c)
        {
            return (b - a).Cross(c - a).Normalised();
        }

        public static Vector3 operator +(Vector3 a, Vector2 b)
        {
            return new Vector3(a.X + b.X, a.Y + b.Y, a.Z);
        }
        public static Vector3 operator +(Vector3 a, Vector2I b)
        {
            return new Vector3(a.X + b.X, a.Y + b.Y, a.Z);
        }
        public static Vector3 operator -(Vector3 a, Vector2 b)
        {
            return new Vector3(a.X - b.X, a.Y - b.Y, a.Z);
        }
        public static Vector3 operator -(Vector3 a, Vector2I b)
        {
            return new Vector3(a.X - b.X, a.Y - b.Y, a.Z);
        }

        //public static Vector3 operator +(Vector3I a, Vector3 b)
        //{
        //    return new Vector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        //}
        //public static Vector3 operator -(Vector3I a, Vector3 b)
        //{
        //    return new Vector3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        //}
        //public static Vector3 operator *(Vector3I a, Vector3 b)
        //{
        //    return new Vector3(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        //}
        //public static Vector3 operator /(Vector3I a, Vector3 b)
        //{
        //    return new Vector3(a.X / b.X, a.Y / b.Y, a.Z / b.Z);
        //}

        public static implicit operator Vector3((double, double, double) v)
        {
            return new Vector3(v.Item1, v.Item2, v.Item3);
        }
        public static implicit operator Vector3((float, float, float) v)
        {
            return new Vector3(v.Item1, v.Item2, v.Item3);
        }
        public static implicit operator Vector3((Vector2, floatv) v) => new Vector3(v.Item1, v.Item2);
        public static implicit operator Vector3((floatv, Vector2) v) => new Vector3(v.Item1, v.Item2);
        public static implicit operator Vector3((Vector2I, floatv) v) => new Vector3(v.Item1, v.Item2);
        public static implicit operator Vector3((floatv, Vector2I) v) => new Vector3(v.Item1, v.Item2);
        public static implicit operator Vector3(double v) => new Vector3(v);
        public static implicit operator Vector3(float v) => new Vector3(v);
    }
}
