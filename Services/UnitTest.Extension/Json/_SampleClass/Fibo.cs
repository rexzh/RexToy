using System;

namespace UnitTest.Json
{
    class Fibo
    {
        private int[] arr = new int[] { 1, 1, 2, 3, 5 };
        public int[] Arr
        {
            get { return arr; }
            set { arr = value; }
        }
    }
}
