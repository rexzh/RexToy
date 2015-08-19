using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

using RexToy.DesignPattern;
using RexToy.Compiler.Semantic;
using RexToy.ExpressionLanguage;

namespace RexToy.Template
{
    using RexToy.Template.Tokens;
    using RexToy.Template.AST;

    class TemplateVisitor : VisitorBase<int, SimpleNode, IfNode, ForNode, ParaNode, LetNode, RemarkNode, IncludeNode, BreakNode, ContinueNode>
    {
        private ExpressionLanguageEngine _engine;
        private ITemplateContext _ctx;

        private StringBuilder _str;
        public string Result
        {
            get { return (_str != null) ? _str.ToString() : string.Empty; }
        }

        internal TemplateVisitor(ITemplateContext ctx, ExpressionLanguageEngineConfig cfg)
        {
            _ctx = ctx;
            _engine = ExpressionLanguageEngine.CreateEngine(cfg, _ctx);

            _str = new StringBuilder();
        }

        public override int Visit(IncludeNode obj)
        {
            if (!obj.Parsed)
                ExceptionHelper.ThrowIncludeNotParseYet(obj.Path);
            return obj.ParsedNode.Accept(this);
        }

        public override int Visit(RemarkNode obj)
        {
            return 0;
        }

        public override int Visit(LetNode obj)
        {
            _ctx.Assign(obj.VarName, _engine.Eval(obj.Expression));
            return 0;
        }

        public override int Visit(ParaNode obj)
        {
            int len = 0;
            foreach (Node node in obj.ParamNodes)
            {
                len += node.Accept(this);
            }
            return len;
        }

        public override int Visit(ForNode obj)
        {
            if (!_ctx.ExistVar(obj.Var))
                _ctx.AddVar(obj.Var);

            object enumerable = _engine.Eval(obj.Enumerable);
            var list = enumerable as IEnumerable;
            if (list == null)
                ExceptionHelper.ThrowTargetNotEnumerable(obj.Enumerable);

            int len = 0;
            foreach (object var in list)
            {
                try
                {
                    _ctx.Assign(obj.Var, var);
                    len += obj.BlockNode.Accept(this);
                }
                catch (Exception ex)
                {
                    Exception inner = ex;
                    while (inner is TargetInvocationException && inner.InnerException != null)
                    {
                        inner = inner.InnerException;
                    }

                    if (inner is BreakException)
                        break;

                    if (inner is ContinueException)
                        continue;

                    throw;
                }
            }

            return len;
        }

        public override int Visit(IfNode obj)
        {
            bool b = _engine.EvalToBool(_engine.Eval(obj.Expr));
            if (b)
            {
                if (obj.TrueNode != null)
                    return obj.TrueNode.Accept(this);
                else
                    return 0;
            }
            else
            {
                if (obj.FalseNode != null)
                    return obj.FalseNode.Accept(this);
                else
                    return 0;
            }
        }

        public override int Visit(SimpleNode obj)
        {
            switch (obj.Token.TokenType)
            {
                case TokenType.Expression:
                    object result = _engine.Eval(obj.Token.TokenValue);
                    if (result != null)
                    {
                        _str.Append(result.ToString());
                        return result.ToString().Length;
                    }
                    else
                        return 0;

                case TokenType.Text:
                    _str.Append(obj.Token.TokenValue);
                    return obj.Token.TokenValue.Length;

                default:
                    ExceptionHelper.ThrowSimpleNodeInvalidTokenType(obj.Token.TokenType);
                    return 0;
            }
        }

        public override int Visit(BreakNode obj)
        {
            throw new BreakException();
        }

        public override int Visit(ContinueNode obj)
        {
            throw new ContinueException();
        }
    }
}
