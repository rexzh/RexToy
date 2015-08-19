using System;
using System.Collections.Generic;

using RexToy.Collections;

namespace RexToy.IoC
{
    public interface IObjectBuildContext
    {
        IKernal Kernal { get; }
        object Instance { get; set; }
        bool LifeCycleManagement { get; }
        IReadOnlyList<IPolicy> Policies { get; }
        IList<object> InjectedParameters { get; }
        Type ServiceType { get; }
        Type ComponentType { get; }

        bool SkipCreationPolicy { get; set; }
        bool SkipInitializationPolicy { get; set; }
        bool SkipPostInitializationPolicy { get; set; }
    }
}
