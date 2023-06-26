using System;

namespace Zene.Structs
{
    public unsafe class Matrix2x4 : IMatrix
    {
        public int Rows => 2;
        public int Columns => 4;

        public Matrix2x4(Vector4 row0, Vector4 row1)
        {
            Row0 = row0;
            Row1 = row1;
        }

        public Matrix2x4(params double[] matrix)
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
        public Matrix2x4(IMatrix matrix)
        {
            MatrixSpan ms = matrix.MatrixData();

            _matrix[0] = ms[0, 0];
            _matrix[1] = ms[1, 0];
            _matrix[2] = ms[2, 0];
            _matrix[3] = ms[3, 0];

            _matrix[4] = ms[0, 1];
            _matrix[5] = ms[1, 1];
            _matrix[6] = ms[2, 1];
            _matrix[7] = ms[3, 1];
        }
        public Matrix2x4()
        {
            _matrix = new double[8];
        }

        private readonly double[] _matrix = new double[8];

        public ReadOnlySpan<double> Data => _matrix;

        public double this[int x, int y]
        {
            get
            {
                if (x >= Columns || y >= Rows)
                {
                    throw new IndexOutOfRangeException($"X: {x} and Y: {y} are outside the {Columns} x {Rows} range of {nameof(Matrix2x4)}.");
                }

                return _matrix[x + (y * Columns)];
            }
            set
            {
                if (x >= Columns || y >= Rows)
                {
                    throw new IndexOutOfRangeException($"X: {x} and Y: {y} are outside the {Columns} x {Rows} range of {nameof(Matrix2x4)}.");
                }

                _matrix[x + (y * Columns)] = value;
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

        public MatrixSpan MatrixData() => new MatrixSpan(2, 4, _matrix);

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

        public static bool operator ==(Matrix2x4 a, Matrix2x4 b) => Equals(a, b);

        public static bool operator !=(Matrix2x4 a, Matrix2x4 b) => !Equals(a, b);

        public static MultiplyMatrix operator *(Matrix2x4 a, IMatrix b) => new MultiplyMatrix(a, b);

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
    }
}
