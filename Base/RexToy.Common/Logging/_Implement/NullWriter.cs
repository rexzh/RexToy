using System;

namespace RexToy.Logging
{
    public sealed class NullWriter : IWriter
    {
        internal static NullWriter Instance = new NullWriter();

        #region IDisposable Members

        public void Dispose()
        {
        }

        #endregion

        #region IWriter Members

        public void Write(string msg)
        {
        }

        #endregion
    }
}
