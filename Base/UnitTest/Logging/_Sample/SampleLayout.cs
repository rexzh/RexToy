using System;
using System.Collections.Generic;

using RexToy.Logging;

namespace UnitTest.Logging
{
    class SampleLayout : ILayout
    {
        #region ILayout Members

        public string Format(string name, LogLevel level, string msg)
        {
            return msg;
        }

        #endregion
    }
}
