using System;
using System.Collections.Generic;

using RexToy.DesignPattern;
using RexToy.Collections;
using RexToy.Xml;
using RexToy.Configuration;

namespace RexToy.IoC
{
    class ComponentInfoFactory
    {
        private const string ID_ATTR = "@id";
        private const string LIFECYCLE_MANAGEMENT_ATTR = "@lifecycleManagement";

        private const string POLICY = "policy";
        private const string SERVICE_TYPE = "serviceType";
        private const string COMPONENT_TYPE = "componentType";

        private IKernalConfig _cfg;
        public ComponentInfoFactory(IKernalConfig cfg)
        {
            _cfg = cfg;
        }

        public IComponentInfo CreateInfo(XAccessor x)
        {
            string id = x.GetStringValue(ID_ATTR);
            bool? lcManage = x.GetValue<bool>(LIFECYCLE_MANAGEMENT_ATTR);
            Type serviceType = null;
            string xServiceType = x.GetStringValue(SERVICE_TYPE);
            if (!string.IsNullOrEmpty(xServiceType))
            {
                serviceType = Reflector.LoadType(xServiceType);
            }
            Type componentType = null;
            string xComponentType = x.GetStringValue(COMPONENT_TYPE);
            if (!string.IsNullOrEmpty(xComponentType))
            {
                componentType = Reflector.LoadType(xComponentType);
            }

            List<IPolicy> policies = new List<IPolicy>();
            XAccessor xPolicies = x.NavigateToSingleOrNull(POLICY);
            if (xPolicies != null)
            {
                foreach (var xPolicy in xPolicies.Children)
                {
                    if (xPolicy.IsComment)
                        continue;
                    IPolicy policy = CreatePolicy(xPolicy);
                    policies.Add(policy);
                }
            }

            IComponentInfo info = new ComponentInfo(id, serviceType, componentType, policies, lcManage ?? false);
            return info;
        }

        private IPolicy CreatePolicy(XAccessor xPolicy)
        {
            IReadOnlyDictionary<string, Type> dict = _cfg.LoadCustomizedPolicies();
            IPolicy policy = null;
            //Note:check build-in first
            switch (xPolicy.LocalName)
            {
                case "singleton":
                    policy = new SingletonPolicy();
                    policy.Initialize(xPolicy);
                    break;

                case "factory":
                    policy = new FactoryPolicy();
                    policy.Initialize(xPolicy);
                    break;

                case "instance":
                    policy = new InstancePolicy();
                    policy.Initialize(xPolicy);
                    break;

                case "stateless":
                    policy = new StatelessPolicy();
                    policy.Initialize(xPolicy);
                    break;

                case "activator":
                    policy = new ActivatorPolicy();
                    policy.Initialize(xPolicy);
                    break;

                case "setter":
                    policy = new SetterPolicy();
                    policy.Initialize(xPolicy);
                    break;

                default:
                    if (dict.ContainsKey(xPolicy.LocalName))
                    {
                        policy = Activator.CreateInstance(dict[xPolicy.LocalName]) as IPolicy;
                        policy.Initialize(xPolicy);
                    }
                    break;
            }

            if (policy == null)
                throw new UnknownPolicyException(string.Format("Unknown policy:[{0}].", xPolicy.LocalName));
            return policy;
        }
    }
}
