using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace RexToy.ORM.Dialect
{
    static class Evaluator
    {
        public static Expression PartialEval(Expression expression, Func<Expression, bool> fnCanBeEvaluated)
        {
            Nominator n = new Nominator(fnCanBeEvaluated);
            SubtreeEvaluator se = new SubtreeEvaluator(n.Nominate(expression));
            return se.Eval(expression);
        }

        public static bool CanBeEvaluatedLocally(Expression expression)
        {
            return expression.NodeType != ExpressionType.Parameter;
        }
    }
}
