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
        
        public void MatrixData(MatrixSpan ms);
        
        public static MultiplyMatrix operator *(IMatrix left, IMatrix right) => new MultiplyMatrix(left, right);
    }
}
