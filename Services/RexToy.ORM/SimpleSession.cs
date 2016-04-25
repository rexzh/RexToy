using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Data;

using RexToy.ORM.Session;
using RexToy.ORM.QueryModel;

namespace RexToy.ORM
{
    /// <summary>
    /// Methods are not thread safety
    /// </summary>
    class SimpleSession : ISession
    {
        private ISQLExecutor _exe;
        private IEntityManager _mgr;
        private IEntityQuery _qry;
        private INativeSQL _native;
        private IDbTransaction _tx;
        internal SimpleSession(ISQLExecutor exe, IEntityManager mgr, IEntityQuery qry, INativeSQL native)
        {
            exe.ThrowIfNullArgument(nameof(exe));
            mgr.ThrowIfNullArgument(nameof(mgr));
            qry.ThrowIfNullArgument(nameof(qry));
            native.ThrowIfNullArgument(nameof(native));

            _exe = exe;
            _mgr = mgr;
            _qry = qry;
            _native = native;
        }

        private bool _disposed;
        private void CheckDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException(this.GetType().Name);
        }

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        ~SimpleSession()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                if (_tx != null)
                    _tx.Dispose();
                _exe.Dispose();
            }

            _disposed = true;
        }

        #region IEntityManager Members

        public T FindByPK<T>(T pk)
        {
            CheckDisposed();
            return _mgr.FindByPK(pk);
        }

        public List<T> FindBy<T>(Expression<Func<T, bool>> func)
        {
            CheckDisposed();
            return _mgr.FindBy(func);
        }

        public List<T> FindBy<T>()
        {
            CheckDisposed();
            return _mgr.FindBy<T>();
        }

        public List<T> FindBy<T>(Expression<Func<T, bool>> where, Expression<Func<T, object>> order, OrderType type = OrderType.Asc)
        {
            CheckDisposed();
            return _mgr.FindBy(where, order);
        }

        public List<T> FindBy<T>(Expression<Func<T, bool>> where, Expression<Func<T, object>> order, int top, OrderType type = OrderType.Asc)
        {
            CheckDisposed();
            return _mgr.FindBy(where, order, top);
        }

        public T Create<T>(T entity)
        {
            CheckDisposed();
            bool internalCommit = false;
            if (_tx == null)
            {
                internalCommit = true;
                _tx = _exe.BeginTransaction();
            }
            try
            {
                _mgr.Create(entity);
                if (internalCommit)
                    _tx.Commit();
                return entity;
            }
            catch
            {
                if (internalCommit)
                    _tx.Rollback();
                throw;
            }
        }

        public long Update<T>(T entity)
        {
            CheckDisposed();
            return _mgr.Update(entity);
        }

        public long Remove<T>(T entity)
        {
            CheckDisposed();
            return _mgr.Remove(entity);
        }

        public long RemoveBy<T>(Expression<Func<T, bool>> func)
        {
            CheckDisposed();
            return _mgr.RemoveBy(func);
        }

        #endregion

        #region IEntityQuery Members

        public RowSet Query(IQuery query)
        {
            CheckDisposed();
            return new RowSet(_qry.Query(query));
        }

        public long QueryCount(IQuery query)
        {
            CheckDisposed();
            return _qry.QueryCount(query);
        }

        public RowSet PagedQuery(IQuery query, uint numberPerPage, uint page)
        {
            CheckDisposed();
            return new RowSet(_qry.PagedQuery(query, numberPerPage, page));
        }

        #endregion

        #region INativeSQL Members

        public RowSet ExecuteQuery(string sql)
        {
            CheckDisposed();
            return new RowSet(_native.ExecuteQuery(sql));
        }

        public object ExecuteScalar(string sql)
        {
            CheckDisposed();
            return _native.ExecuteScalar(sql);
        }

        public long ExecuteNonQuery(string sql)
        {
            CheckDisposed();
            return _native.ExecuteNonQuery(sql);
        }

        public RowSet ExecuteQuery(IDbCommand cmd)
        {
            CheckDisposed();
            return new RowSet(_native.ExecuteQuery(cmd));
        }

        public object ExecuteScalar(IDbCommand cmd)
        {
            CheckDisposed();
            return _native.ExecuteScalar(cmd);
        }

        public long ExecuteNonQuery(IDbCommand cmd)
        {
            CheckDisposed();
            return _native.ExecuteNonQuery(cmd);
        }

        #endregion

        #region ITransactional Members

        public void BeginTransaction()
        {
            CheckDisposed();
            if (_tx != null)
            {
                ExceptionHelper.ThrowTransactionPending();
            }
            _tx = _exe.BeginTransaction();
        }

        public void BeginTransaction(IsolationLevel il)
        {
            CheckDisposed();
            if (_tx != null)
            {
                ExceptionHelper.ThrowTransactionPending();
            }
            _tx = _exe.BeginTransaction(il);
        }

        public void CommitTransaction()
        {
            CheckDisposed();
            if (_tx == null)
            {
                ExceptionHelper.ThrowNoTransactionPending();
            }
            _tx.Commit();
            _tx = null;
        }

        public void RollbackTransaction()
        {
            CheckDisposed();
            if (_tx == null)
            {
                ExceptionHelper.ThrowNoTransactionPending();
            }
            _tx.Rollback();
            _tx = null;
        }

        #endregion

        public IDbConnection Connection
        {
            get { return _exe.Connection; }
        }
    }
}
