using System;
using System.Collections.Generic;

namespace RexToy.DesignPattern
{
    public abstract class DisposableBase : IDisposable
    {
        private bool _disposed;
        protected void CheckDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException(this.GetType().FullName);
        }

        ~DisposableBase()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            try
            {
                if (disposing)
                {
                    DisposeManagedObject();
                }

                DisposeUnmanagedObject();
            }
            finally
            {
                _disposed = true;
            }
        }

        protected virtual void DisposeManagedObject()
        {
        }

        protected virtual void DisposeUnmanagedObject()
        {
        }
    }
}
