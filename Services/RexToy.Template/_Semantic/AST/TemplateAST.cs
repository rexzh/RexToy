using System;
using System.Collections.Generic;
using System.Text;

using RexToy.DesignPattern;
using RexToy.Compiler.Semantic;

namespace RexToy.Template.AST
{
    public class TemplateAST : AbstractSyntaxTree
    {
        public TemplateAST(Node node)
        {
            this.Root = node;
        }
    }
}
