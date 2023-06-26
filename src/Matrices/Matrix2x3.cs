using System;

namespace Zene.Structs
{
    public unsafe class Matrix2x3 : IMatrix
    {
        public int Rows => 2;
        public int Columns => 3;

        public Matrix2x3(Vector3 row0, Vector3 row1)
        {
            Row0 = row0;
            Row1 = row1;
        }

        public Matrix2x3(params double[] matrix)
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
        public Matrix2x3(IMatrix matrix)
        {
            MatrixSpan ms = matrix.MatrixData();

            _matrix[0] = ms[0, 0];
            _matrix[1] = ms[1, 0];
            _matrix[2] = ms[2, 0];

            _matrix[3] = ms[0, 1];
            _matrix[4] = ms[1, 1];
            _matrix[5] = ms[2, 1];
        }
        public Matrix2x3()
        {
            _matrix = new double[6];
        }

        private readonly double[] _matrix = new double[6];

        public ReadOnlySpan<double> Data => _matrix;

        public double this[int x, int y]
        {
            get
            {
                if (x >= Columns || y >= Rows)
                {
                    throw new IndexOutOfRangeException($"X: {x} and Y: {y} are outside the {Columns} x {Rows} range of {nameof(Matrix2x3)}.");
                }

                return _matrix[x + (y * Columns)];
            }
            set
            {
                if (x >= Columns || y >= Rows)
                {
                    throw new IndexOutOfRangeException($"X: {x} and Y: {y} are outside the {Columns} x {Rows} range of {nameof(Matrix2x3)}.");
                }

                _matrix[x + (y * Columns)] = value;
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

        public MatrixSpan MatrixData() => new MatrixSpan(2, 3, _matrix);

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

        public static bool operator ==(Matrix2x3 a, Matrix2x3 b) => Equals(a, b);

        public static bool operator !=(Matrix2x3 a, Matrix2x3 b) => !Equals(a, b);

        public static MultiplyMatrix operator *(Matrix2x3 a, IMatrix b) => new MultiplyMatrix(a, b);

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
    }
}
