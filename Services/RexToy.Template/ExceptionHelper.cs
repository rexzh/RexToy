using System;

namespace RexToy.Template
{
    using RexToy.Template.Tokens;

    static class ExceptionHelper
    {
        public static void ThrowOnlyFileNameAllowed(string fileName)
        {
            throw new TemplateParseException(string.Format("[{0}] is not valid, only file name is allowed, no directory info.", fileName));
        }

        public static void ThrowParseError(long position)
        {
            throw new TemplateParseException(string.Format("Error occur at position: [{0}].", position));
        }

        public static void ThrowTokenEvalTypeError(long position)
        {
            throw new TemplateParseException(string.Format("Error status when eval token at position: [{0}].", position));
        }

        public static void ThrowIfBlockNotEnd()
        {
            throw new TemplateParseException("[If] block not correct end.");
        }

        public static void ThrowForBlockNotEnd()
        {
            throw new TemplateParseException("[For] block not correct end.");
        }

        public static void ThrowKeywordExpected(string keyword)
        {
            throw new TemplateParseException(string.Format("Keyword [{0}] expected.", keyword));
        }

        public static void ThrowSyntaxError(string tokenValue)
        {
            throw new TemplateParseException(string.Format("Syntax error in token [{0}].", tokenValue));
        }

        public static void ThrowInvalidVarName(string varName)
        {
            throw new TemplateParseException(string.Format("[{0}] is not a valid var name.", varName));
        }

        public static void ThrowIncludeAlreadyParsed(string templateName)
        {
            throw new TemplateParseException(string.Format("Template include file [{0}] already parsed.", templateName));
        }

        public static void ThrowFileNotFound(string temaplateName)
        {
            throw new System.IO.FileNotFoundException("Parse [include]", temaplateName);
        }

        public static void ThrowIncludeNotParseYet(string templateName)
        {
            throw new TemplateRenderException(string.Format("Included template [{0}] not parsed yet.", templateName));
        }

        public static void ThrowSimpleNodeInvalidTokenType(TokenType type)
        {
            throw new TemplateRenderException(string.Format("Simple node has error token type [{0}].", type));
        }

        public static void ThrowTargetNotEnumerable(string item)
        {
            throw new TemplateRenderException(string.Format("Loop target [{0}] in #for is not enumerable.", item));
        }

        public static void ThrowWrapped(Exception inner)
        {
            throw new TemplateRenderException("Template render failed.", inner);
        }
    }
}
