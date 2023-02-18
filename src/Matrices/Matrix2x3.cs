using System;

namespace Zene.Structs
{
    public unsafe class Matrix2x3
    {
        public const int Rows = 2;
        public const int Columns = 3;

        public Matrix2x3(Vector3 row0, Vector3 row1)
        {
            Row0 = row0;
            Row1 = row1;
        }

        public Matrix2x3(double[] matrix)
        {
            if (matrix.Length < (Rows * Columns))
            {
                throw new Exception("Matrix needs to have at least 2 rows and 3 columns.");
            }

            _matrix[0] = matrix[0];
            _matrix[1] = matrix[1];
            _matrix[2] = matrix[2];
            _matrix[3] = matrix[3];
            _matrix[4] = matrix[4];
            _matrix[5] = matrix[5];
        }
        public Matrix2x3()
        {
            _matrix = new double[6];
        }

        private readonly double[] _matrix = new double[Rows * Columns];

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

        public double this[int x, int y]
        {
            get
            {
                if (x >= Columns || y >= Rows)
                {
                    throw new IndexOutOfRangeException($"X: {x} and Y: {y} are outside the {Columns} x {Rows} range of {nameof(Matrix2x3)}.");
                }

                return _matrix[x + (y * Rows)];
            }
            set
            {
                if (x >= Columns || y >= Rows)
                {
                    throw new IndexOutOfRangeException($"X: {x} and Y: {y} are outside the {Columns} x {Rows} range of {nameof(Matrix2x3)}.");
                }

                _matrix[x + (y * Rows)] = value;
            }
        }

        public Vector3 Row0
        {
            get
            {
                return new Vector3(_matrix[0], _matrix[1], _matrix[2]);
            }
            set
            {
                _matrix[0] = value.X;
                _matrix[1] = value.Y;
                _matrix[2] = value.Z;
            }
        }

        public Vector3 Row1
        {
            get
            {
                return new Vector3(_matrix[3], _matrix[4], _matrix[5]);
            }
            set
            {
                _matrix[3] = value.X;
                _matrix[4] = value.Y;
                _matrix[5] = value.Z;
            }
        }

        public Vector2 Column0
        {
            get
            {
                return new Vector2(_matrix[0], _matrix[3]);
            }
            set
            {
                _matrix[0] = value.X;
                _matrix[3] = value.Y;
            }
        }

        public Vector2 Column1
        {
            get
            {
                return new Vector2(_matrix[1], _matrix[4]);
            }
            set
            {
                _matrix[1] = value.X;
                _matrix[4] = value.Y;
            }
        }

        public Vector2 Column2
        {
            get
            {
                return new Vector2(_matrix[2], _matrix[5]);
            }
            set
            {
                _matrix[2] = value.X;
                _matrix[5] = value.Y;
            }
        }

        /// <summary>
        /// Sets the contents of this matrix, to the contents of <paramref name="matrix"/>.
        /// </summary>
        /// <param name="matrix">THe source matrix.</param>
        public void Set(Matrix2x3 matrix) => matrix._matrix.CopyTo(_matrix, 0);

        public Matrix2x3 Add(Matrix2x3 matrix)
        {
            if (matrix is null)
            {
                matrix = Identity;
            }

            return new Matrix2x3(
                Row0 + matrix.Row0,
                Row1 + matrix.Row1);
        }

        public Matrix2x3 Subtract(Matrix2x3 matrix)
        {
            if (matrix is null)
            {
                matrix = Identity;
            }

            return new Matrix2x3(
                Row0 - matrix.Row0,
                Row1 - matrix.Row1);
        }

        public Matrix2x3 Multiply(double value)
        {
            return new Matrix2x3(
                Row0 * value,
                Row1 * value);
        }

        public Matrix2x3 Multiply(Matrix3 matrix)
        {
            if (matrix is null)
            {
                matrix = Matrix3.Identity;
            }

            return new Matrix2x3(
                (
                    /*x:0 y:0*/(_matrix[0] * matrix[0, 0]) + (_matrix[1] * matrix[0, 1]) + (_matrix[2] * matrix[0, 2]),
                    /*x:1 y:0*/(_matrix[0] * matrix[1, 0]) + (_matrix[1] * matrix[1, 1]) + (_matrix[2] * matrix[1, 2]),
                    /*x:2 y:0*/(_matrix[0] * matrix[2, 0]) + (_matrix[1] * matrix[2, 1]) + (_matrix[2] * matrix[2, 2])
                ),
                (
                    /*x:0 y:1*/(_matrix[3] * matrix[0, 0]) + (_matrix[4] * matrix[0, 1]) + (_matrix[5] * matrix[0, 2]),
                    /*x:1 y:1*/(_matrix[3] * matrix[1, 0]) + (_matrix[4] * matrix[1, 1]) + (_matrix[5] * matrix[1, 2]),
                    /*x:2 y:1*/(_matrix[3] * matrix[2, 0]) + (_matrix[4] * matrix[2, 1]) + (_matrix[5] * matrix[2, 2])
                ));
        }

        public Matrix2 Multiply(Matrix3x2 matrix)
        {
            if (matrix is null)
            {
                matrix = Matrix3x2.Identity;
            }

            return new Matrix2(
                (
                    /*x:0 y:0*/(_matrix[0] * matrix[0, 0]) + (_matrix[1] * matrix[0, 1]) + (_matrix[2] * matrix[0, 2]),
                    /*x:1 y:0*/(_matrix[0] * matrix[1, 0]) + (_matrix[1] * matrix[1, 1]) + (_matrix[2] * matrix[1, 2])
                ),
                (
                    /*x:0 y:1*/(_matrix[3] * matrix[0, 0]) + (_matrix[4] * matrix[0, 1]) + (_matrix[5] * matrix[0, 2]),
                    /*x:1 y:1*/(_matrix[3] * matrix[1, 0]) + (_matrix[4] * matrix[1, 1]) + (_matrix[5] * matrix[1, 2])
                ));
        }

        public Matrix2x4 Multiply(Matrix3x4 matrix)
        {
            if (matrix is null)
            {
                matrix = Matrix3x4.Identity;
            }

            return new Matrix2x4(
                (
                    /*x:0 y:0*/(_matrix[0] * matrix[0, 0]) + (_matrix[1] * matrix[0, 1]) + (_matrix[2] * matrix[0, 2]),
                    /*x:1 y:0*/(_matrix[0] * matrix[1, 0]) + (_matrix[1] * matrix[1, 1]) + (_matrix[2] * matrix[1, 2]),
                    /*x:2 y:0*/(_matrix[0] * matrix[2, 0]) + (_matrix[1] * matrix[2, 1]) + (_matrix[2] * matrix[2, 2]),
                    /*x:3 y:0*/(_matrix[0] * matrix[3, 0]) + (_matrix[1] * matrix[3, 1]) + (_matrix[2] * matrix[3, 2])
                ),
                (
                    /*x:0 y:1*/(_matrix[3] * matrix[0, 0]) + (_matrix[4] * matrix[0, 1]) + (_matrix[5] * matrix[0, 2]),
                    /*x:1 y:1*/(_matrix[3] * matrix[1, 0]) + (_matrix[4] * matrix[1, 1]) + (_matrix[5] * matrix[1, 2]),
                    /*x:2 y:1*/(_matrix[3] * matrix[2, 0]) + (_matrix[4] * matrix[2, 1]) + (_matrix[5] * matrix[2, 2]),
                    /*x:3 y:1*/(_matrix[3] * matrix[3, 0]) + (_matrix[4] * matrix[3, 1]) + (_matrix[5] * matrix[3, 2])
                ));
        }

        public double Trace() => _matrix[0] + _matrix[4];

        public Matrix3x2 Transpose() => new Matrix3x2(Column0, Column1, Column2);

        public override bool Equals(object obj)
        {
            return obj is Matrix2x3 matrix && matrix is not null &&
                _matrix[0] == matrix._matrix[0] &&
                _matrix[1] == matrix._matrix[1] &&
                _matrix[2] == matrix._matrix[2] &&
                _matrix[3] == matrix._matrix[3] &&
                _matrix[4] == matrix._matrix[4] &&
                _matrix[5] == matrix._matrix[5];
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_matrix[0], _matrix[1], _matrix[2], _matrix[3], _matrix[4], _matrix[5]);
        }

        public float[] GetGLData()
        {
            return new float[]
            {
                (float)_matrix[0],
                (float)_matrix[1],
                (float)_matrix[2],
                (float)_matrix[3],
                (float)_matrix[4],
                (float)_matrix[5]
            };
        }

        public override string ToString()
        {
            return $@"[{_matrix[0]}, {_matrix[1]}, {_matrix[2]}]
[{_matrix[3]}, {_matrix[4]}, {_matrix[5]}]";
        }
        public string ToString(string format)
        {
            return $@"[{_matrix[0].ToString(format)}, {_matrix[1].ToString(format)}, {_matrix[2].ToString(format)}]
[{_matrix[3].ToString(format)}, {_matrix[4].ToString(format)}, {_matrix[5].ToString(format)}]";
        }

        public static bool operator ==(Matrix2x3 a, Matrix2x3 b)
        {
            if (a is null)
            {
                a = Identity;
            }
            if (b is null)
            {
                b = Identity;
            }

            return a.Equals(b);
        }

        public static bool operator !=(Matrix2x3 a, Matrix2x3 b)
        {
            if (a is null)
            {
                a = Identity;
            }
            if (b is null)
            {
                b = Identity;
            }

            return !a.Equals(b);
        }

        public static Matrix2x3 operator +(Matrix2x3 a, Matrix2x3 b)
        {
            if (a is null)
            {
                a = Identity;
            }

            return a.Add(b);
        }

        public static Matrix2x3 operator -(Matrix2x3 a, Matrix2x3 b)
        {
            if (a is null)
            {
                a = Identity;
            }

            return a.Subtract(b);
        }

        public static Matrix2x3 operator *(Matrix2x3 a, Matrix3 b)
        {
            if (a is null)
            {
                a = Identity;
            }

            return a.Multiply(b);
        }

        public static Matrix2 operator *(Matrix2x3 a, Matrix3x2 b)
        {
            if (a is null)
            {
                a = Identity;
            }

            return a.Multiply(b);
        }

        public static Matrix2x4 operator *(Matrix2x3 a, Matrix3x4 b)
        {
            if (a is null)
            {
                a = Identity;
            }

            return a.Multiply(b);
        }

        public static Matrix2x3 operator *(Matrix2x3 a, double b)
        {
            if (a is null)
            {
                a = Identity;
            }

            return a.Multiply(b);
        }

        public static Matrix2x3 operator *(double a, Matrix2x3 b)
        {
            if (b is null)
            {
                b = Identity;
            }

            return b.Multiply(a);
        }

        public static Matrix2x3 Zero { get; } = new Matrix2x3(Vector3.Zero, Vector3.Zero);

        public static Matrix2x3 Identity { get; } = CreateIdentity();

        public static Matrix2x3 CreateIdentity()
        {
            return new Matrix2x3(new Vector3(1, 0, 0), new Vector3(0, 1, 0));
        }

        public static Matrix2x3 CreateRotation(Radian angle)
        {
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);

            return new Matrix2x3(new Vector3(cos, sin, 0), new Vector3(-sin, cos, 0));
        }

        public static Matrix2x3 CreateScale(double scale)
        {
            return new Matrix2x3(new Vector3(scale, 0, 0), new Vector3(0, scale, 0));
        }
        public static Matrix2x3 CreateScale(double scaleX, double scaleY)
        {
            return new Matrix2x3(new Vector3(scaleX, 0, 0), new Vector3(0, scaleY, 0));
        }
        public static Matrix2x3 CreateScale(Vector2 scale)
        {
            return CreateScale(scale.X, scale.Y);
        }

        public static Matrix2x3 CreateTranslation(double xy)
        {
            return new Matrix2x3(
                new Vector3(1, 0, xy),
                new Vector3(0, 1, xy));
        }
        public static Matrix2x3 CreateTranslation(double x, double y)
        {
            return new Matrix2x3(
                new Vector3(1, 0, x),
                new Vector3(0, 1, y));
        }
        public static Matrix2x3 CreateTranslation(Vector3 xy)
        {
            return new Matrix2x3(
                new Vector3(1, 0, xy.X),
                new Vector3(0, 1, xy.Y));
        }

        public static implicit operator Matrix2x3(Matrix2x3<double> matrix)
        {
            return new Matrix2x3((Vector3)matrix.Row0, (Vector3)matrix.Row1);
        }
        public static explicit operator Matrix2x3(Matrix2x3<float> matrix)
        {
            return new Matrix2x3((Vector3)matrix.Row0, (Vector3)matrix.Row1);
        }

        public static implicit operator Matrix2x3<double>(Matrix2x3 matrix)
        {
            if (matrix is null)
            {
                matrix = Identity;
            }

            return new Matrix2x3<double>((Vector3<double>)matrix.Row0, (Vector3<double>)matrix.Row1);
        }
        public static explicit operator Matrix2x3<float>(Matrix2x3 matrix)
        {
            if (matrix is null)
            {
                matrix = Identity;
            }

            return new Matrix2x3<float>((Vector3<float>)matrix.Row0, (Vector3<float>)matrix.Row1);
        }
    }
}
