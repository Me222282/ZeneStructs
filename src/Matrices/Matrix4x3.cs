using System;

namespace Zene.Structs
{
    public unsafe class Matrix4x3
    {
        public const int Rows = 4;
        public const int Columns = 3;

        public Matrix4x3(Vector3 row0, Vector3 row1, Vector3 row2, Vector3 row3)
        {
            Row0 = row0;
            Row1 = row1;
            Row2 = row2;
            Row3 = row3;
        }

        public Matrix4x3(double[] matrix)
        {
            if (matrix.Length < (Rows * Columns))
            {
                throw new Exception("Matrix needs to have at least 4 rows and 3 columns.");
            }

            _matrix[0] = matrix[0];
            _matrix[1] = matrix[1];
            _matrix[2] = matrix[2];

            _matrix[3] = matrix[3];
            _matrix[4] = matrix[4];
            _matrix[5] = matrix[5];

            _matrix[6] = matrix[6];
            _matrix[7] = matrix[7];
            _matrix[8] = matrix[8];

            _matrix[9] = matrix[9];
            _matrix[10] = matrix[10];
            _matrix[11] = matrix[11];
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
                    throw new IndexOutOfRangeException($"X: {x} and Y: {y} are outside the {Columns} x {Rows} range of {nameof(Matrix4x3)}.");
                }

                return _matrix[x + (y * Rows)];
            }
            set
            {
                if (x >= Columns || y >= Rows)
                {
                    throw new IndexOutOfRangeException($"X: {x} and Y: {y} are outside the {Columns} x {Rows} range of {nameof(Matrix4x3)}.");
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

        public Vector3 Row2
        {
            get
            {
                return new Vector3(_matrix[6], _matrix[7], _matrix[8]);
            }
            set
            {
                _matrix[6] = value.X;
                _matrix[7] = value.Y;
                _matrix[8] = value.Z;
            }
        }

        public Vector3 Row3
        {
            get
            {
                return new Vector3(_matrix[9], _matrix[10], _matrix[11]);
            }
            set
            {
                _matrix[9] = value.X;
                _matrix[10] = value.Y;
                _matrix[11] = value.Z;
            }
        }

        public Vector4 Column0
        {
            get
            {
                return new Vector4(_matrix[0], _matrix[3], _matrix[6], _matrix[9]);
            }
            set
            {
                _matrix[0] = value.X;
                _matrix[3] = value.Y;
                _matrix[6] = value.Z;
                _matrix[9] = value.W;
            }
        }

        public Vector4 Column1
        {
            get
            {
                return new Vector4(_matrix[1], _matrix[4], _matrix[7], _matrix[10]);
            }
            set
            {
                _matrix[1] = value.X;
                _matrix[4] = value.Y;
                _matrix[7] = value.Z;
                _matrix[10] = value.W;
            }
        }

        public Vector4 Column2
        {
            get
            {
                return new Vector4(_matrix[2], _matrix[5], _matrix[8], _matrix[11]);
            }
            set
            {
                _matrix[2] = value.X;
                _matrix[5] = value.Y;
                _matrix[8] = value.Z;
                _matrix[11] = value.W;
            }
        }

        public Matrix4x3 Add(Matrix4x3 matrix)
        {
            if (matrix is null)
            {
                matrix = Identity;
            }

            return new Matrix4x3(
                Row0 + matrix.Row0,
                Row1 + matrix.Row1,
                Row2 + matrix.Row2,
                Row3 + matrix.Row3);
        }

        public Matrix4x3 Subtract(Matrix4x3 matrix)
        {
            if (matrix is null)
            {
                matrix = Identity;
            }

            return new Matrix4x3(
                Row0 - matrix.Row0,
                Row1 - matrix.Row1,
                Row2 - matrix.Row2,
                Row3 - matrix.Row3);
        }

        public Matrix4x3 Multiply(double value)
        {
            return new Matrix4x3(
                Row0 * value,
                Row1 * value,
                Row2 * value,
                Row3 * value);
        }

        public Matrix4x3 Multiply(Matrix3 matrix)
        {
            if (matrix is null)
            {
                matrix = Matrix3.Identity;
            }

            return new Matrix4x3(
                (
                    /*x:0 y:0*/(_matrix[0] * matrix[0, 0]) + (_matrix[1] * matrix[0, 1]) + (_matrix[2] * matrix[0, 2]),
                    /*x:1 y:0*/(_matrix[0] * matrix[1, 0]) + (_matrix[1] * matrix[1, 1]) + (_matrix[2] * matrix[1, 2]),
                    /*x:2 y:0*/(_matrix[0] * matrix[2, 0]) + (_matrix[1] * matrix[2, 1]) + (_matrix[2] * matrix[2, 2])
                ),
                (
                    /*x:0 y:1*/(_matrix[3] * matrix[0, 0]) + (_matrix[4] * matrix[0, 1]) + (_matrix[5] * matrix[0, 2]),
                    /*x:1 y:1*/(_matrix[3] * matrix[1, 0]) + (_matrix[4] * matrix[1, 1]) + (_matrix[5] * matrix[1, 2]),
                    /*x:2 y:1*/(_matrix[3] * matrix[2, 0]) + (_matrix[4] * matrix[2, 1]) + (_matrix[5] * matrix[2, 2])
                ),
                (
                    /*x:0 y:1*/(_matrix[6] * matrix[0, 0]) + (_matrix[7] * matrix[0, 1]) + (_matrix[8] * matrix[0, 2]),
                    /*x:0 y:1*/(_matrix[6] * matrix[1, 0]) + (_matrix[7] * matrix[1, 1]) + (_matrix[8] * matrix[1, 2]),
                    /*x:0 y:1*/(_matrix[6] * matrix[2, 0]) + (_matrix[7] * matrix[2, 1]) + (_matrix[8] * matrix[2, 2])
                ),
                (
                    /*x:0 y:1*/(_matrix[9] * matrix[0, 0]) + (_matrix[10] * matrix[0, 1]) + (_matrix[11] * matrix[0, 2]),
                    /*x:0 y:1*/(_matrix[9] * matrix[0, 0]) + (_matrix[10] * matrix[1, 1]) + (_matrix[11] * matrix[1, 2]),
                    /*x:0 y:1*/(_matrix[9] * matrix[2, 0]) + (_matrix[10] * matrix[2, 1]) + (_matrix[11] * matrix[2, 2])
                ));
        }

        public Matrix4x2 Multiply(Matrix3x2 matrix)
        {
            if (matrix is null)
            {
                matrix = Matrix3x2.Identity;
            }

            return new Matrix4x2(
                (
                    /*x:0 y:0*/(_matrix[0] * matrix[0, 0]) + (_matrix[1] * matrix[0, 1]) + (_matrix[2] * matrix[0, 2]),
                    /*x:1 y:0*/(_matrix[0] * matrix[1, 0]) + (_matrix[1] * matrix[1, 1]) + (_matrix[2] * matrix[1, 2])
                ),
                (
                    /*x:0 y:1*/(_matrix[3] * matrix[0, 0]) + (_matrix[4] * matrix[0, 1]) + (_matrix[5] * matrix[0, 2]),
                    /*x:1 y:1*/(_matrix[3] * matrix[1, 0]) + (_matrix[4] * matrix[1, 1]) + (_matrix[5] * matrix[1, 2])
                ),
                (
                    /*x:0 y:1*/(_matrix[6] * matrix[0, 0]) + (_matrix[7] * matrix[0, 1]) + (_matrix[8] * matrix[0, 2]),
                    /*x:0 y:1*/(_matrix[6] * matrix[1, 0]) + (_matrix[7] * matrix[1, 1]) + (_matrix[8] * matrix[1, 2])
                ),
                (
                    /*x:0 y:1*/(_matrix[9] * matrix[0, 0]) + (_matrix[10] * matrix[0, 1]) + (_matrix[11] * matrix[0, 2]),
                    /*x:0 y:1*/(_matrix[9] * matrix[1, 0]) + (_matrix[10] * matrix[1, 1]) + (_matrix[11] * matrix[1, 2])
                ));
        }

        public Matrix4 Multiply(Matrix3x4 matrix)
        {
            if (matrix is null)
            {
                matrix = Matrix3x4.Identity;
            }

            return new Matrix4(
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
                ),
                (
                    /*x:0 y:2*/(_matrix[6] * matrix[0, 0]) + (_matrix[7] * matrix[0, 1]) + (_matrix[8] * matrix[0, 2]),
                    /*x:1 y:2*/(_matrix[6] * matrix[1, 0]) + (_matrix[7] * matrix[1, 1]) + (_matrix[8] * matrix[1, 2]),
                    /*x:2 y:2*/(_matrix[6] * matrix[2, 0]) + (_matrix[7] * matrix[2, 1]) + (_matrix[8] * matrix[2, 2]),
                    /*x:3 y:2*/(_matrix[6] * matrix[3, 0]) + (_matrix[7] * matrix[3, 1]) + (_matrix[8] * matrix[3, 2])
                ),
                (
                    /*x:0 y:3*/(_matrix[9] * matrix[0, 0]) + (_matrix[10] * matrix[0, 1]) + (_matrix[11] * matrix[0, 2]),
                    /*x:1 y:3*/(_matrix[9] * matrix[1, 0]) + (_matrix[10] * matrix[1, 1]) + (_matrix[11] * matrix[1, 2]),
                    /*x:2 y:3*/(_matrix[9] * matrix[2, 0]) + (_matrix[10] * matrix[2, 1]) + (_matrix[11] * matrix[2, 2]),
                    /*x:3 y:3*/(_matrix[9] * matrix[3, 0]) + (_matrix[10] * matrix[3, 1]) + (_matrix[11] * matrix[3, 2])
                ));
        }

        public double Trace() => _matrix[0] + _matrix[4] + _matrix[8];

        public Matrix4x3 Invert()
        {
            Matrix3 inverseRotation = new Matrix3(
                (Vector3)Column0,
                (Vector3)Column1,
                (Vector3)Column2);
            inverseRotation.Row0 /= inverseRotation.Row0.SquaredLength;
            inverseRotation.Row1 /= inverseRotation.Row1.SquaredLength;
            inverseRotation.Row2 /= inverseRotation.Row2.SquaredLength;

            Vector3 translation = Row3;

            return new Matrix4x3(
                inverseRotation.Row0,
                inverseRotation.Row1,
                inverseRotation.Row2,
                new Vector3(
                    -inverseRotation.Row0.Dot(translation),
                    -inverseRotation.Row1.Dot(translation),
                    -inverseRotation.Row2.Dot(translation)
                ));
        }

        public Matrix3x4 Transpose() => new Matrix3x4(Column0, Column1, Column2);

        public override bool Equals(object obj)
        {
            return obj is Matrix4x3 matrix && matrix is not null &&
                _matrix[0] == matrix._matrix[0] &&
                _matrix[1] == matrix._matrix[1] &&
                _matrix[2] == matrix._matrix[2] &&
                _matrix[3] == matrix._matrix[3] &&
                _matrix[4] == matrix._matrix[4] &&
                _matrix[5] == matrix._matrix[5] &&
                _matrix[6] == matrix._matrix[6] &&
                _matrix[7] == matrix._matrix[7] &&
                _matrix[8] == matrix._matrix[8] &&
                _matrix[9] == matrix._matrix[9] &&
                _matrix[10] == matrix._matrix[10] &&
                _matrix[11] == matrix._matrix[12];
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(_matrix[0]);
            hash.Add(_matrix[1]);
            hash.Add(_matrix[2]);
            hash.Add(_matrix[3]);
            hash.Add(_matrix[4]);
            hash.Add(_matrix[5]);
            hash.Add(_matrix[6]);
            hash.Add(_matrix[7]);
            hash.Add(_matrix[8]);
            hash.Add(_matrix[9]);
            hash.Add(_matrix[10]);
            hash.Add(_matrix[11]);

            return hash.ToHashCode();
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
                (float)_matrix[7],
                (float)_matrix[8],
                (float)_matrix[9],
                (float)_matrix[10],
                (float)_matrix[11]
            };
        }

        public override string ToString()
        {
            return $@"[{_matrix[0]}, {_matrix[1]}, {_matrix[2]}]
[{_matrix[3]}, {_matrix[4]}, {_matrix[5]}]
[{_matrix[6]}, {_matrix[7]}, {_matrix[8]}]
[{_matrix[9]}, {_matrix[10]}, {_matrix[11]}]";
        }
        public string ToString(string format)
        {
            return $@"[{_matrix[0].ToString(format)}, {_matrix[1].ToString(format)}, {_matrix[2].ToString(format)}]
[{_matrix[3].ToString(format)}, {_matrix[4].ToString(format)}, {_matrix[5].ToString(format)}]
[{_matrix[6].ToString(format)}, {_matrix[7].ToString(format)}, {_matrix[8].ToString(format)}]
[{_matrix[9].ToString(format)}, {_matrix[10].ToString(format)}, {_matrix[11].ToString(format)}]";
        }

        public static bool operator ==(Matrix4x3 a, Matrix4x3 b)
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

        public static bool operator !=(Matrix4x3 a, Matrix4x3 b)
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

        public static Matrix4x3 operator +(Matrix4x3 a, Matrix4x3 b)
        {
            if (a is null)
            {
                a = Identity;
            }

            return a.Add(b);
        }

        public static Matrix4x3 operator -(Matrix4x3 a, Matrix4x3 b)
        {
            if (a is null)
            {
                a = Identity;
            }

            return a.Subtract(b);
        }

        public static Matrix4x3 operator *(Matrix4x3 a, Matrix3 b)
        {
            if (a is null)
            {
                a = Identity;
            }

            return a.Multiply(b);
        }

        public static Matrix4x2 operator *(Matrix4x3 a, Matrix3x2 b)
        {
            if (a is null)
            {
                a = Identity;
            }

            return a.Multiply(b);
        }

        public static Matrix4 operator *(Matrix4x3 a, Matrix3x4 b)
        {
            if (a is null)
            {
                a = Identity;
            }

            return a.Multiply(b);
        }

        public static Matrix4x3 operator *(Matrix4x3 a, double b)
        {
            if (a is null)
            {
                a = Identity;
            }

            return a.Multiply(b);
        }

        public static Matrix4x3 operator *(double a, Matrix4x3 b)
        {
            if (b is null)
            {
                b = Identity;
            }

            return b.Multiply(a);
        }

        public static Matrix4x3 Zero { get; } = new Matrix4x3(Vector3.Zero, Vector3.Zero, Vector3.Zero, Vector3.Zero);

        public static Matrix4x3 Identity { get; } = new Matrix4x3(new Vector3(1, 0, 0), new Vector3(0, 1, 0), new Vector3(0, 0, 1), Vector3.Zero);

        public static Matrix4x3 CreateScale(double scale)
        {
            return new Matrix4x3(
                new Vector3(scale, 0, 0),
                new Vector3(0, scale, 0),
                new Vector3(0, 0, scale),
                Vector3.Zero);
        }
        public static Matrix4x3 CreateScale(double scaleX, double scaleY, double scaleZ)
        {
            return new Matrix4x3(
                new Vector3(scaleX, 0, 0),
                new Vector3(0, scaleY, 0),
                new Vector3(0, 0, scaleZ),
                Vector3.Zero);
        }
        public static Matrix4x3 CreateScale(double scaleX, double scaleY)
        {
            return new Matrix4x3(
                new Vector3(scaleX, 0, 0),
                new Vector3(0, scaleY, 0),
                new Vector3(0, 0, 1),
                Vector3.Zero);
        }
        public static Matrix4x3 CreateScale(Vector3 scale) => CreateScale(scale.X, scale.Y, scale.Z);
        public static Matrix4x3 CreateScale(Vector2 scale) => CreateScale(scale.X, scale.Y);

        public static Matrix4x3 CreateTranslation(double xyz)
        {
            return new Matrix4x3(
                new Vector3(1, 0, 0),
                new Vector3(0, 1, 0),
                new Vector3(0, 0, 1),
                new Vector3(xyz));
        }
        public static Matrix4x3 CreateTranslation(double x, double y, double z)
        {
            return new Matrix4x3(
                new Vector3(1, 0, 0),
                new Vector3(0, 1, 0),
                new Vector3(0, 0, 1),
                new Vector3(x, y, z));
        }
        public static Matrix4x3 CreateTranslation(double x, double y)
        {
            return new Matrix4x3(
                new Vector3(1, 0, 0),
                new Vector3(0, 1, 0),
                new Vector3(0, 0, 1),
                new Vector3(x, y, 0));
        }
        public static Matrix4x3 CreateTranslation(Vector3 xyz) => CreateTranslation(xyz.X, xyz.Y, xyz.Z);
        public static Matrix4x3 CreateTranslation(Vector2 xy) => CreateTranslation(xy.X, xy.Y);

        public static Matrix4x3 CreateBox(IBox box)
        {
            Vector2 c = box.Centre;

            return new Matrix4x3(
                new Vector3(box.Width, 0, 0),
                new Vector3(0, box.Height, 0),
                new Vector3(0, 0, 1),
                new Vector3(c.X, c.Y, 0));
        }
        public static Matrix4x3 CreateBox(IBox box, double depth)
        {
            Vector2 c = box.Centre;

            return new Matrix4x3(
                new Vector3(box.Width, 0, 0),
                new Vector3(0, box.Height, 0),
                new Vector3(0, 0, 1),
                new Vector3(c.X, c.Y, depth));
        }
        public static Matrix4x3 CreateBox(IBox3 box)
        {
            Vector3 c = box.Centre;

            return new Matrix4x3(
                new Vector3(box.Width, 0, 0),
                new Vector3(0, box.Height, 0),
                new Vector3(0, 0, box.Depth),
                new Vector3(c.X, c.Y, c.Z));
        }

        public static Matrix4x3 CreateRotationX(Radian angle)
        {
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);

            return new Matrix4x3(
                new Vector3(1, 0, 0),
                new Vector3(0, cos, sin),
                new Vector3(0, -sin, cos),
                Vector3.Zero);
        }
        public static Matrix4x3 CreateRotationY(Radian angle)
        {
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);

            return new Matrix4x3(
                new Vector3(cos, 0, -sin),
                new Vector3(0, 1, 0),
                new Vector3(sin, 0, cos),
                Vector3.Zero);
        }
        public static Matrix4x3 CreateRotationZ(Radian angle)
        {
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);

            return new Matrix4x3(
                new Vector3(cos, sin, 0),
                new Vector3(-sin, cos, 0),
                new Vector3(0, 0, 1),
                Vector3.Zero);
        }

        public static Matrix4x3 CreateRotation(Vector3 axis, Radian angle)
        {
            axis.Normalise();
            double axisX = axis.X, axisY = axis.Y, axisZ = axis.Z;

            double cos = Math.Cos(-angle);
            double sin = Math.Sin(-angle);
            double t = 1.0f - cos;

            double tXX = t * axisX * axisX;
            double tXY = t * axisX * axisY;
            double tXZ = t * axisX * axisZ;
            double tYY = t * axisY * axisY;
            double tYZ = t * axisY * axisZ;
            double tZZ = t * axisZ * axisZ;

            double sinX = sin * axisX;
            double sinY = sin * axisY;
            double sinZ = sin * axisZ;

            return new Matrix4x3(
                new Vector3(tXX + cos, tXY - sinZ, tXZ + sinY),
                new Vector3(tXY + sinZ, tYY + cos, tYZ - sinX),
                new Vector3(tXZ - sinY, tYZ + sinX, tZZ + cos),
                Vector3.Zero);
        }

        public static implicit operator Matrix4x3(Matrix4x3<double> matrix)
        {
            return new Matrix4x3((Vector3)matrix.Row0, (Vector3)matrix.Row1, (Vector3)matrix.Row2, (Vector3)matrix.Row3);
        }
        public static explicit operator Matrix4x3(Matrix4x3<float> matrix)
        {
            return new Matrix4x3((Vector3)matrix.Row0, (Vector3)matrix.Row1, (Vector3)matrix.Row2, (Vector3)matrix.Row3);
        }

        public static implicit operator Matrix4x3<double>(Matrix4x3 matrix)
        {
            if (matrix is null)
            {
                matrix = Identity;
            }

            return new Matrix4x3<double>((Vector3<double>)matrix.Row0, (Vector3<double>)matrix.Row1, (Vector3<double>)matrix.Row2, (Vector3<double>)matrix.Row3);
        }
        public static explicit operator Matrix4x3<float>(Matrix4x3 matrix)
        {
            if (matrix is null)
            {
                matrix = Identity;
            }

            return new Matrix4x3<float>((Vector3<float>)matrix.Row0, (Vector3<float>)matrix.Row1, (Vector3<float>)matrix.Row2, (Vector3<float>)matrix.Row3);
        }
    }
}
