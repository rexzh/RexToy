using System;

using RexToy.ORM.MappingInfo;

namespace RexToy.ORM.Dialect.OleDb
{
    class FilterExpressionVisitor : AbstractFilterExpressionVisitor
    {
        public FilterExpressionVisitor(ISQLTranslator tr, IObjectMapInfoCache cache)
            : base(tr, cache)
        {
        }
    }
}
