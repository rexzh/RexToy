using System;
using System.Reflection;

namespace RexToy.AOP.Services
{
    static class ContainerExceptionHelper
    {
        public static void ThrowComponentTransactionNotDefine(Type type, MethodBase method)
        {
            throw new TransactionManagementException(string.Format("Method [{0}] is marked as AutoComplete but Component [{1}] have no Transaction attribute.", method.Name, type.Name));
        }
    }
}
