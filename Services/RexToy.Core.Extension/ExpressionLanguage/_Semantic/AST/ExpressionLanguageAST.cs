using System;
using System.Collections.Generic;

using RexToy.DesignPattern;
using RexToy.Compiler.Semantic;

namespace RexToy.ExpressionLanguage.AST
{
    public class ExpressionLanguageAST : AbstractSyntaxTree
    {
        public ExpressionLanguageAST(Node root)
        {
            this.Root = root;
        }

        public R Accept<R>(VisitorBase<R> v)
        {
            return this.Root.Accept(v);
        }
    }
}
