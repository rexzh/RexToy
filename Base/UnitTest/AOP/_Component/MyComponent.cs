using System;
using System.Collections.Generic;

using RexToy.AOP;

namespace UnitTest.AOP
{
    class MyComponent : Component
    {
        public bool Run(string s, ref int i)
        {
            if (string.IsNullOrEmpty(s))
            {
                i = 0;
                return false;
            }
            i = s.Length;
            return true;
        }

        public void AddCount()
        {
            _count++;
        }

        public virtual void Reset()
        {
            _count = 0;
        }

        protected int _count;
        public int Count
        {
            get { return _count; }
        }

        public int AddOne(int n)
        {
            return n + 1;
        }
    }
}
