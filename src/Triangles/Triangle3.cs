using System;

namespace Zene.Structs
{
    /// <summary>
    /// Defines a triangle by its corner points in 3 dimensional space.
    /// </summary>
    public struct Triangle3
    {
        /// <summary>
        /// Creates a triangle from 3 points.
        /// </summary>
        public Triangle3(Vector3 a, Vector3 b, Vector3 c)
        {
            A = a;
            B = b;
            C = c;
        }

        /// <summary>
        /// The first point fo the triangle.
        /// </summary>
        public Vector3 A { get; set; }
        /// <summary>
        /// The second point fo the triangle.
        /// </summary>
        public Vector3 B { get; set; }
        /// <summary>
        /// The third point fo the triangle.
        /// </summary>
        public Vector3 C { get; set; }

        private const double _div3 = 1d / 3d;
        /// <summary>
        /// The central point of the triangle.
        /// </summary>
        public Vector3 Centre
        {
            get => (A + B + C) * _div3;
            set
            {
                Vector3 offset = value - Centre;
                A += offset;
                B += offset;
                C += offset;
            }
        }

        /// <summary>
        /// The area of the triangle.
        /// </summary>
        public double Area => (A - B).Cross(B - C).Length * 0.5;
        /// <summary>
        /// The squared area of the triangle.
        /// </summary>
        public double SquaredArea => (A - B).Cross(B - C).SquaredLength * 0.25;

        /// <summary>
        /// Determines whether <paramref name="point"/> is contained within this triangle.
        /// </summary>
        public bool Contains(Vector3 point)
        {
            Vector3 norm = (B - A).Cross(C - A);
            // not in same plane
            if ((point - A).Dot(norm) != 0d) { return false; }

            Vector2 a;
            Vector2 b;
            Vector2 c;
            Vector2 p;

            if (A.X == B.X && B.X == C.X)
            {
                a = new Vector2(A.Y, A.Z);
                b = new Vector2(B.Y, B.Z);
                c = new Vector2(C.Y, C.Z);
                p = new Vector2(point.Y, point.Z);
            }
            else if (A.Y == B.Y && B.Y == C.Y)
            {
                a = new Vector2(A.X, A.Z);
                b = new Vector2(B.X, B.Z);
                c = new Vector2(C.X, C.Z);
                p = new Vector2(point.X, point.Z);
            }
            else
            {
                a = new Vector2(A.X, A.Y);
                b = new Vector2(B.X, B.Y);
                c = new Vector2(C.X, C.Y);
                p = new Vector2(point.X, point.Y);
            }

            double s = (a.X - c.X) * (p.Y - c.Y) - (a.Y - c.Y) * (p.X - c.X);
            double t = (b.X - a.X) * (p.Y - a.Y) - (b.Y - a.Y) * (p.X - a.X);

            if ((s < 0) != (t < 0) && s != 0 && t != 0) { return false; }

            double d = (c.X - b.X) * (p.Y - b.Y) - (c.Y - b.Y) * (p.X - b.X);
            return d == 0 || (d < 0) == (s + t <= 0);
        }

        /// <summary>
        /// Determines whether this triangle is right angled.
        /// </summary>
        public bool IsRightAngle()
        {
            Vector3 ab = B - A;
            Vector3 ac = C - A;
            Vector3 bc = C - B;

            return ab.Dot(ac) == 0d ||
                ab.Dot(bc) == 0d ||
                bc.Dot(ac) == 0d;
        }
        /// <summary>
        /// Determines whether this triangle is equilateral.
        /// </summary>
        public bool IsEquilateral()
        {
            Vector3 ab = B - A;
            Vector3 ac = C - A;
            Vector3 bc = C - B;

            double a = Math.Abs(ab.Dot(ac));
            double b = Math.Abs(ab.Dot(bc));
            double c = Math.Abs(bc.Dot(ac));

            return a == b && b == c;
        }
        /// <summary>
        /// Determines whether this triangle is iscosceles.
        /// </summary>
        public bool IsIsosceles()
        {
            Vector3 ab = B - A;
            Vector3 ac = C - A;
            Vector3 bc = C - B;

            double a = Math.Abs(ab.Dot(ac));
            double b = Math.Abs(ab.Dot(bc));
            double c = Math.Abs(bc.Dot(ac));

            return a == b || b == c || a == c;
        }
        /// <summary>
        /// Determines whether this triangle is scalene.
        /// </summary>
        public bool IsScalene()
        {
            Vector3 ab = B - A;
            Vector3 ac = C - A;
            Vector3 bc = C - B;

            double a = Math.Abs(ab.Dot(ac));
            double b = Math.Abs(ab.Dot(bc));
            double c = Math.Abs(bc.Dot(ac));

            return a != b && b != c && a != c;
        }

        /// <summary>
        /// Returns the triangle properties of this triangle.
        /// </summary>
        public TriangleProperties GetProperties()
        {
            Vector3 ab = B - A;
            Vector3 ac = C - A;
            Vector3 bc = C - B;

            double a = Math.Abs(ab.Dot(ac));
            double b = Math.Abs(ab.Dot(bc));
            double c = Math.Abs(bc.Dot(ac));

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
