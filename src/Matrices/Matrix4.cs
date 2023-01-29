using System;

namespace Zene.Structs
{
    public unsafe class Matrix4
    {
        public const int Rows = 4;
        public const int Columns = 4;

        public Matrix4(Vector4 row0, Vector4 row1, Vector4 row2, Vector4 row3)
        {
            Row0 = row0;
            Row1 = row1;
            Row2 = row2;
            Row3 = row3;
        }
        public Matrix4(double[] matrix)
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
        public Matrix4()
        {
            _matrix = new double[16];
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
                    throw new IndexOutOfRangeException($"X: {x} and Y: {y} are outside the {Columns} x {Rows} range of {nameof(Matrix4)}.");
                }

                return _matrix[x + (y * Rows)];
            }
            set
            {
                if (x >= Columns || y >= Rows)
                {
                    throw new IndexOutOfRangeException($"X: {x} and Y: {y} are outside the {Columns} x {Rows} range of {nameof(Matrix4)}.");
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

        /// <summary>
        /// Sets the contents of this matrix, to the contents of <paramref name="matrix"/>.
        /// </summary>
        /// <param name="matrix">THe source matrix.</param>
        public void Set(Matrix4 matrix)
        {
            matrix._matrix.CopyTo(_matrix, 0);
        }

        public Matrix4 Add(Matrix4 matrix)
        {
            if (matrix is null)
            {
                matrix = Identity;
            }

            return new Matrix4(
                Row0 + matrix.Row0,
                Row1 + matrix.Row1,
                Row2 + matrix.Row2,
                Row3 + matrix.Row3);
        }

        public Matrix4 Subtract(Matrix4 matrix)
        {
            if (matrix is null)
            {
                matrix = Identity;
            }

            return new Matrix4(
                Row0 - matrix.Row0,
                Row1 - matrix.Row1,
                Row2 - matrix.Row2,
                Row3 - matrix.Row3);
        }

        public Matrix4 Multiply(double value)
        {
            return new Matrix4(
                Row0 * value,
                Row1 * value,
                Row2 * value,
                Row3 * value);
        }

        public Matrix4 Multiply(Matrix4 matrix)
        {
            if (matrix is null)
            {
                matrix = Identity;
            }

            return new Matrix4(
                (
                    /*x:0 y:0*/(_matrix[0] * matrix[0, 0]) + (_matrix[1] * matrix[0, 1]) + (_matrix[2] * matrix[0, 2]) + (_matrix[3] * matrix[0, 3]),
                    /*x:1 y:0*/(_matrix[0] * matrix[1, 0]) + (_matrix[1] * matrix[1, 1]) + (_matrix[2] * matrix[1, 2]) + (_matrix[3] * matrix[1, 3]),
                    /*x:2 y:0*/(_matrix[0] * matrix[2, 0]) + (_matrix[1] * matrix[2, 1]) + (_matrix[2] * matrix[2, 2]) + (_matrix[3] * matrix[2, 3]),
                    /*x:3 y:0*/(_matrix[0] * matrix[3, 0]) + (_matrix[1] * matrix[3, 1]) + (_matrix[2] * matrix[3, 2]) + (_matrix[3] * matrix[3, 3])
                ),
                (
                    /*x:0 y:1*/(_matrix[4] * matrix[0, 0]) + (_matrix[5] * matrix[0, 1]) + (_matrix[6] * matrix[0, 2]) + (_matrix[7] * matrix[0, 3]),
                    /*x:1 y:1*/(_matrix[4] * matrix[1, 0]) + (_matrix[5] * matrix[1, 1]) + (_matrix[6] * matrix[1, 2]) + (_matrix[7] * matrix[1, 3]),
                    /*x:2 y:1*/(_matrix[4] * matrix[2, 0]) + (_matrix[5] * matrix[2, 1]) + (_matrix[6] * matrix[2, 2]) + (_matrix[7] * matrix[2, 3]),
                    /*x:3 y:1*/(_matrix[4] * matrix[3, 0]) + (_matrix[5] * matrix[3, 1]) + (_matrix[6] * matrix[3, 2]) + (_matrix[7] * matrix[3, 3])
                ),
                (
                    /*x:0 y:2*/(_matrix[8] * matrix[0, 0]) + (_matrix[9] * matrix[0, 1]) + (_matrix[10] * matrix[0, 2]) + (_matrix[11] * matrix[0, 3]),
                    /*x:1 y:2*/(_matrix[8] * matrix[1, 0]) + (_matrix[9] * matrix[1, 1]) + (_matrix[10] * matrix[1, 2]) + (_matrix[11] * matrix[1, 3]),
                    /*x:2 y:2*/(_matrix[8] * matrix[2, 0]) + (_matrix[9] * matrix[2, 1]) + (_matrix[10] * matrix[2, 2]) + (_matrix[11] * matrix[2, 3]),
                    /*x:3 y:2*/(_matrix[8] * matrix[3, 0]) + (_matrix[9] * matrix[3, 1]) + (_matrix[10] * matrix[3, 2]) + (_matrix[11] * matrix[3, 3])
                ),
                (
                    /*x:0 y:3*/(_matrix[12] * matrix[0, 0]) + (_matrix[13] * matrix[0, 1]) + (_matrix[14] * matrix[0, 2]) + (_matrix[15] * matrix[0, 3]),
                    /*x:1 y:3*/(_matrix[12] * matrix[1, 0]) + (_matrix[13] * matrix[1, 1]) + (_matrix[14] * matrix[1, 2]) + (_matrix[15] * matrix[1, 3]),
                    /*x:2 y:3*/(_matrix[12] * matrix[2, 0]) + (_matrix[13] * matrix[2, 1]) + (_matrix[14] * matrix[2, 2]) + (_matrix[15] * matrix[2, 3]),
                    /*x:3 y:3*/(_matrix[12] * matrix[3, 0]) + (_matrix[13] * matrix[3, 1]) + (_matrix[14] * matrix[3, 2]) + (_matrix[15] * matrix[3, 3])
                ));
        }

        public Matrix4x2 Multiply(Matrix4x2 matrix)
        {
            if (matrix is null)
            {
                matrix = Matrix4x2.Identity;
            }

            return new Matrix4x2(
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
                ),
                (
                    /*X:0 Y:3*/(_matrix[12] * matrix[0, 0]) + (_matrix[13] * matrix[0, 1]) + (_matrix[14] * matrix[0, 2]) + (_matrix[15] * matrix[0, 3]),
                    /*X:1 Y:3*/(_matrix[12] * matrix[1, 0]) + (_matrix[13] * matrix[1, 1]) + (_matrix[14] * matrix[1, 2]) + (_matrix[15] * matrix[1, 3])
                ));
        }

        public Matrix4x3 Multiply(Matrix4x3 matrix)
        {
            if (matrix is null)
            {
                matrix = Matrix4x3.Identity;
            }

            return new Matrix4x3(
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
                    /*X:0 Y:2*/(_matrix[8] * matrix[0, 0]) + (_matrix[9] * matrix[0, 1]) + (_matrix[10] * matrix[0, 2]) + (_matrix[11] * matrix[0, 3]),
                    /*X:1 Y:2*/(_matrix[8] * matrix[1, 0]) + (_matrix[9] * matrix[1, 1]) + (_matrix[10] * matrix[1, 2]) + (_matrix[11] * matrix[1, 3]),
                    /*X:2 Y:2*/(_matrix[8] * matrix[2, 0]) + (_matrix[9] * matrix[2, 1]) + (_matrix[10] * matrix[2, 2]) + (_matrix[11] * matrix[2, 3])
                ),
                (
                    /*X:0 Y:2*/(_matrix[12] * matrix[0, 0]) + (_matrix[13] * matrix[0, 1]) + (_matrix[14] * matrix[0, 2]) + (_matrix[15] * matrix[0, 3]),
                    /*X:1 Y:3*/(_matrix[12] * matrix[1, 0]) + (_matrix[13] * matrix[1, 1]) + (_matrix[14] * matrix[1, 2]) + (_matrix[15] * matrix[1, 3]),
                    /*X:2 Y:3*/(_matrix[12] * matrix[2, 0]) + (_matrix[13] * matrix[2, 1]) + (_matrix[14] * matrix[2, 2]) + (_matrix[15] * matrix[2, 3])
                ));
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
            return obj is Matrix4 matrix && matrix is not null &&
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
                (float)_matrix[11],
                (float)_matrix[12],
                (float)_matrix[13],
                (float)_matrix[14],
                (float)_matrix[15]
            };
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

        public static bool operator ==(Matrix4 a, Matrix4 b)
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

        public static bool operator !=(Matrix4 a, Matrix4 b)
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

        public static Matrix4 operator +(Matrix4 a, Matrix4 b)
        {
            if (a is null)
            {
                a = Identity;
            }

            return a.Add(b);
        }

        public static Matrix4 operator -(Matrix4 a, Matrix4 b)
        {
            if (a is null)
            {
                a = Identity;
            }

            return a.Subtract(b);
        }

        public static Matrix4 operator *(Matrix4 a, Matrix4 b)
        {
            if (a is null)
            {
                a = Identity;
            }

            return a.Multiply(b);
        }

        public static Matrix4x2 operator *(Matrix4 a, Matrix4x2 b)
        {
            if (a is null)
            {
                a = Identity;
            }

            return a.Multiply(b);
        }

        public static Matrix4x3 operator *(Matrix4 a, Matrix4x3 b)
        {
            if (a is null)
            {
                a = Identity;
            }

            return a.Multiply(b);
        }

        public static Matrix4 operator *(Matrix4 a, double b)
        {
            if (a is null)
            {
                a = Identity;
            }

            return a.Multiply(b);
        }

        public static Matrix4 operator *(double a, Matrix4 b)
        {
            if (b is null)
            {
                b = Identity;
            }

            return b.Multiply(a);
        }

        public static Matrix4 Zero { get; } = new Matrix4(Vector4.Zero, Vector4.Zero, Vector4.Zero, Vector4.Zero);

        public static Matrix4 Identity { get; } = new Matrix4(new Vector4(1, 0, 0, 0), new Vector4(0, 1, 0, 0), new Vector4(0, 0, 1, 0), new Vector4(0, 0, 0, 1));

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

        public static implicit operator Matrix4(Matrix4<double> matrix)
        {
            return new Matrix4((Vector4)matrix.Row0, (Vector4)matrix.Row1, (Vector4)matrix.Row2, (Vector4)matrix.Row3);
        }
        public static explicit operator Matrix4(Matrix4<float> matrix)
        {
            return new Matrix4((Vector4)matrix.Row0, (Vector4)matrix.Row1, (Vector4)matrix.Row2, (Vector4)matrix.Row3);
        }

        public static implicit operator Matrix4<double>(Matrix4 matrix)
        {
            if (matrix is null)
            {
                matrix = Identity;
            }

            return new Matrix4<double>((Vector4<double>)matrix.Row0, (Vector4<double>)matrix.Row1, (Vector4<double>)matrix.Row2, (Vector4<double>)matrix.Row3);
        }
        public static explicit operator Matrix4<float>(Matrix4 matrix)
        {
            if (matrix is null)
            {
                matrix = Identity;
            }

            return new Matrix4<float>((Vector4<float>)matrix.Row0, (Vector4<float>)matrix.Row1, (Vector4<float>)matrix.Row2, (Vector4<float>)matrix.Row3);
        }
    }
}
