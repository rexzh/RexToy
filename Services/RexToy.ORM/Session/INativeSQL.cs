using System;
using System.Data;

using RexToy.ORM.DbAccess;

namespace RexToy.ORM.Session
{
    public interface INativeSQL
    {
        DataTable ExecuteQuery(string sql);
        object ExecuteScalar(string sql);
        long ExecuteNonQuery(string sql);

        DataTable ExecuteQuery(string sql, int timeout);
        object ExecuteScalar(string sql, int timeout);
        long ExecuteNonQuery(string sql, int timeout);

        DataTable ExecuteQuery(IDbCommand cmd);
        object ExecuteScalar(IDbCommand cmd);
        long ExecuteNonQuery(IDbCommand cmd);
    }
}
