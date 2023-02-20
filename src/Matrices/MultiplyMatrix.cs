using System;

namespace Zene.Structs
{
    public class MultiplyMatrix : IMatrix
    {
        public MultiplyMatrix(IMatrix left, IMatrix right)
        {
            Left = left;
            Right = right;
        }

        public int Rows => Left.Rows;
        public int Columns => Right.Columns;

        public IMatrix Left { get; set; }
        public IMatrix Right { get; set; }

        public MatrixSpan MatrixData()
        {
            if (Left == null)
            {
                if (Right == null)
                {
                    return MatrixSpan.Identity;
                }

                return Right.MatrixData();
            }
            if (Right == null)
            {
                return Left.MatrixData();
            }

            MatrixSpan a = Left.MatrixData();
            MatrixSpan b = Right.MatrixData();

            if (a.Rows == 0 || a.Columns == 0) { return b; }
            if (b.Rows == 0 || b.Columns == 0) { return a; }

            int r = a.Rows;
            int c = b.Columns;
            int times = Math.Max(a.Columns, b.Rows);

            double[] data = new double[r * c];
            int i = 0;
            for (int x = 0; x < c; x++)
            {
                for (int y = 0; y < r; y++)
                {
                    data[i] = GetValue(a, y, b, x, times);
                    i++;
                }
            }

            return new MatrixSpan(r, c, data);
        }
        private static double GetValue(MatrixSpan a, int y, MatrixSpan b, int x, int times)
        {
            double value = 0;

            for (int i = 0; i < times; i++)
            {
                value += a[y, i] * b[i, x];
            }

            return value;
        }
    }
}
