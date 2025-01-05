using System;

namespace Zene.Structs
{
    public unsafe struct Matrix2x3 : IMatrix
    {
        public int Rows => 2;
        public int Columns => 3;
        
        public bool Constant => true;
        
        public Matrix2x3(Vector3 row0, Vector3 row1)
        {
            Row0 = row0;
            Row1 = row1;
        }

        public Matrix2x3(params double[] matrix)
        {
            if (matrix.Length < (Rows * Columns))
            {
                throw new Exception("Matrix needs to have at least 2 rows and 3 columns.");
            }

            _matrix[0] = matrix[0];
            _matrix[1] = matrix[1];
            _matrix[2] = matrix[2];

            _matrix[3] = matrix[3];
            _matrix[4] = matrix[4];
            _matrix[5] = matrix[5];
        }
        public Matrix2x3(IMatrix matrix)
        {
            fixed (void* ptr = _matrix)
            {
                matrix.MatrixData(new MatrixSpan(2, 3, new Span<double>(ptr, 6)));
            }
        }

        internal fixed double _matrix[6];

        public double this[int x, int y]
        {
            get
            {
                if (x >= Columns || y >= Rows)
                {
                    throw new IndexOutOfRangeException($"X: {x} and Y: {y} are outside the {Columns} x {Rows} range of {nameof(Matrix2x3)}.");
                }

                return _matrix[x + (y * Columns)];
            }
            set
            {
                if (x >= Columns || y >= Rows)
                {
                    throw new IndexOutOfRangeException($"X: {x} and Y: {y} are outside the {Columns} x {Rows} range of {nameof(Matrix2x3)}.");
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

        public Vector2 Column0
        {
            get
            {
                return new Vector2(_matrix[0], _matrix[3]);
            }
            set
            {
                _matrix[0] = value.X;
                _matrix[3] = value.Y;
            }
        }

        public Vector2 Column1
        {
            get
            {
                return new Vector2(_matrix[1], _matrix[4]);
            }
            set
            {
                _matrix[1] = value.X;
                _matrix[4] = value.Y;
            }
        }

        public Vector2 Column2
        {
            get
            {
                return new Vector2(_matrix[2], _matrix[5]);
            }
            set
            {
                _matrix[2] = value.X;
                _matrix[5] = value.Y;
            }
        }

        public double Trace() => _matrix[0] + _matrix[4];

        public Matrix3x2 Transpose() => new Matrix3x2(Column0, Column1, Column2);

        public override bool Equals(object obj)
        {
            return obj is Matrix2x3 matrix &&
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

        public void MatrixData(MatrixSpan ms)
        {
            fixed (void* ptr = _matrix)
            {
                Span<double> s = new Span<double>(ptr, 6);
                ms.Fill(s, 2, 3);
            }
        }

        public override string ToString()
        {
            return $@"[{_matrix[0]}, {_matrix[1]}, {_matrix[2]}]
[{_matrix[3]}, {_matrix[4]}, {_matrix[5]}]";
        }
        public string ToString(string format)
        {
            return $@"[{_matrix[0].ToString(format)}, {_matrix[1].ToString(format)}, {_matrix[2].ToString(format)}]
[{_matrix[3].ToString(format)}, {_matrix[4].ToString(format)}, {_matrix[5].ToString(format)}]";
        }

        public static bool operator ==(Matrix2x3 a, Matrix2x3 b) => Equals(a, b);

        public static bool operator !=(Matrix2x3 a, Matrix2x3 b) => !Equals(a, b);

        public static MultiplyMatrix operator *(Matrix2x3 a, IMatrix b) => new MultiplyMatrix(a, b);
        
        public static Matrix2x3 operator *(Matrix2x3 a, double b)
        {
            Matrix2x3 m = new Matrix2x3();

            m._matrix[0] = a._matrix[0] * b;
            m._matrix[1] = a._matrix[1] * b;
            m._matrix[2] = a._matrix[2] * b;
            m._matrix[3] = a._matrix[3] * b;
            m._matrix[4] = a._matrix[4] * b;
            m._matrix[5] = a._matrix[5] * b;

            return m;
        }
        public static Matrix2x3 operator *(double b, Matrix2x3 a)
        {
            Matrix2x3 m = new Matrix2x3();

            m._matrix[0] = a._matrix[0] * b;
            m._matrix[1] = a._matrix[1] * b;
            m._matrix[2] = a._matrix[2] * b;
            m._matrix[3] = a._matrix[3] * b;
            m._matrix[4] = a._matrix[4] * b;
            m._matrix[5] = a._matrix[5] * b;

            return m;
        }
        
        public static Matrix2x3 operator +(Matrix2x3 a, Matrix2x3 b)
        {
            Matrix2x3 m = new Matrix2x3();

            m._matrix[0] = a._matrix[0] + b._matrix[0];
            m._matrix[1] = a._matrix[1] + b._matrix[1];
            m._matrix[2] = a._matrix[2] + b._matrix[2];
            m._matrix[3] = a._matrix[3] + b._matrix[3];
            m._matrix[4] = a._matrix[4] + b._matrix[4];
            m._matrix[5] = a._matrix[5] + b._matrix[5];

            return m;
        }
        public static Matrix2x3 operator -(Matrix2x3 a, Matrix2x3 b)
        {
            Matrix2x3 m = new Matrix2x3();

            m._matrix[0] = a._matrix[0] - b._matrix[0];
            m._matrix[1] = a._matrix[1] - b._matrix[1];
            m._matrix[2] = a._matrix[2] - b._matrix[2];
            m._matrix[3] = a._matrix[3] - b._matrix[3];
            m._matrix[4] = a._matrix[4] - b._matrix[4];
            m._matrix[5] = a._matrix[5] - b._matrix[5];

            return m;
        }
        
        public static Matrix2x3 operator *(Matrix2x3 a, Matrix3 b)
        {
            Matrix2x3 m = new Matrix2x3();

            m._matrix[0] = (a._matrix[0] * b._matrix[0]) + (a._matrix[1] * b._matrix[3]) + (a._matrix[2] * b._matrix[6]);
            m._matrix[1] = (a._matrix[0] * b._matrix[1]) + (a._matrix[1] * b._matrix[4]) + (a._matrix[2] * b._matrix[7]);
            m._matrix[2] = (a._matrix[0] * b._matrix[2]) + (a._matrix[1] * b._matrix[5]) + (a._matrix[2] * b._matrix[8]);
            
            m._matrix[3] = (a._matrix[3] * b._matrix[0]) + (a._matrix[4] * b._matrix[3]) + (a._matrix[5] * b._matrix[6]);
            m._matrix[4] = (a._matrix[3] * b._matrix[1]) + (a._matrix[4] * b._matrix[4]) + (a._matrix[5] * b._matrix[7]);
            m._matrix[5] = (a._matrix[3] * b._matrix[2]) + (a._matrix[4] * b._matrix[5]) + (a._matrix[5] * b._matrix[8]);

            return m;
        }

        public static Matrix2 operator *(Matrix2x3 a, Matrix3x2 b)
        {
            Matrix2 m = new Matrix2();

            m._matrix[0] = (a._matrix[0] * b._matrix[0]) + (a._matrix[1] * b._matrix[2]) + (a._matrix[2] * b._matrix[4]);
            m._matrix[1] = (a._matrix[0] * b._matrix[1]) + (a._matrix[1] * b._matrix[3]) + (a._matrix[2] * b._matrix[5]);
            
            m._matrix[2] = (a._matrix[3] * b._matrix[0]) + (a._matrix[4] * b._matrix[2]) + (a._matrix[5] * b._matrix[4]);
            m._matrix[3] = (a._matrix[3] * b._matrix[1]) + (a._matrix[4] * b._matrix[3]) + (a._matrix[5] * b._matrix[5]);

            return m;
        }

        public static Matrix2x4 operator *(Matrix2x3 a, Matrix3x4 b)
        {
            Matrix2x4 m = new Matrix2x4();

            m._matrix[0] = (a._matrix[0] * b._matrix[0]) + (a._matrix[1] * b._matrix[4]) + (a._matrix[2] * b._matrix[8]);
            m._matrix[1] = (a._matrix[0] * b._matrix[1]) + (a._matrix[1] * b._matrix[5]) + (a._matrix[2] * b._matrix[9]);
            m._matrix[2] = (a._matrix[0] * b._matrix[2]) + (a._matrix[1] * b._matrix[6]) + (a._matrix[2] * b._matrix[10]);
            m._matrix[3] = (a._matrix[0] * b._matrix[3]) + (a._matrix[1] * b._matrix[7]) + (a._matrix[2] * b._matrix[11]);
            
            m._matrix[4] = (a._matrix[3] * b._matrix[0]) + (a._matrix[4] * b._matrix[4]) + (a._matrix[5] * b._matrix[8]);
            m._matrix[5] = (a._matrix[3] * b._matrix[1]) + (a._matrix[4] * b._matrix[5]) + (a._matrix[5] * b._matrix[9]);
            m._matrix[6] = (a._matrix[3] * b._matrix[2]) + (a._matrix[4] * b._matrix[6]) + (a._matrix[5] * b._matrix[10]);
            m._matrix[7] = (a._matrix[3] * b._matrix[3]) + (a._matrix[4] * b._matrix[7]) + (a._matrix[5] * b._matrix[11]);

            return m;
        }
        
        private static Matrix2x3 _zero = new Matrix2x3(Vector3.Zero, Vector3.Zero);
        public static ref Matrix2x3 Zero => ref _zero;

        private static Matrix2x3 _identity = new Matrix2x3(new Vector3(1, 0, 0), new Vector3(0, 1, 0));
        public static ref Matrix2x3 Identity => ref _identity;
        
        public static Matrix2x3 CreateRotation(Radian angle)
        {
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);

            return new Matrix2x3(new Vector3(cos, sin, 0), new Vector3(-sin, cos, 0));
        }

        public static Matrix2x3 CreateScale(double scale)
        {
            return new Matrix2x3(new Vector3(scale, 0, 0), new Vector3(0, scale, 0));
        }
        public static Matrix2x3 CreateScale(double scaleX, double scaleY)
        {
            return new Matrix2x3(new Vector3(scaleX, 0, 0), new Vector3(0, scaleY, 0));
        }
        public static Matrix2x3 CreateScale(Vector2 scale)
        {
            return CreateScale(scale.X, scale.Y);
        }

        public static Matrix2x3 CreateTranslation(double xy)
        {
            return new Matrix2x3(
                new Vector3(1, 0, xy),
                new Vector3(0, 1, xy));
        }
        public static Matrix2x3 CreateTranslation(double x, double y)
        {
            return new Matrix2x3(
                new Vector3(1, 0, x),
                new Vector3(0, 1, y));
        }
        public static Matrix2x3 CreateTranslation(Vector3 xy)
        {
            return new Matrix2x3(
                new Vector3(1, 0, xy.X),
                new Vector3(0, 1, xy.Y));
        }
    }
}
