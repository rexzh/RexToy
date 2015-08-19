using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace RexToy.ORM
{
    public static class PredicateExtension
    {
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> exprLeft, Expression<Func<T, bool>> exprRight)
        {
            var invokedExpr = Expression.Invoke(exprRight, exprLeft.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>(Expression.OrElse(exprLeft.Body, invokedExpr), exprLeft.Parameters);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> exprLeft, Expression<Func<T, bool>> exprRight)
        {
            var invokedExpr = Expression.Invoke(exprRight, exprLeft.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(exprLeft.Body, invokedExpr), exprLeft.Parameters);
        }
    }
}
