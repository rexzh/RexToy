using System;

using RexToy.ORM.MappingInfo;

namespace RexToy.ORM.Dialect.OleDb
{
    class MappingConditionExpressionVisitor : AbstractMappingConditionExpressionVisitor
    {
        internal MappingConditionExpressionVisitor(ISQLTranslator tr)
            : base(tr)
        {
        }
    }
}
