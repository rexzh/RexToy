using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace RexToy.ORM.Dialect
{
    class Nominator : ExpressionVisitor
    {
        private Func<Expression, bool> _fnCanBeEvaluated;
        private HashSet<Expression> _candidates;
        private bool _cannotBeEvaluated;

        internal Nominator(Func<Expression, bool> fnCanBeEvaluated)
        {
            this._fnCanBeEvaluated = fnCanBeEvaluated;
        }

        internal HashSet<Expression> Nominate(Expression expression)
        {
            this._candidates = new HashSet<Expression>();
            this.Visit(expression);
            return this._candidates;
        }

        public override Expression Visit(Expression expression)
        {
            if (expression != null)
            {
                bool saveCannotBeEvaluated = this._cannotBeEvaluated;
                this._cannotBeEvaluated = false;
                base.Visit(expression);
                if (!this._cannotBeEvaluated)
                {
                    if (this._fnCanBeEvaluated(expression))
                    {
                        this._candidates.Add(expression);
                    }
                    else
                    {
                        this._cannotBeEvaluated = true;
                    }
                }
                this._cannotBeEvaluated |= saveCannotBeEvaluated;
            }
            return expression;
        }
    }
}
