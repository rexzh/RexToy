using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using RexToy.ORM.QueryModel;

namespace RexToy.ORM
{
    public static class QueryExtension
    {
        public static Query And<T1>(this Query query, Expression<Func<T1, bool>> expr)
        {
            query.ThrowIfNullArgument(nameof(query));
            expr.ThrowIfNullArgument(nameof(expr));

            query.Criteria = query.Criteria.And(new SingleExpressionCriteria(expr));
            return query;
        }

        public static Query And<T1, T2>(this Query query, Expression<Func<T1, T2, bool>> expr)
        {
            query.ThrowIfNullArgument(nameof(query));
            expr.ThrowIfNullArgument(nameof(expr));

            query.Criteria = query.Criteria.And(new SingleExpressionCriteria(expr));
            return query;
        }

        public static Query And<T1, T2, T3>(this Query query, Expression<Func<T1, T2, T3, bool>> expr)
        {
            query.ThrowIfNullArgument(nameof(query));
            expr.ThrowIfNullArgument(nameof(expr));

            query.Criteria = query.Criteria.And(new SingleExpressionCriteria(expr));
            return query;
        }

        public static Query And<T1, T2, T3, T4>(this Query query, Expression<Func<T1, T2, T3, T4, bool>> expr)
        {
            query.ThrowIfNullArgument(nameof(query));
            expr.ThrowIfNullArgument(nameof(expr));

            query.Criteria = query.Criteria.And(new SingleExpressionCriteria(expr));
            return query;
        }

        public static Query Or<T1>(this Query query, Expression<Func<T1, bool>> expr)
        {
            query.ThrowIfNullArgument(nameof(query));
            expr.ThrowIfNullArgument(nameof(expr));

            query.Criteria = query.Criteria.Or(new SingleExpressionCriteria(expr));
            return query;
        }

        public static Query Or<T1, T2>(this Query query, Expression<Func<T1, T2, bool>> expr)
        {
            query.ThrowIfNullArgument(nameof(query));
            expr.ThrowIfNullArgument(nameof(expr));

            query.Criteria = query.Criteria.Or(new SingleExpressionCriteria(expr));
            return query;
        }

        public static Query Or<T1, T2, T3>(this Query query, Expression<Func<T1, T2, T3, bool>> expr)
        {
            query.ThrowIfNullArgument(nameof(query));
            expr.ThrowIfNullArgument(nameof(expr));

            query.Criteria = query.Criteria.Or(new SingleExpressionCriteria(expr));
            return query;
        }

        public static Query Or<T1, T2, T3, T4>(this Query query, Expression<Func<T1, T2, T3, T4, bool>> expr)
        {
            query.ThrowIfNullArgument(nameof(query));
            expr.ThrowIfNullArgument(nameof(expr));

            query.Criteria = query.Criteria.Or(new SingleExpressionCriteria(expr));
            return query;
        }
    }
}
