using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Data;

using RexToy.ORM.QueryModel;
using RexToy.ORM.Session;

namespace RexToy.ORM
{
    public interface ISession : ITransactional, IDisposable
    {
        #region Same as IEntityManager
        T FindByPK<T>(T pk);
        List<T> FindBy<T>(Expression<Func<T, bool>> where);
        List<T> FindBy<T>();

        EntityResult<T> Find<T>(Expression<Func<T, bool>> where);
        EntityResult<T> Find<T>();

        T Create<T>(T entity);
        long Update<T>(T entity);

        long Remove<T>(T entity);
        long RemoveBy<T>(Expression<Func<T, bool>> where);
        #endregion

        #region Same as IEntityQuery
        RowSet Query(IQuery query);
        long QueryCount(IQuery query);
        RowSet PagedQuery(IQuery query, uint numberPerPage, uint page);
        #endregion
        
        #region Same as INativeSQL
        RowSet ExecuteQuery(string sql);
        object ExecuteScalar(string sql);
        long ExecuteNonQuery(string sql);
        RowSet ExecuteQuery(IDbCommand cmd);
        object ExecuteScalar(IDbCommand cmd);
        long ExecuteNonQuery(IDbCommand cmd);
        #endregion

        IDbConnection Connection { get; }
    }
}
