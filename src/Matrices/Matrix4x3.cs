using System;

namespace Zene.Structs
{
    public unsafe struct Matrix4x3 : IMatrix
    {
        public int Rows => 4;
        public int Columns => 3;
        
        public bool Constant => true;
        
        public Matrix4x3(Vector3 row0, Vector3 row1, Vector3 row2, Vector3 row3)
        {
            Row0 = row0;
            Row1 = row1;
            Row2 = row2;
            Row3 = row3;
        }

        public Matrix4x3(params floatv[] matrix)
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
        public Matrix4x3(IMatrix matrix)
        {
            fixed (void* ptr = _matrix)
            {
                matrix.MatrixData(new MatrixSpan(4, 3, new Span<floatv>(ptr, 12)));
            }
        }

        internal fixed floatv _matrix[12];

        public floatv this[int x, int y]
        {
            get
            {
                if (x >= Columns || y >= Rows)
                {
                    throw new IndexOutOfRangeException($"X: {x} and Y: {y} are outside the {Columns} x {Rows} range of {nameof(Matrix4x3)}.");
                }

                return _matrix[x + (y * Columns)];
            }
            set
            {
                if (x >= Columns || y >= Rows)
                {
                    throw new IndexOutOfRangeException($"X: {x} and Y: {y} are outside the {Columns} x {Rows} range of {nameof(Matrix4x3)}.");
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

        public floatv Trace() => _matrix[0] + _matrix[4] + _matrix[8];

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
            return obj is Matrix4x3 matrix && this == matrix;
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
                ms.Fill(s, 4, 3);
            }
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
                a._matrix[11] == b._matrix[12];
        }

        public static bool operator !=(Matrix4x3 a, Matrix4x3 b) => !(a == b);

        public static MultiplyMatrix operator *(Matrix4x3 a, IMatrix b) => new MultiplyMatrix(a, b);
        
        public static Matrix4x3 operator *(Matrix4x3 a, floatv b)
        {
            Matrix4x3 m = new Matrix4x3();

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
        public static Matrix4x3 operator *(floatv b, Matrix4x3 a)
        {
            Matrix4x3 m = new Matrix4x3();

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
        
        public static Matrix4x3 operator +(Matrix4x3 a, Matrix4x3 b)
        {
            Matrix4x3 m = new Matrix4x3();

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
        public static Matrix4x3 operator -(Matrix4x3 a, Matrix4x3 b)
        {
            Matrix4x3 m = new Matrix4x3();

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

            return m;
        }
        
        public static Matrix4x3 operator *(Matrix4x3 a, Matrix3 b)
        {
            Matrix4x3 m = new Matrix4x3();

            m._matrix[0] = (a._matrix[0] * b._matrix[0]) + (a._matrix[1] * b._matrix[3]) + (a._matrix[2] * b._matrix[6]);
            m._matrix[1] = (a._matrix[0] * b._matrix[1]) + (a._matrix[1] * b._matrix[4]) + (a._matrix[2] * b._matrix[7]);
            m._matrix[2] = (a._matrix[0] * b._matrix[2]) + (a._matrix[1] * b._matrix[5]) + (a._matrix[2] * b._matrix[8]);
            
            m._matrix[3] = (a._matrix[3] * b._matrix[0]) + (a._matrix[4] * b._matrix[3]) + (a._matrix[5] * b._matrix[6]);
            m._matrix[4] = (a._matrix[3] * b._matrix[1]) + (a._matrix[4] * b._matrix[4]) + (a._matrix[5] * b._matrix[7]);
            m._matrix[5] = (a._matrix[3] * b._matrix[2]) + (a._matrix[4] * b._matrix[5]) + (a._matrix[5] * b._matrix[8]);
            
            m._matrix[6] = (a._matrix[6] * b._matrix[0]) + (a._matrix[7] * b._matrix[3]) + (a._matrix[8] * b._matrix[6]);
            m._matrix[7] = (a._matrix[6] * b._matrix[1]) + (a._matrix[7] * b._matrix[4]) + (a._matrix[8] * b._matrix[7]);
            m._matrix[8] = (a._matrix[6] * b._matrix[2]) + (a._matrix[7] * b._matrix[5]) + (a._matrix[8] * b._matrix[8]);
            
            m._matrix[9] = (a._matrix[9] * b._matrix[0]) + (a._matrix[10] * b._matrix[3]) + (a._matrix[11] * b._matrix[6]);
            m._matrix[10] = (a._matrix[9] * b._matrix[1]) + (a._matrix[10] * b._matrix[4]) + (a._matrix[11] * b._matrix[7]);
            m._matrix[11] = (a._matrix[9] * b._matrix[2]) + (a._matrix[10] * b._matrix[5]) + (a._matrix[11] * b._matrix[8]);
            
            return m;
        }

        public static Matrix4x2 operator *(Matrix4x3 a, Matrix3x2 b)
        {
            Matrix4x2 m = new Matrix4x2();

            m._matrix[0] = (a._matrix[0] * b._matrix[0]) + (a._matrix[1] * b._matrix[2]) + (a._matrix[2] * b._matrix[4]);
            m._matrix[1] = (a._matrix[0] * b._matrix[1]) + (a._matrix[1] * b._matrix[3]) + (a._matrix[2] * b._matrix[5]);
            
            m._matrix[2] = (a._matrix[3] * b._matrix[0]) + (a._matrix[4] * b._matrix[2]) + (a._matrix[5] * b._matrix[4]);
            m._matrix[3] = (a._matrix[3] * b._matrix[1]) + (a._matrix[4] * b._matrix[3]) + (a._matrix[5] * b._matrix[5]);
            
            m._matrix[4] = (a._matrix[6] * b._matrix[0]) + (a._matrix[7] * b._matrix[2]) + (a._matrix[8] * b._matrix[4]);
            m._matrix[5] = (a._matrix[6] * b._matrix[1]) + (a._matrix[7] * b._matrix[3]) + (a._matrix[8] * b._matrix[5]);
            
            m._matrix[6] = (a._matrix[9] * b._matrix[0]) + (a._matrix[10] * b._matrix[2]) + (a._matrix[11] * b._matrix[4]);
            m._matrix[7] = (a._matrix[9] * b._matrix[1]) + (a._matrix[10] * b._matrix[3]) + (a._matrix[11] * b._matrix[5]);
            
            return m;
        }

        public static Matrix4 operator *(Matrix4x3 a, Matrix3x4 b)
        {
            Matrix4 m = new Matrix4();

            m._matrix[0] = (a._matrix[0] * b._matrix[0]) + (a._matrix[1] * b._matrix[4]) + (a._matrix[2] * b._matrix[8]);
            m._matrix[1] = (a._matrix[0] * b._matrix[1]) + (a._matrix[1] * b._matrix[5]) + (a._matrix[2] * b._matrix[9]);
            m._matrix[2] = (a._matrix[0] * b._matrix[2]) + (a._matrix[1] * b._matrix[6]) + (a._matrix[2] * b._matrix[10]);
            m._matrix[3] = (a._matrix[0] * b._matrix[3]) + (a._matrix[1] * b._matrix[7]) + (a._matrix[2] * b._matrix[11]);
            
            m._matrix[4] = (a._matrix[3] * b._matrix[0]) + (a._matrix[4] * b._matrix[4]) + (a._matrix[5] * b._matrix[8]);
            m._matrix[5] = (a._matrix[3] * b._matrix[1]) + (a._matrix[4] * b._matrix[5]) + (a._matrix[5] * b._matrix[9]);
            m._matrix[6] = (a._matrix[3] * b._matrix[2]) + (a._matrix[4] * b._matrix[6]) + (a._matrix[5] * b._matrix[10]);
            m._matrix[7] = (a._matrix[3] * b._matrix[3]) + (a._matrix[4] * b._matrix[7]) + (a._matrix[5] * b._matrix[11]);
            
            m._matrix[8] = (a._matrix[6] * b._matrix[0]) + (a._matrix[7] * b._matrix[4]) + (a._matrix[8] * b._matrix[8]);
            m._matrix[9] = (a._matrix[6] * b._matrix[1]) + (a._matrix[7] * b._matrix[5]) + (a._matrix[8] * b._matrix[9]);
            m._matrix[10] = (a._matrix[6] * b._matrix[2]) + (a._matrix[7] * b._matrix[6]) + (a._matrix[8] * b._matrix[10]);
            m._matrix[11] = (a._matrix[6] * b._matrix[3]) + (a._matrix[7] * b._matrix[7]) + (a._matrix[8] * b._matrix[11]);
            
            m._matrix[12] = (a._matrix[9] * b._matrix[0]) + (a._matrix[10] * b._matrix[4]) + (a._matrix[11] * b._matrix[8]);
            m._matrix[13] = (a._matrix[9] * b._matrix[1]) + (a._matrix[10] * b._matrix[5]) + (a._matrix[11] * b._matrix[9]);
            m._matrix[14] = (a._matrix[9] * b._matrix[2]) + (a._matrix[10] * b._matrix[6]) + (a._matrix[11] * b._matrix[10]);
            m._matrix[15] = (a._matrix[9] * b._matrix[3]) + (a._matrix[10] * b._matrix[7]) + (a._matrix[11] * b._matrix[11]);
            
            return m;
        }
        
        private static Matrix4x3 _zero = new Matrix4x3(Vector3.Zero, Vector3.Zero, Vector3.Zero, Vector3.Zero);
        public static ref Matrix4x3 Zero => ref _zero;

        private static Matrix4x3 _identity = new Matrix4x3(new Vector3(1, 0, 0), new Vector3(0, 1, 0), new Vector3(0, 0, 1), Vector3.Zero);
        public static ref Matrix4x3 Identity => ref _identity;
        
        public static Matrix4x3 CreateScale(floatv scale)
        {
            return new Matrix4x3(
                new Vector3(scale, 0, 0),
                new Vector3(0, scale, 0),
                new Vector3(0, 0, scale),
                Vector3.Zero);
        }
        public static Matrix4x3 CreateScale(floatv scaleX, floatv scaleY, floatv scaleZ)
        {
            return new Matrix4x3(
                new Vector3(scaleX, 0, 0),
                new Vector3(0, scaleY, 0),
                new Vector3(0, 0, scaleZ),
                Vector3.Zero);
        }
        public static Matrix4x3 CreateScale(floatv scaleX, floatv scaleY)
        {
            return new Matrix4x3(
                new Vector3(scaleX, 0, 0),
                new Vector3(0, scaleY, 0),
                new Vector3(0, 0, 1),
                Vector3.Zero);
        }
        public static Matrix4x3 CreateScale(Vector3 scale) => CreateScale(scale.X, scale.Y, scale.Z);
        public static Matrix4x3 CreateScale(Vector2 scale) => CreateScale(scale.X, scale.Y);

        public static Matrix4x3 CreateTranslation(floatv xyz)
        {
            return new Matrix4x3(
                new Vector3(1, 0, 0),
                new Vector3(0, 1, 0),
                new Vector3(0, 0, 1),
                new Vector3(xyz));
        }
        public static Matrix4x3 CreateTranslation(floatv x, floatv y, floatv z)
        {
            return new Matrix4x3(
                new Vector3(1, 0, 0),
                new Vector3(0, 1, 0),
                new Vector3(0, 0, 1),
                new Vector3(x, y, z));
        }
        public static Matrix4x3 CreateTranslation(floatv x, floatv y)
        {
            return new Matrix4x3(
                new Vector3(1, 0, 0),
                new Vector3(0, 1, 0),
                new Vector3(0, 0, 1),
                new Vector3(x, y, 0));
        }
        public static Matrix4x3 CreateTranslation(Vector3 xyz) => CreateTranslation(xyz.X, xyz.Y, xyz.Z);
        public static Matrix4x3 CreateTranslation(Vector2 xy) => CreateTranslation(xy.X, xy.Y);

        public static Matrix4x3 CreateBox(Box box)
        {
            return new Matrix4x3(
                new Vector3(box.Width, 0, 0),
                new Vector3(0, box.Height, 0),
                new Vector3(0, 0, 1),
                new Vector3(box.X, box.Y, 0));
        }
        public static Matrix4x3 CreateBox(Box box, floatv depth)
        {
            return new Matrix4x3(
                new Vector3(box.Width, 0, 0),
                new Vector3(0, box.Height, 0),
                new Vector3(0, 0, 1),
                new Vector3(box.X, box.Y, depth));
        }
        //public static Matrix4x3 CreateBox(IBox3 box)
        //{
        //    Vector3 c = box.Centre;

        //    return new Matrix4x3(
        //        new Vector3(box.Width, 0, 0),
        //        new Vector3(0, box.Height, 0),
        //        new Vector3(0, 0, box.Depth),
        //        new Vector3(c.X, c.Y, c.Z));
        //}

        public static Matrix4x3 CreateRotationX(Radian angle)
        {
            floatv cos = Maths.Cos(angle);
            floatv sin = Maths.Sin(angle);

            return new Matrix4x3(
                new Vector3(1, 0, 0),
                new Vector3(0, cos, sin),
                new Vector3(0, -sin, cos),
                Vector3.Zero);
        }
        public static Matrix4x3 CreateRotationY(Radian angle)
        {
            floatv cos = Maths.Cos(angle);
            floatv sin = Maths.Sin(angle);

            return new Matrix4x3(
                new Vector3(cos, 0, -sin),
                new Vector3(0, 1, 0),
                new Vector3(sin, 0, cos),
                Vector3.Zero);
        }
        public static Matrix4x3 CreateRotationZ(Radian angle)
        {
            floatv cos = Maths.Cos(angle);
            floatv sin = Maths.Sin(angle);

            return new Matrix4x3(
                new Vector3(cos, sin, 0),
                new Vector3(-sin, cos, 0),
                new Vector3(0, 0, 1),
                Vector3.Zero);
        }

        public static Matrix4x3 CreateRotation(Vector3 axis, Radian angle)
        {
            axis.Normalise();
            floatv axisX = axis.X, axisY = axis.Y, axisZ = axis.Z;

            floatv cos = Maths.Cos(-angle);
            floatv sin = Maths.Sin(-angle);
            floatv t = 1.0f - cos;

            floatv tXX = t * axisX * axisX;
            floatv tXY = t * axisX * axisY;
            floatv tXZ = t * axisX * axisZ;
            floatv tYY = t * axisY * axisY;
            floatv tYZ = t * axisY * axisZ;
            floatv tZZ = t * axisZ * axisZ;

            floatv sinX = sin * axisX;
            floatv sinY = sin * axisY;
            floatv sinZ = sin * axisZ;

            return new Matrix4x3(
                new Vector3(tXX + cos, tXY - sinZ, tXZ + sinY),
                new Vector3(tXY + sinZ, tYY + cos, tYZ - sinX),
                new Vector3(tXZ - sinY, tYZ + sinX, tZZ + cos),
                Vector3.Zero);
        }
    }
}
