using System;

namespace Zene.Structs
{
    public unsafe class Matrix3 : IMatrix
    {
        public int Rows => 3;
        public int Columns => 3;

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
        public Matrix3()
        {
            _matrix = new double[9];
        }

        private readonly double[] _matrix = new double[9];

        public ReadOnlySpan<double> Data => _matrix;

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
            return obj is Matrix3 matrix && matrix is not null &&
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

        public MatrixSpan MatrixData() => new MatrixSpan(3, 3, _matrix);

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

        public static bool operator ==(Matrix3 a, Matrix3 b) => Equals(a, b);

        public static bool operator !=(Matrix3 a, Matrix3 b) => !Equals(a, b);

        public static MultiplyMatrix operator *(Matrix3 a, IMatrix b) => new MultiplyMatrix(a, b);

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
        public static Matrix3 CreateScale(double scaleX, double scaleY)
        {
            return new Matrix3(
                new Vector3(scaleX, 0, 0),
                new Vector3(0, scaleY, 0),
                new Vector3(0, 0, 1));
        }
        public static Matrix3 CreateScale(Vector3 scale) => CreateScale(scale.X, scale.Y, scale.Z);
        public static Matrix3 CreateScale(Vector2 scale) => CreateScale(scale.X, scale.Y);

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
        public static Matrix3 CreateTranslation(Vector2 xy) => CreateTranslation(xy.X, xy.Y);

        public static Matrix3 CreateBox(IBox box)
        {
            Vector2 c = box.Centre;

            return new Matrix3(
                new Vector3(box.Width, 0, 0),
                new Vector3(0, box.Height, 0),
                new Vector3(c.X, c.Y, 1));
        }
    }
}
