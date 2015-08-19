using System;
using System.Collections.Generic;

using RexToy.Compiler.Lexical;

namespace RexToy.Compiler.Semantic
{
    public class Node_N : Node
    {
        private IList<Node> _nodes;
        protected IList<Node> Nodes
        {
            get { return _nodes; }
            set { _nodes = value; }
        }
    }
}
