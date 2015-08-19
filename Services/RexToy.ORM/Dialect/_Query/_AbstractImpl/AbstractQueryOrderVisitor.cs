using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

using RexToy.ORM.QueryModel;

namespace RexToy.ORM.Dialect
{
    public abstract class AbstractQueryOrderVisitor : IQueryOrderVisitor
    {
        protected ISQLTranslator _tr;
        protected IOrderExpressionVisitor _oev;
        protected AbstractQueryOrderVisitor(ISQLTranslator tr, IOrderExpressionVisitor oev)
        {
            tr.ThrowIfNullArgument(nameof(tr));
            oev.ThrowIfNullArgument(nameof(oev));

            _tr = tr;
            _oev = oev;
        }

        protected StringBuilder _str;
        public virtual StringBuilder BuildOrderClause(OrderCollection order, IEnumerable<SingleEntityView> svList)
        {
            _str = new StringBuilder();
            foreach (var o in order)
            {
                _str.Append(_oev.GetOrderByClause(o.OrderExpression, svList));
                _str.Append(o.OrderType == OrderType.Asc ? _tr.Asc : _tr.Desc);
                _str.Append(_tr.ColumnDelimiter);
            }
            _str.RemoveEnd(_tr.ColumnDelimiter);
            return _str;
        }

        public virtual StringBuilder BuildOrderSelectColumns(OrderCollection order, IEnumerable<SingleEntityView> svList)
        {
            _str = new StringBuilder();
            foreach (var o in order)
            {
                _str.Append(_oev.GetOrderByClause(o.OrderExpression, svList));
                _str.Append(_tr.ColumnDelimiter);
            }
            _str.RemoveEnd(_tr.ColumnDelimiter);
            return _str;
        }
    }
}
