using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using RexToy.ORM.MappingInfo;
using RexToy.ORM.QueryModel;

namespace RexToy.ORM.Dialect
{
    public interface IMappingSQLEmit
    {
        string FindByPK<T>(T pk);
        string FindBy<T>(Expression<Func<T, bool>> func);
        string FindBy<T>(Expression<Func<T, bool>> where, Expression<Func<T, object>> order, OrderType type = OrderType.Asc);
        string FindBy<T>(Expression<Func<T, bool>> where, Expression<Func<T, object>> order, int top, OrderType type = OrderType.Asc);
        string FindBy<T>();
        string FindIdentity<T>();

        string Create<T>(T entity);
        string Update<T>(T entity);

        string Remove<T>(T entity);
        string RemoveBy<T>(Expression<Func<T, bool>> func);

        IPropertyMapInfo GetPrimaryKeyNeedBinding<T>();
    }
}
