using System;
using System.Text;

namespace Zene.Structs
{
    public interface IMatrix
    {
        public int Rows { get; }
        public int Columns { get; }

        //public double this[int x, int y] { get; }
        
        public bool Constant { get; }
        
        public MatrixSpan MatrixData();
        /*
        public double SafeGet(ReadOnlySpan<double> data, int x, int y)
        {
            if (x >= Columns || y >= Rows)
            {
                return x == y ? 1d : 0d;
            }

            return data[x + (Rows * y)];
        }*/

        public static MultiplyMatrix operator *(IMatrix left, IMatrix right) => new MultiplyMatrix(left, right);
    }
    /*
    public unsafe struct MatrixSpan
    {
        private fixed double data[16];
        public int Length => 16;

        public double this[int index]
        {
            get => data[index];
            set => data[index] = value;
        }
        public double this[Index index]
        {
            get => data[index.Value];
            set => data[index.Value] = value;
        }
    }*/

    public readonly unsafe ref struct MatrixSpan
    {
        public MatrixSpan(int rows, int columns, ReadOnlySpan<double> data)
        {
            Rows = rows;
            Columns = columns;
            _data = data;
        }

        public int Rows { get; }
        public int Columns { get; }

        public int Length => _data.Length;

        private readonly ReadOnlySpan<double> _data;

        public double this[int x, int y]
        {
            get
            {
                if (x >= Columns || y >= Rows)
                {
                    return x == y ? 1d : 0d;
                }

                return _data[x + (Columns * y)];
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(Rows * Columns * 2);

            int index = 0;
            for (int y = 0; y < Rows; y++)
            {
                sb.Append('[');

                for (int x = 0; x < Columns - 1; x++)
                {
                    sb.Append(_data[index]);
                    sb.Append(", ");
                    index++;
                }
                sb.Append(_data[index]);
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
                    sb.Append(_data[index].ToString(format));
                    sb.Append(", ");
                    index++;
                }
                sb.Append(_data[index].ToString(format));
                index++;

                sb.Append(']');
                if (y + 1 < Rows)
                {
                    sb.Append('\n');
                }
            }

            return sb.ToString();
        }

        public static MatrixSpan Identity => new MatrixSpan(0, 0, Array.Empty<double>());

        public double* Pointer
        {
            get
            {
                fixed (double* ptr = &_data[0])
                {
                    return ptr;
                }
            }
        }
    }
}
