using System;

namespace Zene.Structs
{
    /// <summary>
    /// An object that stores a 2 x 2 matrix grid of <see cref="double"/>. 
    /// </summary>
    public unsafe struct Matrix2
    {
        /// <summary>
        /// The number of rows in this matrix.
        /// </summary>
        public const int Rows = 2;
        /// <summary>
        /// The number of columns in this matrix.
        /// </summary>
        public const int Columns = 2;

        /// <summary>
        /// Creates a 2 x 2 matrix from its row values.
        /// </summary>
        /// <param name="row0">The first row of the matrix.</param>
        /// <param name="row1">The second row of the matrix.</param>
        public Matrix2(Vector2 row0, Vector2 row1)
        {
            Row0 = row0;
            Row1 = row1;
        }
        /// <summary>
        /// Creates a 2 x 2 matrix from an array of row major values.
        /// </summary>
        /// <param name="matrix">The values to store in the matrix.</param>
        public Matrix2(params double[] matrix)
        {
            if (matrix.Length < (Rows * Columns))
            {
                throw new Exception("Matrix needs to have at least 2 rows and 2 columns.");
            }

            _matrix[0] = matrix[0];
            _matrix[1] = matrix[1];
            _matrix[2] = matrix[2];
            _matrix[3] = matrix[3];
        }

        private fixed double _matrix[Rows * Columns];

        /// <summary>
        /// A span that points to the data in this matrix.
        /// </summary>
        public ReadOnlySpan<double> Data
        {
            get
            {
                ReadOnlySpan<double> value;

                fixed (double* ptr = _matrix)
                {
                    value = new ReadOnlySpan<double>(ptr, Rows * Columns);
                }

                return value;
            }
        }

        /// <summary>
        /// Gets a value from the matrix at a given location.
        /// </summary>
        /// <param name="x">The x value of the location.</param>
        /// <param name="y">The y value of the location.</param>
        /// <returns></returns>
        public double this[int x, int y]
        {
            get
            {
                if (x >= Columns || y >= Rows)
                {
                    throw new IndexOutOfRangeException($"X: {x} and Y: {y} are outside the {Columns} x {Rows} range of {nameof(Matrix2)}.");
                }

                return _matrix[x + (y * Rows)];
            }
            set
            {
                if (x >= Columns || y >= Rows)
                {
                    throw new IndexOutOfRangeException($"X: {x} and Y: {y} are outside the {Columns} x {Rows} range of {nameof(Matrix2)}.");
                }

                _matrix[x + (y * Rows)] = value;
            }
        }

        /// <summary>
        /// The first row of the matrix.
        /// </summary>
        public Vector2 Row0
        {
            get
            {
                return new Vector2(_matrix[0], _matrix[1]);
            }
            set
            {
                _matrix[0] = value.X;
                _matrix[1] = value.Y;
            }
        }
        /// <summary>
        /// The second row of the matrix.
        /// </summary>
        public Vector2 Row1
        {
            get
            {
                return new Vector2(_matrix[2], _matrix[3]);
            }
            set
            {
                _matrix[2] = value.X;
                _matrix[3] = value.Y;
            }
        }
        /// <summary>
        /// The first column of the matrix.
        /// </summary>
        public Vector2 Column0
        {
            get
            {
                return new Vector2(_matrix[0], _matrix[2]);
            }
            set
            {
                _matrix[0] = value.X;
                _matrix[2] = value.Y;
            }
        }
        /// <summary>
        /// The second column of the matrix.
        /// </summary>
        public Vector2 Column1
        {
            get
            {
                return new Vector2(_matrix[1], _matrix[3]);
            }
            set
            {
                _matrix[1] = value.X;
                _matrix[3] = value.Y;
            }
        }

        /// <summary>
        /// Returns this matrix added to another.
        /// </summary>
        /// <param name="matrix">The matrix to add to this.</param>
        /// <returns></returns>
        public Matrix2 Add(ref Matrix2 matrix)
        {
            return new Matrix2(
                Row0 + matrix.Row0,
                Row1 + matrix.Row1);
        }

        /// <summary>
        /// Returns this matrix subtracted from another.
        /// </summary>
        /// <param name="matrix">The matrix to subtract from this.</param>
        /// <returns></returns>
        public Matrix2 Subtract(ref Matrix2 matrix)
        {
            return new Matrix2(
                Row0 - matrix.Row0,
                Row1 - matrix.Row1);
        }

        /// <summary>
        /// Returns this matrix mutliplied by a <see cref="double"/> value.
        /// </summary>
        /// <param name="value">The value to multiply by.</param>
        /// <returns></returns>
        public Matrix2 Multiply(double value)
        {
            return new Matrix2(
                Row0 * value,
                Row1 * value);
        }

        /// <summary>
        /// Returns this matrix mutliplied by <paramref name="matrix"/>.
        /// </summary>
        /// <param name="matrix">The <see cref="Matrix2"/> to multiply by.</param>
        /// <returns></returns>
        public Matrix2 Multiply(ref Matrix2 matrix)
        {
            return new Matrix2(
                (/*x:0 y:0*/(_matrix[0] * matrix[0, 0]) + (_matrix[1] * matrix[0, 1]), /*x:1 y:0*/(_matrix[0] * matrix[1, 0]) + (_matrix[1] * matrix[1, 1])),
                (/*x:0 y:1*/(_matrix[2] * matrix[0, 0]) + (_matrix[3] * matrix[0, 1]), /*x:1 y:1*/(_matrix[2] * matrix[1, 0]) + (_matrix[3] * matrix[1, 1])));
        }

        /// <summary>
        /// Returns this matrix mutliplied by <paramref name="matrix"/>.
        /// </summary>
        /// <param name="matrix">The <see cref="Matrix2x3"/> to multiply by.</param>
        /// <returns></returns>
        public Matrix2x3 Multiply(ref Matrix2x3 matrix)
        {
            return new Matrix2x3(
                (
                    /*x:0 y:0*/(_matrix[0] * matrix[0, 0]) + (_matrix[1] * matrix[0, 1]),
                    /*x:1 y:0*/(_matrix[0] * matrix[1, 0]) + (_matrix[1] * matrix[1, 1]), 
                    /*x:2 y:0*/(_matrix[0] * matrix[2, 0]) + (_matrix[1] * matrix[2, 1])
                ),
                (
                    /*x:0 y:1*/(_matrix[2] * matrix[0, 0]) + (_matrix[3] * matrix[0, 1]),
                    /*x:1 y:1*/(_matrix[2] * matrix[1, 0]) + (_matrix[3] * matrix[1, 1]),
                    /*x:2 y:1*/(_matrix[2] * matrix[2, 0]) + (_matrix[3] * matrix[2, 1])
                ));
        }

        /// <summary>
        /// Returns this matrix mutliplied by <paramref name="matrix"/>.
        /// </summary>
        /// <param name="matrix">The <see cref="Matrix2x4"/> to multiply by.</param>
        /// <returns></returns>
        public Matrix2x4 Multiply(ref Matrix2x4 matrix)
        {
            return new Matrix2x4(
                (
                    /*x:0 y:0*/(_matrix[0] * matrix[0, 0]) + (_matrix[1] * matrix[0, 1]),
                    /*x:1 y:0*/(_matrix[0] * matrix[1, 0]) + (_matrix[1] * matrix[1, 1]),
                    /*x:2 y:0*/(_matrix[0] * matrix[2, 0]) + (_matrix[1] * matrix[2, 1]),
                    /*x:3 y:0*/(_matrix[0] * matrix[3, 0]) + (_matrix[1] * matrix[3, 1])
                ),
                (
                    /*x:0 y:1*/(_matrix[2] * matrix[0, 0]) + (_matrix[3] * matrix[0, 1]),
                    /*x:1 y:1*/(_matrix[2] * matrix[1, 0]) + (_matrix[3] * matrix[1, 1]),
                    /*x:2 y:1*/(_matrix[2] * matrix[2, 0]) + (_matrix[3] * matrix[2, 1]),
                    /*x:3 y:1*/(_matrix[2] * matrix[3, 0]) + (_matrix[3] * matrix[3, 1])
                ));
        }

        public double Determinant()
        {
            return (_matrix[0] * _matrix[3]) - (_matrix[1] * _matrix[2]);
        }

        public double Trace() => _matrix[0] + _matrix[3];

        /// <summary>
        /// Returns a normalised version of this matrix.
        /// </summary>
        /// <returns></returns>
        public Matrix2 Normalize()
        {
            double det = Determinant();

            return new Matrix2(Row0 / det, Row1 / det);
        }

        /// <summary>
        /// Returns an inverted version of this matrix.
        /// </summary>
        /// <returns></returns>
        public Matrix2 Invert()
        {
            double det = Determinant();

            if (det == 0)
            {
                throw new InvalidOperationException("Matrix is singular and cannot be inverted.");
            }

            double invDet = 1.0 / det;

            return new Matrix2(
                (_matrix[3] * invDet, _matrix[1] * invDet),
                (_matrix[2] * invDet, _matrix[0] * invDet));
        }

        /// <summary>
        /// Returns a transposed version of this matrix.
        /// </summary>
        /// <returns></returns>
        public Matrix2 Transpose() => new Matrix2(Column0, Column1);

        public override bool Equals(object obj)
        {
            return obj is Matrix2 matrix &&
                _matrix[0] == matrix._matrix[0] &&
                _matrix[1] == matrix._matrix[1] &&
                _matrix[2] == matrix._matrix[2] &&
                _matrix[3] == matrix._matrix[3];
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_matrix[0], _matrix[1], _matrix[2], _matrix[3]);
        }

        /// <summary>
        /// Gets the data used by OpenGL when parsing this matrix.
        /// </summary>
        /// <returns></returns>
        public float[] GetGLData()
        {
            return new float[]
            {
                (float)_matrix[0],
                (float)_matrix[1],
                (float)_matrix[2],
                (float)_matrix[3]
            };
        }

        public override string ToString()
        {
            return $@"[{_matrix[0]}, {_matrix[1]}]
[{_matrix[2]}, {_matrix[3]}]";
        }
        public string ToString(string format)
        {
            return $@"[{_matrix[0].ToString(format)}, {_matrix[1].ToString(format)}]
[{_matrix[2].ToString(format)}, {_matrix[3].ToString(format)}]";
        }

        public static bool operator ==(Matrix2 a, Matrix2 b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Matrix2 a, Matrix2 b)
        {
            return !a.Equals(b);
        }

        public static Matrix2 operator +(Matrix2 a, Matrix2 b)
        {
            return a.Add(ref b);
        }

        public static Matrix2 operator -(Matrix2 a, Matrix2 b)
        {
            return a.Subtract(ref b);
        }

        public static Matrix2 operator *(Matrix2 a, Matrix2 b)
        {
            return a.Multiply(ref b);
        }

        public static Matrix2x3 operator *(Matrix2 a, Matrix2x3 b)
        {
            return a.Multiply(ref b);
        }

        public static Matrix2x4 operator *(Matrix2 a, Matrix2x4 b)
        {
            return a.Multiply(ref b);
        }

        public static Matrix2 operator *(Matrix2 a, double b)
        {
            return a.Multiply(b);
        }

        public static Matrix2 operator *(double a, Matrix2 b)
        {
            return b.Multiply(a);
        }

        /// <summary>
        /// Creates a 2 x 2 matrix that stores rotational data.
        /// </summary>
        /// <param name="angle">The angle of the rotation.</param>
        /// <returns></returns>
        public static Matrix2 CreateRotation(Radian angle)
        {
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);

            return new Matrix2(new Vector2(cos, sin), new Vector2(-sin, cos));
        }

        /// <summary>
        /// Creates a 2 x 2 matrix that stores scale data.
        /// </summary>
        /// <param name="scale">The x and y scale.</param>
        /// <returns></returns>
        public static Matrix2 CreateScale(double scale)
        {
            return new Matrix2(new Vector2(scale, 0), new Vector2(0, scale));
        }

        /// <summary>
        /// Creates a 2 x 2 matrix that stores scale data.
        /// </summary>
        /// <param name="scaleX">The x scale.</param>
        /// <param name="scaleY">The y scale.</param>
        /// <returns></returns>
        public static Matrix2 CreateScale(double scaleX, double scaleY)
        {
            return new Matrix2(new Vector2(scaleX, 0), new Vector2(0, scaleY));
        }

        /// <summary>
        /// Creates a 2 x 2 matrix that stores scale data.
        /// </summary>
        /// <param name="scale">The x and y scale.</param>
        /// <returns></returns>
        public static Matrix2 CreateScale(Vector2 scale)
        {
            return CreateScale(scale.X, scale.Y);
        }

        public static implicit operator Matrix2(Matrix2<double> matrix)
        {
            return new Matrix2((Vector2)matrix.Row0, (Vector2)matrix.Row1);
        }
        public static explicit operator Matrix2(Matrix2<float> matrix)
        {
            return new Matrix2((Vector2)matrix.Row0, (Vector2)matrix.Row1);
        }

        public static implicit operator Matrix2<double>(Matrix2 matrix)
        {
            return new Matrix2<double>((Vector2<double>)matrix.Row0, (Vector2<double>)matrix.Row1);
        }
        public static explicit operator Matrix2<float>(Matrix2 matrix)
        {
            return new Matrix2<float>((Vector2<float>)matrix.Row0, (Vector2<float>)matrix.Row1);
        }
    }
}
