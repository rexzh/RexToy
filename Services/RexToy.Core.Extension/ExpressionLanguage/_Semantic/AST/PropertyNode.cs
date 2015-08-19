using System;
using System.Collections.Generic;

using RexToy.Compiler.Semantic;

namespace RexToy.ExpressionLanguage.AST
{
    public class PropertyNode : Node_2
    {
        internal PropertyNode(Node variable, Node property)
        {
            variable.ThrowIfNullArgument(nameof(variable));
            property.ThrowIfNullArgument(nameof(property));
            Node1 = variable;
            Node2 = property;
        }

        public Node Variable
        {
            get { return Node1; }
        }

        public Node Property
        {
            get { return Node2; }
        }
    }
}
