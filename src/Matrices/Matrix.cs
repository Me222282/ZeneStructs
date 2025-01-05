using System;
using System.Text;

namespace Zene.Structs
{
    public struct Matrix : IMatrix
    {
        public Matrix(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            _matrix = new double[rows * columns];
        }

        public int Rows { get; }
        public int Columns { get; }
        
        public bool Constant => true;
        
        private readonly double[] _matrix;
        public int Length => _matrix.Length;

        public double this[int x, int y]
        {
            get => _matrix[x + (y * Columns)];
            set => _matrix[x + (y * Columns)] = value;
        }
        public double this[int index]
        {
            get => _matrix[index];
            set => _matrix[index] = value;
        }
        public double this[Index index]
        {
            get => _matrix[index.Value];
            set => _matrix[index.Value] = value;
        }

        public void MatrixData(MatrixSpan ms) => ms.Fill(_matrix, Rows, Columns);

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(Rows * Columns * 2);

            int index = 0;
            for (int y = 0; y < Rows; y++)
            {
                sb.Append('[');

                for (int x = 0; x < Columns - 1; x++)
                {
                    sb.Append(_matrix[index]);
                    sb.Append(", ");
                    index++;
                }
                sb.Append(_matrix[index]);
                index++;

                sb.Append(']');
                if (y + 1 < Rows)
                {
                    sb.Append('\n');
                }
            }

            return sb.ToString();
        }
        public string ToString(string format)
        {
            StringBuilder sb = new StringBuilder(Rows * Columns * 2);

            int index = 0;
            for (int y = 0; y < Rows; y++)
            {
                sb.Append('[');

                for (int x = 0; x < Columns - 1; x++)
                {
                    sb.Append(_matrix[index].ToString(format));
                    sb.Append(", ");
                    index++;
                }
                sb.Append(_matrix[index].ToString(format));
                index++;

                sb.Append(']');
                if (y + 1 < Rows)
                {
                    sb.Append('\n');
                }
            }

            return sb.ToString();
        }

        public static MultiplyMatrix operator *(Matrix a, IMatrix b) => new MultiplyMatrix(a, b);

        public static Matrix Identity { get; } = new Matrix(0, 0);
    }
}
