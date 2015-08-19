using System;
using System.Collections.Generic;
using System.Text;

using RexToy.Compiler.Semantic;

namespace RexToy.ExpressionLanguage.AST
{
    public class ClassQualifierNode : Node
    {
        private string _classname;
        internal ClassQualifierNode(List<string> strList)
        {
            strList.ThrowIfNullArgument(nameof(strList));

            StringBuilder str = new StringBuilder();
            for (int idx = 0; idx < strList.Count - 1; idx++)
            {
                str.Append(strList[idx]).Append(".");
            }
            str.Append(strList[strList.Count - 1]);
            _classname = str.ToString();
        }

        public string ClassName
        {
            get { return _classname; }
        }
    }
}
