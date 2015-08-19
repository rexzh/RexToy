using System;
using System.Collections.Generic;
using System.Reflection;

using RexToy.Compiler.Semantic;

namespace RexToy.ExpressionLanguage.AST
{
    public class IndexerNode : Node_2
    {
        internal IndexerNode(Node variable, ParamListNode paramList)
        {
            variable.ThrowIfNullArgument(nameof(variable));
            paramList.ThrowIfNullArgument(nameof(paramList));

            Node1 = variable;
            Node2 = paramList;
        }

        public Node Variable
        {
            get { return Node1; }
        }

        public Node Args
        {
            get { return Node2; }
        }
    }
}
