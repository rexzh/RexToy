using System;

namespace RexToy
{
    public interface ISmartInvoker
    {
        object Invoke(string method, object[] args);
        object InvokeConstructor(object[] args);
    }
}
