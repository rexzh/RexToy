using System;
using System.Collections.Generic;
using System.Reflection;

using RexToy.Compiler.Lexical;
using RexToy.Compiler.Semantic;

namespace RexToy.ExpressionLanguage.AST
{
    public class MethodNode : Node_3
    {
        internal MethodNode(Node variable, Node method, ParamListNode args)
        {
            method.ThrowIfNullArgument(nameof(method));
            args.ThrowIfNullArgument(nameof(args));
            
            Node1 = variable;//Note:null is acceptable, static method call.
            Node2 = method;
            Node3 = args;
        }

        public Node Variable
        {
            get { return Node1; }
        }

        public Node Method
        {
            get { return Node2; }
        }

        public Node Args
        {
            get { return Node3; }
        }
    }
}
