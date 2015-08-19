using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

using RexToy.ORM.QueryModel;

namespace RexToy.ORM.Dialect
{
    public interface IFilterExpressionVisitor
    {
        StringBuilder GetWhereClause(Expression expr, IEnumerable<SingleEntityView> svList);
    }
}
