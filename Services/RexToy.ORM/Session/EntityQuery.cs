using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Data;

using RexToy.Logging;
using RexToy.ORM.Dialect;
using RexToy.ORM.QueryModel;

namespace RexToy.ORM.Session
{
    internal class EntityQuery : IEntityQuery
    {
        private static ILog _log = LogContext.GetLogger<EntityQuery>();

        private IQuerySQLEmit _emit;
        private ISQLExecutor _exe;
        public EntityQuery(ISQLExecutor exe, IQuerySQLEmit emit)
        {
            exe.ThrowIfNullArgument(nameof(exe));
            emit.ThrowIfNullArgument(nameof(emit));

            _exe = exe;
            _emit = emit;
        }

        #region IEntityQuery Members

        public DataTable Query(IQuery query)
        {
            try
            {
                string sql = _emit.Query(query);
                _log.Debug(sql);
                return _exe.ExecuteQuery(sql);
            }
            catch (Exception ex)
            {
                throw ex.CreateWrapException<ORMException>();
            }
        }

        public long QueryCount(IQuery query)
        {
            try
            {
                string sql = _emit.QueryCount(query);
                _log.Debug(sql);
                object val = _exe.ExecuteScalar(sql);
                return TypeCast.ChangeType<long>(val);
            }
            catch (Exception ex)
            {
                throw ex.CreateWrapException<ORMException>();
            }
        }

        public DataTable PagedQuery(IQuery query, uint numberPerPage, uint page)
        {
            try
            {
                string sql = _emit.PagedQuery(query, numberPerPage, page);
                _log.Debug(sql);
                return _exe.ExecuteQuery(sql);
            }
            catch (Exception ex)
            {
                throw ex.CreateWrapException<ORMException>();
            }
        }

        #endregion
    }
}
