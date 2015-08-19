using System;
using System.Collections.Generic;
using System.Reflection;

using RexToy.Configuration;

namespace RexToy.WebService.Configuration
{
    class XmlWebServiceConfig : ModuleConfig, IWebServiceConfig
    {
        private const string RESTFUL = "restful";
        private const string BASEURL = "baseurl";
        private const string ASSEMBLY = "assembly";

        public XmlWebServiceConfig()
        {
            this.Initialize();
        }

        private void Initialize()
        {
            _baseurl = GlobalConfig.ReadValue(RESTFUL, BASEURL).TrimEnd('/');
            if (!_baseurl.StartsWith('/'))
                _baseurl = '/' + _baseurl;

            string[] dlls = GlobalConfig.ReadValue(RESTFUL, ASSEMBLY).Split(';', StringSplitOptions.RemoveEmptyEntries);
            List<Assembly> assemblies = new List<Assembly>();
            foreach (var dll in dlls)
            {
                Assembly a = RexToy.Reflector.LoadAssembly(Runtime.GetPath(dll));
                assemblies.Add(a);
            }
            _assemblies = assemblies.ToArray();
        }

        private string _baseurl;
        public string BaseUrl
        {
            get
            {
                return _baseurl;
            }
        }

        private Assembly[] _assemblies;
        public Assembly[] Assemblies
        {
            get { return _assemblies; }
        }
    }
}
