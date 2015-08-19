using System;

namespace RexToy.Logging
{
    public class VStudioLayout : ILayout
    {
        #region ILayout Members

        public string Format(string name, LogLevel level, string msg)
        {
            return string.Format("{0}[{1}][{2}]>{3}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), name, level, msg);
        }

        #endregion
    }
}
