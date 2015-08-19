using System;
using System.Collections.Generic;

using RexToy.ExpressionLanguage;

namespace UnitTest.ExpressionLanguage
{
    class Eval : IEvalContext
    {
        private List<string> numbers;
        private Dictionary<string, int?> dict;
        public Eval()
        {
            numbers = new List<string>();
            numbers.Add("One");
            numbers.Add("Two");
            numbers.Add("Three");
            dict = new Dictionary<string, int?>();
            dict.Add("One", 1);
            dict.Add("Two", 2);
            dict.Add("Three", 3);
        }

        public float Devide(float a, float b)
        {
            return a / b;
        }

        #region IEvalContext Members

        public object Resolve(string param)
        {
            switch (param)
            {
                case "a0":
                    return 10;
                case "a":
                    return 10;
                case "b":
                    return 5;
                case "c":
                    return new int[][] { new int[] { 1, 0 }, new int[] { 0, 1 } };
                case "numbers":
                    return numbers;
                case "dict":
                    return dict;
                case "dialect":
                    return "SQL";
                case "this":
                    return this;
            }
            return null;
        }

        public void Assign(string param, object value)
        {
        }

        #endregion
    }
}
