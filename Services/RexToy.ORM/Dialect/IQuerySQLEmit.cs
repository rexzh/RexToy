using System;
using System.Collections.Generic;

using RexToy.ORM.QueryModel;

namespace RexToy.ORM.Dialect
{
    public interface IQuerySQLEmit
    {
        string Query(IQuery query);
        string QueryCount(IQuery query);
        string PagedQuery(IQuery query, uint numberPerPage, uint page);
    }
}
