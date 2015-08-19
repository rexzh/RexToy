using System;
using System.Text;
using System.Collections.Generic;

using RexToy.Compiler.Lexical;
using RexToy.Compiler.Semantic;
using RexToy.Template.Tokens;

namespace RexToy.Template.AST
{
    public class ForNode : Node_1
    {
        public Node BlockNode
        {
            get { return Node; }
            set { Node = value; }
        }

        private string _var;
        public string Var
        {
            get { return _var; }
        }

        private string _enumerable;
        public string Enumerable
        {
            get { return _enumerable; }
        }

        public ForNode(Token<TokenType> t)
        {
            //Note:Syntax: for <var> in <enumerable var>
            StringBuilder statement = new StringBuilder(t.TokenValue);
            statement.RemoveSpaceAtBegin();
            statement.RemoveKeywordAtBegin(TemplateKeywords.For);
            statement.RemoveSpaceAtBegin();

            _var = statement.ExtractFirstToken();
            if (!_var.IsValidVariableName())
                ExceptionHelper.ThrowInvalidVarName(_var);

            statement.RemoveSpaceAtBegin();
            statement.RemoveKeywordAtBegin(TemplateKeywords.In);

            _enumerable = statement.ToString().Trim();
            if (_enumerable.Length == 0)
                ExceptionHelper.ThrowSyntaxError(t.TokenValue);
        }
    }
}