﻿using System;

namespace Zene.Structs
{
    public unsafe struct Matrix4x2 : IMatrix
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
        
        public Matrix4x2(params floatv[] matrix)
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
            fixed (void* ptr = _matrix)
            {
                matrix.MatrixData(new MatrixSpan(4, 2, new Span<floatv>(ptr, 8)));
            }
        }

        internal fixed floatv _matrix[8];

        public floatv this[int x, int y]
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

        public floatv Trace() => _matrix[0] + _matrix[3];

        public Matrix2x4 Transpose() => new Matrix2x4(Column0, Column1);

        public override bool Equals(object obj)
        {
            return obj is Matrix4x2 matrix && this == matrix;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_matrix[0], _matrix[1], _matrix[2], _matrix[3], _matrix[4], _matrix[5], _matrix[6], _matrix[7]);
        }

        public void MatrixData(MatrixSpan ms)
        {
            fixed (void* ptr = _matrix)
            {
                Span<floatv> s = new Span<floatv>(ptr, 8);
                ms.Fill(s, 4, 2);
            }
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
            return a._matrix[0] == b._matrix[0] &&
                a._matrix[1] == b._matrix[1] &&
                a._matrix[2] == b._matrix[2] &&
                a._matrix[3] == b._matrix[3] &&
                a._matrix[4] == b._matrix[4] &&
                a._matrix[5] == b._matrix[5] &&
                a._matrix[6] == b._matrix[6] &&
                a._matrix[7] == b._matrix[7];
        }

        public static bool operator !=(Matrix4x2 a, Matrix4x2 b) => !(a == b);

        public static MultiplyMatrix operator *(Matrix4x2 a, IMatrix b) => new MultiplyMatrix(a, b);
        
        public static Matrix4x2 operator *(Matrix4x2 a, floatv b)
        {
            Matrix4x2 m = new Matrix4x2();

            m._matrix[0] = a._matrix[0] * b;
            m._matrix[1] = a._matrix[1] * b;
            m._matrix[2] = a._matrix[2] * b;
            m._matrix[3] = a._matrix[3] * b;
            m._matrix[4] = a._matrix[4] * b;
            m._matrix[5] = a._matrix[5] * b;
            m._matrix[6] = a._matrix[6] * b;
            m._matrix[7] = a._matrix[7] * b;

            return m;
        }
        public static Matrix4x2 operator *(floatv b, Matrix4x2 a)
        {
            Matrix4x2 m = new Matrix4x2();

            m._matrix[0] = a._matrix[0] * b;
            m._matrix[1] = a._matrix[1] * b;
            m._matrix[2] = a._matrix[2] * b;
            m._matrix[3] = a._matrix[3] * b;
            m._matrix[4] = a._matrix[4] * b;
            m._matrix[5] = a._matrix[5] * b;
            m._matrix[6] = a._matrix[6] * b;
            m._matrix[7] = a._matrix[7] * b;

            return m;
        }
        
        public static Matrix4x2 operator +(Matrix4x2 a, Matrix4x2 b)
        {
            Matrix4x2 m = new Matrix4x2();

            m._matrix[0] = a._matrix[0] + b._matrix[0];
            m._matrix[1] = a._matrix[1] + b._matrix[1];
            m._matrix[2] = a._matrix[2] + b._matrix[2];
            m._matrix[3] = a._matrix[3] + b._matrix[3];
            m._matrix[4] = a._matrix[4] + b._matrix[4];
            m._matrix[5] = a._matrix[5] + b._matrix[5];
            m._matrix[6] = a._matrix[6] + b._matrix[6];
            m._matrix[7] = a._matrix[7] + b._matrix[7];

            return m;
        }
        public static Matrix4x2 operator -(Matrix4x2 a, Matrix4x2 b)
        {
            Matrix4x2 m = new Matrix4x2();

            m._matrix[0] = a._matrix[0] - b._matrix[0];
            m._matrix[1] = a._matrix[1] - b._matrix[1];
            m._matrix[2] = a._matrix[2] - b._matrix[2];
            m._matrix[3] = a._matrix[3] - b._matrix[3];
            m._matrix[4] = a._matrix[4] - b._matrix[4];
            m._matrix[5] = a._matrix[5] - b._matrix[5];
            m._matrix[6] = a._matrix[6] - b._matrix[6];
            m._matrix[7] = a._matrix[7] - b._matrix[7];

            return m;
        }
        
        public static Matrix4x2 operator *(Matrix4x2 a, Matrix2 b)
        {
            Matrix4x2 m = new Matrix4x2();

            m._matrix[0] = (a._matrix[0] * b._matrix[0]) + (a._matrix[1] * b._matrix[2]);
            m._matrix[1] = (a._matrix[0] * b._matrix[1]) + (a._matrix[1] * b._matrix[3]);
            
            m._matrix[2] = (a._matrix[2] * b._matrix[0]) + (a._matrix[3] * b._matrix[2]);
            m._matrix[3] = (a._matrix[2] * b._matrix[1]) + (a._matrix[3] * b._matrix[3]);
            
            m._matrix[4] = (a._matrix[4] * b._matrix[0]) + (a._matrix[5] * b._matrix[2]);
            m._matrix[5] = (a._matrix[4] * b._matrix[1]) + (a._matrix[5] * b._matrix[3]);
            
            m._matrix[6] = (a._matrix[6] * b._matrix[0]) + (a._matrix[7] * b._matrix[2]);
            m._matrix[7] = (a._matrix[6] * b._matrix[1]) + (a._matrix[7] * b._matrix[3]);

            return m;
        }

        public static Matrix4x3 operator *(Matrix4x2 a, Matrix2x3 b)
        {
            Matrix4x3 m = new Matrix4x3();

            m._matrix[0] = (a._matrix[0] * b._matrix[0]) + (a._matrix[1] * b._matrix[3]);
            m._matrix[1] = (a._matrix[0] * b._matrix[1]) + (a._matrix[1] * b._matrix[4]);
            m._matrix[2] = (a._matrix[0] * b._matrix[2]) + (a._matrix[1] * b._matrix[5]);
            
            m._matrix[3] = (a._matrix[2] * b._matrix[0]) + (a._matrix[3] * b._matrix[3]);
            m._matrix[4] = (a._matrix[2] * b._matrix[1]) + (a._matrix[3] * b._matrix[4]);
            m._matrix[5] = (a._matrix[2] * b._matrix[2]) + (a._matrix[3] * b._matrix[5]);
            
            m._matrix[6] = (a._matrix[4] * b._matrix[0]) + (a._matrix[5] * b._matrix[3]);
            m._matrix[7] = (a._matrix[4] * b._matrix[1]) + (a._matrix[5] * b._matrix[4]);
            m._matrix[8] = (a._matrix[4] * b._matrix[2]) + (a._matrix[5] * b._matrix[5]);
            
            m._matrix[9] = (a._matrix[6] * b._matrix[0]) + (a._matrix[7] * b._matrix[3]);
            m._matrix[10] = (a._matrix[6] * b._matrix[1]) + (a._matrix[7] * b._matrix[4]);
            m._matrix[11] = (a._matrix[6] * b._matrix[2]) + (a._matrix[7] * b._matrix[5]);

            return m;
        }

        public static Matrix4 operator *(Matrix4x2 a, Matrix2x4 b)
        {
            Matrix4 m = new Matrix4();

            m._matrix[0] = (a._matrix[0] * b._matrix[0]) + (a._matrix[1] * b._matrix[4]);
            m._matrix[1] = (a._matrix[0] * b._matrix[1]) + (a._matrix[1] * b._matrix[5]);
            m._matrix[2] = (a._matrix[0] * b._matrix[2]) + (a._matrix[1] * b._matrix[6]);
            m._matrix[3] = (a._matrix[0] * b._matrix[3]) + (a._matrix[1] * b._matrix[7]);
            
            m._matrix[4] = (a._matrix[2] * b._matrix[0]) + (a._matrix[3] * b._matrix[4]);
            m._matrix[5] = (a._matrix[2] * b._matrix[1]) + (a._matrix[3] * b._matrix[5]);
            m._matrix[6] = (a._matrix[2] * b._matrix[2]) + (a._matrix[3] * b._matrix[6]);
            m._matrix[7] = (a._matrix[2] * b._matrix[3]) + (a._matrix[3] * b._matrix[7]);
            
            m._matrix[8] = (a._matrix[4] * b._matrix[0]) + (a._matrix[5] * b._matrix[4]);
            m._matrix[9] = (a._matrix[4] * b._matrix[1]) + (a._matrix[5] * b._matrix[5]);
            m._matrix[10] = (a._matrix[4] * b._matrix[2]) + (a._matrix[5] * b._matrix[6]);
            m._matrix[11] = (a._matrix[4] * b._matrix[3]) + (a._matrix[5] * b._matrix[7]);
            
            m._matrix[12] = (a._matrix[6] * b._matrix[0]) + (a._matrix[7] * b._matrix[4]);
            m._matrix[13] = (a._matrix[6] * b._matrix[1]) + (a._matrix[7] * b._matrix[5]);
            m._matrix[14] = (a._matrix[6] * b._matrix[2]) + (a._matrix[7] * b._matrix[6]);
            m._matrix[15] = (a._matrix[6] * b._matrix[3]) + (a._matrix[7] * b._matrix[7]);

            return m;
        }
        
        private static Matrix4x2 _zero = new Matrix4x2(Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero);
        public static ref Matrix4x2 Zero => ref _zero;

        private static Matrix4x2 _identity = new Matrix4x2(new Vector2(1, 0), new Vector2(0, 1), Vector2.Zero, Vector2.Zero);
        public static ref Matrix4x2 Identity => ref _identity;
        
        public static Matrix4x2 CreateRotation(Radian angle)
        {
            floatv cos = Maths.Cos(angle);
            floatv sin = Maths.Sin(angle);

            return new Matrix4x2(new Vector2(cos, sin), new Vector2(-sin, cos), Vector2.Zero, Vector2.Zero);
        }

        public static Matrix4x2 CreateScale(floatv scale)
        {
            return new Matrix4x2(new Vector2(scale, 0), new Vector2(0, scale), Vector2.Zero, Vector2.Zero);
        }
        public static Matrix4x2 CreateScale(floatv scaleX, floatv scaleY)
        {
            return new Matrix4x2(new Vector2(scaleX, 0), new Vector2(0, scaleY), Vector2.Zero, Vector2.Zero);
        }
        public static Matrix4x2 CreateScale(Vector2 scale) => CreateScale(scale.X, scale.Y);

        public static Matrix4x2 CreateTranslation(floatv xy)
        {
            return new Matrix4x2(
                new Vector2(1, 0),
                new Vector2(0, 1),
                Vector2.Zero,
                new Vector2(xy, xy));
        }
        public static Matrix4x2 CreateTranslation(floatv x, floatv y)
        {
            return new Matrix4x2(
                new Vector2(1, 0),
                new Vector2(0, 1),
                Vector2.Zero,
                new Vector2(x, y));
        }
        public static Matrix4x2 CreateTranslation(Vector2 xy) => CreateTranslation(xy.X, xy.Y);

        public static Matrix4x2 CreateBox(Box box)
        {
            return new Matrix4x2(
                new Vector2(box.Width, 0),
                new Vector2(0, box.Height),
                new Vector2(0, 0),
                new Vector2(box.X, box.Y));
        }
    }
}
