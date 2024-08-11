using System;

namespace Zene.Structs
{
    public class MultiplyMatrix : IMatrix
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

        public MatrixSpan MatrixData()
        {
            // if (!Constant) { _dataCache = null; }
            // else if (_dataCache != null)
            // {
            //     return new MatrixSpan(_left.Rows, _right.Columns, _dataCache);
            // }

            if (_left == null)
            {
                if (_right == null)
                {
                    return MatrixSpan.Identity;
                }

                return _right.MatrixData();
            }
            if (_right == null)
            {
                return _left.MatrixData();
            }

            MatrixSpan a = _left.MatrixData();
            MatrixSpan b = _right.MatrixData();

            if (a.Rows == 0 || a.Columns == 0) { return b; }
            if (b.Rows == 0 || b.Columns == 0) { return a; }

            int r = a.Rows;
            int c = b.Columns;
            int times = Math.Max(a.Columns, b.Rows);

            double[] data = new double[r * c];
            for (int y = 0; y < r; y++)
            {
                for (int x = 0; x < c; x++)
                {
                    data[x + (y * r)] = GetValue(a, y, b, x, times);
                }
            }

            // if (Constant) { _dataCache = data; }

            return new MatrixSpan(r, c, data);
        }
        private static double GetValue(MatrixSpan a, int y, MatrixSpan b, int x, int times)
        {
            double value = 0;

            for (int i = 0; i < times; i++)
            {
                value += a[i, y] * b[x, i];
            }

            return value;
        }

        public static MultiplyMatrix operator *(MultiplyMatrix a, IMatrix b) => new MultiplyMatrix(a, b);
    }
}
