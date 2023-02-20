namespace Zene.Structs
{
    public class ReferenceMatrix : IMatrix
    {
        public ReferenceMatrix(IMatrix source)
        {
            Source = source;
        }

        public int Rows => Source.Rows;
        public int Columns => Source.Columns;

        public IMatrix Source { get; set; }

        public MatrixSpan MatrixData()
        {
            if (Source == null)
            {
                return MatrixSpan.Identity;
            }

            return Source.MatrixData();
        }
    }
}
