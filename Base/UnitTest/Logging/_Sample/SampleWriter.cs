using System;
using System.Collections.Generic;

using RexToy.Logging;

namespace UnitTest.Logging
{
    class SampleWriter : IWriter
    {
        private string _msg;
        public string Msg
        {
            get
            {
                string msg = _msg;
                _msg = null;
                return msg;
            }
        }

        #region IWriter Members

        public void Write(string msg)
        {
            _msg = msg;
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
        }

        #endregion
    }
}
