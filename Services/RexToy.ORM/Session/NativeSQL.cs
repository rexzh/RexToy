using System;
using System.Collections.Generic;
using System.Data;

using RexToy.ORM.DbAccess;
using RexToy.Logging;

namespace RexToy.ORM.Session
{
    internal class NativeSQL : INativeSQL
    {
        private static ILog _log = LogContext.GetLogger<EntityManager>();

        public NativeSQL(ISQLExecutor exe)
        {
            exe.ThrowIfNullArgument(nameof(exe));
            _exe = exe;
        }

        private ISQLExecutor _exe;

        #region INativeSQL Members

        public DataTable ExecuteQuery(string sql)
        {
            return _exe.ExecuteQuery(sql);
        }

        public object ExecuteScalar(string sql)
        {
            return _exe.ExecuteScalar(sql);
        }

        public long ExecuteNonQuery(string sql)
        {
            _exe.ExecuteNonQuery(sql);
            return _exe.AffectedRows;
        }

        public DataTable ExecuteQuery(string sql, int timeout)
        {
            return _exe.ExecuteQuery(sql, timeout);
        }

        public object ExecuteScalar(string sql, int timeout)
        {
            return _exe.ExecuteScalar(sql, timeout);
        }

        public long ExecuteNonQuery(string sql, int timeout)
        {
            _exe.ExecuteNonQuery(sql, timeout);
            return _exe.AffectedRows;
        }

        public DataTable ExecuteQuery(IDbCommand cmd)
        {
            return _exe.ExecuteQuery(cmd);
        }

        public object ExecuteScalar(IDbCommand cmd)
        {
            return _exe.ExecuteScalar(cmd);
        }

        public long ExecuteNonQuery(IDbCommand cmd)
        {
            _exe.ExecuteNonQuery(cmd);
            return _exe.AffectedRows;
        }
        #endregion
    }
}
