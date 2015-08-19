using System;
using System.Collections.Generic;

namespace RexToy.Compiler.Semantic
{
    public class Node_3 : Node
    {
        private Node _node1;
        protected Node Node1
        {
            get { return _node1; }
            set { _node1 = value; }
        }

        private Node _node2;
        protected Node Node2
        {
            get { return _node2; }
            set { _node2 = value; }
        }

        private Node _node3;
        protected Node Node3
        {
            get { return _node3; }
            set { _node3 = value; }
        }
    }
}
