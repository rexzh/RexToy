using System;
using System.Collections.Generic;

using RexToy.Logging;
using RexToy.ORM.Dialect;
using RexToy.ORM.Session;

namespace RexToy.ORM
{
    class Database : IDatabase
    {
        private static ILog _log = LogContext.GetLogger<Database>();

        private ISQLExecutor _exe;
        private IModelSQLEmit _emit;
        private IMetaQuery _meta;
        internal Database(ISQLExecutor exe, IModelSQLEmit emit, IMetaQuery meta)
        {
            exe.ThrowIfNullArgument(nameof(exe));
            emit.ThrowIfNullArgument(nameof(emit));
            meta.ThrowIfNullArgument(nameof(meta));

            _exe = exe;
            _emit = emit;
            _meta = meta;
        }

        public void CreateTable<T>()
        {
            CheckDisposed();

            string sql = _emit.CreateTable<T>();
            _log.Debug(sql);
            _exe.ExecuteNonQuery(sql);
        }

        public void DropTable<T>()
        {
            CheckDisposed();

            string sql = _emit.DropTable<T>();
            _log.Debug(sql);
            _exe.ExecuteNonQuery(sql);
        }

        public void TruncateTable<T>()
        {
            CheckDisposed();

            string sql = _emit.TruncateTable<T>();
            _log.Debug(sql);
            _exe.ExecuteNonQuery(sql);
        }

        public IDatabaseMeta QueryMeta()
        {
            CheckDisposed();
            
            return _meta.ReadMetaInfo();
        }

        private void CheckDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException(this.GetType().Name);
        }

        private bool _disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                _exe.Dispose();
            }

            _disposed = true;
        }

        ~Database()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
