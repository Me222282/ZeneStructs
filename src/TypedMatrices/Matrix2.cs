using System;

namespace Zene.Structs
{
    public struct Matrix2<T> where T : unmanaged
    {
        public Matrix2(Vector2<T> row0, Vector2<T> row1)
        {
            _matrix = new T[,]
            {
                { row0.X, row1.X },
                { row0.Y, row1.Y }
            };
        }
        public Matrix2(T[] matrix)
        {
            _matrix = new T[2, 2];

            if (matrix.Length < 4)
            {
                throw new Exception("Matrix needs to have at least 2 rows and 2 columns.");
            }

            _matrix[0, 0] = matrix[0];
            _matrix[1, 0] = matrix[1];
            _matrix[0, 1] = matrix[2];
            _matrix[1, 1] = matrix[3];
        }

        private readonly T[,] _matrix;
        public T[,] Data
        {
            get
            {
                return _matrix;
            }
        }

        public T this[int x, int y]
        {
            get
            {
                if (x >= 2 || y >= 2)
                {
                    throw new IndexOutOfRangeException($"X: {x} and Y: {y} are outside the 2 x 2 range of matrix2.");
                }

                return _matrix[x, y];
            }
            set
            {
                if (x >= 2 || y >= 2)
                {
                    throw new IndexOutOfRangeException($"X: {x} and Y: {y} are outside the 2 x 2 range of matrix2.");
                }

                _matrix[x, y] = value;
            }
        }

        public Vector2<T> Row0
        {
            get
            {
                return new Vector2<T>(_matrix[0, 0], _matrix[1, 0]);
            }
            set
            {
                _matrix[0, 0] = value.X;
                _matrix[1, 0] = value.Y;
            }
        }
        public Vector2<T> Row1
        {
            get
            {
                return new Vector2<T>(_matrix[0, 1], _matrix[1, 1]);
            }
            set
            {
                _matrix[0, 1] = value.X;
                _matrix[1, 1] = value.Y;
            }
        }
        public Vector2<T> Column0
        {
            get
            {
                return new Vector2<T>(_matrix[0, 0], _matrix[0, 1]);
            }
            set
            {
                _matrix[0, 0] = value.X;
                _matrix[0, 1] = value.Y;
            }
        }
        public Vector2<T> Column1
        {
            get
            {
                return new Vector2<T>(_matrix[1, 0], _matrix[1, 1]);
            }
            set
            {
                _matrix[1, 0] = value.X;
                _matrix[1, 1] = value.Y;
            }
        }

        public override bool Equals(object obj)
        {
            return obj is Matrix2<T> matrix &&
                _matrix == matrix._matrix;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(_matrix);
        }

        public Matrix2<T> Transpose() => new Matrix2<T>(Column0, Column1);

        public static bool operator ==(Matrix2<T> a, Matrix2<T> b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(Matrix2<T> a, Matrix2<T> b)
        {
            return !a.Equals(b);
        }
    }
}
