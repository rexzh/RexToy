﻿using System;
using System.Collections.Generic;
using System.Text;

using RexToy.ORM.QueryModel;

namespace RexToy.ORM.Dialect.OleDb
{
    class QueryCriteriaVisitor : AbstractQueryCriteriaVisitor
    {
        public QueryCriteriaVisitor(ISQLTranslator tr, IFilterExpressionVisitor fv)
            : base(tr, fv)
        {
        }
    }
}
