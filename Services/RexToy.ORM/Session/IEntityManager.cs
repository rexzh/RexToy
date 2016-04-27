using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace RexToy.ORM.Session
{
    public interface IEntityManager
    {
        T FindByPK<T>(T pk);
        List<T> FindBy<T>(Expression<Func<T, bool>> where);
        List<T> FindBy<T>();
        
        List<T> FindBy<T>(Expression<Func<T, bool>> where, Expression<Func<T, object>> order, OrderType type = OrderType.Asc);
        List<T> FindBy<T>(Expression<Func<T, bool>> where, Expression<Func<T, object>> order, int top, OrderType type = OrderType.Asc);

        T Create<T>(T entity);
        long Update<T>(T entity);

        long Remove<T>(T entity);
        long RemoveBy<T>(Expression<Func<T, bool>> where);
    }
}
