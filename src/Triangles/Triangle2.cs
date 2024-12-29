﻿namespace Zene.Structs
{
    /// <summary>
    /// Defines a triangle by its corner points in 2 dimensional space.
    /// </summary>
    public struct Triangle2
    {
        /// <summary>
        /// Creates a triangle from 3 points.
        /// </summary>
        public Triangle2(Vector2 a, Vector2 b, Vector2 c)
        {
            A = a;
            B = b;
            C = c;
        }

        /// <summary>
        /// The first point fo the triangle.
        /// </summary>
        public Vector2 A { get; set; }
        /// <summary>
        /// The second point fo the triangle.
        /// </summary>
        public Vector2 B { get; set; }
        /// <summary>
        /// The third point fo the triangle.
        /// </summary>
        public Vector2 C { get; set; }

        private const double _div3 = 1d / 3d;
        /// <summary>
        /// The central point of the triangle.
        /// </summary>
        public Vector2 Centre
        {
            get => (A + B + C) * _div3;
            set
            {
                Vector2 offset = value - Centre;
                A += offset;
                B += offset;
                C += offset;
            }
        }

        /// <summary>
        /// Determines whether this triangle is right angled.
        /// </summary>
        public bool IsRightAngle()
        {
            Vector2 ab = B - A;
            Vector2 ac = C - A;
            Vector2 bc = C - B;

            return ab.Dot(ac) == 0d ||
                ab.Dot(bc) == 0d ||
                bc.Dot(ac) == 0d;
        }
        /// <summary>
        /// Determines whether this triangle is equilateral.
        /// </summary>
        public bool IsEquilateral()
        {
            Vector2 ab = B - A;
            Vector2 ac = C - A;
            Vector2 bc = C - B;

            double a = ab.Dot(ac);
            double b = ab.Dot(bc);
            double c = bc.Dot(ac);

            return a == b && b == c;
        }
        /// <summary>
        /// Determines whether this triangle is iscosceles.
        /// </summary>
        public bool IsIsosceles()
        {
            Vector2 ab = B - A;
            Vector2 ac = C - A;
            Vector2 bc = C - B;

            double a = ab.Dot(ac);
            double b = ab.Dot(bc);
            double c = bc.Dot(ac);

            return a == b || b == c || a == c;
        }
        /// <summary>
        /// Determines whether this triangle is scalene.
        /// </summary>
        public bool IsScalene()
        {
            Vector2 ab = B - A;
            Vector2 ac = C - A;
            Vector2 bc = C - B;

            double a = ab.Dot(ac);
            double b = ab.Dot(bc);
            double c = bc.Dot(ac);

            return a != b && b != c && a != c;
        }

        /// <summary>
        /// Returns the triangle properties of this triangle.
        /// </summary>
        public TriangleProperties GetProperties()
        {
            Vector2 ab = B - A;
            Vector2 ac = C - A;
            Vector2 bc = C - B;

            double a = ab.Dot(ac);
            double b = ab.Dot(bc);
            double c = bc.Dot(ac);

            TriangleType tt;
            TrianglePoint tp = TrianglePoint.None;

            bool rr = true;
            if (a == 0d) { tp = TrianglePoint.A; }
            else if (b == 0d) { tp = TrianglePoint.B; }
            else if (c == 0d) { tp = TrianglePoint.C; }
            else { rr = false; }

            if (a == b && b == c)
            {
                tt = TriangleType.Equilateral;
            }
            else if (a == b)
            {
                tt = TriangleType.Isosceles;
                tp = TrianglePoint.C;
            }
            else if (a == c)
            {
                tt = TriangleType.Isosceles;
                tp = TrianglePoint.B;
            }
            else if (b == c)
            {
                tt = TriangleType.Isosceles;
                tp = TrianglePoint.A;
            }
            else
            {
                tt = TriangleType.Scalene;
            }

            return new TriangleProperties(rr, tt, tp);
        }
    }
}