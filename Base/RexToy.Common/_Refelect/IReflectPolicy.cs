using System;
using System.Reflection;

namespace RexToy
{
    public interface IReflectPolicy
    {
        bool IncludeNonPublic { get; }
        bool IncludeStatic { get; }
        bool IgnoreCase { get; }
        bool BindInstance { get; }

        BindingFlags BindingFlags { get; }
    }
}
