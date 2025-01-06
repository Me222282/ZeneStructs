using System;
using System.Text;

namespace Zene.Structs
{
    public unsafe ref struct MatrixSpan
    {
        public MatrixSpan(int rows, int columns, Span<floatv> data)
        {
            Rows = rows;
            Columns = columns;
            Data = data;
        }

        public int Rows;
        public int Columns;

        public int Length => Data.Length;

        public readonly Span<floatv> Data;

        public floatv this[int x, int y]
        {
            get => Data[x + (Columns * y)];
            set => Data[x + (Columns * y)] = value;
        }

        public void Fill(ReadOnlySpan<floatv> s, int r, int c)
        {
            if (r == Rows && c == Columns)
            {
                s.CopyTo(Data);
                return;
            }

            c = Math.Min(c, Columns);
            r = Math.Min(r, Rows);
            for (int x = 0; x < c; x++)
            {
                for (int y = 0; y < r; y++)
                {
                    this[x, y] = s[x + (c * y)];
                }
            }

            Padding(r, c);
        }
        public void Padding(int r, int c)
        {
            int l = Math.Min(Rows, Columns);
            for (int i = Math.Min(r, c); i < l; i++)
            {
                this[i, i] = 1;
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
                    sb.Append(Data[index]);
                    sb.Append(", ");
                    index++;
                }
                sb.Append(Data[index]);
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
                    sb.Append(Data[index].ToString(format));
                    sb.Append(", ");
                    index++;
                }
                sb.Append(Data[index].ToString(format));
                index++;

                sb.Append(']');
                if (y + 1 < Rows)
                {
                    sb.Append('\n');
                }
            }

            return sb.ToString();
        }

        public static MatrixSpan Identity => new MatrixSpan(0, 0, new Span<floatv>());

        public floatv* Pointer
        {
            get
            {
                fixed (floatv* ptr = &Data[0])
                {
                    return ptr;
                }
            }
        }
    }
}
