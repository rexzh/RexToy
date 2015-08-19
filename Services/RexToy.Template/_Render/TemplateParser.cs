using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using RexToy.Logging;

namespace RexToy.Template
{
    using RexToy.Template.AST;

    class TemplateParser
    {
        private static ILog _log = LogContext.GetLogger<TemplateParser>();

        private TemplateEngine _engine;
        public TemplateParser(TemplateEngine engine)
        {
            _engine = engine;
        }

        public TemplateAST Parse(string path)
        {
            string templateFile = Path.Combine(_engine.Path, path);

            _log.Debug("Begin parse template file [{0}].", templateFile);

            if (!File.Exists(templateFile))
                ExceptionHelper.ThrowFileNotFound(templateFile);

            using (StreamReader r = new StreamReader(templateFile))
            {
                string template = r.ReadToEnd();
                LexicalParser lp = new LexicalParser();
                lp.SetParseContent(template);
                SemanticParser sp = new SemanticParser();
                sp.SetParseContent(lp.Parse());
                TemplateAST ast = sp.Parse();

                _log.Debug("Parse template file [{0}] success.", templateFile);

                ParseIncludeTemplate(ast);
                return ast;
            }
        }

        private void ParseIncludeTemplate(TemplateAST ast)
        {
            IncludeNodeVisitor v = new IncludeNodeVisitor();
            ast.Root.Accept(v);

            foreach (IncludeNode inc in v.Includes)
            {
                TemplateParser inner = new TemplateParser(_engine);
                TemplateAST innerAst = inner.Parse(inc.Path.UnBracketing(StringPair.DoubleQuote));
                inc.Parse(innerAst);
            }
        }
    }
}
