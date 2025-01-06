using System;

namespace Zene.Structs
{
    /// <summary>
    /// A intermediary type between matrix calculations can fixed vector types.
    /// </summary>
    public readonly struct VariableVector
    {
        /// <summary>
        /// Creates a <see cref="VariableVector"/> from an array of values.
        /// </summary>
        /// <param name="d">The values to store in the vector.</param>
        public VariableVector(params floatv[] d)
        {
            _values = d;
        }
        /// <summary>
        /// Creates a <see cref="VariableVector"/> from a <see cref="ReadOnlySpan{floatv}"/> of values.
        /// </summary>
        /// <param name="d">The values to store in the vector.</param>
        public VariableVector(ReadOnlySpan<floatv> d)
        {
            _values = d.ToArray();
        }

        /// <summary>
        /// The number of Dimensions this vector contains.
        /// </summary>
        public int Dimensions => _values.Length;
        private readonly floatv[] _values;

        /// <summary>
        /// Gets the value at a given dimension.
        /// </summary>
        /// <remarks>
        /// Values outside the dimension range return 0.
        /// </remarks>
        /// <param name="d">The dimension to query.</param>
        /// <returns></returns>
        public floatv this[int d]
        {
            get
            {
                if (d >= Dimensions || d < 0) { return 0; }

                return _values[d];
            }
        }

        public static implicit operator Vector2(VariableVector vv) => new Vector2(vv[0], vv[1]);
        public static implicit operator Vector3(VariableVector vv) => new Vector3(vv[0], vv[1], vv[2]);
        public static implicit operator Vector4(VariableVector vv) => new Vector4(vv[0], vv[1], vv[2], vv[3]);
        public static implicit operator Vector2I(VariableVector vv) => new Vector2I(vv[0], vv[1]);
        public static implicit operator Vector3I(VariableVector vv) => new Vector3I(vv[0], vv[1], vv[2]);
        public static implicit operator Vector4I(VariableVector vv) => new Vector4I(vv[0], vv[1], vv[2], vv[3]);
    }
}
