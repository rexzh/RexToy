using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace RexToy.ORM.Session
{
    public interface IEntityManager
    {
        T FindByPK<T>(T pk);
        List<T> FindBy<T>(Expression<Func<T, bool>> func);
        List<T> FindBy<T>();

        T Create<T>(T entity);
        void Update<T>(T entity);

        void Remove<T>(T entity);
        long RemoveBy<T>(Expression<Func<T, bool>> func);
    }
}
