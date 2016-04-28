using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RexToy.ORM.Session
{
    public class OrderedEntityResult<T>
    {
        private IEntityManager _mgr;
        private Expression<Func<T, object>> _orderby;
        private Expression<Func<T, bool>> _where;
        private OrderType _order;
        internal OrderedEntityResult(IEntityManager mgr, Expression<Func<T, bool>> where, Expression<Func<T, object>> orderby)
        {
            _mgr = mgr;
            _where = where;
            _orderby = orderby;
        }

        public List<T> Top(int top)
        {
            if (_where == null)
                return _mgr.FindBy(_orderby, top, _order);
            else
                return _mgr.FindBy(_where, _orderby, top, _order);
        }

        public OrderedEntityResult<T> Asc()
        {
            _order = OrderType.Asc;
            return this;
        }

        public OrderedEntityResult<T> Desc()
        {
            _order = OrderType.Desc;
            return this;
        }
    }
}
