using System;
using System.Collections.Generic;

namespace RexToy.Compiler.Semantic
{
    public abstract class AbstractSyntaxTree
    {
        private Node _root;
        public Node Root
        {
            get { return _root; }
            protected set { _root = value; }
        }
    }
}
