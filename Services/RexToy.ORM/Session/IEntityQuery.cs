using System;
using System.Collections.Generic;
using System.Data;

using RexToy.ORM.QueryModel;

namespace RexToy.ORM.Session
{
    public interface IEntityQuery
    {
        DataTable Query(IQuery query);
        long QueryCount(IQuery query);

        DataTable PagedQuery(IQuery query, uint numberPerPage, uint page);
    }
}
