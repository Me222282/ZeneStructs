using System;

namespace Zene.Structs
{
    public unsafe struct Matrix3x4 : IMatrix
    {
        public int Rows => 3;
        public int Columns => 4;
        
        public bool Constant => true;
        
        public Matrix3x4(Vector4 row0, Vector4 row1, Vector4 row2)
        {
            Row0 = row0;
            Row1 = row1;
            Row2 = row2;
        }

        public Matrix3x4(params floatv[] matrix)
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
        public Matrix3x4(IMatrix matrix)
        {
            fixed (void* ptr = _matrix)
            {
                matrix.MatrixData(new MatrixSpan(3, 4, new Span<floatv>(ptr, 12)));
            }
        }

        internal fixed floatv _matrix[12];

        public floatv this[int x, int y]
        {
            get
            {
                if (x >= Columns || y >= Rows)
                {
                    throw new IndexOutOfRangeException($"X: {x} and Y: {y} are outside the {Columns} x {Rows} range of {nameof(Matrix3x4)}.");
                }

                return _matrix[x + (y * Columns)];
            }
            set
            {
                if (x >= Columns || y >= Rows)
                {
                    throw new IndexOutOfRangeException($"X: {x} and Y: {y} are outside the {Columns} x {Rows} range of {nameof(Matrix3x4)}.");
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

        public floatv Trace() => _matrix[0] + _matrix[5] + _matrix[10];

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
            return obj is Matrix3x4 matrix && this == matrix;
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

        public void MatrixData(MatrixSpan ms)
        {
            fixed (void* ptr = _matrix)
            {
                Span<floatv> s = new Span<floatv>(ptr, 12);
                ms.Fill(s, 3, 4);
            }
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
            return a._matrix[0] == b._matrix[0] &&
                a._matrix[1] == b._matrix[1] &&
                a._matrix[2] == b._matrix[2] &&
                a._matrix[3] == b._matrix[3] &&
                a._matrix[4] == b._matrix[4] &&
                a._matrix[5] == b._matrix[5] &&
                a._matrix[6] == b._matrix[6] &&
                a._matrix[7] == b._matrix[7] &&
                a._matrix[8] == b._matrix[8] &&
                a._matrix[9] == b._matrix[9] &&
                a._matrix[10] == b._matrix[10] &&
                a._matrix[11] == b._matrix[11];
        }

        public static bool operator !=(Matrix3x4 a, Matrix3x4 b) => !(a == b);

        public static MultiplyMatrix operator *(Matrix3x4 a, IMatrix b) => new MultiplyMatrix(a, b);
        
        public static Matrix3x4 operator *(Matrix3x4 a, floatv b)
        {
            Matrix3x4 m = new Matrix3x4();

            m._matrix[0] = a._matrix[0] * b;
            m._matrix[1] = a._matrix[1] * b;
            m._matrix[2] = a._matrix[2] * b;
            m._matrix[3] = a._matrix[3] * b;
            m._matrix[4] = a._matrix[4] * b;
            m._matrix[5] = a._matrix[5] * b;
            m._matrix[6] = a._matrix[6] * b;
            m._matrix[7] = a._matrix[7] * b;
            m._matrix[8] = a._matrix[8] * b;
            m._matrix[9] = a._matrix[9] * b;
            m._matrix[10] = a._matrix[10] * b;
            m._matrix[11] = a._matrix[11] * b;

            return m;
        }
        public static Matrix3x4 operator *(floatv b, Matrix3x4 a)
        {
            Matrix3x4 m = new Matrix3x4();

            m._matrix[0] = a._matrix[0] * b;
            m._matrix[1] = a._matrix[1] * b;
            m._matrix[2] = a._matrix[2] * b;
            m._matrix[3] = a._matrix[3] * b;
            m._matrix[4] = a._matrix[4] * b;
            m._matrix[5] = a._matrix[5] * b;
            m._matrix[6] = a._matrix[6] * b;
            m._matrix[7] = a._matrix[7] * b;
            m._matrix[8] = a._matrix[8] * b;
            m._matrix[9] = a._matrix[9] * b;
            m._matrix[10] = a._matrix[10] * b;
            m._matrix[11] = a._matrix[11] * b;

            return m;
        }
        
        public static Matrix3x4 operator +(Matrix3x4 a, Matrix3x4 b)
        {
            Matrix3x4 m = new Matrix3x4();

            m._matrix[0] = a._matrix[0] + b._matrix[0];
            m._matrix[1] = a._matrix[1] + b._matrix[1];
            m._matrix[2] = a._matrix[2] + b._matrix[2];
            m._matrix[3] = a._matrix[3] + b._matrix[3];
            m._matrix[4] = a._matrix[4] + b._matrix[4];
            m._matrix[5] = a._matrix[5] + b._matrix[5];
            m._matrix[6] = a._matrix[6] + b._matrix[6];
            m._matrix[7] = a._matrix[7] + b._matrix[7];
            m._matrix[8] = a._matrix[8] + b._matrix[8];
            m._matrix[9] = a._matrix[9] + b._matrix[9];
            m._matrix[10] = a._matrix[10] + b._matrix[10];
            m._matrix[11] = a._matrix[11] + b._matrix[11];

            return m;
        }
        public static Matrix3x4 operator -(Matrix3x4 a, Matrix3x4 b)
        {
            Matrix3x4 m = new Matrix3x4();

            m._matrix[0] = a._matrix[0] - b._matrix[0];
            m._matrix[1] = a._matrix[1] - b._matrix[1];
            m._matrix[2] = a._matrix[2] - b._matrix[2];
            m._matrix[3] = a._matrix[3] - b._matrix[3];
            m._matrix[4] = a._matrix[4] - b._matrix[4];
            m._matrix[5] = a._matrix[5] - b._matrix[5];
            m._matrix[6] = a._matrix[6] - b._matrix[6];
            m._matrix[7] = a._matrix[7] - b._matrix[7];
            m._matrix[8] = a._matrix[8] - b._matrix[8];
            m._matrix[9] = a._matrix[9] - b._matrix[9];
            m._matrix[10] = a._matrix[10] - b._matrix[10];
            m._matrix[11] = a._matrix[11] - b._matrix[11];
            m._matrix[12] = a._matrix[12] - b._matrix[12];

            return m;
        }
        
        public static Matrix3x2 operator *(Matrix3x4 a, Matrix4x2 b)
        {
            Matrix3x2 m = new Matrix3x2();

            m._matrix[0] = (a._matrix[0] * b._matrix[0]) + (a._matrix[1] * b._matrix[2]) + (a._matrix[2] * b._matrix[4]) + (a._matrix[3] * b._matrix[6]);
            m._matrix[1] = (a._matrix[0] * b._matrix[1]) + (a._matrix[1] * b._matrix[3]) + (a._matrix[2] * b._matrix[5]) + (a._matrix[3] * b._matrix[7]);
            
            m._matrix[2] = (a._matrix[4] * b._matrix[0]) + (a._matrix[5] * b._matrix[2]) + (a._matrix[6] * b._matrix[4]) + (a._matrix[7] * b._matrix[6]);
            m._matrix[3] = (a._matrix[4] * b._matrix[1]) + (a._matrix[5] * b._matrix[3]) + (a._matrix[6] * b._matrix[5]) + (a._matrix[7] * b._matrix[7]);
            
            m._matrix[4] = (a._matrix[8] * b._matrix[0]) + (a._matrix[9] * b._matrix[2]) + (a._matrix[10] * b._matrix[4]) + (a._matrix[11] * b._matrix[6]);
            m._matrix[5] = (a._matrix[8] * b._matrix[1]) + (a._matrix[9] * b._matrix[3]) + (a._matrix[10] * b._matrix[5]) + (a._matrix[11] * b._matrix[7]);
            
            return m;
        }

        public static Matrix3 operator *(Matrix3x4 a, Matrix4x3 b)
        {
            Matrix3 m = new Matrix3();

            m._matrix[0] = (a._matrix[0] * b._matrix[0]) + (a._matrix[1] * b._matrix[3]) + (a._matrix[2] * b._matrix[6]) + (a._matrix[3] * b._matrix[9]);
            m._matrix[1] = (a._matrix[0] * b._matrix[1]) + (a._matrix[1] * b._matrix[4]) + (a._matrix[2] * b._matrix[7]) + (a._matrix[3] * b._matrix[10]);
            m._matrix[2] = (a._matrix[0] * b._matrix[2]) + (a._matrix[1] * b._matrix[5]) + (a._matrix[2] * b._matrix[8]) + (a._matrix[3] * b._matrix[11]);
            
            m._matrix[3] = (a._matrix[4] * b._matrix[0]) + (a._matrix[5] * b._matrix[3]) + (a._matrix[6] * b._matrix[6]) + (a._matrix[7] * b._matrix[9]);
            m._matrix[4] = (a._matrix[4] * b._matrix[1]) + (a._matrix[5] * b._matrix[4]) + (a._matrix[6] * b._matrix[7]) + (a._matrix[7] * b._matrix[10]);
            m._matrix[5] = (a._matrix[4] * b._matrix[2]) + (a._matrix[5] * b._matrix[5]) + (a._matrix[6] * b._matrix[8]) + (a._matrix[7] * b._matrix[11]);
            
            m._matrix[6] = (a._matrix[8] * b._matrix[0]) + (a._matrix[9] * b._matrix[3]) + (a._matrix[10] * b._matrix[6]) + (a._matrix[11] * b._matrix[9]);
            m._matrix[7] = (a._matrix[8] * b._matrix[1]) + (a._matrix[9] * b._matrix[4]) + (a._matrix[10] * b._matrix[7]) + (a._matrix[11] * b._matrix[10]);
            m._matrix[8] = (a._matrix[8] * b._matrix[2]) + (a._matrix[9] * b._matrix[5]) + (a._matrix[10] * b._matrix[8]) + (a._matrix[11] * b._matrix[11]);
            
            return m;
        }

        public static Matrix3x4 operator *(Matrix3x4 a, Matrix4 b)
        {
            Matrix3x4 m = new Matrix3x4();

            m._matrix[0] = (a._matrix[0] * b._matrix[0]) + (a._matrix[1] * b._matrix[4]) + (a._matrix[2] * b._matrix[8]) + (a._matrix[3] * b._matrix[12]);
            m._matrix[1] = (a._matrix[0] * b._matrix[1]) + (a._matrix[1] * b._matrix[5]) + (a._matrix[2] * b._matrix[9]) + (a._matrix[3] * b._matrix[13]);
            m._matrix[2] = (a._matrix[0] * b._matrix[2]) + (a._matrix[1] * b._matrix[6]) + (a._matrix[2] * b._matrix[10]) + (a._matrix[3] * b._matrix[14]);
            m._matrix[3] = (a._matrix[0] * b._matrix[3]) + (a._matrix[1] * b._matrix[7]) + (a._matrix[2] * b._matrix[11]) + (a._matrix[3] * b._matrix[15]);
            
            m._matrix[4] = (a._matrix[4] * b._matrix[0]) + (a._matrix[5] * b._matrix[4]) + (a._matrix[6] * b._matrix[8]) + (a._matrix[7] * b._matrix[12]);
            m._matrix[5] = (a._matrix[4] * b._matrix[1]) + (a._matrix[5] * b._matrix[5]) + (a._matrix[6] * b._matrix[9]) + (a._matrix[7] * b._matrix[13]);
            m._matrix[6] = (a._matrix[4] * b._matrix[2]) + (a._matrix[5] * b._matrix[6]) + (a._matrix[6] * b._matrix[10]) + (a._matrix[7] * b._matrix[14]);
            m._matrix[7] = (a._matrix[4] * b._matrix[3]) + (a._matrix[5] * b._matrix[7]) + (a._matrix[6] * b._matrix[11]) + (a._matrix[7] * b._matrix[15]);
            
            m._matrix[8] = (a._matrix[8] * b._matrix[0]) + (a._matrix[9] * b._matrix[4]) + (a._matrix[10] * b._matrix[8]) + (a._matrix[11] * b._matrix[12]);
            m._matrix[9] = (a._matrix[8] * b._matrix[1]) + (a._matrix[9] * b._matrix[5]) + (a._matrix[10] * b._matrix[9]) + (a._matrix[11] * b._matrix[13]);
            m._matrix[10] = (a._matrix[8] * b._matrix[2]) + (a._matrix[9] * b._matrix[6]) + (a._matrix[10] * b._matrix[10]) + (a._matrix[11] * b._matrix[14]);
            m._matrix[11] = (a._matrix[8] * b._matrix[3]) + (a._matrix[9] * b._matrix[7]) + (a._matrix[10] * b._matrix[11]) + (a._matrix[11] * b._matrix[15]);
            
            return m;
        }
        
        private static Matrix3x4 _zero = new Matrix3x4(Vector4.Zero, Vector4.Zero, Vector4.Zero);
        public static ref Matrix3x4 Zero => ref _zero;

        private static Matrix3x4 _identity = new Matrix3x4(new Vector4(1, 0, 0, 0), new Vector4(0, 1, 0, 0), new Vector4(0, 0, 1, 0));
        public static ref Matrix3x4 Identity => ref _identity;
        
        public static Matrix3x4 CreateRotation(Vector3 axis, Radian angle)
        {
            // normalize and create a local copy of the vector.
            axis.Normalise();
            floatv axisX = axis.X;
            floatv axisY = axis.Y;
            floatv axisZ = axis.Z;

            // calculate angles
            floatv cos = Maths.Cos(-angle);
            floatv sin = Maths.Sin(-angle);
            floatv t = 1.0f - cos;

            // do the conversion math once
            floatv tXX = t * axisX * axisX;
            floatv tXY = t * axisX * axisY;
            floatv tXZ = t * axisX * axisZ;
            floatv tYY = t * axisY * axisY;
            floatv tYZ = t * axisY * axisZ;
            floatv tZZ = t * axisZ * axisZ;

            floatv sinX = sin * axisX;
            floatv sinY = sin * axisY;
            floatv sinZ = sin * axisZ;

            return new Matrix3x4(
                new Vector4(tXX + cos, tXY - sinZ, tXZ + sinY, 0),
                new Vector4(tXY + sinZ, tYY + cos, tYZ - sinX, 0),
                new Vector4(tXZ - sinY, tYZ + sinX, tZZ + cos, 0));
        }

        public static Matrix3x4 CreateRotationX(Radian angle)
        {
            floatv cos = Maths.Cos(angle);
            floatv sin = Maths.Sin(angle);

            return new Matrix3x4(
                new Vector4(1, 0, 0, 0),
                new Vector4(0, cos, sin, 0),
                new Vector4(0, -sin, cos, 0));
        }
        public static Matrix3x4 CreateRotationY(Radian angle)
        {
            floatv cos = Maths.Cos(angle);
            floatv sin = Maths.Sin(angle);

            return new Matrix3x4(
                new Vector4(cos, 0, -sin, 0),
                new Vector4(0, 1, 0, 0),
                new Vector4(sin, 0, cos, 0));
        }
        public static Matrix3x4 CreateRotationZ(Radian angle)
        {
            floatv cos = Maths.Cos(angle);
            floatv sin = Maths.Sin(angle);

            return new Matrix3x4(
                new Vector4(cos, sin, 0, 0),
                new Vector4(-sin, cos, 0, 0),
                new Vector4(0, 0, 1, 0));
        }

        public static Matrix3x4 CreateScale(floatv scale)
        {
            return new Matrix3x4(
                new Vector4(scale, 0, 0, 0),
                new Vector4(0, scale, 0, 0),
                new Vector4(0, 0, scale, 0));
        }
        public static Matrix3x4 CreateScale(floatv scaleX, floatv scaleY, floatv scaleZ)
        {
            return new Matrix3x4(
                new Vector4(scaleX, 0, 0, 0),
                new Vector4(0, scaleY, 0, 0),
                new Vector4(0, 0, scaleZ, 0));
        }
        public static Matrix3x4 CreateScale(floatv scaleX, floatv scaleY)
        {
            return new Matrix3x4(
                new Vector4(scaleX, 0, 0, 0),
                new Vector4(0, scaleY, 0, 0),
                new Vector4(0, 0, 1, 0));
        }
        public static Matrix3x4 CreateScale(Vector3 scale) => CreateScale(scale.X, scale.Y, scale.Z);
        public static Matrix3x4 CreateScale(Vector2 scale) => CreateScale(scale.X, scale.Y);

        public static Matrix3x4 CreateTranslation(floatv xy)
        {
            return new Matrix3x4(
                new Vector4(1, 0, 0, 0),
                new Vector4(0, 1, 0, 0),
                new Vector4(xy, xy, 1, 0));
        }
        public static Matrix3x4 CreateTranslation(floatv x, floatv y)
        {
            return new Matrix3x4(
                new Vector4(1, 0, 0, 0),
                new Vector4(0, 1, 0, 0),
                new Vector4(x, y, 1, 0));
        }
        public static Matrix3x4 CreateTranslation(Vector2 xy) => CreateTranslation(xy.X, xy.Y);

        public static Matrix3x4 CreateBox(Box box)
        {
            return new Matrix3x4(
                new Vector4(box.Width, 0, 0, 0),
                new Vector4(0, box.Height, 0, 0),
                new Vector4(box.X, box.Y, 1, 0));
        }
    }
}
