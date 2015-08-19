using System;
using System.Collections.Generic;
using System.Text;

using RexToy.ExpressionLanguage;

namespace RexToy.Template
{
    public interface ITemplateContext : IEvalContext
    {
        bool ExistVar(string name);
        void AddVar(string name);
        void RemoveVar(string name);
    }
}
