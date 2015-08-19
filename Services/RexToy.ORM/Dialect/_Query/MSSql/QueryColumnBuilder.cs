using System;
using System.Collections.Generic;
using System.Text;

using RexToy.ORM.MappingInfo;
using RexToy.ORM.QueryModel;

namespace RexToy.ORM.Dialect.MSSql
{
    class QueryColumnBuilder : AbstractQueryColumnBuilder
    {
        public QueryColumnBuilder(ISQLTranslator tr, IObjectMapInfoCache cache)
            : base(tr, cache)
        {
        }
    }
}
