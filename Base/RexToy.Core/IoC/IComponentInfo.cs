using System;
using System.Collections.Generic;

using RexToy.Collections;

namespace RexToy.IoC
{
    interface IComponentInfo
    {
        string Id { get; }
        Type ServiceType { get; }
        Type ComponentType { get; }

        IReadOnlyList<IPolicy> BuildPolicies { get; }
        bool LifecycleManagement { get; }
    }
}
