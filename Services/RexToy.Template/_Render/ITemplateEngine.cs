using System;

using RexToy.ExpressionLanguage;

namespace RexToy.Template
{
    public interface ITemplateEngine
    {
        string Path { get; }
        string Render(string fileName);
        string RenderRaw(string textTemplate);

        ITemplateContext Context { get; }
    }
}
