// namespace Zene.Structs
// {
//     public class ReferenceMatrix : IMatrix
//     {
//         public ReferenceMatrix(IMatrix source)
//         {
//             Source = source;
//         }

//         public int Rows => Source.Rows;
//         public int Columns => Source.Columns;
        
//         public bool Constant => Source is null || Source.Constant;
        
//         public IMatrix Source { get; set; }

//         public MatrixSpan MatrixData()
//         {
//             if (Source == null)
//             {
//                 return MatrixSpan.Identity;
//             }

//             return Source.MatrixData();
//         }

//         public static MultiplyMatrix operator *(ReferenceMatrix a, IMatrix b) => new MultiplyMatrix(a, b);
//     }
// }
