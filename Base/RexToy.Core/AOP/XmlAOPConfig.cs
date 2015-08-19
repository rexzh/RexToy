using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using RexToy.Xml;
using RexToy.Configuration;

namespace RexToy.Configuration
{
    [SuppressMessage("Microsoft.Performance", "CA1812")]
    internal class XmlAOPConfig : ModuleConfig, IAOPConfig
    {
        private const string AOP = "aop";
        private const string AOP_CONFIG = "aop-config";
        private const string SINK_FACTORY = "sink-factory";

        #region IAOPConfig Members

        public string LoadAOPInfoPath()
        {
            return Runtime.GetPath(this.GlobalConfig.ReadValue(AOP, AOP_CONFIG));
        }

        public Type LoadSinkFactory()
        {
            return this.GlobalConfig.ReadType(AOP, SINK_FACTORY);
        }

        #endregion
    }
}
