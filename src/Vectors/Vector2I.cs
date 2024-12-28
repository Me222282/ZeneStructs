using System;

namespace Zene.Structs
{
    /// <summary>
    /// An object that stores a 2 dimensional <see cref="int"/> vector.
    /// </summary>
    public struct Vector2I
    {
        /// <summary>
        /// Creates a 2 dimensional vector from a single <see cref="int"/>.
        /// </summary>
        /// <param name="value">The value to set to both <see cref="X"/> and <see cref="Y"/>.</param>
        public Vector2I(int value)
        {
            X = value;
            Y = value;
        }
        /// <summary>
        /// Creates a 2 dimensional vector from a single <see cref="double"/>.
        /// </summary>
        /// <param name="value">The value to set to both <see cref="X"/> and <see cref="Y"/>.</param>
        public Vector2I(double value)
        {
            X = (int)value;
            Y = X;
        }
        /// <summary>
        /// Creates a 2 dimensional vector from two <see cref="int"/>.
        /// </summary>
        /// <param name="x">The value to set to <see cref="X"/>.</param>
        /// <param name="y">The value to set to <see cref="Y"/>.</param>
        public Vector2I(int x, int y)
        {
            X = x;
            Y = y;
        }
        /// <summary>
        /// Creates a 2 dimensional vector from two <see cref="double"/>.
        /// </summary>
        /// <param name="x">The value to set to <see cref="X"/>.</param>
        /// <param name="y">The value to set to <see cref="Y"/>.</param>
        public Vector2I(double x, double y)
        {
            X = (int)x;
            Y = (int)y;
        }
        /// <summary>
        /// Creates a 2 dimensional vector from an <see cref="double"/> based vector.
        /// </summary>
        /// <param name="xy">The vector to reference for <see cref="X"/> and <see cref="Y"/>.</param>
        public Vector2I(Vector2 xy)
        {
            X = (int)xy.X;
            Y = (int)xy.Y;
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
        public int SquaredLength
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
        public double Distance(Vector2I b)
        {
            return Math.Sqrt(SquaredDistance(b));
        }
        /// <summary>
        /// The squared distance from this vector to <paramref name="b"/>.
        /// </summary>
        /// <param name="b">The vector to reference.</param>
        /// <returns></returns>
        public int SquaredDistance(Vector2I b)
        {
            return ((b.X - X) * (b.X - X)) + ((b.Y - Y) * (b.Y - Y));
        }

        /// <summary>
        /// The dot product of this vector to <paramref name="b"/>.
        /// </summary>
        /// <param name="b">The vector to reference.</param>
        /// <returns></returns>
        public int Dot(Vector2I b)
        {
            return (X * b.X) + (Y * b.Y);
        }
        /// <summary>
        /// The perpendicular dot product of this vector to <paramref name="b"/>.
        /// </summary>
        /// <param name="b">The vector to reference.</param>
        /// <returns></returns>
        public int PerpDot(Vector2I b)
        {
            return (X * b.Y) - (Y * b.X);
        }

        /// <summary>
        /// A linear interpolation between this vector and <paramref name="b"/>.
        /// </summary>
        /// <param name="b">The vector to interpolate to.</param>
        /// <param name="blend">The percentage interpolation.</param>
        /// <returns></returns>
        public Vector2I Lerp(Vector2I b, double blend)
        {
            return new Vector2I(
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
        public Vector2I BaryCentric(Vector2I b, Vector2I c, int u, int v)
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
            if (sl == 0d) { return Vector2.Zero; }
            if (sl == 1d) { return this; }

            double scale = 1d / Math.Sqrt(sl);
            return new Vector2(X * scale, Y * scale);
        }

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
        /// Returns this vector rotated around origin by 90 degrees.
        /// </summary>
        /// <returns></returns>
        public Vector2I Rotated90()
        {
            return new Vector2I(-Y, X);
        }
        /// <summary>
        /// Returns this vector rotated around origin by 270 degrees.
        /// </summary>
        /// <returns></returns>
        public Vector2I Rotated270()
        {
            return new Vector2I(Y, -X);
        }
        /// <summary>
        /// Rotates this vector around origin by 90 degrees.
        /// </summary>
        /// <returns></returns>
        public void Rotate90()
        {
            int t = X;
            X = -Y;
            Y = t;
        }
        /// <summary>
        /// Rotates this vector around origin by 270 degrees.
        /// </summary>
        /// <returns></returns>
        public void Rotate270()
        {
            int t = X;
            X = Y;
            Y = -t;
        }
        
        public void Deconstruct(out int x, out int y)
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
                    obj is Vector2I p &&
                    X == p.X && Y == p.Y
                ) ||
                (
                    obj is Vector2 pd &&
                    X == pd.X && Y == pd.Y
                );
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public static bool operator ==(Vector2I a, Vector2I b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(Vector2I a, Vector2I b)
        {
            return !a.Equals(b);
        }
        public static bool operator ==(Vector2I a, Vector2 b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(Vector2I a, Vector2 b)
        {
            return !a.Equals(b);
        }

        /*
        public static Vector2I operator +(Vector2I a, int b)
        {
            return new Vector2I(a.X + b, a.Y + b);
        }
        public static Vector2I operator +(int a, Vector2I b)
        {
            return b + a;
        }*/
        public static Vector2I operator +(Vector2I a, Vector2I b)
        {
            return new Vector2I(a.X + b.X, a.Y + b.Y);
        }
        public static Vector2I operator +(Vector2I a, VariableVector b)
        {
            return new Vector2I(a.X + b[0], a.Y + b[1]);
        }
        public static Vector2I operator +(VariableVector a, Vector2I b)
        {
            return new Vector2I(a[0] + b.X, a[1] + b.Y);
        }

        /*
        public static Vector2I operator -(Vector2I a, int b)
        {
            return new Vector2I(a.X - b, a.Y - b);
        }*/
        public static Vector2I operator -(Vector2I v)
        {
            return new Vector2I(-v.X, -v.Y);
        }
        public static Vector2I operator -(Vector2I a, Vector2I b)
        {
            return new Vector2I(a.X - b.X, a.Y - b.Y);
        }
        public static Vector2I operator -(Vector2I a, VariableVector b)
        {
            return new Vector2I(a.X - b[0], a.Y - b[1]);
        }
        public static Vector2I operator -(VariableVector a, Vector2I b)
        {
            return new Vector2I(a[0] - b.X, a[1] - b.Y);
        }

        /*
        public static Vector2I operator *(Vector2I a, int b)
        {
            return new Vector2I(a.X * b, a.Y * b);
        }
        public static Vector2I operator *(int a, Vector2I b)
        {
            return b * a;
        }*/
        public static Vector2I operator *(Vector2I a, Vector2I b)
        {
            return new Vector2I(a.X * b.X, a.Y * b.Y);
        }
        public static Vector2I operator *(Vector2I a, VariableVector b)
        {
            return new Vector2I(a.X * b[0], a.Y * b[1]);
        }
        public static Vector2I operator *(VariableVector a, Vector2I b)
        {
            return new Vector2I(a[0] * b.X, a[1] * b.Y);
        }

        public static VariableVector operator *(Vector2I a, IMatrix b)
        {
            return a.MultiplyMatrix(b.MatrixData());
        }
        public static VariableVector operator *(IMatrix a, Vector2I b)
        {
            return b.MultiplyMatrix(a.MatrixData());
        }

        /*
        public static Vector2I operator /(Vector2I a, int b)
        {
            return new Vector2I(a.X / b, a.Y / b);
        }*/
        public static Vector2I operator /(Vector2I a, Vector2I b)
        {
            return new Vector2I(a.X / b.X, a.Y / b.Y);
        }
        public static Vector2I operator /(Vector2I a, VariableVector b)
        {
            return new Vector2I(a.X / b[0], a.Y / b[1]);
        }
        public static Vector2I operator /(VariableVector a, Vector2I b)
        {
            return new Vector2I(a[0] / b.X, a[1] / b.Y);
        }

        public static explicit operator Vector2I(Vector3 p)
        {
            return new Vector2I(p.X, p.Y);
        }
        public static explicit operator Vector2I(Vector4 p)
        {
            return new Vector2I(p.X, p.Y);
        }
        public static explicit operator Vector2I(Vector3I p)
        {
            return new Vector2I(p.X, p.Y);
        }
        public static explicit operator Vector2I(Vector4I p)
        {
            return new Vector2I(p.X, p.Y);
        }
        public static explicit operator Vector2I(Vector2 p)
        {
            return new Vector2I(p);
        }

        /// <summary>
        /// A vector with both <see cref="X"/> and <see cref="Y"/> set to 0.
        /// </summary>
        public static Vector2I Zero { get; } = new Vector2I(0, 0);
        /// <summary>
        /// A vector with both <see cref="X"/> and <see cref="Y"/> set to 1.
        /// </summary>
        public static Vector2I One { get; } = new Vector2I(1, 1);

        public static Vector2 operator +(Vector2I a, Vector2 b)
        {
            return new Vector2(a.X + b.X, a.Y + b.Y);
        }
        public static Vector2 operator -(Vector2I a, Vector2 b)
        {
            return new Vector2(a.X - b.X, a.Y - b.Y);
        }
        public static Vector2 operator /(Vector2I a, Vector2 b)
        {
            return new Vector2(a.X / b.X, a.Y / b.Y);
        }
        public static Vector2 operator *(Vector2I a, Vector2 b)
        {
            return new Vector2(a.X * b.X, a.Y * b.Y);
        }

        public static implicit operator Vector2I((int, int) v) => new Vector2I(v.Item1, v.Item2);
        public static implicit operator Vector2I(int v) => new Vector2I(v);
    }
}
