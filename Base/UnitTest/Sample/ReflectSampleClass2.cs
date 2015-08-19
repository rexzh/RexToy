using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitTest
{
    class ReflectSampleClass2
    {
        public int Sum(int a, int b)
        {
            return a + b;
        }

        public void Swap(ref int a, ref int b)
        {
            int c = a;
            a = b;
            b = c;
        }

        public int Mod(int n, int m = 2)
        {
            return n % m;
        }

        public int Acc(int a, int b, params int[] arr)
        {
            int sum = a + b;
            foreach (int i in arr)
                sum += i;
            return sum;
        }
    }
}
