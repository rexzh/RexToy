using System;

namespace RexToy.ExpressionLanguage
{
    class ThrowPolicy : IEvalExceptionHandlingPolicy
    {
        public bool Throw
        {
            get { return true; }
        }

        public void Handle(Exception ex)
        {
            //Note:Will be throw later, not handle here.
        }
    }
}
