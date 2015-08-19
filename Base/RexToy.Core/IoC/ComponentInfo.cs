using System;
using System.Collections.Generic;
using System.Linq;

using RexToy.Collections;

namespace RexToy.IoC
{
    class ComponentInfo : IComponentInfo
    {
        public ComponentInfo(string id, Type serviceType, Type componentType, IList<IPolicy> policies, bool lifeManagement)
        {
            id.ThrowIfNullArgument(nameof(id));

            _id = id;
            _serviceType = serviceType;
            _componentType = componentType;

            if (_serviceType == null && _componentType == null)
                ExceptionHelper.ThrowInfoNotComplete(_id);
            _lifecycleManagement = lifeManagement;

            _buildPolicies = new List<IPolicy>();

            var preCreation = policies.Where(p => p.Stage == Stages.PreCreation).ToArray();
            if (preCreation.Length == 0)//If no policy define for PreCreation, stateless will be default policy
                _buildPolicies.Add(new StatelessPolicy());
            else
                _buildPolicies.AddRange(preCreation);

            var creation = policies.Where(p => p.Stage == Stages.Creation).ToArray();
            if (creation.Length == 0)//If no policy define for Creation, activator will be default policy
                _buildPolicies.Add(new ActivatorPolicy());
            else
                _buildPolicies.AddRange(creation);

            _buildPolicies.AddRange(policies.Where(p => p.Stage == Stages.Initialization || p.Stage == Stages.PostInitialization));
        }

        #region IComponentInfo Members

        private string _id;
        public string Id
        {
            get { return _id; }
        }

        private Type _serviceType;
        public Type ServiceType
        {
            get { return _serviceType; }
        }

        private Type _componentType;
        public Type ComponentType
        {
            get { return _componentType; }
        }

        private List<IPolicy> _buildPolicies;
        public IReadOnlyList<IPolicy> BuildPolicies
        {
            get { return _buildPolicies; }
        }

        private bool _lifecycleManagement;
        public bool LifecycleManagement
        {
            get { return _lifecycleManagement; }
        }

        #endregion
    }
}
