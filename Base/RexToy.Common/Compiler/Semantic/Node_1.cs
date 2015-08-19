using System;
using System.Collections.Generic;

namespace RexToy.Compiler.Semantic
{
    public class Node_1 : Node
    {
        private Node _node;
        protected Node Node
        {
            get { return _node; }
            set { _node = value; }
        }
    }
}
