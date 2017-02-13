using System;
using System.Collections.Generic;
using System.Data;

namespace RexToy.ORM.Session
{
    internal class SQLExecutor : ISQLExecutor
    {
        private IDbConnection _cnn;
        private IDbTransaction _tx;
        private string _databaseId;
        public SQLExecutor(string databaseId, IDbConnection cnn)
        {
            cnn.ThrowIfNullArgument(nameof(cnn));
            databaseId.ThrowIfNullArgument(nameof(databaseId));

            _databaseId = databaseId;
            _cnn = cnn;
            _cnn.Open();
        }

        #region ISQLExecutor Members
        private long _affectedRows;
        public long AffectedRows
        {
            get { return _affectedRows; }
        }

        public DataTable ExecuteQuery(string sql, int timeout = -1)
        {
            CheckDispose();

            var provider = CoreFactory.CreateDbProvider(_databaseId);
            using (IDbCommand cmd = provider.CreateTextCommand(sql))
            {
                if (timeout > 0)
                    cmd.CommandTimeout = timeout;
                cmd.Connection = _cnn;
                if (_tx != null)
                    cmd.Transaction = _tx;
                IDbDataAdapter da = provider.CreateAdapter(cmd);

                DataSet ds = new DataSet();
                _affectedRows = da.Fill(ds);

                return ds.Tables[0];
            }
        }

        public object ExecuteScalar(string sql, int timeout = -1)
        {
            CheckDispose();

            var provider = CoreFactory.CreateDbProvider(_databaseId);
            using (IDbCommand cmd = provider.CreateTextCommand(sql))
            {
                if (timeout > 0)
                    cmd.CommandTimeout = timeout;
                cmd.Connection = _cnn;
                if (_tx != null)
                    cmd.Transaction = _tx;

                return cmd.ExecuteScalar();
            }
        }

        public void ExecuteNonQuery(string sql, int timeout = -1)
        {
            CheckDispose();

            var provider = CoreFactory.CreateDbProvider(_databaseId);
            using (IDbCommand cmd = provider.CreateTextCommand(sql))
            {
                if (timeout > 0)
                    cmd.CommandTimeout = timeout;
                cmd.Connection = _cnn;
                if (_tx != null)
                    cmd.Transaction = _tx;
                _affectedRows = cmd.ExecuteNonQuery();
            }
        }

        public DataTable ExecuteQuery(IDbCommand cmd)
        {
            CheckDispose();

            var provider = CoreFactory.CreateDbProvider(_databaseId);
            cmd.Connection = _cnn;
            if (_tx != null)
                cmd.Transaction = _tx;

            IDbDataAdapter da = provider.CreateAdapter(cmd);
            DataSet ds = new DataSet();
            _affectedRows = da.Fill(ds);
            return ds.Tables[0];
        }

        public object ExecuteScalar(IDbCommand cmd)
        {
            CheckDispose();

            cmd.Connection = _cnn;
            if (_tx != null)
                cmd.Transaction = _tx;
            return cmd.ExecuteScalar();
        }

        public void ExecuteNonQuery(IDbCommand cmd)
        {
            CheckDispose();

            cmd.Connection = _cnn;
            if (_tx != null)
                cmd.Transaction = _tx;
            _affectedRows = cmd.ExecuteNonQuery();
        }

        public IDbTransaction BeginTransaction()
        {
            _tx = _cnn.BeginTransaction();
            return _tx;
        }

        public IDbTransaction BeginTransaction(IsolationLevel il)
        {
            _tx = _cnn.BeginTransaction(il);
            return _tx;
        }

        public IDbConnection Connection
        {
            get { return _cnn; }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        private void CheckDispose()
        {
            if (_disposed)
                throw new ObjectDisposedException(this.GetType().Name);
        }

        ~SQLExecutor()
        {
            Dispose(false);
        }

        private bool _disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                _cnn.Dispose();
            }

            _disposed = true;
        }
    }
}
