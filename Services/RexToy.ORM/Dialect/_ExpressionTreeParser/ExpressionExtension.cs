using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace RexToy.ORM.Dialect
{
    static class ExpressionExtension
    {
        public static Expression PartialEval(this Expression expr)
        {
            return Evaluator.PartialEval(expr, Evaluator.CanBeEvaluatedLocally);
        }

        public static bool IsConstantNullExpression(this Expression expr)
        {
            var c = expr as ConstantExpression;
            return c != null && c.Value == null;
        }
    }
}
