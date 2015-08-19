using System;

namespace RexToy.Logging
{
    public class NullLayout : ILayout
    {
        internal static NullLayout Instance = new NullLayout();

        #region ILayout Members

        public string Format(string name, LogLevel level, string msg)
        {
            return string.Empty;
        }

        #endregion
    }
}
