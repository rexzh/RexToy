using System;
using System.Collections.Generic;
using System.Text;

using RexToy.ORM.QueryModel;

namespace RexToy.ORM.Dialect
{
    public interface IQueryOrderVisitor
    {
        StringBuilder BuildOrderClause(OrderCollection order, IEnumerable<SingleEntityView> svList);
        StringBuilder BuildOrderSelectColumns(OrderCollection order, IEnumerable<SingleEntityView> svList);
    }
}
