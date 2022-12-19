using System;

namespace Zene.Structs
{
    public unsafe class Matrix3x2
    {
        public const int Rows = 3;
        public const int Columns = 2;

        public Matrix3x2(Vector2 row0, Vector2 row1, Vector2 row2)
        {
            Row0 = row0;
            Row1 = row1;
            Row2 = row2;
        }

        public Matrix3x2(double[] matrix)
        {
            if (matrix.Length < (Rows * Columns))
            {
                throw new Exception("Matrix needs to have at least 3 rows and 2 columns.");
            }

            _matrix[0] = matrix[0];
            _matrix[1] = matrix[1];

            _matrix[2] = matrix[2];
            _matrix[3] = matrix[3];

            _matrix[4] = matrix[4];
            _matrix[5] = matrix[5];
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
                    throw new IndexOutOfRangeException($"X: {x} and Y: {y} are outside the {Columns} x {Rows} range of {nameof(Matrix3x2)}.");
                }

                return _matrix[x + (y * Rows)];
            }
            set
            {
                if (x >= Columns || y >= Rows)
                {
                    throw new IndexOutOfRangeException($"X: {x} and Y: {y} are outside the {Columns} x {Rows} range of {nameof(Matrix3x2)}.");
                }

                _matrix[x + (y * Rows)] = value;
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
                return new Vector2(_matrix[3], _matrix[4]);
            }
            set
            {
                _matrix[3] = value.X;
                _matrix[4] = value.Y;
            }
        }

        public Vector2 Row2
        {
            get
            {
                return new Vector2(_matrix[5], _matrix[6]);
            }
            set
            {
                _matrix[5] = value.X;
                _matrix[6] = value.Y;
            }
        }

        public Vector3 Column0
        {
            get
            {
                return new Vector3(_matrix[0], _matrix[3], _matrix[5]);
            }
            set
            {
                _matrix[0] = value.X;
                _matrix[3] = value.Y;
                _matrix[5] = value.Z;
            }
        }

        public Vector3 Column1
        {
            get
            {
                return new Vector3(_matrix[1], _matrix[4], _matrix[6]);
            }
            set
            {
                _matrix[1] = value.X;
                _matrix[4] = value.Y;
                _matrix[6] = value.Z;
            }
        }

        public Matrix3x2 Add(Matrix3x2 matrix)
        {
            if (matrix is null)
            {
                matrix = Identity;
            }

            return new Matrix3x2(
                Row0 + matrix.Row0,
                Row1 + matrix.Row1,
                Row2 + matrix.Row2);
        }

        public Matrix3x2 Subtract(Matrix3x2 matrix)
        {
            if (matrix is null)
            {
                matrix = Identity;
            }

            return new Matrix3x2(
                Row0 - matrix.Row0,
                Row1 - matrix.Row1,
                Row2 - matrix.Row2);
        }

        public Matrix3x2 Multiply(double value)
        {
            return new Matrix3x2(
                Row0 * value,
                Row1 * value,
                Row2 * value);
        }

        public Matrix3x2 Multiply(Matrix2 matrix)
        {
            if (matrix is null)
            {
                matrix = Matrix2.Identity;
            }

            return new Matrix3x2(
                (
                    /*x:0 y:0*/(_matrix[0] * matrix[0, 0]) + (_matrix[1] * matrix[0, 1]),
                    /*x:1 y:0*/(_matrix[0] * matrix[1, 0]) + (_matrix[1] * matrix[1, 1])
                ),
                (
                    /*x:0 y:1*/(_matrix[2] * matrix[0, 0]) + (_matrix[3] * matrix[0, 1]),
                    /*x:1 y:1*/(_matrix[2] * matrix[1, 0]) + (_matrix[3] * matrix[1, 1])
                ),
                (
                    /*x:0 y:2*/(_matrix[4] * matrix[0, 0]) + (_matrix[5] * matrix[0, 1]),
                    /*x:1 y:2*/(_matrix[4] * matrix[1, 0]) + (_matrix[5] * matrix[1, 1])
                ));
        }

        public Matrix3 Multiply(Matrix2x3 matrix)
        {
            if (matrix is null)
            {
                matrix = Matrix2x3.Identity;
            }

            return new Matrix3(
                (
                    /*x:0 y:0*/(_matrix[0] * matrix[0, 0]) + (_matrix[1] * matrix[0, 1]),
                    /*x:1 y:0*/(_matrix[0] * matrix[1, 0]) + (_matrix[1] * matrix[1, 1]),
                    /*x:2 y:0*/(_matrix[0] * matrix[2, 0]) + (_matrix[1] * matrix[2, 1])
                ),
                (
                    /*x:0 y:1*/(_matrix[2] * matrix[0, 0]) + (_matrix[3] * matrix[0, 1]),
                    /*x:1 y:1*/(_matrix[2] * matrix[1, 0]) + (_matrix[3] * matrix[1, 1]),
                    /*x:2 y:1*/(_matrix[2] * matrix[2, 0]) + (_matrix[3] * matrix[2, 1])
                ),
                (
                    /*x:0 y:2*/(_matrix[4] * matrix[0, 0]) + (_matrix[5] * matrix[0, 1]),
                    /*x:1 y:2*/(_matrix[4] * matrix[1, 0]) + (_matrix[5] * matrix[1, 1]),
                    /*x:2 y:2*/(_matrix[4] * matrix[2, 0]) + (_matrix[5] * matrix[2, 1])
                ));
        }

        public Matrix3x4 Multiply(Matrix2x4 matrix)
        {
            if (matrix is null)
            {
                matrix = Matrix2x4.Identity;
            }

            return new Matrix3x4(
                (
                    /*x:0 y:0*/(_matrix[0] * matrix[0, 0]) + (_matrix[1] * matrix[0, 1]),
                    /*x:1 y:0*/(_matrix[0] * matrix[1, 0]) + (_matrix[1] * matrix[1, 1]),
                    /*x:2 y:0*/(_matrix[0] * matrix[2, 0]) + (_matrix[1] * matrix[2, 1]),
                    /*x:2 y:0*/(_matrix[0] * matrix[3, 0]) + (_matrix[1] * matrix[3, 1])
                ),
                (
                    /*x:0 y:1*/(_matrix[2] * matrix[0, 0]) + (_matrix[3] * matrix[0, 1]),
                    /*x:1 y:1*/(_matrix[2] * matrix[1, 0]) + (_matrix[3] * matrix[1, 1]),
                    /*x:2 y:1*/(_matrix[2] * matrix[2, 0]) + (_matrix[3] * matrix[2, 1]),
                    /*x:2 y:1*/(_matrix[2] * matrix[3, 0]) + (_matrix[3] * matrix[3, 1])
                ),
                (
                    /*x:0 y:2*/(_matrix[4] * matrix[0, 0]) + (_matrix[5] * matrix[0, 1]),
                    /*x:1 y:2*/(_matrix[4] * matrix[1, 0]) + (_matrix[5] * matrix[1, 1]),
                    /*x:2 y:2*/(_matrix[4] * matrix[2, 0]) + (_matrix[5] * matrix[2, 1]),
                    /*x:2 y:2*/(_matrix[4] * matrix[3, 0]) + (_matrix[5] * matrix[3, 1])
                ));
        }

        public double Trace() => _matrix[0] + _matrix[3];

        public Matrix2x3 Transpose() => new Matrix2x3(Column0, Column1);

        public override bool Equals(object obj)
        {
            return obj is Matrix3x2 matrix && matrix is not null &&
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
            return $@"[{_matrix[0]}, {_matrix[1]}]
[{_matrix[3]}, {_matrix[4]}]
[{_matrix[5]}, {_matrix[6]}]";
        }
        public string ToString(string format)
        {
            return $@"[{_matrix[0].ToString(format)}, {_matrix[1].ToString(format)}]
[{_matrix[3].ToString(format)}, {_matrix[4].ToString(format)}]
[{_matrix[5].ToString(format)}, {_matrix[6].ToString(format)}]";
        }

        public static bool operator ==(Matrix3x2 a, Matrix3x2 b)
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

        public static bool operator !=(Matrix3x2 a, Matrix3x2 b)
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

        public static Matrix3x2 operator +(Matrix3x2 a, Matrix3x2 b)
        {
            if (a is null)
            {
                a = Identity;
            }

            return a.Add(b);
        }

        public static Matrix3x2 operator -(Matrix3x2 a, Matrix3x2 b)
        {
            if (a is null)
            {
                a = Identity;
            }

            return a.Subtract(b);
        }

        public static Matrix3 operator *(Matrix3x2 a, Matrix2x3 b)
        {
            if (a is null)
            {
                a = Identity;
            }

            return a.Multiply(b);
        }

        public static Matrix3x2 operator *(Matrix3x2 a, Matrix2 b)
        {
            if (a is null)
            {
                a = Identity;
            }

            return a.Multiply(b);
        }

        public static Matrix3x2 operator *(Matrix3x2 a, double b)
        {
            if (a is null)
            {
                a = Identity;
            }

            return a.Multiply(b);
        }

        public static Matrix3x2 operator *(double a, Matrix3x2 b)
        {
            if (b is null)
            {
                b = Identity;
            }

            return b.Multiply(a);
        }

        public static Matrix3x2 Zero { get; } = new Matrix3x2(Vector2.Zero, Vector2.Zero, Vector2.Zero);

        public static Matrix3x2 Identity { get; } = new Matrix3x2(new Vector2(1, 0), new Vector2(0, 1), Vector2.Zero);

        public static Matrix3x2 CreateRotation(Radian angle)
        {
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);

            return new Matrix3x2(new Vector2(cos, sin), new Vector2(-sin, cos), Vector2.Zero);
        }

        public static Matrix3x2 CreateScale(double scale)
        {
            return new Matrix3x2(new Vector2(scale, 0), new Vector2(0, scale), Vector2.Zero);
        }
        public static Matrix3x2 CreateScale(double scaleX, double scaleY)
        {
            return new Matrix3x2(new Vector2(scaleX, 0), new Vector2(0, scaleY), Vector2.Zero);
        }
        public static Matrix3x2 CreateScale(Vector2 scale) => CreateScale(scale.X, scale.Y);

        public static Matrix3x2 CreateTranslation(double xy)
        {
            return new Matrix3x2(
                new Vector2(1, 0),
                new Vector2(0, 1),
                new Vector2(xy));
        }
        public static Matrix3x2 CreateTranslation(double x, double y)
        {
            return new Matrix3x2(
                new Vector2(1, 0),
                new Vector2(0, 1),
                new Vector2(x, y));
        }
        public static Matrix3x2 CreateTranslation(Vector2 xy) => CreateTranslation(xy.X, xy.Y);

        public static Matrix3x2 CreateBox(IBox box)
        {
            Vector2 c = box.Centre;

            return new Matrix3x2(
                new Vector2(box.Width, 0),
                new Vector2(0, box.Height),
                new Vector2(c.X, c.Y));
        }

        public static implicit operator Matrix3x2(Matrix3x2<double> matrix)
        {
            return new Matrix3x2((Vector2)matrix.Row0, (Vector2)matrix.Row1, (Vector2)matrix.Row2);
        }
        public static explicit operator Matrix3x2(Matrix3x2<float> matrix)
        {
            return new Matrix3x2((Vector2)matrix.Row0, (Vector2)matrix.Row1, (Vector2)matrix.Row2);
        }

        public static implicit operator Matrix3x2<double>(Matrix3x2 matrix)
        {
            if (matrix is null)
            {
                matrix = Identity;
            }

            return new Matrix3x2<double>((Vector2<double>)matrix.Row0, (Vector2<double>)matrix.Row1, (Vector2<double>)matrix.Row2);
        }
        public static explicit operator Matrix3x2<float>(Matrix3x2 matrix)
        {
            if (matrix is null)
            {
                matrix = Identity;
            }

            return new Matrix3x2<float>((Vector2<float>)matrix.Row0, (Vector2<float>)matrix.Row1, (Vector2<float>)matrix.Row2);
        }
    }
}
