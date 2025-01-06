using System;

namespace Zene.Structs
{
    public struct MultiplyMatrix : IMatrix
    {
        public MultiplyMatrix(IMatrix left, IMatrix right)
        {
            _left = left;
            _right = right;
        }

        public int Rows => _left.Rows;
        public int Columns => _right.Columns;
        
        public bool Constant => (_left is null || _left.Constant) && (_right is null || _right.Constant);
        
        private IMatrix _left;
        public IMatrix Left
        {
            get => _left;
            set
            {
                _left = value;
                // _dataCache = null;
            }
        }
        private IMatrix _right;
        public IMatrix Right
        {
            get => _right;
            set
            {
                _right = value;
                // _dataCache = null;
            }
        }

        //  private double[] _dataCache = null;

        public void MatrixData(MatrixSpan ms)
        {
            // if (!Constant) { _dataCache = null; }
            // else if (_dataCache != null)
            // {
            //     return new MatrixSpan(_left.Rows, _right.Columns, _dataCache);
            // }

            Vector2I ls = (_left.Columns, _left.Rows);
            Vector2I rs = (_right.Columns, _right.Rows);

            if (_left == null || ls.X == 0 || ls.Y == 0)
            {
                if (_right == null || rs.X == 0 || rs.Y == 0)
                {
                    ms.Padding(0, 0);
                    return;
                }

                _right.MatrixData(ms);
                return;
            }
            if (_right == null || rs.X == 0 || rs.Y == 0)
            {
                _left.MatrixData(ms);
                return;
            }

            MatrixSpan a = new MatrixSpan(ls.Y, ls.X, stackalloc floatv[ls.Y * ls.X]);
            _left.MatrixData(a);
            MatrixSpan b = new MatrixSpan(rs.Y, rs.X, stackalloc floatv[rs.Y * rs.X]);
            _right.MatrixData(b);

            int r = Math.Min(a.Rows, ms.Rows);
            int c = Math.Min(b.Columns, ms.Columns);
            int times = Math.Max(a.Columns, b.Rows);

            for (int y = 0; y < r; y++)
            {
                int off1 = y * a.Columns;
                int off2 = y * ms.Columns;
                for (int x = 0; x < c; x++)
                {
                    ms.Data[x + off2] = GetValue(a, off1, b, x, times);
                }
            }

            ms.Padding(r, c);

            // if (Constant) { _dataCache = data; }
        }
        private static floatv GetValue(MatrixSpan a, int off, MatrixSpan b, int x, int times)
        {
            floatv value = 0;

            for (int i = 0; i < times; i++)
            {
                value += a.Data[i + off] * b[x, i];
            }

            return value;
        }

        public static MultiplyMatrix operator *(MultiplyMatrix a, IMatrix b) => new MultiplyMatrix(a, b);
    }
}
