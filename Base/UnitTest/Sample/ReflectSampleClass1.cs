using System;

namespace UnitTest
{
    class ReflectSampleClass1
    {
        public static int V = 10;

        private static int p = 9;
        public static int P
        {
            get { return p; }
            set { p = value; }
        }

        private string _name = "sample1";
        protected string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private int _count = 3;
        public int Count
        {
            get { return _count; }
            set { _count = value; }
        }

        public int X = 5;

        public int this[int idx]
        {
            get { return idx; }
            set { }
        }
    }
}
