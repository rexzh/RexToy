using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace RexToy.ORM.Session
{
    public class EntityResult<T>
    {
        private IEntityManager _mgr;
        internal EntityResult(IEntityManager mgr)
        {
            _mgr = mgr;
        }

        public OrderedEntityResult<T> Orderby(Expression<Func<T, object>> orderby)
        {
            orderby.ThrowIfNullArgument(nameof(orderby));
            var r = new OrderedEntityResult<T>(_mgr, _where, orderby);
            return r;
        }

        private Expression<Func<T, bool>> _where;
        internal void Where(Expression<Func<T, bool>> where)
        {
            _where = where;
        }
    }
}
