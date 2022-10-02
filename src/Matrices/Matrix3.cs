using System;

namespace Zene.Structs
{
    public unsafe struct Matrix3
    {
        public const int Rows = 3;
        public const int Columns = 3;

        public Matrix3(Vector3 row0, Vector3 row1, Vector3 row2)
        {
            Row0 = row0;
            Row1 = row1;
            Row2 = row2;
        }

        public Matrix3(double[] matrix)
        {
            if (matrix.Length < (Rows * Columns))
            {
                throw new Exception("Matrix needs to have at least 3 rows and 3 columns.");
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
                    throw new IndexOutOfRangeException($"X: {x} and Y: {y} are outside the {Columns} x {Rows} range of {nameof(Matrix3)}.");
                }

                return _matrix[x + (y * Rows)];
            }
            set
            {
                if (x >= Columns || y >= Rows)
                {
                    throw new IndexOutOfRangeException($"X: {x} and Y: {y} are outside the {Columns} x {Rows} range of {nameof(Matrix3)}.");
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

        public Vector3 Column0
        {
            get
            {
                return new Vector3(_matrix[0], _matrix[3], _matrix[6]);
            }
            set
            {
                _matrix[0] = value.X;
                _matrix[3] = value.Y;
                _matrix[6] = value.Z;
            }
        }

        public Vector3 Column1
        {
            get
            {
                return new Vector3(_matrix[1], _matrix[4], _matrix[7]);
            }
            set
            {
                _matrix[1] = value.X;
                _matrix[4] = value.Y;
                _matrix[7] = value.Z;
            }
        }

        public Vector3 Column2
        {
            get
            {
                return new Vector3(_matrix[2], _matrix[5], _matrix[8]);
            }
            set
            {
                _matrix[2] = value.X;
                _matrix[5] = value.Y;
                _matrix[8] = value.Z;
            }
        }

        public Matrix3 Add(ref Matrix3 matrix)
        {
            return new Matrix3(
                Row0 + matrix.Row0,
                Row1 + matrix.Row1,
                Row2 + matrix.Row2);
        }

        public Matrix3 Subtract(ref Matrix3 matrix)
        {
            return new Matrix3(
                Row0 - matrix.Row0,
                Row1 - matrix.Row1,
                Row2 - matrix.Row2);
        }

        public Matrix3 Multiply(double value)
        {
            return new Matrix3(
                Row0 * value,
                Row1 * value,
                Row2 * value);
        }

        public Matrix3 Multiply(ref Matrix3 matrix)
        {
            return new Matrix3(
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
                    /*x:0 y:2*/(_matrix[6] * matrix[0, 0]) + (_matrix[7] * matrix[0, 1]) + (_matrix[8] * matrix[0, 2]),
                    /*x:1 y:2*/(_matrix[6] * matrix[1, 0]) + (_matrix[7] * matrix[1, 1]) + (_matrix[8] * matrix[1, 2]),
                    /*x:2 y:2*/(_matrix[6] * matrix[2, 0]) + (_matrix[7] * matrix[2, 1]) + (_matrix[8] * matrix[2, 2])
                ));
        }

        public Matrix3x2 Multiply(ref Matrix3x2 matrix)
        {
            return new Matrix3x2(
                (
                    /*x:0 y:0*/(_matrix[0] * matrix[0, 0]) + (_matrix[1] * matrix[0, 1]) + (_matrix[2] * matrix[0, 2]),
                    /*x:1 y:0*/(_matrix[0] * matrix[1, 0]) + (_matrix[1] * matrix[1, 1]) + (_matrix[2] * matrix[1, 2])
                ),
                (
                    /*x:0 y:1*/(_matrix[3] * matrix[0, 0]) + (_matrix[4] * matrix[0, 1]) + (_matrix[5] * matrix[0, 2]),
                    /*x:1 y:1*/(_matrix[3] * matrix[1, 0]) + (_matrix[4] * matrix[1, 1]) + (_matrix[5] * matrix[1, 2])
                ),
                (
                    /*x:0 y:2*/(_matrix[6] * matrix[0, 0]) + (_matrix[7] * matrix[0, 1]) + (_matrix[8] * matrix[0, 2]),
                    /*x:1 y:2*/(_matrix[6] * matrix[1, 0]) + (_matrix[7] * matrix[1, 1]) + (_matrix[8] * matrix[1, 2])
                ));
        }

        public Matrix3x4 Multiply(ref Matrix3x4 matrix)
        {
            return new Matrix3x4(
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
                ));
        }

        public double Determinant()
        {
            return (_matrix[0] * _matrix[4] * _matrix[8]) + (_matrix[1] * _matrix[5] * _matrix[6]) + (_matrix[2] * _matrix[3] * _matrix[7])
                - (_matrix[2] * _matrix[4] * _matrix[6]) - (_matrix[0] * _matrix[5] * _matrix[7]) - (_matrix[1] * _matrix[3] * _matrix[8]);
        }

        public double Trace() => _matrix[0] + _matrix[4] + _matrix[8];

        public Matrix3 Normalize()
        {
            double det = Determinant();

            return new Matrix3(
                Row0 / det,
                Row1 / det,
                Row2 / det);
        }

        public Matrix3 Invert()
        {
            double det = Determinant();

            if (det == 0)
            {
                throw new InvalidOperationException("Matrix is singular and cannot be inverted.");
            }

            double invDet = 1.0 / det;

            return new Matrix3(
                (
                    /*x:0 y:0*/((+_matrix[4] * _matrix[8]) - (_matrix[5] * _matrix[7])) * invDet,
                    /*x:1 y:0*/((-_matrix[1] * _matrix[8]) + (_matrix[2] * _matrix[7])) * invDet,
                    /*x:2 y:0*/((+_matrix[1] * _matrix[5]) - (_matrix[2] * _matrix[4])) * invDet
                ),
                (
                    /*x:0 y:1*/((-_matrix[3] * _matrix[8]) + (_matrix[5] * _matrix[6])) * invDet,
                    /*x:1 y:1*/((+_matrix[0] * _matrix[8]) - (_matrix[2] * _matrix[6])) * invDet,
                    /*x:2 y:1*/((-_matrix[0] * _matrix[5]) + (_matrix[2] * _matrix[3])) * invDet
                ),
                (
                    /*x:0 y:2*/((+_matrix[3] * _matrix[7]) - (_matrix[4] * _matrix[6])) * invDet,
                    /*x:1 y:2*/((-_matrix[0] * _matrix[7]) + (_matrix[1] * _matrix[6])) * invDet,
                    /*x:2 y:2*/((+_matrix[0] * _matrix[4]) - (_matrix[1] * _matrix[3])) * invDet
                ));
        }

        public Matrix3 Transpose() => new Matrix3(Column0, Column1, Column2);

        public override bool Equals(object obj)
        {
            return obj is Matrix3 matrix &&
                _matrix[0] == matrix._matrix[0] &&
                _matrix[1] == matrix._matrix[1] &&
                _matrix[2] == matrix._matrix[2] &&
                _matrix[3] == matrix._matrix[3] &&
                _matrix[4] == matrix._matrix[4] &&
                _matrix[5] == matrix._matrix[5] &&
                _matrix[6] == matrix._matrix[6] &&
                _matrix[7] == matrix._matrix[7] &&
                _matrix[8] == matrix._matrix[8];
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
                (float)_matrix[8]
            };
        }

        public override string ToString()
        {
            return $@"[{_matrix[0]}, {_matrix[1]}, {_matrix[2]}]
[{_matrix[3]}, {_matrix[4]}, {_matrix[5]}]
[{_matrix[6]}, {_matrix[7]}, {_matrix[8]}]";
        }
        public string ToString(string format)
        {
            return $@"[{_matrix[0].ToString(format)}, {_matrix[1].ToString(format)}, {_matrix[2].ToString(format)}]
[{_matrix[3].ToString(format)}, {_matrix[4].ToString(format)}, {_matrix[5].ToString(format)}]
[{_matrix[6].ToString(format)}, {_matrix[7].ToString(format)}, {_matrix[8].ToString(format)}]";
        }

        public static bool operator ==(Matrix3 a, Matrix3 b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Matrix3 a, Matrix3 b)
        {
            return !a.Equals(b);
        }

        public static Matrix3 operator +(Matrix3 a, Matrix3 b)
        {
            return a.Add(ref b);
        }

        public static Matrix3 operator -(Matrix3 a, Matrix3 b)
        {
            return a.Subtract(ref b);
        }

        public static Matrix3 operator *(Matrix3 a, Matrix3 b)
        {
            return a.Multiply(ref b);
        }

        public static Matrix3x2 operator *(Matrix3 a, Matrix3x2 b)
        {
            return a.Multiply(ref b);
        }

        public static Matrix3x4 operator *(Matrix3 a, Matrix3x4 b)
        {
            return a.Multiply(ref b);
        }

        public static Matrix3 operator *(Matrix3 a, double b)
        {
            return a.Multiply(b);
        }

        public static Matrix3 operator *(double a, Matrix3 b)
        {
            return b.Multiply(a);
        }

        private static Matrix3 _zero = new Matrix3(Vector3.Zero, Vector3.Zero, Vector3.Zero);
        public static ref Matrix3 Zero => ref _zero;

        private static Matrix3 _identity = new Matrix3(new Vector3(1, 0, 0), new Vector3(0, 1, 0), new Vector3(0, 0, 1));
        public static ref Matrix3 Identity => ref _identity;

        public static Matrix3 CreateRotation(Vector3 axis, Radian angle)
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

            return new Matrix3(
                new Vector3(tXX + cos, tXY - sinZ, tXZ + sinY),
                new Vector3(tXY + sinZ, tYY + cos, tYZ - sinX),
                new Vector3(tXZ - sinY, tYZ + sinX, tZZ + cos));
        }

        public static Matrix3 CreateRotationX(Radian angle)
        {
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);

            return new Matrix3(
                new Vector3(1, 0, 0),
                new Vector3(0, cos, sin),
                new Vector3(0, -sin, cos));
        }

        public static Matrix3 CreateRotationY(Radian angle)
        {
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);

            return new Matrix3(
                new Vector3(cos, 0, -sin),
                new Vector3(0, 1, 0),
                new Vector3(sin, 0, cos));
        }

        public static Matrix3 CreateRotationZ(Radian angle)
        {
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);

            return new Matrix3(
                new Vector3(cos, sin, 0),
                new Vector3(-sin, cos, 0),
                new Vector3(0, 0, 1));
        }

        public static Matrix3 CreateScale(double scale)
        {
            return new Matrix3(
                new Vector3(scale, 0, 0),
                new Vector3(0, scale, 0),
                new Vector3(0, 0, scale));
        }

        public static Matrix3 CreateScale(double scaleX, double scaleY, double scaleZ)
        {
            return new Matrix3(
                new Vector3(scaleX, 0, 0),
                new Vector3(0, scaleY, 0),
                new Vector3(0, 0, scaleZ));
        }

        public static Matrix3 CreateScale(Vector3 scale)
        {
            return CreateScale(scale.X, scale.Y, scale.Z);
        }

        public static Matrix3 CreateTranslation(double xy)
        {
            return new Matrix3(
                new Vector3(1, 0, 0),
                new Vector3(0, 1, 0),
                new Vector3(xy, xy, 1));
        }
        public static Matrix3 CreateTranslation(double x, double y)
        {
            return new Matrix3(
                new Vector3(1, 0, 0),
                new Vector3(0, 1, 0),
                new Vector3(x, y, 1));
        }
        public static Matrix3 CreateTranslation(Vector2 xy)
        {
            return CreateTranslation(xy.X, xy.Y);
        }

        public static implicit operator Matrix3(Matrix3<double> matrix)
        {
            return new Matrix3((Vector3)matrix.Row0, (Vector3)matrix.Row1, (Vector3)matrix.Row2);
        }
        public static explicit operator Matrix3(Matrix3<float> matrix)
        {
            return new Matrix3((Vector3)matrix.Row0, (Vector3)matrix.Row1, (Vector3)matrix.Row2);
        }

        public static implicit operator Matrix3<double>(Matrix3 matrix)
        {
            return new Matrix3<double>((Vector3<double>)matrix.Row0, (Vector3<double>)matrix.Row1, (Vector3<double>)matrix.Row2);
        }
        public static explicit operator Matrix3<float>(Matrix3 matrix)
        {
            return new Matrix3<float>((Vector3<float>)matrix.Row0, (Vector3<float>)matrix.Row1, (Vector3<float>)matrix.Row2);
        }
    }
}
