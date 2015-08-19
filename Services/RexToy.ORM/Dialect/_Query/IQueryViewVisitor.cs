using System;
using System.Collections.Generic;
using System.Text;

using RexToy.Collections;
using RexToy.ORM.MappingInfo;
using RexToy.ORM.QueryModel;

namespace RexToy.ORM.Dialect
{
    public interface IQueryViewVisitor
    {
        StringBuilder BuildJoinClause(View view);
        IReadOnlyList<SingleEntityView> VisitAlias(View view);
    }
}
