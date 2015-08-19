using System;

namespace RexToy.AOP.Services
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public sealed class AutoCompleteAttribute : Attribute
    {
    }
}