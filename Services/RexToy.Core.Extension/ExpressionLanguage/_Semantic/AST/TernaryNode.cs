using System;
using System.Collections.Generic;
using System.Text;

using RexToy.Compiler.Semantic;

namespace RexToy.ExpressionLanguage.AST
{
    public class TernaryNode : Node_3
    {
        internal TernaryNode(Node condition, Node trueValue, Node falseValue)
        {
            condition.ThrowIfNullArgument(nameof(condition));
            trueValue.ThrowIfNullArgument(nameof(trueValue));
            falseValue.ThrowIfNullArgument(nameof(falseValue));

            Node1 = condition;
            Node2 = trueValue;
            Node3 = falseValue;
        }

        public Node Condition
        {
            get { return Node1; }
        }

        public Node TrueValue
        {
            get { return Node2; }
        }

        public Node FalseValue
        {
            get { return Node3; }
        }
    }
}
