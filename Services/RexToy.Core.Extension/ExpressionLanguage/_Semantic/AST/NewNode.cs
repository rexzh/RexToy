using System;
using System.Collections.Generic;

using RexToy.Compiler.Semantic;

namespace RexToy.ExpressionLanguage.AST
{
    public class NewNode : Node_2
    {
        public NewNode(Node typeNode, ParamListNode paramList)
        {
            this.Node1 = typeNode;
            this.Node2 = paramList;
        }

        public ClassQualifierNode Type
        {
            get { return (ClassQualifierNode)Node1; }
        }

        public ParamListNode CtorParams
        {
            get { return (ParamListNode)Node2; }
        }
    }
}
