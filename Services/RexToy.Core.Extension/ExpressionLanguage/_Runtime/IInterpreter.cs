using System;
using System.Collections.Generic;

namespace RexToy.ExpressionLanguage
{
    using RexToy.ExpressionLanguage.AST;

    public interface IInterpreter
    {
        ExpressionLanguageAST Parser(string expression);
    }
}
