﻿using System;
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

        public void ExecuteNonQuery(string sql)
        {
            _exe.ExecuteNonQuery(sql);
        }

        public DataTable ExecuteQuery(IDbCommand cmd)
        {
            return _exe.ExecuteQuery(cmd);
        }

        public object ExecuteScalar(IDbCommand cmd)
        {
            return _exe.ExecuteScalar(cmd);
        }

        public void ExecuteNonQuery(IDbCommand cmd)
        {
            _exe.ExecuteNonQuery(cmd);
        }

        #endregion
    }
}
