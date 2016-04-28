using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace RexToy.ORM.Session
{
    public class EntityResult<T>
    {
        private IEntityManager _mgr;
        public EntityResult(IEntityManager mgr)
        {
            _mgr = mgr;
        }

        private Expression<Func<T, object>> _orderby;
        private OrderType _order;
        public EntityResult<T> Orderby(Expression<Func<T, object>> orderby, OrderType order = OrderType.Asc)
        {
            if (_orderby != null)
            {
                //TODO:throw
            }
            _orderby = orderby;
            _order = order;
            return this;
        }

        public List<T> Top(int top)
        {
            if (_where == null)
                return _mgr.FindBy(_orderby, top, _order);
            else
                return _mgr.FindBy(_where, _orderby, top, _order);
        }

        private Expression<Func<T, bool>> _where;
        internal void Where(Expression<Func<T, bool>> where)
        {
            if (_where != null)
            {
                //TODO:throw
            }
            _where = where;
        }
    }
}
