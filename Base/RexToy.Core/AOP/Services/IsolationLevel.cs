using System;

namespace RexToy.AOP.Services
{
    public enum IsolationLevel
    {
        ReadCommitted,
        ReadUncommitted,
        RepeatableRead,
        Serializable
    }
}
