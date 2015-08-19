using System;
using System.Collections.Generic;

using RexToy.Compiler.Semantic;

namespace RexToy.ExpressionLanguage.AST
{
    public class AutoIntArrayNode : Node_2
    {
        internal AutoIntArrayNode(Node begin, Node end)
        {
            begin.ThrowIfNullArgument(nameof(begin));
            end.ThrowIfNullArgument(nameof(end));

            Node1 = begin;
            Node2 = end;
        }

        public Node Begin
        {
            get { return Node1; }
        }

        public Node End
        {
            get { return Node2; }
        }
    }
}
