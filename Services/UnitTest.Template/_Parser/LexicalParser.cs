using System;
using System.Collections.Generic;
using System.Text;

using RexToy;
using RexToy.Compiler.Lexical;
using RexToy.Template;
using RexToy.Template.Tokens;

namespace UnitTest.Template
{
    public class LexicalParser
    {
        private object core;
        public LexicalParser()
        {
            core = Reflector.LoadInstance("RexToy.Template.LexicalParser,RexToy.Template");
        }

        public void SetParseContent(string template)
        {
            Reflector.Bind(core).Invoke("SetParseContent", new object[] { template });
        }

        public List<Token<TokenType>> Parse()
        {
            object result = Reflector.Bind(core).Invoke("Parse", null);
            return (List<Token<TokenType>>)result;
        }
    }
}
