using System;
using System.Collections.Generic;
using System.Data;

namespace RexToy.ORM.Session
{
    public interface ISQLExecutor : IDisposable
    {
        long AffectedRows { get; }

        DataTable ExecuteQuery(string sql, int timeout = -1);
        object ExecuteScalar(string sql, int timeout = -1);
        void ExecuteNonQuery(string sql, int timeout = -1);

        DataTable ExecuteQuery(IDbCommand cmd);
        object ExecuteScalar(IDbCommand cmd);
        void ExecuteNonQuery(IDbCommand cmd);

        IDbTransaction BeginTransaction();
        IDbTransaction BeginTransaction(IsolationLevel il);

        IDbConnection Connection { get; }
    }
}
