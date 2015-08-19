using System;
using System.Collections.Generic;
using System.Text;

using RexToy.ORM.QueryModel;

namespace RexToy.ORM.Dialect.MSSql
{
    class QueryOrderVisitor : AbstractQueryOrderVisitor
    {        
        public QueryOrderVisitor(ISQLTranslator tr, IOrderExpressionVisitor oev)
            :base(tr,oev)
        {
        }
    }
}
