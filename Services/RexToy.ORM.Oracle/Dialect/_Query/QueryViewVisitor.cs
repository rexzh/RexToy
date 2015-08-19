using System;
using System.Text;

using RexToy.ORM.MappingInfo;
using RexToy.ORM.QueryModel;

namespace RexToy.ORM.Dialect.Oracle
{
    class QueryViewVisitor : AbstractQueryViewVisitor
    {
        public QueryViewVisitor(ISQLTranslator tr, IJoinExpressionVisitor jv, IObjectMapInfoCache cache)
            : base(tr, jv, cache)
        {
        }
    }
}