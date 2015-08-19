using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using RexToy.ORM.QueryModel;

namespace RexToy.ORM
{
    public static class ViewExtension
    {
        public static Query Where(this View view, Expression<Func<bool>> expr)
        {
            view.ThrowIfNullArgument(nameof(view));
            expr.ThrowIfNullArgument(nameof(expr));

            Criteria c = new SingleExpressionCriteria(expr);
            return new Query(view, c);
        }

        public static Query Where<T1>(this View view, Expression<Func<T1, bool>> expr)
        {
            view.ThrowIfNullArgument(nameof(view));
            expr.ThrowIfNullArgument(nameof(expr));

            Criteria c = new SingleExpressionCriteria(expr);
            return new Query(view, c);
        }

        public static Query Where<T1, T2>(this View view, Expression<Func<T1, T2, bool>> expr)
        {
            view.ThrowIfNullArgument(nameof(view));
            expr.ThrowIfNullArgument(nameof(expr));

            Criteria c = new SingleExpressionCriteria(expr);
            return new Query(view, c);
        }

        public static Query Where<T1, T2, T3>(this View view, Expression<Func<T1, T2, T3, bool>> expr)
        {
            view.ThrowIfNullArgument(nameof(view));
            expr.ThrowIfNullArgument(nameof(expr));

            Criteria c = new SingleExpressionCriteria(expr);
            return new Query(view, c);
        }

        public static Query Where<T1, T2, T3, T4>(this View view, Expression<Func<T1, T2, T3, T4, bool>> expr)
        {
            view.ThrowIfNullArgument(nameof(view));
            expr.ThrowIfNullArgument(nameof(expr));

            Criteria c = new SingleExpressionCriteria(expr);
            return new Query(view, c);
        }

        public static IQuery OrderBy<T>(this View view, Expression<Func<T, object>> expr, OrderType order = OrderType.Asc)
        {
            Query q = new Query(view, null);
            OrderCollection orderColl = new OrderCollection();
            orderColl.AppendOrder(new Order(expr, order));
            q.Order = orderColl;
            return q;
        }
    }
}


