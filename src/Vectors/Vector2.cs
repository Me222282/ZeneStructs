﻿using System;

namespace Zene.Structs
{
    /// <summary>
    /// An object that stores a 2 dimensional <see cref="double"/> vector.
    /// </summary>
    public struct Vector2
    {
        /// <summary>
        /// Creates a 2 dimensional vector from a single <see cref="int"/>.
        /// </summary>
        /// <param name="value">The value to set to both <see cref="X"/> and <see cref="Y"/>.</param>
        public Vector2(int value)
        {
            X = value;
            Y = X;
        }
        /// <summary>
        /// Creates a 2 dimensional vector from a single <see cref="double"/>.
        /// </summary>
        /// <param name="value">The value to set to both <see cref="X"/> and <see cref="Y"/>.</param>
        public Vector2(double value)
        {
            X = value;
            Y = value;
        }
        /// <summary>
        /// Creates a 2 dimensional vector from two <see cref="int"/>.
        /// </summary>
        /// <param name="x">The value to set to <see cref="X"/>.</param>
        /// <param name="y">The value to set to <see cref="Y"/>.</param>
        public Vector2(int x, int y)
        {
            X = x;
            Y = y;
        }
        /// <summary>
        /// Creates a 2 dimensional vector from two <see cref="double"/>.
        /// </summary>
        /// <param name="x">The value to set to <see cref="X"/>.</param>
        /// <param name="y">The value to set to <see cref="Y"/>.</param>
        public Vector2(double x, double y)
        {
            X = x;
            Y = y;
        }
        /// <summary>
        /// Creates a 2 dimensional vector from an <see cref="int"/> based vector.
        /// </summary>
        /// <param name="xy">The vector to reference for <see cref="X"/> and <see cref="Y"/>.</param>
        public Vector2(Vector2I xy)
        {
            X = xy.X;
            Y = xy.Y;
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
        /// The length of the vector (distance from origin).
        /// </summary>
        public double Length
        {
            get
            {
                return Math.Sqrt((X * X) + (Y * Y));
            }
        }
        /// <summary>
        /// The squared length of the vector (distance from origin squared).
        /// </summary>
        public double SquaredLength
        {
            get
            {
                return (X * X) + (Y * Y);
            }
        }

        /// <summary>
        /// The distance from this vector to <paramref name="b"/>.
        /// </summary>
        /// <param name="b">The vector to reference.</param>
        /// <returns></returns>
        public double Distance(Vector2 b)
        {
            return Math.Sqrt(SquaredDistance(b));
        }
        /// <summary>
        /// The squared distance from this vector to <paramref name="b"/>.
        /// </summary>
        /// <param name="b">The vector to reference.</param>
        /// <returns></returns>
        public double SquaredDistance(Vector2 b)
        {
            return ((b.X - X) * (b.X - X)) + ((b.Y - Y) * (b.Y - Y));
        }

        /// <summary>
        /// The dot product of this vector to <paramref name="b"/>.
        /// </summary>
        /// <param name="b">The vector to reference.</param>
        /// <returns></returns>
        public double Dot(Vector2 b)
        {
            return (X * b.X) + (Y * b.Y);
        }
        /// <summary>
        /// The perpendicular dot product of this vector to <paramref name="b"/>.
        /// </summary>
        /// <param name="b">The vector to reference.</param>
        /// <returns></returns>
        public double PerpDot(Vector2 b)
        {
            return (X * b.Y) - (Y * b.X);
        }
        /// <summary>
        /// Determines whether this vector is parallel to <paramref name="b"/>.
        /// </summary>
        /// <param name="b">The vector to reference.</param>
        /// <returns></returns>
        public bool IsParallel(Vector2 b)
            => (this * this * b.SquaredLength) == (b * b * SquaredLength);

        /// <summary>
        /// A linear interpolation between this vector and <paramref name="b"/>.
        /// </summary>
        /// <param name="b">The vector to interpolate to.</param>
        /// <param name="blend">The percentage interpolation.</param>
        /// <returns></returns>
        public Vector2 Lerp(Vector2 b, double blend)
        {
            return new Vector2(
                (blend * (b.X - X)) + X,
                (blend * (b.Y - Y)) + Y);
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
        public Vector2 BaryCentric(Vector2 b, Vector2 c, double u, double v)
        {
            return (this + ((b - this) * u)) + ((c - this) * v);
        }

        /// <summary>
        /// Returns a normalised version of this vector.
        /// </summary>
        /// <returns></returns>
        public Vector2 Normalised()
        {
            double sl = SquaredLength;
            if (sl == 0d) { return Zero; }
            if (sl == 1d) { return this; }

            double scale = 1d / Math.Sqrt(sl);
            return new Vector2(X * scale, Y * scale);
        }
        /// <summary>
        /// Normalises this vector.
        /// </summary>
        /// <returns></returns>
        public void Normalise()
        {
            double sl = SquaredLength;
            if (sl == 1d || sl == 0d) { return; }

            double scale = 1d / Math.Sqrt(sl);
            X *= scale;
            Y *= scale;
        }

        /// <summary>
        /// Returns this vector as a <see cref="Vector2I"/> rounded to the nearest value.
        /// </summary>
        /// <returns></returns>
        public Vector2I RoundedInt() => new Vector2I(Math.Round(X), Math.Round(Y));

        /// <summary>
        /// Returns this vector multiplied by <paramref name="matrix"/>.
        /// </summary>
        /// <param name="matrix">The <see cref="MatrixSpan"/> to multiply by.</param>
        /// <returns></returns>
        public VariableVector MultiplyMatrix(MatrixSpan matrix)
        {
            if (matrix.Columns > 2)
            {
                throw new Exception();
            }

            double[] vs = new double[matrix.Rows];

            for (int i = 0; i < matrix.Rows; i++)
            {
                vs[i] = (matrix[0, i] * X) + (matrix[1, i] * Y);
            }

            return new VariableVector(vs);
        }
        
        /// <summary>
        /// Returns this vector rotated around <paramref name="point"/> by <paramref name="angle"/> radians.
        /// </summary>
        /// <param name="point">The point to rotate about.</param>
        /// <param name="angle">The angle of rotation.</param>
        /// <returns></returns>
        public Vector2 Rotated(Vector2 point, Radian angle)
        {
            double sin = Math.Sin(angle);
            double cos = Math.Cos(angle);

            // Translate to origin
            Vector2 newP = this - point;

            newP.X = (newP.X * cos) - (newP.Y * sin);
            newP.Y = (newP.X * sin) + (newP.Y * cos);

            return newP + point;
        }
        /// <summary>
        /// Returns this vector rotated around origin by <paramref name="angle"/> radians.
        /// </summary>
        /// <param name="angle">The angle of rotation.</param>
        /// <returns></returns>
        public Vector2 Rotated(Radian angle)
        {
            double sin = Math.Sin(angle);
            double cos = Math.Cos(angle);

            return new Vector2((X * cos) - (Y * sin), (X * sin) + (Y * cos));
        }
        /// <summary>
        /// Rotates this vector around <paramref name="point"/> by <paramref name="angle"/> radians.
        /// </summary>
        /// <param name="point">The point to rotate about.</param>
        /// <param name="angle">The angle of rotation.</param>
        /// <returns></returns>
        public void Rotate(Vector2 point, Radian angle)
        {
            double sin = Math.Sin(angle);
            double cos = Math.Cos(angle);

            // Translate to origin
            X -= point.X;
            Y -= point.Y;

            X = ((X * cos) - (Y * sin)) + point.X;
            Y = (X * sin) + (Y * cos) + point.Y;
        }
        /// <summary>
        /// Rotates this vector around origin by <paramref name="angle"/> radians.
        /// </summary>
        /// <param name="angle">The angle of rotation.</param>
        /// <returns></returns>
        public void Rotate(Radian angle)
        {
            double sin = Math.Sin(angle);
            double cos = Math.Cos(angle);

            X = (X * cos) - (Y * sin);
            Y = (X * sin) + (Y * cos);
        }
        /// <summary>
        /// Returns this vector rotated around origin by 90 degrees.
        /// </summary>
        /// <returns></returns>
        public Vector2 Rotated90()
        {
            return new Vector2(-Y, X);
        }
        /// <summary>
        /// Returns this vector rotated around origin by 270 degrees.
        /// </summary>
        /// <returns></returns>
        public Vector2 Rotated270()
        {
            return new Vector2(Y, -X);
        }
        /// <summary>
        /// Rotates this vector around origin by 90 degrees.
        /// </summary>
        /// <returns></returns>
        public void Rotate90()
        {
            double t = X;
            X = -Y;
            Y = t;
        }
        /// <summary>
        /// Rotates this vector around origin by 270 degrees.
        /// </summary>
        /// <returns></returns>
        public void Rotate270()
        {
            double t = X;
            X = Y;
            Y = -t;
        }

        /// <summary>
        /// Determines the smallest distance a point is from a line.
        /// </summary>
        /// <param name="l">The line to compare to.</param>
        public Vector2 Relative(Line2 l)
        {
            return new Vector2((X - l.GetX(Y)) / 2, (Y - l.GetY(X)) / 2);
        }
        
        public void Deconstruct(out double x, out double y)
        {
            x = X;
            y = Y;
        }
        
        public override string ToString()
        {
            return $"X:{X}, Y:{Y}";
        }
        public string ToString(string format)
        {
            return $"X:{X.ToString(format)}, Y:{Y.ToString(format)}";
        }

        public override bool Equals(object obj)
        {
            return
                (
                    obj is Vector2 p &&
                    X == p.X && Y == p.Y
                ) ||
                (
                    obj is Vector2I pi &&
                    X == pi.X && Y == pi.Y
                );
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public static bool operator ==(Vector2 a, Vector2 b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(Vector2 a, Vector2 b)
        {
            return !a.Equals(b);
        }
        public static bool operator ==(Vector2 a, Vector2I b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(Vector2 a, Vector2I b)
        {
            return !a.Equals(b);
        }
        /*
        public static Vector2 operator +(Vector2 a, double b)
        {
            return new Vector2(a.X + b, a.Y + b);
        }
        public static Vector2 operator +(double a, Vector2 b)
        {
            return b + a;
        }*/
        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X + b.X, a.Y + b.Y);
        }
        public static Vector2 operator +(Vector2 a, VariableVector b)
        {
            return new Vector2(a.X + b[0], a.Y + b[1]);
        }
        public static Vector2 operator +(VariableVector a, Vector2 b)
        {
            return new Vector2(a[0] + b.X, a[1] + b.Y);
        }
        /*
        public static Vector2 operator -(Vector2 a, double b)
        {
            return new Vector2(a.X - b, a.Y - b);
        }*/
        public static Vector2 operator -(Vector2 v)
        {
            return new Vector2(-v.X, -v.Y);
        }
        public static Vector2 operator -(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X - b.X, a.Y - b.Y);
        }
        public static Vector2 operator -(Vector2 a, VariableVector b)
        {
            return new Vector2(a.X - b[0], a.Y - b[1]);
        }
        public static Vector2 operator -(VariableVector a, Vector2 b)
        {
            return new Vector2(a[0] - b.X, a[1] - b.Y);
        }
        /*
        public static Vector2 operator *(Vector2 a, double b)
        {
            return new Vector2(a.X * b, a.Y * b);
        }
        public static Vector2 operator *(double a, Vector2 b)
        {
            return b * a;
        }*/
        public static Vector2 operator *(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X * b.X, a.Y * b.Y);
        }
        public static Vector2 operator *(Vector2 a, VariableVector b)
        {
            return new Vector2(a.X * b[0], a.Y * b[1]);
        }
        public static Vector2 operator *(VariableVector a, Vector2 b)
        {
            return new Vector2(a[0] * b.X, a[1] * b.Y);
        }

        public static VariableVector operator *(Vector2 a, IMatrix b)
        {
            return a.MultiplyMatrix(b.MatrixData());
        }
        public static VariableVector operator *(IMatrix a, Vector2 b)
        {
            return b.MultiplyMatrix(a.MatrixData());
        }
        /*
        public static Vector2 operator /(Vector2 a, double b)
        {
            return new Vector2(a.X / b, a.Y / b);
        }*/
        public static Vector2 operator /(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X / b.X, a.Y / b.Y);
        }
        public static Vector2 operator /(Vector2 a, VariableVector b)
        {
            return new Vector2(a.X / b[0], a.Y / b[1]);
        }
        public static Vector2 operator /(VariableVector a, Vector2 b)
        {
            return new Vector2(a[0] / b.X, a[1] / b.Y);
        }

        public static explicit operator Vector2(Vector3 p)
        {
            return new Vector2(p.X, p.Y);
        }
        public static explicit operator Vector2(Vector4 p)
        {
            return new Vector2(p.X, p.Y);
        }
        public static explicit operator Vector2(Vector3I p)
        {
            return new Vector2(p.X, p.Y);
        }
        public static explicit operator Vector2(Vector4I p)
        {
            return new Vector2(p.X, p.Y);
        }
        public static implicit operator Vector2(Vector2I p)
        {
            return new Vector2(p);
        }

        /// <summary>
        /// A vector with both <see cref="X"/> and <see cref="Y"/> set to 0.
        /// </summary>
        public static Vector2 Zero { get; } = new Vector2(0, 0);
        /// <summary>
        /// A vector with both <see cref="X"/> and <see cref="Y"/> set to 1.
        /// </summary>
        public static Vector2 One { get; } = new Vector2(1, 1);
        /// <summary>
        /// A vector with both <see cref="X"/> and <see cref="Y"/> set to <see cref="double.PositiveInfinity"/>.
        /// </summary>
        public static Vector2 PositiveInfinity { get; } = new Vector2(double.PositiveInfinity, double.PositiveInfinity);
        /// <summary>
        /// A vector with both <see cref="X"/> and <see cref="Y"/> set to <see cref="double.NegativeInfinity"/>.
        /// </summary>
        public static Vector2 NegativeInfinity { get; } = new Vector2(double.NegativeInfinity, double.NegativeInfinity);

        public static Vector2 operator +(Vector2 a, Vector2I b)
        {
            return new Vector2(a.X + b.X, a.Y + b.Y);
        }
        public static Vector2 operator -(Vector2 a, Vector2I b)
        {
            return new Vector2(a.X - b.X, a.Y - b.Y);
        }
        public static Vector2 operator /(Vector2 a, Vector2I b)
        {
            return new Vector2(a.X / b.X, a.Y / b.Y);
        }
        public static Vector2 operator *(Vector2 a, Vector2I b)
        {
            return new Vector2(a.X * b.X, a.Y * b.Y);
        }

        public static implicit operator Vector2((double, double) v) => new Vector2(v.Item1, v.Item2);
        public static implicit operator Vector2(double v) => new Vector2(v);
    }
}
