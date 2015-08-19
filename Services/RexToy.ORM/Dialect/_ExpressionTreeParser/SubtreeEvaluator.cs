using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace RexToy.ORM.Dialect
{   
    class SubtreeEvaluator : ExpressionVisitor
    {
        private HashSet<Expression> _candidates;

        internal SubtreeEvaluator(HashSet<Expression> candidates)
        {
            this._candidates = candidates;
        }

        internal Expression Eval(Expression exp)
        {
            return this.Visit(exp);
        }

        public override Expression Visit(Expression expr)
        {
            if (expr == null)
            {
                return null;
            }
            if (this._candidates.Contains(expr))
            {
                return this.Evaluate(expr);
            }
            return base.Visit(expr);
        }

        private Expression Evaluate(Expression expr)
        {
            if (expr.NodeType == ExpressionType.Constant)
            {
                return expr;
            }
            LambdaExpression lambda = Expression.Lambda(expr);
            Delegate fn = lambda.Compile();
            return Expression.Constant(fn.DynamicInvoke(null), expr.Type);
        }
    }
}
