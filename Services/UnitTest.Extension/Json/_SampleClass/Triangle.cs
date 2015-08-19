using System;

namespace UnitTest.Json
{
    class Triangle
    {
        private Point p1 = new Point();
        public Point P1
        {
            get { return p1; }
            set { p1 = value; }
        }

        private Point p2 = new Point();
        public Point P2
        {
            get { return p2; }
            set { p2 = value; }
        }

        private Point p3 = new Point();
        public Point P3
        {
            get { return p3; }
            set { p3 = value; }
        }
    }
}
