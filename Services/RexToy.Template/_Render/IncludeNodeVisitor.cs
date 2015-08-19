using System;
using System.Collections.Generic;

using RexToy.DesignPattern;

namespace RexToy.Template
{
    using RexToy.Template.AST;

    class IncludeNodeVisitor : VisitorBase<bool, SimpleNode, IfNode, ForNode, ParaNode, LetNode, RemarkNode, IncludeNode, BreakNode, ContinueNode>
    {
        private List<IncludeNode> _includes;
        public IEnumerable<IncludeNode> Includes
        {
            get { return _includes; }
        }

        public IncludeNodeVisitor()
        {
            _includes = new List<IncludeNode>();
        }

        public override bool Visit(RemarkNode obj)
        {
            return false;
        }

        public override bool Visit(LetNode obj)
        {
            return false;
        }

        public override bool Visit(ParaNode obj)
        {
            foreach (var p in obj.ParamNodes)
            {
                p.Accept(this);
            }
            return false;
        }

        public override bool Visit(ForNode obj)
        {
            obj.BlockNode.Accept(this);
            return false;
        }

        public override bool Visit(IfNode obj)
        {
            if (obj.TrueNode != null)
                obj.TrueNode.Accept(this);
            if (obj.FalseNode != null)
                obj.FalseNode.Accept(this);
            return false;
        }

        public override bool Visit(SimpleNode obj)
        {
            return false;
        }

        public override bool Visit(IncludeNode obj)
        {
            _includes.Add(obj);
            return true;
        }

        public override bool Visit(BreakNode obj)
        {
            return false;
        }

        public override bool Visit(ContinueNode obj)
        {
            return false;
        }
    }
}
