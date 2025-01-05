using System;

namespace Zene.Structs
{
    /// <summary>
    /// An object that stores a 2 x 2 matrix grid of <see cref="double"/>. 
    /// </summary>
    public unsafe struct Matrix2 : IMatrix
    {
        /// <summary>
        /// The number of rows in this matrix.
        /// </summary>
        public int Rows => 2;
        /// <summary>
        /// The number of columns in this matrix.
        /// </summary>
        public int Columns => 2;
        
        public bool Constant => true;
        
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
        public Matrix2(IMatrix matrix)
        {
            fixed (void* ptr = _matrix)
            {
                matrix.MatrixData(new MatrixSpan(2, 2, new Span<double>(ptr, 4)));
            }
        }

        internal fixed double _matrix[4];

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

                return _matrix[x + (y * Columns)];
            }
            set
            {
                if (x >= Columns || y >= Rows)
                {
                    throw new IndexOutOfRangeException($"X: {x} and Y: {y} are outside the {Columns} x {Rows} range of {nameof(Matrix2)}.");
                }

                _matrix[x + (y * Columns)] = value;
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

        public override int GetHashCode() => HashCode.Combine(_matrix[0], _matrix[1], _matrix[2], _matrix[3]);

        public void MatrixData(MatrixSpan ms)
        {
            fixed (void* ptr = _matrix)
            {
                Span<double> s = new Span<double>(ptr, 4);
                ms.Fill(s, 2, 2);
            }
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

        public static bool operator ==(Matrix2 a, Matrix2 b) => Equals(a, b);
        public static bool operator !=(Matrix2 a, Matrix2 b) => !Equals(a, b);

        public static MultiplyMatrix operator *(Matrix2 a, IMatrix b) => new MultiplyMatrix(a, b);
        
        public static Matrix2 operator *(Matrix2 a, double b)
        {
            Matrix2 m = new Matrix2();

            m._matrix[0] = a._matrix[0] * b;
            m._matrix[1] = a._matrix[1] * b;
            m._matrix[2] = a._matrix[2] * b;
            m._matrix[3] = a._matrix[3] * b;

            return m;
        }
        public static Matrix2 operator *(double b, Matrix2 a)
        {
            Matrix2 m = new Matrix2();

            m._matrix[0] = a._matrix[0] * b;
            m._matrix[1] = a._matrix[1] * b;
            m._matrix[2] = a._matrix[2] * b;
            m._matrix[3] = a._matrix[3] * b;

            return m;
        }
        
        public static Matrix2 operator +(Matrix2 a, Matrix2 b)
        {
            Matrix2 m = new Matrix2();

            m._matrix[0] = a._matrix[0] + b._matrix[0];
            m._matrix[1] = a._matrix[1] + b._matrix[1];
            m._matrix[2] = a._matrix[2] + b._matrix[2];
            m._matrix[3] = a._matrix[3] + b._matrix[3];

            return m;
        }
        public static Matrix2 operator -(Matrix2 a, Matrix2 b)
        {
            Matrix2 m = new Matrix2();

            m._matrix[0] = a._matrix[0] - b._matrix[0];
            m._matrix[1] = a._matrix[1] - b._matrix[1];
            m._matrix[2] = a._matrix[2] - b._matrix[2];
            m._matrix[3] = a._matrix[3] - b._matrix[3];

            return m;
        }
        
        public static Matrix2 operator *(Matrix2 a, Matrix2 b)
        {
            Matrix2 m = new Matrix2();

            m._matrix[0] = (a._matrix[0] * b._matrix[0]) + (a._matrix[1] * b._matrix[2]);
            m._matrix[1] = (a._matrix[0] * b._matrix[1]) + (a._matrix[1] * b._matrix[3]);
            
            m._matrix[2] = (a._matrix[2] * b._matrix[0]) + (a._matrix[3] * b._matrix[2]);
            m._matrix[3] = (a._matrix[2] * b._matrix[1]) + (a._matrix[3] * b._matrix[3]);

            return m;
        }

        public static Matrix2x3 operator *(Matrix2 a, Matrix2x3 b)
        {
            Matrix2x3 m = new Matrix2x3();

            m._matrix[0] = (a._matrix[0] * b._matrix[0]) + (a._matrix[1] * b._matrix[3]);
            m._matrix[1] = (a._matrix[0] * b._matrix[1]) + (a._matrix[1] * b._matrix[4]);
            m._matrix[2] = (a._matrix[0] * b._matrix[2]) + (a._matrix[1] * b._matrix[5]);

            m._matrix[3] = (a._matrix[2] * b._matrix[0]) + (a._matrix[3] * b._matrix[3]);
            m._matrix[4] = (a._matrix[2] * b._matrix[1]) + (a._matrix[3] * b._matrix[4]);
            m._matrix[5] = (a._matrix[2] * b._matrix[2]) + (a._matrix[3] * b._matrix[5]);

            return m;
        }

        public static Matrix2x4 operator *(Matrix2 a, Matrix2x4 b)
        {
            Matrix2x4 m = new Matrix2x4();

            m._matrix[0] = (a._matrix[0] * b._matrix[0]) + (a._matrix[1] * b._matrix[4]);
            m._matrix[1] = (a._matrix[0] * b._matrix[1]) + (a._matrix[1] * b._matrix[5]);
            m._matrix[2] = (a._matrix[0] * b._matrix[2]) + (a._matrix[1] * b._matrix[6]);
            m._matrix[3] = (a._matrix[0] * b._matrix[3]) + (a._matrix[1] * b._matrix[7]);
            
            m._matrix[4] = (a._matrix[2] * b._matrix[0]) + (a._matrix[3] * b._matrix[4]);
            m._matrix[5] = (a._matrix[2] * b._matrix[1]) + (a._matrix[3] * b._matrix[5]);
            m._matrix[6] = (a._matrix[2] * b._matrix[2]) + (a._matrix[3] * b._matrix[6]);
            m._matrix[7] = (a._matrix[2] * b._matrix[3]) + (a._matrix[3] * b._matrix[7]);

            return m;
        }
        
        private static Matrix2 _zero = new Matrix2(Vector2.Zero, Vector2.Zero);
        public static ref Matrix2 Zero => ref _zero;

        private static Matrix2 _identity = new Matrix2(new Vector2(1, 0), new Vector2(0, 1));
        public static ref Matrix2 Identity => ref _identity;
        
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
    }
}
