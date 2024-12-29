namespace Zene.Structs
{
    public enum TriangleType
    {
        Scalene,
        Isosceles,
        Equilateral
    }
    public enum TrianglePoint
    {
        None = 0,
        A,
        B,
        C
    }

    /// <summary>
    /// A structure to store the properties of a triangle.
    /// </summary>
    public readonly struct TriangleProperties
    {
        public TriangleProperties(bool r, TriangleType tt, TrianglePoint tp)
        {
            IsRightAngle = r;
            Type = tt;
            ReferencePoint = tp;
        }

        /// <summary>
        /// Is it a right angle triangle?
        /// </summary>
        public bool IsRightAngle { get; }
        /// <summary>
        /// The type of triangle being represented.
        /// </summary>
        public TriangleType Type { get; }
        /// <summary>
        /// The significant, if any, of the triangle.
        /// </summary>
        /// <remarks>
        /// Either represents the central point of an isosceles or the point at 90°.
        /// </remarks>
        public TrianglePoint ReferencePoint { get; }
    }
}
