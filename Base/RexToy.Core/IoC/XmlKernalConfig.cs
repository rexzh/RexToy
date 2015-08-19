using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using RexToy.Xml;
using RexToy.Collections;
using RexToy.Configuration;

namespace RexToy.Configuration
{
    [SuppressMessage("Microsoft.Performance", "CA1812")]
    internal class XmlKernalConfig : ModuleConfig, IKernalConfig
    {
        private const string KERNAL = "kernal";

        private const string KERNAL_PREFIX = "kernal.";
        private const string POLICY_PREFIX = "policy.";

        #region IKernalConfig Members

        public string LoadComponentInfoPath(string kernalId)
        {
            var keys = this.GlobalConfig.GetAllKeysInSection(KERNAL);
            foreach (string key in keys)
            {
                if (key == KERNAL_PREFIX + kernalId)
                    return Runtime.GetPath(this.GlobalConfig.ReadValue(KERNAL, key));
            }
            Assertion.Fail("Kernal id=[{0}] config file not found.", kernalId);
            return null;
        }

        public IReadOnlyDictionary<string, Type> LoadCustomizedPolicies()
        {
            Dictionary<string, Type> dict = new Dictionary<string, Type>();
            var keys = this.GlobalConfig.GetAllKeysInSection(KERNAL);
            foreach (string key in keys)
            {
                if (key.StartsWith(POLICY_PREFIX))
                {
                    string policyId = key.RemoveBegin(POLICY_PREFIX);
                    dict[policyId] = this.GlobalConfig.ReadType(KERNAL, key);
                }
            }
            return dict;
        }

        #endregion
    }
}
