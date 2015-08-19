using System;
using System.Collections.Generic;
using System.Text;

using RexToy.Collections;
using RexToy.ORM.QueryModel;

namespace RexToy.ORM.Dialect
{
    public interface IQueryCriteriaVisitor
    {
        StringBuilder BuildWhereFilters(Criteria criteria, IEnumerable<SingleEntityView> svList);
    }
}
