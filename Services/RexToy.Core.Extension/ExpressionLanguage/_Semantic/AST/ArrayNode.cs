using System;
using System.Collections.Generic;

using RexToy.Collections;
using RexToy.Compiler.Semantic;

namespace RexToy.ExpressionLanguage.AST
{
    public class ArrayNode : Node_N
    {
        internal ArrayNode(List<Node> nodes)
        {
            nodes.ThrowIfNullArgument(nameof(nodes));

            Nodes = nodes;
        }

        public IList<Node> Elements
        {
            get { return Nodes; }
        }
    }
}
