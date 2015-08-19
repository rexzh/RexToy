using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Data;

using RexToy.ORM.QueryModel;
using RexToy.ORM.Session;

namespace RexToy.ORM
{
    public interface ISession : IEntityManager, ITransactional, IDisposable
    {
        #region Same as IEntityQuery
        RowSet Query(IQuery query);
        long QueryCount(IQuery query);
        RowSet PagedQuery(IQuery query, uint numberPerPage, uint page);
        #endregion

        #region Same as INativeSQL
        RowSet ExecuteQuery(string sql);
        object ExecuteScalar(string sql);
        void ExecuteNonQuery(string sql);
        RowSet ExecuteQuery(IDbCommand cmd);
        object ExecuteScalar(IDbCommand cmd);
        void ExecuteNonQuery(IDbCommand cmd);
        #endregion

        IDbConnection Connection { get; }
    }
}
