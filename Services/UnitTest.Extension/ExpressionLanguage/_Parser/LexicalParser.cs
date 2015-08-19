using System;
using System.Collections.Generic;

using RexToy;
using RexToy.Compiler.Lexical;
using RexToy.ExpressionLanguage;
using RexToy.ExpressionLanguage.Tokens;

namespace UnitTest.ExpressionLanguage
{
    public class LexicalParser
    {
        private object core;
        public LexicalParser()
        {
            core = Reflector.LoadInstance("RexToy.ExpressionLanguage.LexicalParser,RexToy.Core.Extension");
        }

        public void SetParseContent(string el)
        {
            Reflector.Bind(core).Invoke("SetParseContent", new object[] { el });
        }

        public List<Token<TokenType>> Parse()
        {
            object result = Reflector.Bind(core).Invoke("Parse", null);
            return (List<Token<TokenType>>)result;
        }
    }
}
