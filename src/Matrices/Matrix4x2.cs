using System;

namespace Zene.Structs
{
    public unsafe struct Matrix4x2
    {
        public const int Rows = 4;
        public const int Columns = 2;

        public Matrix4x2(Vector2 row0, Vector2 row1, Vector2 row2, Vector2 row3)
        {
            Row0 = row0;
            Row1 = row1;
            Row2 = row2;
            Row3 = row3;
        }

        public Matrix4x2(double[] matrix)
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

        private fixed double _matrix[Rows * Columns];

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
                    throw new IndexOutOfRangeException($"X: {x} and Y: {y} are outside the {Columns} x {Rows} range of {nameof(Matrix4x2)}.");
                }

                return _matrix[x + (y * Rows)];
            }
            set
            {
                if (x >= Columns || y >= Rows)
                {
                    throw new IndexOutOfRangeException($"X: {x} and Y: {y} are outside the {Columns} x {Rows} range of {nameof(Matrix4x2)}.");
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

        public Matrix4x2 Add(ref Matrix4x2 matrix)
        {
            return new Matrix4x2(
                Row0 + matrix.Row0,
                Row1 + matrix.Row1,
                Row2 + matrix.Row2,
                Row3 + matrix.Row3);
        }

        public Matrix4x2 Subtract(ref Matrix4x2 matrix)
        {
            return new Matrix4x2(
                Row0 - matrix.Row0,
                Row1 - matrix.Row1,
                Row2 - matrix.Row2,
                Row3 - matrix.Row3);
        }

        public Matrix4x2 Multiply(double value)
        {
            return new Matrix4x2(
                Row0 * value,
                Row1 * value,
                Row2 * value,
                Row3 * value);
        }

        public Matrix4x2 Multiply(ref Matrix2 matrix)
        {
            return new Matrix4x2(
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
                ),
                (
                    /*x:0 y:2*/(_matrix[6] * matrix[0, 0]) + (_matrix[7] * matrix[0, 1]),
                    /*x:0 y:2*/(_matrix[6] * matrix[1, 0]) + (_matrix[7] * matrix[1, 1])
                ));
        }

        public Matrix4x3 Multiply(ref Matrix2x3 matrix)
        {
            return new Matrix4x3(
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
                ),
                (
                    /*x:0 y:2*/(_matrix[6] * matrix[0, 0]) + (_matrix[7] * matrix[0, 1]),
                    /*x:0 y:2*/(_matrix[6] * matrix[1, 0]) + (_matrix[7] * matrix[1, 1]),
                    /*x:0 y:2*/(_matrix[6] * matrix[2, 0]) + (_matrix[7] * matrix[2, 1])
                ));
        }

        public Matrix4 Multiply(Matrix2x4 matrix)
        {
            return new Matrix4(
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
                ),
                (
                    /*x:0 y:2*/(_matrix[4] * matrix[0, 0]) + (_matrix[7] * matrix[0, 1]),
                    /*x:0 y:2*/(_matrix[6] * matrix[1, 0]) + (_matrix[7] * matrix[1, 1]),
                    /*x:0 y:2*/(_matrix[6] * matrix[2, 0]) + (_matrix[7] * matrix[2, 1]),
                    /*x:0 y:2*/(_matrix[6] * matrix[3, 0]) + (_matrix[7] * matrix[3, 1])
                ));
        }

        public double Trace() => _matrix[0] + _matrix[3];

        public Matrix2x4 Transpose() => new Matrix2x4(Column0, Column1);

        public override bool Equals(object obj)
        {
            return obj is Matrix4x2 matrix &&
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

        public static bool operator ==(Matrix4x2 a, Matrix4x2 b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Matrix4x2 a, Matrix4x2 b)
        {
            return !a.Equals(b);
        }

        public static Matrix4x2 operator +(Matrix4x2 a, Matrix4x2 b)
        {
            return a.Add(ref b);
        }

        public static Matrix4x2 operator -(Matrix4x2 a, Matrix4x2 b)
        {
            return a.Subtract(ref b);
        }

        public static Matrix4x2 operator *(Matrix4x2 a, Matrix2 b)
        {
            return a.Multiply(ref b);
        }

        public static Matrix4x3 operator *(Matrix4x2 a, Matrix2x3 b)
        {
            return a.Multiply(ref b);
        }

        public static Matrix4 operator *(Matrix4x2 a, Matrix2x4 b)
        {
            return a.Multiply(b);
        }

        public static Matrix4x2 operator *(Matrix4x2 a, double b)
        {
            return a.Multiply(b);
        }

        public static Matrix4x2 operator *(double a, Matrix4x2 b)
        {
            return b.Multiply(a);
        }

        private static Matrix4x2 _zero = new Matrix4x2(Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero);
        public static ref Matrix4x2 Zero => ref _zero;

        private static Matrix4x2 _identity = new Matrix4x2(new Vector2(1, 0), new Vector2(0, 1), Vector2.Zero, Vector2.Zero);
        public static ref Matrix4x2 Identity => ref _identity;

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

        public static implicit operator Matrix4x2(Matrix4x2<double> matrix)
        {
            return new Matrix4x2((Vector2)matrix.Row0, (Vector2)matrix.Row1, (Vector2)matrix.Row2, (Vector2)matrix.Row3);
        }
        public static explicit operator Matrix4x2(Matrix4x2<float> matrix)
        {
            return new Matrix4x2((Vector2)matrix.Row0, (Vector2)matrix.Row1, (Vector2)matrix.Row2, (Vector2)matrix.Row3);
        }

        public static implicit operator Matrix4x2<double>(Matrix4x2 matrix)
        {
            return new Matrix4x2<double>((Vector2<double>)matrix.Row0, (Vector2<double>)matrix.Row1, (Vector2<double>)matrix.Row2, (Vector2<double>)matrix.Row3);
        }
        public static explicit operator Matrix4x2<float>(Matrix4x2 matrix)
        {
            return new Matrix4x2<float>((Vector2<float>)matrix.Row0, (Vector2<float>)matrix.Row1, (Vector2<float>)matrix.Row2, (Vector2<float>)matrix.Row3);
        }
    }
}
