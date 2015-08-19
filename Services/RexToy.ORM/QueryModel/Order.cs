using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace RexToy.ORM.QueryModel
{
    public class Order
    {        
        internal Order(Expression expr, OrderType order)
        {
            expr.ThrowIfNullArgument(nameof(expr));

            _expr = expr;
            _order = order;
        }

        protected Expression _expr;
        public Expression OrderExpression
        {
            get { return _expr; }
        }

        protected OrderType _order;
        public OrderType OrderType
        {
            get { return _order; }
        }
    }
}
