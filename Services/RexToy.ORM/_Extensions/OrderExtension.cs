using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using RexToy.ORM.QueryModel;

namespace RexToy.ORM
{
    public static class OrderExtension
    {
        public static IQuery OrderBy<T>(this Query query, Expression<Func<T, object>> expr, OrderType order = OrderType.Asc)
        {
            query.ThrowIfNullArgument(nameof(query));
            expr.ThrowIfNullArgument(nameof(expr));

            OrderCollection orderColl = new OrderCollection();
            orderColl.AppendOrder(new Order(expr, order));
            query.Order = orderColl;
            return query;
        }

        public static IQuery ThenBy<T>(this IQuery query, Expression<Func<T, object>> expr, OrderType order = OrderType.Asc)
        {
            query.ThrowIfNullArgument(nameof(query));
            expr.ThrowIfNullArgument(nameof(expr));

            query.Order.AppendOrder(new Order(expr, order));
            return query;
        }
    }
}
