using System;
using System.Collections.Generic;

using RexToy;
using RexToy.Compiler.Lexical;
using RexToy.ExpressionLanguage;
using RexToy.ExpressionLanguage.Tokens;
using RexToy.ExpressionLanguage.AST;

namespace UnitTest.ExpressionLanguage
{
    class SymanticParser
    {
        private object core;
        public SymanticParser()
        {
            core = Reflector.LoadInstance("RexToy.ExpressionLanguage.SemanticParser,RexToy.Core.Extension");
        }

        public void SetParseContent(List<Token<TokenType>> list)
        {
            Reflector.Bind(core).Invoke("SetParseContent", new object[] { list });
        }

        public ExpressionLanguageAST Parse()
        {
            object result = Reflector.Bind(core).Invoke("Parse", null);
            return (ExpressionLanguageAST)result;
        }
    }
}
