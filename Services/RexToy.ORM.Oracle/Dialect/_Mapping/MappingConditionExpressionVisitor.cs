using System;
using System.Collections.Generic;

namespace RexToy.ORM.Dialect.Oracle
{
    class MappingConditionExpressionVisitor : AbstractMappingConditionExpressionVisitor
    {
        internal MappingConditionExpressionVisitor(ISQLTranslator tr)
            : base(tr)
        {
        }
    }
}
