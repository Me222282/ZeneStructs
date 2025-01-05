using System;

namespace Zene.Structs
{
    public unsafe struct Matrix4 : IMatrix
    {
        public int Rows => 4;
        public int Columns => 4;
        
        public bool Constant => true;
        
        public Matrix4(Vector4 row0, Vector4 row1, Vector4 row2, Vector4 row3)
        {
            Row0 = row0;
            Row1 = row1;
            Row2 = row2;
            Row3 = row3;
        }
        public Matrix4(params double[] matrix)
        {
            if (matrix.Length < (Rows * Columns))
            {
                throw new Exception("Matrix needs to have at least 4 rows and 4 columns.");
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

            _matrix[12] = matrix[12];
            _matrix[13] = matrix[13];
            _matrix[14] = matrix[14];
            _matrix[15] = matrix[15];
        }
        public Matrix4(IMatrix matrix)
        {
            fixed (void* ptr = _matrix)
            {
                matrix.MatrixData(new MatrixSpan(4, 4, new Span<double>(ptr, 16)));
            }
        }

        internal fixed double _matrix[16];

        public double this[int x, int y]
        {
            get
            {
                if (x >= Columns || y >= Rows)
                {
                    throw new IndexOutOfRangeException($"X: {x} and Y: {y} are outside the {Columns} x {Rows} range of {nameof(Matrix4)}.");
                }

                return _matrix[x + (y * Columns)];
            }
            set
            {
                if (x >= Columns || y >= Rows)
                {
                    throw new IndexOutOfRangeException($"X: {x} and Y: {y} are outside the {Columns} x {Rows} range of {nameof(Matrix4)}.");
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

        public Vector4 Row3
        {
            get
            {
                return new Vector4(_matrix[12], _matrix[13], _matrix[14], _matrix[15]);
            }
            set
            {
                _matrix[12] = value.X;
                _matrix[13] = value.Y;
                _matrix[14] = value.Z;
                _matrix[15] = value.W;
            }
        }

        public Vector4 Column0
        {
            get
            {
                return new Vector4(_matrix[0], _matrix[4], _matrix[8], _matrix[12]);
            }
            set
            {
                _matrix[0] = value.X;
                _matrix[4] = value.Y;
                _matrix[8] = value.Z;
                _matrix[12] = value.W;
            }
        }

        public Vector4 Column1
        {
            get
            {
                return new Vector4(_matrix[1], _matrix[5], _matrix[9], _matrix[13]);
            }
            set
            {
                _matrix[1] = value.X;
                _matrix[5] = value.Y;
                _matrix[9] = value.Z;
                _matrix[13] = value.W;
            }
        }

        public Vector4 Column2
        {
            get
            {
                return new Vector4(_matrix[2], _matrix[6], _matrix[10], _matrix[14]);
            }
            set
            {
                _matrix[2] = value.X;
                _matrix[6] = value.Y;
                _matrix[10] = value.Z;
                _matrix[14] = value.W;
            }
        }

        public Vector4 Column3
        {
            get
            {
                return new Vector4(_matrix[3], _matrix[7], _matrix[11], _matrix[15]);
            }
            set
            {
                _matrix[3] = value.X;
                _matrix[7] = value.Y;
                _matrix[11] = value.Z;
                _matrix[15] = value.W;
            }
        }

        public double Determinant()
        {
            return
                (_matrix[0] * _matrix[5] * _matrix[10] * _matrix[15]) - (_matrix[0] * _matrix[5] * _matrix[11] * _matrix[14]) + (_matrix[0] * _matrix[6] * _matrix[11] * _matrix[13])
                - (_matrix[0] * _matrix[6] * _matrix[9] * _matrix[15]) + (_matrix[0] * _matrix[7] * _matrix[9] * _matrix[14]) - (_matrix[0] * _matrix[7] * _matrix[10] * _matrix[13])
                - (_matrix[1] * _matrix[6] * _matrix[11] * _matrix[12]) + (_matrix[1] * _matrix[6] * _matrix[8] * _matrix[15]) - (_matrix[1] * _matrix[7] * _matrix[8] * _matrix[14])
                + (_matrix[1] * _matrix[7] * _matrix[10] * _matrix[12]) - (_matrix[1] * _matrix[4] * _matrix[10] * _matrix[15]) + (_matrix[1] * _matrix[4] * _matrix[11] * _matrix[14])

                + (_matrix[2] * _matrix[7] * _matrix[8] * _matrix[13]) - (_matrix[2] * _matrix[7] * _matrix[9] * _matrix[12]) + (_matrix[2] * _matrix[4] * _matrix[9] * _matrix[15])
                - (_matrix[2] * _matrix[4] * _matrix[11] * _matrix[13]) + (_matrix[2] * _matrix[5] * _matrix[11] * _matrix[12]) - (_matrix[2] * _matrix[5] * _matrix[8] * _matrix[15])
                - (_matrix[3] * _matrix[4] * _matrix[9] * _matrix[14]) + (_matrix[3] * _matrix[4] * _matrix[10] * _matrix[13]) - (_matrix[3] * _matrix[5] * _matrix[10] * _matrix[12])
                + (_matrix[3] * _matrix[5] * _matrix[8] * _matrix[14]) - (_matrix[3] * _matrix[6] * _matrix[8] * _matrix[13]) + (_matrix[3] * _matrix[6] * _matrix[9] * _matrix[12]);
        }

        public Matrix4 Invert()
        {
            double a = _matrix[0], b = _matrix[4], c = _matrix[8], d = _matrix[12];
            double e = _matrix[1], f = _matrix[5], g = _matrix[9], h = _matrix[13];
            double i = _matrix[2], j = _matrix[6], k = _matrix[10], l = _matrix[14];
            double m = _matrix[3], n = _matrix[7], o = _matrix[11], p = _matrix[15];

            double kp_lo = k * p - l * o;
            double jp_ln = j * p - l * n;
            double jo_kn = j * o - k * n;
            double ip_lm = i * p - l * m;
            double io_km = i * o - k * m;
            double in_jm = i * n - j * m;

            double a11 = +(f * kp_lo - g * jp_ln + h * jo_kn);
            double a12 = -(e * kp_lo - g * ip_lm + h * io_km);
            double a13 = +(e * jp_ln - f * ip_lm + h * in_jm);
            double a14 = -(e * jo_kn - f * io_km + g * in_jm);

            double det = a * a11 + b * a12 + c * a13 + d * a14;

            if (Math.Abs(det) < double.Epsilon)
            {
                throw new InvalidOperationException("Matrix is singular and cannot be inverted.");
            }

            double invDet = 1.0f / det;

            Vector4 row0 = new Vector4(a11, a12, a13, a14) * invDet;

            Vector4 row1 = new Vector4(
                -(b * kp_lo - c * jp_ln + d * jo_kn),
                +(a * kp_lo - c * ip_lm + d * io_km),
                -(a * jp_ln - b * ip_lm + d * in_jm),
                +(a * jo_kn - b * io_km + c * in_jm)) * invDet;

            double gp_ho = g * p - h * o;
            double fp_hn = f * p - h * n;
            double fo_gn = f * o - g * n;
            double ep_hm = e * p - h * m;
            double eo_gm = e * o - g * m;
            double en_fm = e * n - f * m;

            Vector4 row2 = new Vector4(
                +(b * gp_ho - c * fp_hn + d * fo_gn),
                -(a * gp_ho - c * ep_hm + d * eo_gm),
                +(a * fp_hn - b * ep_hm + d * en_fm),
                -(a * fo_gn - b * eo_gm + c * en_fm)) * invDet;

            double gl_hk = g * l - h * k;
            double fl_hj = f * l - h * j;
            double fk_gj = f * k - g * j;
            double el_hi = e * l - h * i;
            double ek_gi = e * k - g * i;
            double ej_fi = e * j - f * i;

            Vector4 row3 = new Vector4(
                -(b * gl_hk - c * fl_hj + d * fk_gj),
                +(a * gl_hk - c * el_hi + d * ek_gi),
                -(a * fl_hj - b * el_hi + d * ej_fi),
                +(a * fk_gj - b * ek_gi + c * ej_fi)) * invDet;

            return new Matrix4(row0, row1, row2, row3);
        }

        public double Trace() => _matrix[0] + _matrix[5] + _matrix[10] + _matrix[15];

        public Matrix4 Normalize()
        {
            double det = Determinant();

            return new Matrix4(
                Row0 / det,
                Row1 / det,
                Row2 / det,
                Row3 / det);
        }

        public Matrix4 Transpose() => new Matrix4(Column0, Column1, Column2, Column3);

        public override bool Equals(object obj)
        {
            return obj is Matrix4 matrix &&
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
                _matrix[11] == matrix._matrix[11] &&
                _matrix[12] == matrix._matrix[12] &&
                _matrix[13] == matrix._matrix[13] &&
                _matrix[14] == matrix._matrix[14] &&
                _matrix[15] == matrix._matrix[15];
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
            hash.Add(_matrix[12]);
            hash.Add(_matrix[13]);
            hash.Add(_matrix[14]);
            hash.Add(_matrix[15]);

            return hash.ToHashCode();
        }

        public void MatrixData(MatrixSpan ms)
        {
            fixed (void* ptr = _matrix)
            {
                Span<double> s = new Span<double>(ptr, 16);
                ms.Fill(s, 4, 4);
            }
        }

        public override string ToString()
        {
            return $@"[{_matrix[0]}, {_matrix[1]}, {_matrix[2]}, {_matrix[3]}]
[{_matrix[4]}, {_matrix[5]}, {_matrix[6]}, {_matrix[7]}]
[{_matrix[8]}, {_matrix[9]}, {_matrix[10]}, {_matrix[11]}]
[{_matrix[12]}, {_matrix[13]}, {_matrix[14]}, {_matrix[15]}]";
        }
        public string ToString(string format)
        {
            return $@"[{_matrix[0].ToString(format)}, {_matrix[1].ToString(format)}, {_matrix[2].ToString(format)}, {_matrix[3].ToString(format)}]
[{_matrix[4].ToString(format)}, {_matrix[5].ToString(format)}, {_matrix[6].ToString(format)}, {_matrix[7].ToString(format)}]
[{_matrix[8].ToString(format)}, {_matrix[9].ToString(format)}, {_matrix[10].ToString(format)}, {_matrix[11].ToString(format)}]
[{_matrix[12].ToString(format)}, {_matrix[13].ToString(format)}, {_matrix[14].ToString(format)}, {_matrix[15].ToString(format)}]";
        }

        public static bool operator ==(Matrix4 a, Matrix4 b) => Equals(a, b);

        public static bool operator !=(Matrix4 a, Matrix4 b) => !Equals(a, b);

        public static MultiplyMatrix operator *(Matrix4 a, IMatrix b) => new MultiplyMatrix(a, b);
        
        public static Matrix4 operator *(Matrix4 a, double b)
        {
            Matrix4 m = new Matrix4();

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
            m._matrix[12] = a._matrix[12] * b;
            m._matrix[13] = a._matrix[13] * b;
            m._matrix[14] = a._matrix[14] * b;
            m._matrix[15] = a._matrix[15] * b;

            return m;
        }
        public static Matrix4 operator *(double b, Matrix4 a)
        {
            Matrix4 m = new Matrix4();

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
            m._matrix[12] = a._matrix[12] * b;
            m._matrix[13] = a._matrix[13] * b;
            m._matrix[14] = a._matrix[14] * b;
            m._matrix[15] = a._matrix[15] * b;

            return m;
        }
        
        public static Matrix4 operator +(Matrix4 a, Matrix4 b)
        {
            Matrix4 m = new Matrix4();

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
            m._matrix[12] = a._matrix[12] + b._matrix[12];
            m._matrix[13] = a._matrix[13] + b._matrix[13];
            m._matrix[14] = a._matrix[14] + b._matrix[14];
            m._matrix[15] = a._matrix[15] + b._matrix[15];

            return m;
        }
        public static Matrix4 operator -(Matrix4 a, Matrix4 b)
        {
            Matrix4 m = new Matrix4();

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
            m._matrix[13] = a._matrix[13] - b._matrix[13];
            m._matrix[14] = a._matrix[14] - b._matrix[14];
            m._matrix[15] = a._matrix[15] - b._matrix[15];

            return m;
        }
        
        public static Matrix4 operator *(Matrix4 a, Matrix4 b)
        {
            Matrix4 m = new Matrix4();

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
            
            m._matrix[12] = (a._matrix[12] * b._matrix[0]) + (a._matrix[13] * b._matrix[4]) + (a._matrix[14] * b._matrix[8]) + (a._matrix[15] * b._matrix[12]);
            m._matrix[13] = (a._matrix[12] * b._matrix[1]) + (a._matrix[13] * b._matrix[5]) + (a._matrix[14] * b._matrix[9]) + (a._matrix[15] * b._matrix[13]);
            m._matrix[14] = (a._matrix[12] * b._matrix[2]) + (a._matrix[13] * b._matrix[6]) + (a._matrix[14] * b._matrix[10]) + (a._matrix[15] * b._matrix[14]);
            m._matrix[15] = (a._matrix[12] * b._matrix[3]) + (a._matrix[13] * b._matrix[7]) + (a._matrix[14] * b._matrix[11]) + (a._matrix[15] * b._matrix[15]);
            
            return m;
        }

        public static Matrix4x2 operator *(Matrix4 a, Matrix4x2 b)
        {
            Matrix4x2 m = new Matrix4x2();

            m._matrix[0] = (a._matrix[0] * b._matrix[0]) + (a._matrix[1] * b._matrix[2]) + (a._matrix[2] * b._matrix[4]) + (a._matrix[3] * b._matrix[6]);
            m._matrix[1] = (a._matrix[0] * b._matrix[1]) + (a._matrix[1] * b._matrix[3]) + (a._matrix[2] * b._matrix[5]) + (a._matrix[3] * b._matrix[7]);
            
            m._matrix[2] = (a._matrix[4] * b._matrix[0]) + (a._matrix[5] * b._matrix[2]) + (a._matrix[6] * b._matrix[4]) + (a._matrix[7] * b._matrix[6]);
            m._matrix[3] = (a._matrix[4] * b._matrix[1]) + (a._matrix[5] * b._matrix[3]) + (a._matrix[6] * b._matrix[5]) + (a._matrix[7] * b._matrix[7]);
            
            m._matrix[4] = (a._matrix[8] * b._matrix[0]) + (a._matrix[9] * b._matrix[2]) + (a._matrix[10] * b._matrix[4]) + (a._matrix[11] * b._matrix[6]);
            m._matrix[5] = (a._matrix[8] * b._matrix[1]) + (a._matrix[9] * b._matrix[3]) + (a._matrix[10] * b._matrix[5]) + (a._matrix[11] * b._matrix[7]);
            
            m._matrix[6] = (a._matrix[12] * b._matrix[0]) + (a._matrix[13] * b._matrix[2]) + (a._matrix[14] * b._matrix[4]) + (a._matrix[15] * b._matrix[6]);
            m._matrix[7] = (a._matrix[12] * b._matrix[1]) + (a._matrix[13] * b._matrix[3]) + (a._matrix[14] * b._matrix[5]) + (a._matrix[15] * b._matrix[7]);
            
            return m;
        }

        public static Matrix4x3 operator *(Matrix4 a, Matrix4x3 b)
        {
            Matrix4x3 m = new Matrix4x3();

            m._matrix[0] = (a._matrix[0] * b._matrix[0]) + (a._matrix[1] * b._matrix[3]) + (a._matrix[2] * b._matrix[6]) + (a._matrix[3] * b._matrix[9]);
            m._matrix[1] = (a._matrix[0] * b._matrix[1]) + (a._matrix[1] * b._matrix[4]) + (a._matrix[2] * b._matrix[7]) + (a._matrix[3] * b._matrix[10]);
            m._matrix[2] = (a._matrix[0] * b._matrix[2]) + (a._matrix[1] * b._matrix[5]) + (a._matrix[2] * b._matrix[8]) + (a._matrix[3] * b._matrix[11]);
            
            m._matrix[3] = (a._matrix[4] * b._matrix[0]) + (a._matrix[5] * b._matrix[3]) + (a._matrix[6] * b._matrix[6]) + (a._matrix[7] * b._matrix[9]);
            m._matrix[4] = (a._matrix[4] * b._matrix[1]) + (a._matrix[5] * b._matrix[4]) + (a._matrix[6] * b._matrix[7]) + (a._matrix[7] * b._matrix[10]);
            m._matrix[5] = (a._matrix[4] * b._matrix[2]) + (a._matrix[5] * b._matrix[5]) + (a._matrix[6] * b._matrix[8]) + (a._matrix[7] * b._matrix[11]);
            
            m._matrix[6] = (a._matrix[8] * b._matrix[0]) + (a._matrix[9] * b._matrix[3]) + (a._matrix[10] * b._matrix[6]) + (a._matrix[11] * b._matrix[9]);
            m._matrix[7] = (a._matrix[8] * b._matrix[1]) + (a._matrix[9] * b._matrix[4]) + (a._matrix[10] * b._matrix[7]) + (a._matrix[11] * b._matrix[10]);
            m._matrix[8] = (a._matrix[8] * b._matrix[2]) + (a._matrix[9] * b._matrix[5]) + (a._matrix[10] * b._matrix[8]) + (a._matrix[11] * b._matrix[11]);
            
            m._matrix[9] = (a._matrix[12] * b._matrix[0]) + (a._matrix[13] * b._matrix[3]) + (a._matrix[14] * b._matrix[6]) + (a._matrix[15] * b._matrix[9]);
            m._matrix[10] = (a._matrix[12] * b._matrix[1]) + (a._matrix[13] * b._matrix[4]) + (a._matrix[14] * b._matrix[7]) + (a._matrix[15] * b._matrix[10]);
            m._matrix[11] = (a._matrix[12] * b._matrix[2]) + (a._matrix[13] * b._matrix[5]) + (a._matrix[14] * b._matrix[8]) + (a._matrix[15] * b._matrix[11]);
            
            return m;
        }
        
        private static Matrix4 _zero = new Matrix4(Vector4.Zero, Vector4.Zero, Vector4.Zero, Vector4.Zero);
        public static ref Matrix4 Zero => ref _zero;

        private static Matrix4 _identity = new Matrix4(new Vector4(1, 0, 0, 0), new Vector4(0, 1, 0, 0), new Vector4(0, 0, 1, 0), new Vector4(0, 0, 0, 1));
        public static ref Matrix4 Identity => ref _identity;
        
        public static Matrix4 CreateRotation(Vector3 axis, Radian angle)
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

            return new Matrix4(
                new Vector4(tXX + cos, tXY - sinZ, tXZ + sinY, 0),
                new Vector4(tXY + sinZ, tYY + cos, tYZ - sinX, 0),
                new Vector4(tXZ - sinY, tYZ + sinX, tZZ + cos, 0),
                new Vector4(0, 0, 0, 1));
        }

        public static Matrix4 CreateRotationX(Radian angle)
        {
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);

            return new Matrix4(
                new Vector4(1, 0, 0, 0),
                new Vector4(0, cos, sin, 0),
                new Vector4(0, -sin, cos, 0),
                new Vector4(0, 0, 0, 1));
        }

        public static Matrix4 CreateRotationY(Radian angle)
        {
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);

            return new Matrix4(
                new Vector4(cos, 0, -sin, 0),
                new Vector4(0, 1, 0, 0),
                new Vector4(sin, 0, cos, 0),
                new Vector4(0, 0, 0, 1));
        }

        public static Matrix4 CreateRotationZ(Radian angle)
        {
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);

            return new Matrix4(
                new Vector4(cos, sin, 0, 0),
                new Vector4(-sin, cos, 0, 0),
                new Vector4(0, 0, 1, 0),
                new Vector4(0, 0, 0, 1));
        }

        public static Matrix4 CreateScale(double scale)
        {
            return new Matrix4(
                new Vector4(scale, 0, 0, 0),
                new Vector4(0, scale, 0, 0),
                new Vector4(0, 0, scale, 0),
                new Vector4(0, 0, 0, 1));
        }

        public static Matrix4 CreateScale(double scaleX, double scaleY, double scaleZ)
        {
            return new Matrix4(
                new Vector4(scaleX, 0, 0, 0),
                new Vector4(0, scaleY, 0, 0),
                new Vector4(0, 0, scaleZ, 0),
                new Vector4(0, 0, 0, 1));
        }

        public static Matrix4 CreateScale(double scaleX, double scaleY)
        {
            return new Matrix4(
                new Vector4(scaleX, 0, 0, 0),
                new Vector4(0, scaleY, 0, 0),
                new Vector4(0, 0, 1, 0),
                new Vector4(0, 0, 0, 1));
        }

        public static Matrix4 CreateScale(Vector3 scale) => CreateScale(scale.X, scale.Y, scale.Z);

        public static Matrix4 CreateScale(Vector2 scale) => CreateScale(scale.X, scale.Y);

        public static Matrix4 CreateTranslation(double xyz)
        {
            return new Matrix4(
                new Vector4(1, 0, 0, 0),
                new Vector4(0, 1, 0, 0),
                new Vector4(0, 0, 1, 0),
                new Vector4(xyz, xyz, xyz, 1));
        }

        public static Matrix4 CreateTranslation(double x, double y, double z)
        {
            return new Matrix4(
                new Vector4(1, 0, 0, 0),
                new Vector4(0, 1, 0, 0),
                new Vector4(0, 0, 1, 0),
                new Vector4(x, y, z, 1));
        }

        public static Matrix4 CreateTranslation(double x, double y)
        {
            return new Matrix4(
                new Vector4(1, 0, 0, 0),
                new Vector4(0, 1, 0, 0),
                new Vector4(0, 0, 1, 0),
                new Vector4(x, y, 0, 1));
        }

        public static Matrix4 CreateTranslation(Vector3 xyz) => CreateTranslation(xyz.X, xyz.Y, xyz.Z);

        public static Matrix4 CreateTranslation(Vector2 xy) => CreateTranslation(xy.X, xy.Y);

        public static Matrix4 CreateBox(IBox box)
        {
            Vector2 c = box.Centre;

            return new Matrix4(
                new Vector4(box.Width, 0, 0, 0),
                new Vector4(0, box.Height, 0, 0),
                new Vector4(0, 0, 1, 0),
                new Vector4(c.X, c.Y, 0, 1));
        }

        public static Matrix4 CreateBox(IBox box, double depth)
        {
            Vector2 c = box.Centre;

            return new Matrix4(
                new Vector4(box.Width, 0, 0, 0),
                new Vector4(0, box.Height, 0, 0),
                new Vector4(0, 0, 1, 0),
                new Vector4(c.X, c.Y, depth, 1));
        }

        public static Matrix4 CreateBox(IBox3 box)
        {
            Vector3 c = box.Centre;

            return new Matrix4(
                new Vector4(box.Width, 0, 0, 0),
                new Vector4(0, box.Height, 0, 0),
                new Vector4(0, 0, box.Depth, 0),
                new Vector4(c.X, c.Y, c.Z, 1));
        }

        public static Matrix4 CreateOrthographicOffCentre(double left, double right, double top, double bottom, double depthNear, double depthFar)
        {
            double invRL = 1.0 / (right - left);
            double invTB = 1.0 / (top - bottom);
            double invFN = 1.0 / (depthFar - depthNear);

            return new Matrix4(
                new Vector4(2 * invRL, 0, 0, 0),
                new Vector4(0, 2 * invTB, 0, 0),
                new Vector4(0, 0, -2 * invFN, 0),
                new Vector4(-(right + left) * invRL, -(top + bottom) * invTB, -(depthFar + depthNear) * invFN, 1));
        }

        public static Matrix4 CreateOrthographic(double width, double height, double depthNear, double depthFar)
        {
            return CreateOrthographicOffCentre(-width / 2, width / 2, height / 2, -height / 2, depthNear, depthFar);
        }

        public static Matrix4 CreatePerspectiveOffCentre(double left, double right, double top, double bottom, double depthNear, double depthFar)
        {
            if (depthNear <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(depthNear));
            }

            if (depthFar <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(depthFar));
            }

            if (depthNear >= depthFar)
            {
                throw new ArgumentOutOfRangeException(nameof(depthNear));
            }
            /*
            double x = 2.0 * depthNear / (right - left);
            double y = 2.0 * depthNear / (top - bottom);
            double a = (right + left) / (right - left);
            double b = (top + bottom) / (top - bottom);
            double c = -(depthFar + depthNear) / (depthFar - depthNear);
            double d = -(2.0 * depthFar * depthNear) / (depthFar - depthNear);

            return new Matrix4(
                new Vector4(x, 0, 0, 0),
                new Vector4(0, y, 0, 0),
                new Vector4(a, b, c, -1),
                new Vector4(0, 0, d, 0));*/

            double widthMulti = 1 / (right - left);
            double heightMulti = 1 / (bottom - top);
            double depthMutli = 1 / (depthFar - depthNear);
            double near2 = depthNear * 2;

            return new Matrix4(
                new Vector4(near2 * widthMulti, 0, 0, 0),
                new Vector4(0, near2 * heightMulti, 0, 0),
                new Vector4(-(right + left) * widthMulti, -(bottom + top) * heightMulti, depthFar * depthMutli, 1),
                new Vector4(0, 0, -depthFar * depthNear * depthMutli, 0));
        }

        public static Matrix4 CreatePerspectiveFieldOfView(Radian fovy, double aspect, double depthNear, double depthFar)
        {
            if (fovy <= 0 || fovy > Math.PI)
            {
                throw new ArgumentOutOfRangeException(nameof(fovy));
            }

            if (aspect <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(aspect));
            }

            if (depthNear <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(depthNear));
            }

            if (depthFar <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(depthFar));
            }
            /*
            double maxY = depthNear * Math.Tan(0.5 * fovy);
            double minY = -maxY;
            double minX = minY * aspect;
            double maxX = maxY * aspect;

            return CreatePerspectiveOffCenter(minX, maxX, minY, maxY, depthNear, depthFar);*/

            double depthMutli = 1 / (depthFar - depthNear);
            double degree = Math.Tan(fovy * 0.5);

            return new Matrix4(
                new Vector4(1 / (aspect * degree), 0, 0, 0),
                new Vector4(0, 1 / degree, 0, 0),
                new Vector4(0, 0, depthFar * depthMutli, 1),
                new Vector4(0, 0, -depthFar * depthNear * depthMutli, 0));
        }

        public static Matrix4 LookAt(Vector3 eye, Vector3 target, Vector3 up)
        {
            Vector3 z = (eye - target).Normalised();
            Vector3 x = up.Cross(z).Normalised();
            Vector3 y = z.Cross(x).Normalised();

            return new Matrix4(
                new Vector4(x.X, y.X, z.X, 0),
                new Vector4(x.Y, y.Y, z.Y, 0),
                new Vector4(x.Z, y.Z, z.Z, 0),
                new Vector4(-((x.X * eye.X) + (x.Y * eye.Y) + (x.Z * eye.Z)), -((y.X * eye.X) + (y.Y * eye.Y) + (y.Z * eye.Z)), -((z.X * eye.X) + (z.Y * eye.Y) + (z.Z * eye.Z)), 1));
        }

        public static Matrix4 LookAt(double eyeX, double eyeY, double eyeZ, double targetX, double targetY, double targetZ, double upX, double upY, double upZ)
        {
            return LookAt(new Vector3(eyeX, eyeY, eyeZ), new Vector3(targetX, targetY, targetZ), new Vector3(upX, upY, upZ));
        }
    }
}
