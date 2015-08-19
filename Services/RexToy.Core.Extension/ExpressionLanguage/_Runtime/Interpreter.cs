using System;
using System.Collections.Generic;

namespace RexToy.ExpressionLanguage
{
    using RexToy.ExpressionLanguage.AST;

    internal class Interpreter : IInterpreter
    {
        #region IInterpreter Members

        public ExpressionLanguageAST Parser(string expression)
        {
            LexicalParser l = new LexicalParser();
            l.SetParseContent(expression);
            SemanticParser s = new SemanticParser();
            s.SetParseContent(l.Parse());
            return s.Parse();
        }

        #endregion
    }
}
