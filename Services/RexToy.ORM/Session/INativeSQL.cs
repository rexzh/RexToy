using System;
using System.Data;

using RexToy.ORM.DbAccess;

namespace RexToy.ORM.Session
{
    public interface INativeSQL
    {
        DataTable ExecuteQuery(string sql);
        object ExecuteScalar(string sql);
        void ExecuteNonQuery(string sql);

        DataTable ExecuteQuery(IDbCommand cmd);
        object ExecuteScalar(IDbCommand cmd);
        void ExecuteNonQuery(IDbCommand cmd);
    }
}
