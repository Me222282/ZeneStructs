namespace Zene.Structs
{
    /// <summary>
    /// An object the encapsulates a 2 dimensional box.
    /// </summary>
    public interface IBox
    {
        /// <summary>
        /// The x position of the left side of the box. Negative.
        /// </summary>
        public floatv Left { get; set; }
        /// <summary>
        /// The x position of the right side of the box. Positive.
        /// </summary>
        public floatv Right { get; set; }
        /// <summary>
        /// The y position of the Bottom size of the box. Negative.
        /// </summary>
        public floatv Bottom { get; set; }
        /// <summary>
        /// The y position of the top side of the box. Positive.
        /// </summary>
        public floatv Top { get; set; }

        /// <summary>
        /// The centre location of the box.
        /// </summary>
        public Vector2 Centre { get; }

        /// <summary>
        /// The width and height of the box.
        /// </summary>
        public Vector2 Size { get; }

        /// <summary>
        /// The x location of the box.
        /// </summary>
        public floatv X { get; }
        /// <summary>
        /// The y location of the box.
        /// </summary>
        public floatv Y { get; }
        /// <summary>
        /// The width of the box (<see cref="Right"/> - <see cref="Left"/>).
        /// </summary>
        public floatv Width { get; }
        /// <summary>
        /// The height of the box (<see cref="Top"/> - <see cref="Bottom"/>).
        /// </summary>
        public floatv Height { get; }
        
        /// <summary>
        /// The top left position of the box.
        /// </summary>
        public Vector2 TopLeft => new Vector2(Top, Left);
        /// <summary>
        /// The top right position of the box.
        /// </summary>
        public Vector2 TopRight => new Vector2(Top, Right);
        /// <summary>
        /// The bottom left position of the box.
        /// </summary>
        public Vector2 BottomLeft => new Vector2(Bottom, Left);
        /// <summary>
        /// The bottom right position of the box.
        /// </summary>
        public Vector2 BottomRight => new Vector2(Bottom, Right);
    }
}
