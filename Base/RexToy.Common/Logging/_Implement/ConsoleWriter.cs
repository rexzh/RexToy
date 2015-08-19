using System;

namespace RexToy.Logging
{
    public sealed class ConsoleWriter : IWriter
    {
        #region IWriter Members

        public void Write(string msg)
        {
            Console.WriteLine(msg);
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
        }

        #endregion
    }
}
