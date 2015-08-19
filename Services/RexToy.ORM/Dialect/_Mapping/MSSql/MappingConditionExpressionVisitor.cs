using System;

using RexToy.ORM.MappingInfo;

namespace RexToy.ORM.Dialect.MSSql
{
    class MappingConditionExpressionVisitor : AbstractMappingConditionExpressionVisitor
    {
        internal MappingConditionExpressionVisitor(ISQLTranslator tr)
            : base(tr)
        {
        }
    }
}
