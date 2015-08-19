using System;
using System.Collections.Generic;

using RexToy.Collections;

namespace RexToy.IoC
{
    internal class ObjectBuildContext : IObjectBuildContext
    {
        private IComponentInfo _info;
        private IKernal _kernal;
        private List<object> _injectedParameters;
        internal ObjectBuildContext(IComponentInfo info, IKernal kernal)
        {
            info.ThrowIfNullArgument(nameof(info));
            kernal.ThrowIfNullArgument(nameof(kernal));

            _info = info;
            _kernal = kernal;
            _injectedParameters = new List<object>();
        }

        public object Instance { get; set; }
        public bool SkipCreationPolicy { get; set; }
        public bool SkipInitializationPolicy { get; set; }
        public bool SkipPostInitializationPolicy { get; set; }

        public bool LifeCycleManagement
        {
            get { return _info.LifecycleManagement; }
        }

        public IReadOnlyList<IPolicy> Policies
        {
            get { return _info.BuildPolicies; }
        }

        public IKernal Kernal
        {
            get { return _kernal; }
        }

        public IList<object> InjectedParameters
        {
            get { return _injectedParameters; }
        }

        public Type ServiceType
        {
            get { return _info.ServiceType; }
        }

        public Type ComponentType
        {
            get { return _info.ComponentType; }
        }
    }
}
