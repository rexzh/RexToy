using System;
using System.Text;

using RexToy.Compiler.Lexical;
using RexToy.Compiler.Semantic;
using RexToy.Template.Tokens;

namespace RexToy.Template.AST
{
    public class IncludeNode : Node_1
    {
        private string _path;
        public string Path
        {
            get { return _path; }
        }

        private bool _parsed;
        public bool Parsed
        {
            get { return _parsed; }
        }

        public IncludeNode(Token<TokenType> token)
        {
            //Note:Syntax: include <path>
            StringBuilder statement = new StringBuilder(token.TokenValue);
            statement.RemoveSpaceAtBegin();
            statement.RemoveKeywordAtBegin(TemplateKeywords.Include);
            _path = statement.ToString().Trim();
            if ((!_path.StartsWith('"')) || (!_path.EndsWith('"')) || _path.Length == 0)
                ExceptionHelper.ThrowSyntaxError(token.TokenValue);
        }

        private Node _parsedNode;
        public Node ParsedNode
        {
            get { return _parsedNode; }
        }

        public void Parse(TemplateAST ast)
        {
            if (_parsed)
                ExceptionHelper.ThrowIncludeAlreadyParsed(_path);

            _parsedNode = ast.Root;
            _parsed = true;
        }
    }
}
