using System;

using RexToy.Logging;

namespace RexToy.ExpressionLanguage
{
    class IgnorePolicy : IEvalExceptionHandlingPolicy
    {
        private static ILog _log = LogContext.GetLogger<IgnorePolicy>();

        public bool Throw
        {
            get { return false; }
        }

        public void Handle(Exception ex)
        {
            _log.Warning(ex.ToString());
        }
    }
}
