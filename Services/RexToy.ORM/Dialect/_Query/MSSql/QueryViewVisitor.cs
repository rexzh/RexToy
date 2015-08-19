using System;
using System.Collections.Generic;
using System.Text;

using RexToy.Collections;
using RexToy.ORM.MappingInfo;
using RexToy.ORM.QueryModel;

namespace RexToy.ORM.Dialect.MSSql
{
    class QueryViewVisitor : AbstractQueryViewVisitor
    {
        public QueryViewVisitor(ISQLTranslator tr, IJoinExpressionVisitor jv, IObjectMapInfoCache cache)
            : base(tr, jv, cache)
        {
        }
    }
}