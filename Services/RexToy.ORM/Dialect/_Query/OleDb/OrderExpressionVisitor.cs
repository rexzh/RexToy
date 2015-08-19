using System;
using System.Text;

using RexToy.ORM.MappingInfo;
using RexToy.ORM.QueryModel;

namespace RexToy.ORM.Dialect.OleDb
{
    class OrderExpressionVisitor : AbstractOrderExpressionVisitor
    {
        public OrderExpressionVisitor(ISQLTranslator tr, IObjectMapInfoCache cache)
            : base(tr, cache)
        {
        }
    }
}
