using System;

namespace Zene.Structs
{
    public unsafe class Matrix2x4
    {
        public const int Rows = 2;
        public const int Columns = 4;

        public Matrix2x4(Vector4 row0, Vector4 row1)
        {
            Row0 = row0;
            Row1 = row1;
        }

        public Matrix2x4(double[] matrix)
        {
            if (matrix.Length < (Rows * Columns))
            {
                throw new Exception("Matrix needs to have at least 2 rows and 4 columns.");
            }

            _matrix[0] = matrix[0];
            _matrix[1] = matrix[1];
            _matrix[2] = matrix[2];
            _matrix[3] = matrix[3];
            _matrix[4] = matrix[4];
            _matrix[5] = matrix[5];
            _matrix[6] = matrix[6];
            _matrix[7] = matrix[7];
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
                    throw new IndexOutOfRangeException($"X: {x} and Y: {y} are outside the {Columns} x {Rows} range of {nameof(Matrix2x4)}.");
                }

                return _matrix[x + (y * Rows)];
            }
            set
            {
                if (x >= Columns || y >= Rows)
                {
                    throw new IndexOutOfRangeException($"X: {x} and Y: {y} are outside the {Columns} x {Rows} range of {nameof(Matrix2x4)}.");
                }

                _matrix[x + (y * Rows)] = value;
            }
        }

        public Vector4 Row0
        {
            get
            {
                return new Vector4(_matrix[0], _matrix[1], _matrix[2], _matrix[3]);
            }
            set
            {
                _matrix[0] = value.X;
                _matrix[1] = value.Y;
                _matrix[2] = value.Z;
                _matrix[3] = value.W;
            }
        }

        public Vector4 Row1
        {
            get
            {
                return new Vector4(_matrix[4], _matrix[5], _matrix[6], _matrix[7]);
            }
            set
            {
                _matrix[4] = value.X;
                _matrix[5] = value.Y;
                _matrix[6] = value.Z;
                _matrix[7] = value.W;
            }
        }

        public Vector2 Column0
        {
            get
            {
                return new Vector2(_matrix[0], _matrix[4]);
            }
            set
            {
                _matrix[0] = value.X;
                _matrix[4] = value.Y;
            }
        }

        public Vector2 Column1
        {
            get
            {
                return new Vector2(_matrix[1], _matrix[5]);
            }
            set
            {
                _matrix[1] = value.X;
                _matrix[5] = value.Y;
            }
        }

        public Vector2 Column2
        {
            get
            {
                return new Vector2(_matrix[2], _matrix[6]);
            }
            set
            {
                _matrix[2] = value.X;
                _matrix[6] = value.Y;
            }
        }

        public Vector2 Column3
        {
            get
            {
                return new Vector2(_matrix[3], _matrix[7]);
            }
            set
            {
                _matrix[3] = value.X;
                _matrix[7] = value.Y;
            }
        }

        public Matrix2x4 Add(Matrix2x4 matrix)
        {
            if (matrix == null)
            {
                matrix = Identity;
            }

            return new Matrix2x4(
                Row0 + matrix.Row0,
                Row1 + matrix.Row1);
        }

        public Matrix2x4 Subtract(Matrix2x4 matrix)
        {
            if (matrix == null)
            {
                matrix = Identity;
            }

            return new Matrix2x4(
                Row0 - matrix.Row0,
                Row1 - matrix.Row1);
        }

        public Matrix2x4 Multiply(double value)
        {
            return new Matrix2x4(
                Row0 * value,
                Row1 * value);
        }

        public Matrix2 Multiply(Matrix4x2 matrix)
        {
            if (matrix == null)
            {
                matrix = Matrix4x2.Identity;
            }

            return new Matrix2(
                (
                    /*X:0 Y:0*/(_matrix[0] * matrix[0, 0]) + (_matrix[1] * matrix[0, 1]) + (_matrix[2] * matrix[0, 2]) + (_matrix[3] * matrix[0, 3]),
                    /*X:1 Y:0*/(_matrix[0] * matrix[1, 0]) + (_matrix[1] * matrix[1, 1]) + (_matrix[2] * matrix[1, 2]) + (_matrix[3] * matrix[1, 3])
                ),
                (
                    /*X:0 Y:1*/(_matrix[4] * matrix[0, 0]) + (_matrix[5] * matrix[0, 1]) + (_matrix[6] * matrix[0, 2]) + (_matrix[7] * matrix[0, 3]),
                    /*X:1 Y:1*/(_matrix[4] * matrix[1, 0]) + (_matrix[5] * matrix[1, 1]) + (_matrix[6] * matrix[1, 2]) + (_matrix[7] * matrix[1, 3])
                ));
        }

        public Matrix2x3 Multiply(Matrix4x3 matrix)
        {
            if (matrix == null)
            {
                matrix = Matrix4x3.Identity;
            }

            return new Matrix2x3(
                (
                    /*X:0 Y:0*/(_matrix[0] * matrix[0, 0]) + (_matrix[1] * matrix[0, 1]) + (_matrix[2] * matrix[0, 2]) + (_matrix[3] * matrix[0, 3]),
                    /*X:1 Y:0*/(_matrix[0] * matrix[1, 0]) + (_matrix[1] * matrix[1, 1]) + (_matrix[2] * matrix[1, 2]) + (_matrix[3] * matrix[1, 3]),
                    /*X:2 Y:0*/(_matrix[0] * matrix[2, 0]) + (_matrix[1] * matrix[2, 1]) + (_matrix[2] * matrix[2, 2]) + (_matrix[3] * matrix[2, 3])
                ),
                (
                    /*X:0 Y:1*/(_matrix[4] * matrix[0, 0]) + (_matrix[5] * matrix[0, 1]) + (_matrix[6] * matrix[0, 2]) + (_matrix[7] * matrix[0, 3]),
                    /*X:1 Y:1*/(_matrix[4] * matrix[1, 0]) + (_matrix[5] * matrix[1, 1]) + (_matrix[6] * matrix[1, 2]) + (_matrix[7] * matrix[1, 3]),
                    /*X:2 Y:1*/(_matrix[4] * matrix[2, 0]) + (_matrix[5] * matrix[2, 1]) + (_matrix[6] * matrix[2, 2]) + (_matrix[7] * matrix[2, 3])
                ));
        }

        public Matrix2x4 Multiply(Matrix4 matrix)
        {
            if (matrix == null)
            {
                matrix = Matrix4.Identity;
            }

            return new Matrix2x4(
                (
                    /*X:0 Y:0*/(_matrix[0] * matrix[0, 0]) + (_matrix[1] * matrix[0, 1]) + (_matrix[2] * matrix[0, 2]) + (_matrix[3] * matrix[0, 3]),
                    /*X:1 Y:0*/(_matrix[0] * matrix[1, 0]) + (_matrix[1] * matrix[1, 1]) + (_matrix[2] * matrix[1, 2]) + (_matrix[3] * matrix[1, 3]),
                    /*X:2 Y:0*/(_matrix[0] * matrix[2, 0]) + (_matrix[1] * matrix[2, 1]) + (_matrix[2] * matrix[2, 2]) + (_matrix[3] * matrix[2, 3]),
                    /*X:3 Y:0*/(_matrix[0] * matrix[3, 0]) + (_matrix[1] * matrix[3, 1]) + (_matrix[2] * matrix[3, 2]) + (_matrix[3] * matrix[3, 3])
                ),
                (
                    /*X:0 Y:1*/(_matrix[4] * matrix[0, 0]) + (_matrix[5] * matrix[0, 1]) + (_matrix[6] * matrix[0, 2]) + (_matrix[7] * matrix[0, 3]),
                    /*X:1 Y:1*/(_matrix[4] * matrix[1, 0]) + (_matrix[5] * matrix[1, 1]) + (_matrix[6] * matrix[1, 2]) + (_matrix[7] * matrix[1, 3]),
                    /*X:2 Y:1*/(_matrix[4] * matrix[2, 0]) + (_matrix[5] * matrix[2, 1]) + (_matrix[6] * matrix[2, 2]) + (_matrix[7] * matrix[2, 3]),
                    /*X:3 Y:1*/(_matrix[4] * matrix[3, 0]) + (_matrix[5] * matrix[3, 1]) + (_matrix[6] * matrix[3, 2]) + (_matrix[7] * matrix[3, 3])
                ));
        }

        public double Trace() => _matrix[0] + _matrix[5];

        public Matrix4x2 Transpose() => new Matrix4x2(Column0, Column1, Column2, Column3);

        public override bool Equals(object obj)
        {
            return obj is Matrix2x4 matrix && matrix is not null &&
                _matrix[0] == matrix._matrix[0] &&
                _matrix[1] == matrix._matrix[1] &&
                _matrix[2] == matrix._matrix[2] &&
                _matrix[3] == matrix._matrix[3] &&
                _matrix[4] == matrix._matrix[4] &&
                _matrix[5] == matrix._matrix[5] &&
                _matrix[6] == matrix._matrix[6] &&
                _matrix[7] == matrix._matrix[7];
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_matrix[0], _matrix[1], _matrix[2], _matrix[3], _matrix[4], _matrix[5], _matrix[6], _matrix[7]);
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
                (float)_matrix[5],
                (float)_matrix[6],
                (float)_matrix[7]
            };
        }

        public override string ToString()
        {
            return $@"[{_matrix[0]}, {_matrix[1]}, {_matrix[2]}, {_matrix[3]}]
[{_matrix[4]}, {_matrix[5]}, {_matrix[6]}, {_matrix[7]}]";
        }
        public string ToString(string format)
        {
            return $@"[{_matrix[0].ToString(format)}, {_matrix[1].ToString(format)}, {_matrix[2].ToString(format)}, {_matrix[3].ToString(format)}]
[{_matrix[4].ToString(format)}, {_matrix[5].ToString(format)}, {_matrix[6].ToString(format)}, {_matrix[7].ToString(format)}]";
        }

        public static bool operator ==(Matrix2x4 a, Matrix2x4 b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Matrix2x4 a, Matrix2x4 b)
        {
            return !a.Equals(b);
        }

        public static Matrix2x4 operator +(Matrix2x4 a, Matrix2x4 b)
        {
            return a.Add(b);
        }

        public static Matrix2x4 operator -(Matrix2x4 a, Matrix2x4 b)
        {
            return a.Subtract(b);
        }

        public static Matrix2 operator *(Matrix2x4 a, Matrix4x2 b)
        {
            return a.Multiply(b);
        }

        public static Matrix2x3 operator *(Matrix2x4 a, Matrix4x3 b)
        {
            return a.Multiply(b);
        }

        public static Matrix2x4 operator *(Matrix2x4 a, Matrix4 b)
        {
            return a.Multiply(b);
        }

        public static Matrix2x4 operator *(Matrix2x4 a, double b)
        {
            return a.Multiply(b);
        }

        public static Matrix2x4 operator *(double a, Matrix2x4 b)
        {
            return b.Multiply(a);
        }

        public static Matrix2x4 Zero { get; } = new Matrix2x4(Vector4.Zero, Vector4.Zero);

        public static Matrix2x4 Identity { get; } = new Matrix2x4(new Vector4(1, 0, 0, 0), new Vector4(0, 1, 0, 0));

        public static Matrix2x4 CreateRotation(Radian angle)
        {
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);

            return new Matrix2x4(new Vector4(cos, sin, 0, 0), new Vector4(-sin, cos, 0, 0));
        }

        public static Matrix2x4 CreateScale(double scale)
        {
            return new Matrix2x4(new Vector4(scale, 0, 0, 0), new Vector4(0, scale, 0, 0));
        }
        public static Matrix2x4 CreateScale(double scaleX, double scaleY)
        {
            return new Matrix2x4(new Vector4(scaleX, 0, 0, 0), new Vector4(0, scaleY, 0, 0));
        }
        public static Matrix2x4 CreateScale(Vector2 scale)
        {
            return CreateScale(scale.X, scale.Y);
        }

        public static implicit operator Matrix2x4(Matrix2x4<double> matrix)
        {
            return new Matrix2x4((Vector4)matrix.Row0, (Vector4)matrix.Row1);
        }
        public static explicit operator Matrix2x4(Matrix2x4<float> matrix)
        {
            return new Matrix2x4((Vector4)matrix.Row0, (Vector4)matrix.Row1);
        }

        public static implicit operator Matrix2x4<double>(Matrix2x4 matrix)
        {
            if (matrix == null)
            {
                matrix = Identity;
            }

            return new Matrix2x4<double>((Vector4<double>)matrix.Row0, (Vector4<double>)matrix.Row1);
        }
        public static explicit operator Matrix2x4<float>(Matrix2x4 matrix)
        {
            if (matrix == null)
            {
                matrix = Identity;
            }

            return new Matrix2x4<float>((Vector4<float>)matrix.Row0, (Vector4<float>)matrix.Row1);
        }
    }
}
