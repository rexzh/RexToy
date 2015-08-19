using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

using RexToy.ORM.QueryModel;

namespace RexToy.ORM.Dialect
{
    public interface IOrderExpressionVisitor
    {
        StringBuilder GetOrderByClause(Expression expr, IEnumerable<SingleEntityView> svList);
    }
}
