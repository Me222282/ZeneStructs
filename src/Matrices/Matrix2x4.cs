using System;

namespace Zene.Structs
{
    public unsafe struct Matrix2x4 : IMatrix
    {
        public int Rows => 2;
        public int Columns => 4;
        
        public bool Constant => true;
        
        public Matrix2x4(Vector4 row0, Vector4 row1)
        {
            Row0 = row0;
            Row1 = row1;
        }

        public Matrix2x4(params double[] matrix)
        {
            if (matrix.Length < (Rows * Columns))
            {
                throw new Exception("Matrix needs to have at least 2 rows and 4 columns.");
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
        public Matrix2x4(IMatrix matrix)
        {
            fixed (void* ptr = _matrix)
            {
                matrix.MatrixData(new MatrixSpan(2, 4, new Span<double>(ptr, 8)));
            }
        }

        internal fixed double _matrix[8];

        public double this[int x, int y]
        {
            get
            {
                if (x >= Columns || y >= Rows)
                {
                    throw new IndexOutOfRangeException($"X: {x} and Y: {y} are outside the {Columns} x {Rows} range of {nameof(Matrix2x4)}.");
                }

                return _matrix[x + (y * Columns)];
            }
            set
            {
                if (x >= Columns || y >= Rows)
                {
                    throw new IndexOutOfRangeException($"X: {x} and Y: {y} are outside the {Columns} x {Rows} range of {nameof(Matrix2x4)}.");
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

        public Vector2 Column0
        {
            get
            {
                return new Vector2(_matrix[0], _matrix[4]);
            }
            set
            {
                _matrix[0] = value.X;
                _matrix[4] = value.Y;
            }
        }

        public Vector2 Column1
        {
            get
            {
                return new Vector2(_matrix[1], _matrix[5]);
            }
            set
            {
                _matrix[1] = value.X;
                _matrix[5] = value.Y;
            }
        }

        public Vector2 Column2
        {
            get
            {
                return new Vector2(_matrix[2], _matrix[6]);
            }
            set
            {
                _matrix[2] = value.X;
                _matrix[6] = value.Y;
            }
        }

        public Vector2 Column3
        {
            get
            {
                return new Vector2(_matrix[3], _matrix[7]);
            }
            set
            {
                _matrix[3] = value.X;
                _matrix[7] = value.Y;
            }
        }

        public double Trace() => _matrix[0] + _matrix[5];

        public Matrix4x2 Transpose() => new Matrix4x2(Column0, Column1, Column2, Column3);

        public override bool Equals(object obj)
        {
            return obj is Matrix2x4 matrix &&
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

        public void MatrixData(MatrixSpan ms)
        {
            fixed (void* ptr = _matrix)
            {
                Span<double> s = new Span<double>(ptr, 8);
                ms.Fill(s, 2, 4);
            }
        }

        public override string ToString()
        {
            return $@"[{_matrix[0]}, {_matrix[1]}, {_matrix[2]}, {_matrix[3]}]
[{_matrix[4]}, {_matrix[5]}, {_matrix[6]}, {_matrix[7]}]";
        }
        public string ToString(string format)
        {
            return $@"[{_matrix[0].ToString(format)}, {_matrix[1].ToString(format)}, {_matrix[2].ToString(format)}, {_matrix[3].ToString(format)}]
[{_matrix[4].ToString(format)}, {_matrix[5].ToString(format)}, {_matrix[6].ToString(format)}, {_matrix[7].ToString(format)}]";
        }

        public static bool operator ==(Matrix2x4 a, Matrix2x4 b) => Equals(a, b);

        public static bool operator !=(Matrix2x4 a, Matrix2x4 b) => !Equals(a, b);

        public static MultiplyMatrix operator *(Matrix2x4 a, IMatrix b) => new MultiplyMatrix(a, b);
        
        public static Matrix2x4 operator *(Matrix2x4 a, double b)
        {
            Matrix2x4 m = new Matrix2x4();

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
        public static Matrix2x4 operator *(double b, Matrix2x4 a)
        {
            Matrix2x4 m = new Matrix2x4();

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
        
        public static Matrix2x4 operator +(Matrix2x4 a, Matrix2x4 b)
        {
            Matrix2x4 m = new Matrix2x4();

            m._matrix[0] = a._matrix[0] + b._matrix[0];
            m._matrix[1] = a._matrix[1] + b._matrix[1];
            m._matrix[2] = a._matrix[2] + b._matrix[2];
            m._matrix[3] = a._matrix[3] + b._matrix[3];
            m._matrix[4] = a._matrix[4] + b._matrix[4];
            m._matrix[5] = a._matrix[5] + b._matrix[5];
            m._matrix[6] = a._matrix[6] + b._matrix[6];
            m._matrix[6] = a._matrix[6] + b._matrix[7];

            return m;
        }
        public static Matrix2x4 operator -(Matrix2x4 a, Matrix2x4 b)
        {
            Matrix2x4 m = new Matrix2x4();

            m._matrix[0] = a._matrix[0] - b._matrix[0];
            m._matrix[1] = a._matrix[1] - b._matrix[1];
            m._matrix[2] = a._matrix[2] - b._matrix[2];
            m._matrix[3] = a._matrix[3] - b._matrix[3];
            m._matrix[4] = a._matrix[4] - b._matrix[4];
            m._matrix[5] = a._matrix[5] - b._matrix[5];
            m._matrix[6] = a._matrix[6] - b._matrix[6];
            m._matrix[6] = a._matrix[6] - b._matrix[7];

            return m;
        }
        
        public static Matrix2 operator *(Matrix2x4 a, Matrix4x2 b)
        {
            Matrix2 m = new Matrix2();

            m._matrix[0] = (a._matrix[0] * b._matrix[0]) + (a._matrix[1] * b._matrix[2]) + (a._matrix[2] * b._matrix[4]) + (a._matrix[3] * b._matrix[6]);
            m._matrix[1] = (a._matrix[0] * b._matrix[1]) + (a._matrix[1] * b._matrix[3]) + (a._matrix[2] * b._matrix[5]) + (a._matrix[3] * b._matrix[7]);
            
            m._matrix[2] = (a._matrix[4] * b._matrix[0]) + (a._matrix[5] * b._matrix[2]) + (a._matrix[6] * b._matrix[4]) + (a._matrix[7] * b._matrix[6]);
            m._matrix[3] = (a._matrix[4] * b._matrix[1]) + (a._matrix[5] * b._matrix[3]) + (a._matrix[6] * b._matrix[5]) + (a._matrix[7] * b._matrix[7]);

            return m;
        }

        public static Matrix2x3 operator *(Matrix2x4 a, Matrix4x3 b)
        {
            Matrix2x3 m = new Matrix2x3();

            m._matrix[0] = (a._matrix[0] * b._matrix[0]) + (a._matrix[1] * b._matrix[3]) + (a._matrix[2] * b._matrix[6]) + (a._matrix[3] * b._matrix[9]);
            m._matrix[1] = (a._matrix[0] * b._matrix[1]) + (a._matrix[1] * b._matrix[4]) + (a._matrix[2] * b._matrix[7]) + (a._matrix[3] * b._matrix[10]);
            m._matrix[2] = (a._matrix[0] * b._matrix[2]) + (a._matrix[1] * b._matrix[5]) + (a._matrix[2] * b._matrix[8]) + (a._matrix[3] * b._matrix[11]);
            
            m._matrix[3] = (a._matrix[4] * b._matrix[0]) + (a._matrix[5] * b._matrix[3]) + (a._matrix[6] * b._matrix[6]) + (a._matrix[7] * b._matrix[9]);
            m._matrix[4] = (a._matrix[4] * b._matrix[1]) + (a._matrix[5] * b._matrix[4]) + (a._matrix[6] * b._matrix[7]) + (a._matrix[7] * b._matrix[10]);
            m._matrix[5] = (a._matrix[4] * b._matrix[2]) + (a._matrix[5] * b._matrix[5]) + (a._matrix[6] * b._matrix[8]) + (a._matrix[7] * b._matrix[11]);

            return m;
        }

        public static Matrix2x4 operator *(Matrix2x4 a, Matrix4 b)
        {
            Matrix2x4 m = new Matrix2x4();

            m._matrix[0] = (a._matrix[0] * b._matrix[0]) + (a._matrix[1] * b._matrix[4]) + (a._matrix[2] * b._matrix[8]) + (a._matrix[3] * b._matrix[12]);
            m._matrix[1] = (a._matrix[0] * b._matrix[1]) + (a._matrix[1] * b._matrix[5]) + (a._matrix[2] * b._matrix[9]) + (a._matrix[3] * b._matrix[13]);
            m._matrix[2] = (a._matrix[0] * b._matrix[2]) + (a._matrix[1] * b._matrix[6]) + (a._matrix[2] * b._matrix[10]) + (a._matrix[3] * b._matrix[14]);
            m._matrix[3] = (a._matrix[0] * b._matrix[3]) + (a._matrix[1] * b._matrix[7]) + (a._matrix[2] * b._matrix[11]) + (a._matrix[3] * b._matrix[15]);
            
            m._matrix[4] = (a._matrix[4] * b._matrix[0]) + (a._matrix[5] * b._matrix[4]) + (a._matrix[6] * b._matrix[8]) + (a._matrix[7] * b._matrix[12]);
            m._matrix[5] = (a._matrix[4] * b._matrix[1]) + (a._matrix[5] * b._matrix[5]) + (a._matrix[6] * b._matrix[9]) + (a._matrix[7] * b._matrix[13]);
            m._matrix[6] = (a._matrix[4] * b._matrix[2]) + (a._matrix[5] * b._matrix[6]) + (a._matrix[6] * b._matrix[10]) + (a._matrix[7] * b._matrix[14]);
            m._matrix[7] = (a._matrix[4] * b._matrix[3]) + (a._matrix[5] * b._matrix[7]) + (a._matrix[6] * b._matrix[11]) + (a._matrix[7] * b._matrix[15]);

            return m;
        }
        
        private static Matrix2x4 _zero = new Matrix2x4(Vector4.Zero, Vector4.Zero);
        public static ref Matrix2x4 Zero => ref _zero;

        private static Matrix2x4 _identity = new Matrix2x4(new Vector4(1, 0, 0, 0), new Vector4(0, 1, 0, 0));
        public static ref Matrix2x4 Identity => ref _identity;
        
        public static Matrix2x4 CreateRotation(Radian angle)
        {
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);

            return new Matrix2x4(new Vector4(cos, sin, 0, 0), new Vector4(-sin, cos, 0, 0));
        }

        public static Matrix2x4 CreateScale(double scale)
        {
            return new Matrix2x4(new Vector4(scale, 0, 0, 0), new Vector4(0, scale, 0, 0));
        }
        public static Matrix2x4 CreateScale(double scaleX, double scaleY)
        {
            return new Matrix2x4(new Vector4(scaleX, 0, 0, 0), new Vector4(0, scaleY, 0, 0));
        }
        public static Matrix2x4 CreateScale(Vector2 scale)
        {
            return CreateScale(scale.X, scale.Y);
        }
    }
}
