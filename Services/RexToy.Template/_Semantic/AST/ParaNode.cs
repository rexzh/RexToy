using System;
using System.Collections.Generic;

using RexToy.Compiler.Semantic;

namespace RexToy.Template.AST
{
    public class ParaNode : Node_N
    {
        public ParaNode()
        {
            Nodes = new List<Node>();
        }

        public IList<Node> ParamNodes
        {
            get { return Nodes; }
        }
    }
}
