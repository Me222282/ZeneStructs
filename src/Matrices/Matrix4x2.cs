using System;

namespace Zene.Structs
{
    public unsafe class Matrix4x2 : IMatrix
    {
        public int Rows => 4;
        public int Columns => 2;
        
        public bool Constant => true;
        
        public Matrix4x2(Vector2 row0, Vector2 row1, Vector2 row2, Vector2 row3)
        {
            Row0 = row0;
            Row1 = row1;
            Row2 = row2;
            Row3 = row3;
        }

        public Matrix4x2(params double[] matrix)
        {
            if (matrix.Length < (Rows * Columns))
            {
                throw new Exception("Matrix needs to have at least 4 rows and 2 columns.");
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
        public Matrix4x2(IMatrix matrix)
        {
            MatrixSpan ms = matrix.MatrixData();

            _matrix[0] = ms[0, 0];
            _matrix[1] = ms[1, 0];

            _matrix[2] = ms[0, 1];
            _matrix[3] = ms[1, 1];

            _matrix[4] = ms[0, 2];
            _matrix[5] = ms[1, 2];

            _matrix[6] = ms[0, 3];
            _matrix[7] = ms[1, 3];
        }
        public Matrix4x2()
        {
            //_matrix = new double[8];
        }

        private readonly double[] _matrix = new double[8];

        public ReadOnlySpan<double> Data => _matrix;

        public double this[int x, int y]
        {
            get
            {
                if (x >= Columns || y >= Rows)
                {
                    throw new IndexOutOfRangeException($"X: {x} and Y: {y} are outside the {Columns} x {Rows} range of {nameof(Matrix4x2)}.");
                }

                return _matrix[x + (y * Columns)];
            }
            set
            {
                if (x >= Columns || y >= Rows)
                {
                    throw new IndexOutOfRangeException($"X: {x} and Y: {y} are outside the {Columns} x {Rows} range of {nameof(Matrix4x2)}.");
                }

                _matrix[x + (y * Columns)] = value;
            }
        }

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

        public Vector2 Row2
        {
            get
            {
                return new Vector2(_matrix[4], _matrix[5]);
            }
            set
            {
                _matrix[4] = value.X;
                _matrix[5] = value.Y;
            }
        }

        public Vector2 Row3
        {
            get
            {
                return new Vector2(_matrix[6], _matrix[7]);
            }
            set
            {
                _matrix[6] = value.X;
                _matrix[7] = value.Y;
            }
        }

        public Vector4 Column0
        {
            get
            {
                return new Vector4(_matrix[0], _matrix[2], _matrix[4], _matrix[6]);
            }
            set
            {
                _matrix[0] = value.X;
                _matrix[2] = value.Y;
                _matrix[4] = value.Z;
                _matrix[6] = value.W;
            }
        }

        public Vector4 Column1
        {
            get
            {
                return new Vector4(_matrix[1], _matrix[3], _matrix[5], _matrix[7]);
            }
            set
            {
                _matrix[1] = value.X;
                _matrix[3] = value.Y;
                _matrix[5] = value.Z;
                _matrix[7] = value.W;
            }
        }

        public double Trace() => _matrix[0] + _matrix[3];

        public Matrix2x4 Transpose() => new Matrix2x4(Column0, Column1);

        public override bool Equals(object obj)
        {
            return obj is Matrix4x2 matrix && matrix is not null &&
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

        public MatrixSpan MatrixData() => new MatrixSpan(4, 2, _matrix);

        public override string ToString()
        {
            return $@"[{_matrix[0]}, {_matrix[1]}]
[{_matrix[2]}, {_matrix[3]}]
[{_matrix[4]}, {_matrix[5]}]
[{_matrix[6]}, {_matrix[7]}]";
        }
        public string ToString(string format)
        {
            return $@"[{_matrix[0].ToString(format)}, {_matrix[1].ToString(format)}]
[{_matrix[2].ToString(format)}, {_matrix[3].ToString(format)}]
[{_matrix[4].ToString(format)}, {_matrix[5].ToString(format)}]
[{_matrix[6].ToString(format)}, {_matrix[7].ToString(format)}]";
        }

        public static bool operator ==(Matrix4x2 a, Matrix4x2 b) => Equals(a, b);

        public static bool operator !=(Matrix4x2 a, Matrix4x2 b) => !Equals(a, b);

        public static MultiplyMatrix operator *(Matrix4x2 a, IMatrix b) => new MultiplyMatrix(a, b);

        public static Matrix4x2 CreateRotation(Radian angle)
        {
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);

            return new Matrix4x2(new Vector2(cos, sin), new Vector2(-sin, cos), Vector2.Zero, Vector2.Zero);
        }

        public static Matrix4x2 CreateScale(double scale)
        {
            return new Matrix4x2(new Vector2(scale, 0), new Vector2(0, scale), Vector2.Zero, Vector2.Zero);
        }
        public static Matrix4x2 CreateScale(double scaleX, double scaleY)
        {
            return new Matrix4x2(new Vector2(scaleX, 0), new Vector2(0, scaleY), Vector2.Zero, Vector2.Zero);
        }
        public static Matrix4x2 CreateScale(Vector2 scale) => CreateScale(scale.X, scale.Y);

        public static Matrix4x2 CreateTranslation(double xy)
        {
            return new Matrix4x2(
                new Vector2(1, 0),
                new Vector2(0, 1),
                Vector2.Zero,
                new Vector2(xy, xy));
        }
        public static Matrix4x2 CreateTranslation(double x, double y)
        {
            return new Matrix4x2(
                new Vector2(1, 0),
                new Vector2(0, 1),
                Vector2.Zero,
                new Vector2(x, y));
        }
        public static Matrix4x2 CreateTranslation(Vector2 xy) => CreateTranslation(xy.X, xy.Y);

        public static Matrix4x2 CreateBox(IBox box)
        {
            Vector2 c = box.Centre;

            return new Matrix4x2(
                new Vector2(box.Width, 0),
                new Vector2(0, box.Height),
                new Vector2(0, 0),
                new Vector2(c.X, c.Y));
        }
    }
}
