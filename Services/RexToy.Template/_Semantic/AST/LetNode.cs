using System;
using System.Text;
using System.Collections.Generic;

using RexToy.Compiler.Lexical;
using RexToy.Compiler.Semantic;
using RexToy.Template.Tokens;

namespace RexToy.Template.AST
{
    public class LetNode : Node
    {
        private string _varName;
        public string VarName
        {
            get { return _varName; }
        }

        private string _expression;
        public string Expression
        {
            get { return _expression; }
        }

        public LetNode(Token<TokenType> t)
        {
            //Note:Syntax: let <var> = <expr>
            StringBuilder statement = new StringBuilder(t.TokenValue);
            statement.RemoveSpaceAtBegin();
            statement.RemoveKeywordAtBegin(TemplateKeywords.Let);
            statement.RemoveSpaceAtBegin();

            //this._varName = statement.ExtractFirstToken();
            int idx = statement.IndexOf('=');
            if (idx < 0)
                ExceptionHelper.ThrowKeywordExpected("=");
            this._varName = statement.ToString().Substring(0, idx).Trim();
            statement.RemoveBegin(_varName);

            if (!_varName.IsValidVariableName())
                ExceptionHelper.ThrowInvalidVarName(_varName);

            statement.RemoveSpaceAtBegin();
            statement.RemoveKeywordAtBegin("=");
            this._expression = statement.ToString().Trim();
            if (_expression.Length == 0)
                ExceptionHelper.ThrowSyntaxError(t.TokenValue);
        }
    }
}
