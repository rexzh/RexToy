using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace RexToy.Logging
{
    public sealed class VStudioWriter : IWriter
    {
        #region IWriter Members

        public void Write(string msg)
        {
            Debug.WriteLine(msg);
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
        }

        #endregion
    }
}
