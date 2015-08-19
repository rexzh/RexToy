using System;
using System.Collections.Generic;
using System.Reflection;

namespace RexToy.AOP
{
    static class ExceptionHelper
    {
        public static void ThrowInvalidProceed(IMethodCallContext ctx)
        {
            throw new WeaveException(string.Format("Calling Proceed() is not valid on [{0}][{1}.{2}].", ctx.Position, ctx.CallMessage.MethodBase.ReflectedType.Name, ctx.CallMessage.MethodName));
        }

        public static void ThrowAspectExecuteError(IMethodCallContext ctx, Exception ex)
        {
            AspectException ae = new AspectException("Exception while executing aspect, check inner exception for detail.", ex);
            if (ctx.ReturnMessage != null && ctx.ReturnMessage.Exception != null)
                ae.OriginalException = ctx.ReturnMessage.Exception;
            throw ae;
        }

        public static void ThrowWeaveException(IMethodCallContext ctx, Exception inner)
        {
            WeaveException we = new WeaveException(string.Format("Weave error at [{0}][{1}.{2}].", ctx.Position, ctx.CallMessage.MethodBase.ReflectedType.Name, ctx.CallMessage.MethodName), inner);
            if (ctx.ReturnMessage != null && ctx.ReturnMessage.Exception != null)
                we.OriginalException = ctx.ReturnMessage.Exception;
            throw we;
        }

        public static void ThrowNotMethodInfo(MethodBase method)
        {
            throw new WeaveException(string.Format("Can not convert to method info [{0}], is it constructor?", method.Name));
        }

        public static void ThrowWeaveSyntax(string weave)
        {
            throw new WeaveException(string.Format("[{0}], Error in weave syntax.", weave));
        }

        public static void ThrowWeaveSyntaxInitialize(string weave, Exception inner)
        {
            throw new WeaveException(string.Format("[{0}], Error while analysis weave syntax.", weave), inner);
        }

        public static void ThrowInvalidAccessModifier(string accessModifier)
        {
            throw new WeaveException(string.Format("Invalid access modifier [{0}].", accessModifier));
        }
    }
}
