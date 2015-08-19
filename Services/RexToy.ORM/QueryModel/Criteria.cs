using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace RexToy.ORM.QueryModel
{
    public abstract class Criteria
    {
        public abstract CriteriaType CriteriaType { get; }
    }

    public class SingleExpressionCriteria : Criteria
    {
        private Expression _expr;
        public Expression Expression
        {
            get { return _expr; }
        }

        public override CriteriaType CriteriaType
        {
            get { return CriteriaType.Single; }
        }

        internal SingleExpressionCriteria(Expression expr)
        {
            expr.ThrowIfNullArgument(nameof(expr));
            _expr = expr;
        }
    }

    public class CompositeCriteria : Criteria
    {
        public override CriteriaType CriteriaType
        {
            get { return CriteriaType.Composite; }
        }

        private Criteria _left;
        public Criteria Left
        {
            get { return _left; }
        }

        private Criteria _right;
        public Criteria Right
        {
            get { return _right; }
        }

        private LogicType _logic;
        public LogicType Logic
        {
            get { return _logic; }
        }

        internal CompositeCriteria(Criteria left, LogicType logic, Criteria right)
        {
            left.ThrowIfNullArgument(nameof(left));
            right.ThrowIfNullArgument(nameof(right));
            logic.ThrowIfEnumOutOfRange();

            _left = left;
            _right = right;
            _logic = logic;
        }
    }
}
