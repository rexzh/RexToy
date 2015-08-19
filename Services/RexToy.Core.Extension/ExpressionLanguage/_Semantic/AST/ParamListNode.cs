using System;
using System.Collections.Generic;

using RexToy.Collections;
using RexToy.Compiler.Semantic;

namespace RexToy.ExpressionLanguage.AST
{
    public class ParamListNode : Node_N
    {
        public ParamListNode(List<Node> args)
        {
            args.ThrowIfNullArgument(nameof(args));

            Nodes = args;
        }

        public void AppendNode(Node node)
        {
            Nodes.Add(node);
        }

        public void InsertNodeAtHead(Node node)
        {
            Nodes.Insert(0, node);
        }

        public IList<Node> Values
        {
            get { return Nodes; }
        }
    }
}
