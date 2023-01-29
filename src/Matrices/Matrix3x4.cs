using System;

namespace Zene.Structs
{
    public unsafe class Matrix3x4
    {
        public const int Rows = 3;
        public const int Columns = 4;

        public Matrix3x4(Vector4 row0, Vector4 row1, Vector4 row2)
        {
            Row0 = row0;
            Row1 = row1;
            Row2 = row2;
        }

        public Matrix3x4(double[] matrix)
        {
            if (matrix.Length < (Rows * Columns))
            {
                throw new Exception("Matrix needs to have at least 3 rows and 4 columns.");
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
        public Matrix3x4()
        {
            _matrix = new double[12];
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
                    throw new IndexOutOfRangeException($"X: {x} and Y: {y} are outside the {Columns} x {Rows} range of {nameof(Matrix3x4)}.");
                }

                return _matrix[x + (y * Rows)];
            }
            set
            {
                if (x >= Columns || y >= Rows)
                {
                    throw new IndexOutOfRangeException($"X: {x} and Y: {y} are outside the {Columns} x {Rows} range of {nameof(Matrix3x4)}.");
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

        public Vector4 Row2
        {
            get
            {
                return new Vector4(_matrix[8], _matrix[9], _matrix[10], _matrix[11]);
            }
            set
            {
                _matrix[8] = value.X;
                _matrix[9] = value.Y;
                _matrix[10] = value.Z;
                _matrix[11] = value.W;
            }
        }

        public Vector3 Column0
        {
            get
            {
                return new Vector3(_matrix[0], _matrix[4], _matrix[8]);
            }
            set
            {
                _matrix[0] = value.X;
                _matrix[4] = value.Y;
                _matrix[8] = value.Z;
            }
        }

        public Vector3 Column1
        {
            get
            {
                return new Vector3(_matrix[1], _matrix[5], _matrix[9]);
            }
            set
            {
                _matrix[1] = value.X;
                _matrix[5] = value.Y;
                _matrix[9] = value.Z;
            }
        }

        public Vector3 Column2
        {
            get
            {
                return new Vector3(_matrix[2], _matrix[6], _matrix[10]);
            }
            set
            {
                _matrix[2] = value.X;
                _matrix[6] = value.Y;
                _matrix[10] = value.Z;
            }
        }

        public Vector3 Column3
        {
            get
            {
                return new Vector3(_matrix[3], _matrix[7], _matrix[11]);
            }
            set
            {
                _matrix[3] = value.X;
                _matrix[7] = value.Y;
                _matrix[11] = value.Z;
            }
        }

        /// <summary>
        /// Sets the contents of this matrix, to the contents of <paramref name="matrix"/>.
        /// </summary>
        /// <param name="matrix">THe source matrix.</param>
        public void Set(Matrix3x4 matrix) => matrix._matrix.CopyTo(_matrix, 0);

        public Matrix3x4 Add(Matrix3x4 matrix)
        {
            if (matrix is null)
            {
                matrix = Identity;
            }

            return new Matrix3x4(
                Row0 + matrix.Row0,
                Row1 + matrix.Row1,
                Row2 + matrix.Row2);
        }

        public Matrix3x4 Subtract(Matrix3x4 matrix)
        {
            if (matrix is null)
            {
                matrix = Identity;
            }

            return new Matrix3x4(
                Row0 - matrix.Row0,
                Row1 - matrix.Row1,
                Row2 - matrix.Row2);
        }

        public Matrix3x4 Multiply(double value)
        {
            return new Matrix3x4(
                Row0 * value,
                Row1 * value,
                Row2 * value);
        }

        public Matrix3x2 Multiply(Matrix4x2 matrix)
        {
            if (matrix is null)
            {
                matrix = Matrix4x2.Identity;
            }

            return new Matrix3x2(
                (
                    /*X:0 Y:0*/(_matrix[0] * matrix[0, 0]) + (_matrix[1] * matrix[0, 1]) + (_matrix[2] * matrix[0, 2]) + (_matrix[3] * matrix[0, 3]),
                    /*X:1 Y:0*/(_matrix[0] * matrix[1, 0]) + (_matrix[1] * matrix[1, 1]) + (_matrix[2] * matrix[1, 2]) + (_matrix[3] * matrix[1, 3])
                ),
                (
                    /*X:0 Y:1*/(_matrix[4] * matrix[0, 0]) + (_matrix[5] * matrix[0, 1]) + (_matrix[6] * matrix[0, 2]) + (_matrix[7] * matrix[0, 3]),
                    /*X:1 Y:1*/(_matrix[4] * matrix[1, 0]) + (_matrix[5] * matrix[1, 1]) + (_matrix[6] * matrix[1, 2]) + (_matrix[7] * matrix[1, 3])
                ),
                (
                    /*X:0 Y:2*/(_matrix[8] * matrix[0, 0]) + (_matrix[9] * matrix[0, 1]) + (_matrix[10] * matrix[0, 2]) + (_matrix[11] * matrix[0, 3]),
                    /*X:1 Y:2*/(_matrix[8] * matrix[1, 0]) + (_matrix[9] * matrix[1, 1]) + (_matrix[10] * matrix[1, 2]) + (_matrix[11] * matrix[1, 3])
                ));
        }

        public Matrix3 Multiply(Matrix4x3 matrix)
        {
            if (matrix is null)
            {
                matrix = Matrix4x3.Identity;
            }

            return new Matrix3(
                (
                    /*X:0 Y:0*/(_matrix[0] * matrix[0, 0]) + (_matrix[1] * matrix[0, 1]) + (_matrix[2] * matrix[0, 2]) + (_matrix[3] * matrix[0, 3]),
                    /*X:1 Y:0*/(_matrix[0] * matrix[1, 0]) + (_matrix[1] * matrix[1, 1]) + (_matrix[2] * matrix[1, 2]) + (_matrix[3] * matrix[1, 3]),
                    /*X:2 Y:0*/(_matrix[0] * matrix[2, 0]) + (_matrix[1] * matrix[2, 1]) + (_matrix[2] * matrix[2, 2]) + (_matrix[3] * matrix[2, 3])
                ),
                (
                    /*X:0 Y:1*/(_matrix[4] * matrix[0, 0]) + (_matrix[5] * matrix[0, 1]) + (_matrix[6] * matrix[0, 2]) + (_matrix[7] * matrix[0, 3]),
                    /*X:1 Y:1*/(_matrix[4] * matrix[1, 0]) + (_matrix[5] * matrix[1, 1]) + (_matrix[6] * matrix[1, 2]) + (_matrix[7] * matrix[1, 3]),
                    /*X:2 Y:1*/(_matrix[4] * matrix[2, 0]) + (_matrix[5] * matrix[2, 1]) + (_matrix[6] * matrix[2, 2]) + (_matrix[7] * matrix[2, 3])
                ),
                (
                    /*X:0 Y:2*/(_matrix[8] * matrix[0, 0]) + (_matrix[5] * matrix[0, 1]) + (_matrix[10] * matrix[0, 2]) + (_matrix[11] * matrix[0, 3]),
                    /*X:1 Y:2*/(_matrix[8] * matrix[1, 0]) + (_matrix[9] * matrix[1, 1]) + (_matrix[10] * matrix[1, 2]) + (_matrix[11] * matrix[1, 3]),
                    /*X:2 Y:2*/(_matrix[8] * matrix[2, 0]) + (_matrix[9] * matrix[2, 1]) + (_matrix[10] * matrix[2, 2]) + (_matrix[11] * matrix[2, 3])
                ));
        }

        public Matrix3x4 Multiply(Matrix4 matrix)
        {
            if (matrix is null)
            {
                matrix = Matrix4.Identity;
            }

            return new Matrix3x4(
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
                ),
                (
                    /*X:0 Y:2*/(_matrix[8] * matrix[0, 0]) + (_matrix[5] * matrix[0, 1]) + (_matrix[10] * matrix[0, 2]) + (_matrix[11] * matrix[0, 3]),
                    /*X:1 Y:2*/(_matrix[8] * matrix[1, 0]) + (_matrix[9] * matrix[1, 1]) + (_matrix[10] * matrix[1, 2]) + (_matrix[11] * matrix[1, 3]),
                    /*X:2 Y:2*/(_matrix[8] * matrix[2, 0]) + (_matrix[9] * matrix[2, 1]) + (_matrix[10] * matrix[2, 2]) + (_matrix[11] * matrix[2, 3]),
                    /*X:3 Y:2*/(_matrix[8] * matrix[3, 0]) + (_matrix[9] * matrix[3, 1]) + (_matrix[10] * matrix[3, 2]) + (_matrix[11] * matrix[3, 3])
                ));
        }

        public double Trace() => _matrix[0] + _matrix[5] + _matrix[10];

        public Matrix4x3 Transpose() => new Matrix4x3(Column0, Column1, Column2, Column3);

        public Matrix3x4 Invert()
        {
            Matrix3 inverseRotation = new Matrix3(Column0, Column1, Column2);
            inverseRotation.Row0 /= inverseRotation.Row0.SquaredLength;
            inverseRotation.Row1 /= inverseRotation.Row1.SquaredLength;
            inverseRotation.Row2 /= inverseRotation.Row2.SquaredLength;

            Vector3 translation = new Vector3(Row0.W, Row1.W, Row2.W);

            return new Matrix3x4(
                new Vector4(inverseRotation.Row0, -inverseRotation.Row0.Dot(translation)),
                new Vector4(inverseRotation.Row1, -inverseRotation.Row1.Dot(translation)),
                new Vector4(inverseRotation.Row2, -inverseRotation.Row2.Dot(translation)));
        }

        public override bool Equals(object obj)
        {
            return obj is Matrix3x4 matrix && matrix is not null &&
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
                _matrix[11] == matrix._matrix[11];
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
            return $@"[{_matrix[0]}, {_matrix[1]}, {_matrix[2]}, {_matrix[3]}]
[{_matrix[4]}, {_matrix[5]}, {_matrix[6]}, {_matrix[7]}]
[{_matrix[8]}, {_matrix[9]}, {_matrix[10]}, {_matrix[11]}]";
        }
        public string ToString(string format)
        {
            return $@"[{_matrix[0].ToString(format)}, {_matrix[1].ToString(format)}, {_matrix[2].ToString(format)}, {_matrix[3].ToString(format)}]
[{_matrix[4].ToString(format)}, {_matrix[5].ToString(format)}, {_matrix[6].ToString(format)}, {_matrix[7].ToString(format)}]
[{_matrix[8].ToString(format)}, {_matrix[9].ToString(format)}, {_matrix[10].ToString(format)}, {_matrix[11].ToString(format)}]";
        }

        public static bool operator ==(Matrix3x4 a, Matrix3x4 b)
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

        public static bool operator !=(Matrix3x4 a, Matrix3x4 b)
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

        public static Matrix3x4 operator +(Matrix3x4 a, Matrix3x4 b)
        {
            if (a is null)
            {
                a = Identity;
            }

            return a.Add(b);
        }

        public static Matrix3x4 operator -(Matrix3x4 a, Matrix3x4 b)
        {
            if (a is null)
            {
                a = Identity;
            }

            return a.Subtract(b);
        }

        public static Matrix3x2 operator *(Matrix3x4 a, Matrix4x2 b)
        {
            if (a is null)
            {
                a = Identity;
            }

            return a.Multiply(b);
        }

        public static Matrix3 operator *(Matrix3x4 a, Matrix4x3 b)
        {
            if (a is null)
            {
                a = Identity;
            }

            return a.Multiply(b);
        }

        public static Matrix3x4 operator *(Matrix3x4 a, Matrix4 b)
        {
            if (a is null)
            {
                a = Identity;
            }

            return a.Multiply(b);
        }

        public static Matrix3x4 operator *(Matrix3x4 a, double b)
        {
            if (a is null)
            {
                a = Identity;
            }

            return a.Multiply(b);
        }

        public static Matrix3x4 operator *(double a, Matrix3x4 b)
        {
            if (b is null)
            {
                b = Identity;
            }

            return b.Multiply(a);
        }

        public static Matrix3x4 Zero { get; } = new Matrix3x4(Vector4.Zero, Vector4.Zero, Vector4.Zero);

        public static Matrix3x4 Identity { get; } = new Matrix3x4(new Vector4(1, 0, 0, 0), new Vector4(0, 1, 0, 0), new Vector4(0, 0, 1, 0));

        public static Matrix3x4 CreateRotation(Vector3 axis, Radian angle)
        {
            // normalize and create a local copy of the vector.
            axis.Normalise();
            double axisX = axis.X;
            double axisY = axis.Y;
            double axisZ = axis.Z;

            // calculate angles
            double cos = Math.Cos(-angle);
            double sin = Math.Sin(-angle);
            double t = 1.0f - cos;

            // do the conversion math once
            double tXX = t * axisX * axisX;
            double tXY = t * axisX * axisY;
            double tXZ = t * axisX * axisZ;
            double tYY = t * axisY * axisY;
            double tYZ = t * axisY * axisZ;
            double tZZ = t * axisZ * axisZ;

            double sinX = sin * axisX;
            double sinY = sin * axisY;
            double sinZ = sin * axisZ;

            return new Matrix3x4(
                new Vector4(tXX + cos, tXY - sinZ, tXZ + sinY, 0),
                new Vector4(tXY + sinZ, tYY + cos, tYZ - sinX, 0),
                new Vector4(tXZ - sinY, tYZ + sinX, tZZ + cos, 0));
        }

        public static Matrix3x4 CreateRotationX(Radian angle)
        {
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);

            return new Matrix3x4(
                new Vector4(1, 0, 0, 0),
                new Vector4(0, cos, sin, 0),
                new Vector4(0, -sin, cos, 0));
        }
        public static Matrix3x4 CreateRotationY(Radian angle)
        {
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);

            return new Matrix3x4(
                new Vector4(cos, 0, -sin, 0),
                new Vector4(0, 1, 0, 0),
                new Vector4(sin, 0, cos, 0));
        }
        public static Matrix3x4 CreateRotationZ(Radian angle)
        {
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);

            return new Matrix3x4(
                new Vector4(cos, sin, 0, 0),
                new Vector4(-sin, cos, 0, 0),
                new Vector4(0, 0, 1, 0));
        }

        public static Matrix3x4 CreateScale(double scale)
        {
            return new Matrix3x4(
                new Vector4(scale, 0, 0, 0),
                new Vector4(0, scale, 0, 0),
                new Vector4(0, 0, scale, 0));
        }
        public static Matrix3x4 CreateScale(double scaleX, double scaleY, double scaleZ)
        {
            return new Matrix3x4(
                new Vector4(scaleX, 0, 0, 0),
                new Vector4(0, scaleY, 0, 0),
                new Vector4(0, 0, scaleZ, 0));
        }
        public static Matrix3x4 CreateScale(double scaleX, double scaleY)
        {
            return new Matrix3x4(
                new Vector4(scaleX, 0, 0, 0),
                new Vector4(0, scaleY, 0, 0),
                new Vector4(0, 0, 1, 0));
        }
        public static Matrix3x4 CreateScale(Vector3 scale) => CreateScale(scale.X, scale.Y, scale.Z);
        public static Matrix3x4 CreateScale(Vector2 scale) => CreateScale(scale.X, scale.Y);

        public static Matrix3x4 CreateTranslation(double xy)
        {
            return new Matrix3x4(
                new Vector4(1, 0, 0, 0),
                new Vector4(0, 1, 0, 0),
                new Vector4(xy, xy, 1, 0));
        }
        public static Matrix3x4 CreateTranslation(double x, double y)
        {
            return new Matrix3x4(
                new Vector4(1, 0, 0, 0),
                new Vector4(0, 1, 0, 0),
                new Vector4(x, y, 1, 0));
        }
        public static Matrix3x4 CreateTranslation(Vector2 xy) => CreateTranslation(xy.X, xy.Y);

        public static Matrix3x4 CreateBox(IBox box)
        {
            Vector2 c = box.Centre;

            return new Matrix3x4(
                new Vector4(box.Width, 0, 0, 0),
                new Vector4(0, box.Height, 0, 0),
                new Vector4(c.X, c.Y, 1, 0));
        }

        public static implicit operator Matrix3x4(Matrix3x4<double> matrix)
        {
            return new Matrix3x4((Vector4)matrix.Row0, (Vector4)matrix.Row1, (Vector4)matrix.Row2);
        }
        public static explicit operator Matrix3x4(Matrix3x4<float> matrix)
        {
            return new Matrix3x4((Vector4)matrix.Row0, (Vector4)matrix.Row1, (Vector4)matrix.Row2);
        }

        public static implicit operator Matrix3x4<double>(Matrix3x4 matrix)
        {
            if (matrix is null)
            {
                matrix = Identity;
            }

            return new Matrix3x4<double>((Vector4<double>)matrix.Row0, (Vector4<double>)matrix.Row1, (Vector4<double>)matrix.Row2);
        }
        public static explicit operator Matrix3x4<float>(Matrix3x4 matrix)
        {
            if (matrix is null)
            {
                matrix = Identity;
            }

            return new Matrix3x4<float>((Vector4<float>)matrix.Row0, (Vector4<float>)matrix.Row1, (Vector4<float>)matrix.Row2);
        }
    }
}
