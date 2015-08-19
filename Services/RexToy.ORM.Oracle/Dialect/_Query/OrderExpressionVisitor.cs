using System;
using System.Text;

using RexToy.ORM.MappingInfo;
using RexToy.ORM.QueryModel;

namespace RexToy.ORM.Dialect.Oracle
{
    class OrderExpressionVisitor : AbstractOrderExpressionVisitor
    {
        public OrderExpressionVisitor(ISQLTranslator tr, IObjectMapInfoCache cache)
            : base(tr, cache)
        {
        }
    }
}
